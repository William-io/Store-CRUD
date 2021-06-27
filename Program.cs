using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client.Documents.Session;
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
            //GetAllProducts();
            GetProducts(1, 3);
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

        //Just some item from the list
        static void GetProducts(int pageNdx, int pageSize)
        {
            int skip = (pageNdx - 1) * pageSize;
            int take = pageSize;

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                List<Product> page = session.Query<Product>()
                    .Statistics(out QueryStatistics stats) //Queryable product
                    .Skip(skip)
                    .Take(take)
                    .ToList();

                System.Console.WriteLine($"Showing results {skip + 1} to {skip + pageSize} of {stats.TotalResults}");

                foreach (Product products in page)
                {
                    System.Console.WriteLine($"Product: {products.Name} \t\t price: {products.Price} \t\t Type: {products.Type}");
                }

            }
        }

        static void CreateCart(string customer)
        {
            //Email indentify
            Cart cart = new Cart();
            cart.Customer = customer;

            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                session.Store(cart);
                session.SaveChanges();
            }
        }
    }
}
