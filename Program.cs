using System;
using Shop.Models;
using Shop.Raven;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            GetProduct(id: "products/67-A");
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

        //Metodo para obter produto e exibir no terminal
        static void GetProduct(string id)
        {
            using (var session = DocumentStoreHolder.Store.OpenSession())
            {
                Product products = session.Load<Product>(id);
                System.Console.WriteLine($"Product: {products.Name} \t\t price: {products.Price}");
            }
        }
    }
}
