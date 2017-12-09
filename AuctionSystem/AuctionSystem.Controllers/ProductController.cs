namespace AuctionSystem.Controllers
{
    using Common;
    using Contracts;
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Timers;

    public class ProductController : IProductController
    {
        // TODO finish the logic for winner

        private static ProductController instance;
        private BidController bidController = BidController.Instance();
        private int days;
        private int hours;
        private int minutes;
        private int seconds;
        private string parsedDays;
        private string parsedHours;
        private string parsedMinutes;
        private string parsedSeconds;
        private int currentProductId;
        private Timer timer;

        public ProductController()
        {
        }

        public static ProductController Instance()
        {
            if (instance == null)
            {
                instance = new ProductController();
            }

            return instance;
        }

        public void CreateProduct(Product product)
        {
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNullOrEmpty(product.Name, nameof(product.Name));
            CoreValidator.ThrowIfNullOrEmpty(product.Description, nameof(product.Description));
            CoreValidator.ThrowIfNegativeOrZero(product.Price, nameof(product.Price));

            using (var db = new AuctionContext())
            {
                var productNew = new Product
                {
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    StartDate = product.StartDate,
                    IsAvailable = product.StartDate <= DateTime.Now && product.EndDate > DateTime.Now,
                    EndDate = product.EndDate,
                    RowVersion = product.RowVersion
                };

                db.Products.Add(productNew);
                db.SaveChanges();
            }
        }

        public bool UpdateProduct(Product product)
        {
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNullOrEmpty(product.Name, nameof(product.Name));
            CoreValidator.ThrowIfNegativeOrZero(product.Price, nameof(product.Price));
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));
            CoreValidator.ThrowIfNullOrEmpty(product.Description, nameof(product.Description));
            CoreValidator.ThrowIfDateIsNotCorrect(product.StartDate.ToString(), nameof(product.StartDate));
            CoreValidator.ThrowIfDateIsNotCorrect(product.EndDate.ToString(), nameof(product.EndDate));

            DateTime date = product.StartDate;
            if (date > product.EndDate)
            {
                throw new ArgumentException("Start date cannot be bigger than the end date");
            }

            DateTime dateEnd = product.EndDate;
            if (dateEnd < product.StartDate)
            {
                throw new ArgumentException("End date cannot be lower than the start date");
            }

            try
            {
                using (var db = new AuctionContext())
                {
                    db.Products.Attach(product);

                    db.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("Someone has already modified this product, please refresh the page.");
            } 
        }

        public bool DeleteProduct(Product product)
        {
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));

            using (var db = new AuctionContext())
            {
                var productNew = GetProductById(product.Id);

                CoreValidator.ThrowIfNull(productNew, nameof(productNew));

                db.Products.Attach(productNew);

                db.Products.Remove(productNew);
                db.Entry(productNew).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();

                return true;
            }
        }

        public Product GetProductByName(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            using (var db = new AuctionContext())
            {
                var product = db.Products
                                    .Include("Bids")
                                    .Include("Bids.User")
                                    .FirstOrDefault(p => p.Name == name);

                CoreValidator.ThrowIfNull(product, nameof(product));

                return product;
            }
        }

        public Product GetProductById(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));

            using (var db = new AuctionContext())
            {
                var product = db.Products.Include("Bids").SingleOrDefault(p => p.Id == id);

                CoreValidator.ThrowIfNull(product, nameof(product));

                return product;
            }
        }

        public Product GetProductByIdWithBidsAndUser(int id)
        {
            CoreValidator.ThrowIfNegativeOrZero(id, nameof(id));

            using (var db = new AuctionContext())
            {
                var product = db.Products
                                    .Include("Bids")
                                    .Include("Bids.User")
                                    .FirstOrDefault(p => p.Id == id);

                CoreValidator.ThrowIfNull(product, nameof(product));

                return product;
            }
        }

        public bool IsProductExisting(string name)
        {
            CoreValidator.ThrowIfNullOrEmpty(name, nameof(name));

            using (var db = new AuctionContext())
            {
                return db.Products.Any(p => p.Name == name);
            }
        }

        public int CountUserBidsForProduct(int id)
        {
            using (var db = new AuctionContext())
            {
                var collection = GetProductUsers(GetProductById(id));
                return collection != null ? collection.Count : 0;
            }
        }

        public IList<User> GetProductUsers(Product product)
        {
            CoreValidator.ThrowIfNull(product, nameof(product));
            CoreValidator.ThrowIfNegativeOrZero(product.Id, nameof(product.Id));

            using (var db = new AuctionContext())
            {
                var collection = db.Products.SingleOrDefault(p => p.Id == product.Id).Bids.Select(u => u.UserId);
                CoreValidator.ThrowIfNull(collection, nameof(collection));
                return collection.Select(userId => db.Users.FirstOrDefault(u => u.Id == userId)).ToList();
            }
        }

        public bool IsProductExistingById(int productId)
        {
            using (var db = new AuctionContext())
            {
                return db.Products.Any(p => p.Id == productId);
            }
        }

        public bool IsProductAvailableForBidding(int productId)
        {
            using (var db = new AuctionContext())
            {
                return db.Products.FirstOrDefault(p => p.Id == productId).IsAvailable;
            }
        }

        public bool MakeProductUnavailable(int productId)
        {
            using (var db = new AuctionContext())
            {
                var currentProduct = db.Products.FirstOrDefault(p => p.Id == productId);

                if (currentProduct.IsAvailable)
                {
                    currentProduct.IsAvailable = false;

                    db.SaveChanges();

                    return true;
                }

                return false;
            }
        }

        public ICollection<int> GetExpiredProductsIds()
        {
            using (var db = new AuctionContext())
            {
                var expiredProducts = db.Products
                                            .Where(p => !p.IsAvailable)
                                            .Select(p => p.Id)
                                            .ToList();

                return expiredProducts;
            }
        }

        public void SyncronizeEndDateWithCurrentDate()
        {
            var products = GetAllAvailableProducts();

            foreach (var product in products)
            {
                 SyncronizeProductData(product);
            }
        }

        private ICollection<Product> GetAllAvailableProducts()
        {
            using (var db = new AuctionContext())
            {
                return db.Products
                                .Where(p => p.IsAvailable)
                                .ToList();
            }
        }

        private void SyncronizeProductData(Product currentProduct)
        {
            var dateDiff = currentProduct.EndDate.Subtract(DateTime.UtcNow).ToString(@"dd\:hh\:mm\:ss").Substring(0, 11);

            this.days = int.Parse(dateDiff.Substring(0, 2));
            this.hours = int.Parse(dateDiff.Substring(3, 2));
            this.minutes = int.Parse(dateDiff.Substring(6, 2));
            this.seconds = int.Parse(dateDiff.Substring(9));

            this.currentProductId = currentProduct.Id;

            this.timer = new Timer(1000);

            this.timer.Elapsed += TimerCallback;
            this.timer.Start();
        }

        private async void TimerCallback(Object sender, ElapsedEventArgs e)
        {
            this.seconds--;

            if (this.days == 0
                && this.hours == 0
                && this.minutes == 0
                && this.seconds == 0)
            {
                var text = $"0{this.days}:0{this.hours}:0{this.minutes}:0{this.seconds}";

                Console.WriteLine(text);

                this.timer.Stop();

                await SetWinners();

                GC.Collect();

                return;
            }

            if (this.seconds < 1)
            {
                this.minutes--;
                this.seconds += 59;
            }

            if (this.days == 0
                && this.hours != 0
                && this.minutes != 0)
            {
                if (this.minutes < 1)
                {
                    this.hours--;
                    this.minutes += 59;
                }

                if (this.hours < 1)
                {
                    this.days--;
                    this.hours += 23;
                }
            }

            if (this.days == 0
                && this.hours == 0
                && this.minutes != 0)
            {
                if (this.minutes < 1)
                {
                    this.hours--;
                    this.minutes += 59;
                }
            }

            if (this.seconds < 10)
            {
                this.parsedSeconds = $"0{this.seconds}";
            }
            else
            {
                this.parsedSeconds = this.seconds.ToString();
            }

            if (this.minutes < 10)
            {
                this.parsedMinutes = $"0{this.minutes}";
            }
            else
            {
                this.parsedMinutes = this.minutes.ToString();
            }

            if (this.hours < 10)
            {
                this.parsedHours = $"0{this.hours}";
            }
            else
            {
                this.parsedHours = this.hours.ToString();
            }

            if (this.days < 10)
            {
                this.parsedDays = $"0{this.days}";
            }
            else
            {
                this.parsedDays = this.days.ToString();
            }

            Console.WriteLine($"{this.parsedDays}:{this.parsedHours}:{this.parsedMinutes}:{this.parsedSeconds}");

            GC.Collect();
        }

        private async Task SetWinners()
        {
            var success = MakeProductUnavailable(currentProductId);

            Console.WriteLine($"Product has expired! : {success}");

            var setWinner = bidController.SetWinnersForProducts();

            Console.WriteLine($"Has winner: {setWinner}");

            //var invoiceController = InvoiceController.Instance();
            //invoiceController.CreateInvoice();
        }
        public IList<Product> GetAllProducts()
         {
             using (var db = new AuctionContext())
             {
                 
                 return db.Products.Include("Bids")
                                   .Include("Bids.User")
                                   .ToList();
             }
         }
    }
}