﻿using ASS7420_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASS7420_1.Data
{
    public class DbInitializer
    {
        public static void Initialize(ShopContext context)
        {
            context.Database.EnsureCreated();
            // Look for any students.
            if (context.Customers.Any())
            {
                return; // DB has been seeded
            }
            var customers = new Customer[]
            {
                new Customer{Name="Carson",Home="1234567",Work="Alexa",Mobile="15498788",Email="15498788@aa.com",Address="CarsonCity"},
                new Customer{Name="Barson",Home="1234567",Work="Alexa",Mobile="15498788",Email="15498788@aa.com",Address="CarsonCity"},
                new Customer{Name="Darson",Home="1234567",Work="Alexa",Mobile="15498788",Email="15498788@aa.com",Address="CarsonCity"},
                new Customer{Name="Earson",Home="1234567",Work="Alexa",Mobile="15498788",Email="15498788@aa.com",Address="CarsonCity"},
                new Customer{Name="Larson",Home="1234567",Work="Alexa",Mobile="15498788",Email="15498788@aa.com",Address="CarsonCity"},
            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            var orders = new Order[]
            {
                new Order{CustomerID=3,Status=Status.deliver,Subtotal=11.56,GST=0.15,Grandtotal=84.26},
                new Order{CustomerID=4,Status=Status.deliver,Subtotal=12.56,GST=0.15,Grandtotal=84.26},
                new Order{CustomerID=1,Status=Status.deliver,Subtotal=13.56,GST=0.15,Grandtotal=84.26},
                new Order{CustomerID=2,Status=Status.deliver,Subtotal=14.56,GST=0.15,Grandtotal=84.26},
            };
            foreach (Order o in orders)
            {
                context.Orders.Add(o);
            }
            context.SaveChanges();
            var orderitems = new Orderitem[]
            {
                new Orderitem { HatID=1,OrderID=1050,Quantity=4},
                new Orderitem{HatID=3,OrderID=1050,Quantity=5},
                new Orderitem{HatID=4,OrderID=1050,Quantity=24},

            };
            foreach (Orderitem ord in orderitems)
            {
                context.Orderitems.Add(ord);
            }
            context.SaveChanges();
            var hats = new Hat[]
                {
                    new Hat { SupplierID=1,CategoryID=2,HatName="KALSD",Price=12.5,Description="FAFDFGGSAD",Image="CDSD"},
                    new Hat { SupplierID=2,CategoryID=2,HatName="KALSD",Price=12.5,Description="FAFDFGGSAD",Image="CDSD"},
                    new Hat { SupplierID=3,CategoryID=2,HatName="KALSD",Price=12.5,Description="FAFDFGGSAD",Image="CDSD"},
                };
            foreach (Hat h in hats)
            {
                context.Hats.Add(h);
            }
            context.SaveChanges();
        }
    }
}
