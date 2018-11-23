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
            if (!context.Products.Any())
            {
                var rnd = new Random();

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
                    "Excepteur sint occaecat cupidatat non proident.",
                    "Nulla non lectus sed nisl molestie malesuada.",
                    "Quisque tincidunt scelerisque libero.",
                    "Duis viverra diam non justo"
                };

                var products = new List<Product>();

                for (int i = 0; i < 40; i++)
                {
                    products.Add(new Product
                    {
                        Name = "Product " + i,
                        Price = prices[rnd.Next(prices.Count)],
                        CategoryId = categories[rnd.Next(categories.Count)].CategoryId,
                        Description = descriptions[rnd.Next(descriptions.Count)],
                        Weight = rnd.Next(50, 1200)
                    });
                }
                context.Products.AddRange(products);


                var dates = new List<DateTime> {
                    new DateTime(2018, 11, 29),
                    new DateTime(2018, 11, 9),
                    new DateTime(2018, 11, 27),
                    new DateTime(2018, 10, 5),
                    new DateTime(2018, 9, 2),
                    new DateTime(2018, 9, 17),

            };
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
                        Status = Status.Shipped,
                        DateCreated = dates[rnd.Next(dates.Count)],
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
