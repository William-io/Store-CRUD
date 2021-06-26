using System;
using System.Collections.Generic;
using System.Linq;
using Shop.Models;
using Shop.Raven;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            //GetProduct(id: "products/67-A");
            GetAllProducts();
        }

        //List products - (Creater)
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

        //Method to get product and display on terminal - (Reader)
        static void GetProduct(string id)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                Product products = session.Load<Product>(id);
                System.Console.WriteLine($"Product: {products.Name} \t\t price: {products.Price}");
            }
        }

        //Method to get product and display on terminal - (Reader, all products)
        static void GetAllProducts()
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                List<Product> all = session.Query<Product>().ToList();

                foreach (Product products in all)
                {
                    System.Console.WriteLine($"Product: {products.Name} \t\t price: {products.Price} \t\t Type: {products.Type}");
                }

            }
        }
    }
}
