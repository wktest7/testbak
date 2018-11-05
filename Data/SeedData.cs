using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApiBakery.Data
{
    public static class SeedData
    {
        public async static Task Seed(BakeryDbContext context, UserManager<AppUser> userMgr)
        {
            if (!context.BakeryDetails.Any())
            {
                var rnd = new Random();

                var details = new BakeryDetails
                {
                    Address = "Kraków, Pawia 5",
                    Name = "testName",
                    Nip = "3334444123",
                    PostalCode = "31-154 Kraków",
                    Phone = "555554444"
                };
                context.BakeryDetails.Add(details);

                var categories = new List<Category>();

                for (int i = 0; i < 8; i++)
                {
                    categories.Add(new Category { Name = "Category " + i });
                }
                context.Categories.AddRange(categories);

                var prices = new List<decimal> { 0.30m, 0.40m, 0.50m, 0.70m, 1m, 1.5m, 2m, 2.5m, 3, 3.2m };
                var descriptions = new List<string>
                {
                    "Lorem ipsum dolor sit amet, consectetuer adipiscing elit",
                    "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    "Nulla non lectus sed nisl molestie malesuada. Vivamus luctus egestas leo.",
                    "Quisque tincidunt scelerisque libero.",
                    "Duis viverra diam non justo"
                };

                var products = new List<Product>();

                for (int i = 0; i < 40; i++)
                {
                    products.Add(new Product
                    {
                        Name = "Products " + i,
                        Price = prices[rnd.Next(prices.Count)],
                        CategoryId = categories[rnd.Next(categories.Count)].CategoryId,
                        Description = descriptions[rnd.Next(descriptions.Count)],
                        Weight = rnd.Next(50, 1200)
                    });
                }
                context.Products.AddRange(products);


                var appUsers = new List<AppUser>();
                var user1 = await userMgr.FindByNameAsync("user1");
                var user2 = await userMgr.FindByNameAsync("user2");
                appUsers.Add(user1);
                appUsers.Add(user2);


                var orders = new List<Order>();

                for (int i = 0; i < 30; i++)
                {
                    orders.Add(new Order
                    {
                        AppUserId = appUsers[rnd.Next(appUsers.Count)].Id,
                        Status = Status.New
                    });
                }
                context.Orders.AddRange(orders);


                var orderItems = new List<OrderItem>();

                for (int i = 0; i < 500; i++)
                {
                    var orderItem = new OrderItem
                    {
                        Quantity = rnd.Next(1, 5),
                        ProductId = products[rnd.Next(products.Count)].ProductId,
                        OrderId = orders[rnd.Next(orders.Count)].OrderId
                    };
                    orderItem.Price = products.Where(x => x.ProductId == orderItem.ProductId).Select(x => x.Price).Single();

                    orderItems.Add(orderItem);
                }
                context.OrdersItems.AddRange(orderItems);

                context.SaveChanges();
            }
        }
    }
}
