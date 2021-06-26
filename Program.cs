using System;
using Shop.Models;
using Shop.Raven;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("");
            CreateProduct(name: "Dell", type: "Accessory", price: 9.10);
            CreateProduct(name: "Mouse Logtech", type: "Accessory", price: 2.10);
        }

        //List products
        static void CreateProduct(string name, string type, double price)
        {
            Product products = new Product();
            products.Name = name;
            products.Type = type;
            products.Price = price;

            //save to database

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                //Access 
                session.Store(products);
                session.SaveChanges();
            }
        }
    }
}
