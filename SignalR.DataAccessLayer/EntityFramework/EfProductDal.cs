using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using SignalR.DataAccessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DataAccessLayer.Repositories;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SignalR.DataAccessLayer.EntityFramework
{
    public class EfProductDal : GenericRepository<Product>, IProductDal
    {
        private readonly SignalRContext _context;

        public EfProductDal(SignalRContext context) : base(context)
        {
            _context = context;
        }

        public List<Product> GetProductWithCategories()
        {
            var values = _context.Products.Include(x => x.Category).ToList();
            return values;

        }

        public int ProductCount()
        {
            using var context = new SignalRContext();
            return context.Products.Count();
        }

        public int ProductCountByCategoryNameDrink()
        {
            using var context = new SignalRContext();

            return context.Products.Where(x => x.CategoryID == (context.Categories.Where(y => y.CategoryName == "İçecek").Select(z => z.CategoryID).FirstOrDefault())).Count();

        }

        public int ProductCountByCategoryNameHamburger()
        {
            using var context = new SignalRContext();

            return context.Products.Where(x => x.CategoryID == (context.Categories.Where(x => x.CategoryName == "Hamburger").Select(z => z.CategoryID).FirstOrDefault())).Count();
        }

        public string ProductNameByMaxPrice()
        {
            using var context = new SignalRContext();
            return context.Products.Where(x => x.Price == (context.Products.Max(x => x.Price))).Select(z => z.ProductName).FirstOrDefault();
        }

        public string ProductNameByMinPrice()
        {
            using var context = new SignalRContext();
            return context.Products.Where(x => x.Price == (context.Products.Min(x => x.Price))).Select(z => z.ProductName).FirstOrDefault();

        }
        public decimal ProductPriceAvg()
        {
            using var context = new SignalRContext();
            return Math.Round(context.Products.Average(x => x.Price),2);

        }

        public decimal ProductAvgPriceByHamburger()
        {
            using var context = new SignalRContext();
            return context.Products.Where(x => x.CategoryID == context.Categories.Where(z => z.CategoryName == "Hamburger").Select(y => y.CategoryID).FirstOrDefault()).Average(x => x.Price);
        }
    }
}
