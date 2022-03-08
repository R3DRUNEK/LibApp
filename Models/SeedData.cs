using System;
using System.Linq;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // MembershipTypes
                if (context.MembershipTypes.Any())
                {
                    Console.WriteLine("MembershipTypes already seeded");
                }
                else
                {
                    context.MembershipTypes.AddRange(
                    new MembershipType
                    {
                        Id = 1,
                        Name = "Pay as You Go",
                        SignUpFee = 0,
                        DurationInMonths = 0,
                        DiscountRate = 0
                    },
                    new MembershipType
                    {
                        Id = 2,
                        Name = "Monthly",
                        SignUpFee = 30,
                        DurationInMonths = 1,
                        DiscountRate = 10
                    },
                    new MembershipType
                    {
                        Id = 3,
                        Name = "Quaterly",
                        SignUpFee = 90,
                        DurationInMonths = 3,
                        DiscountRate = 15
                    },
                    new MembershipType
                    {
                        Id = 4,
                        Name = "Yearly",
                        SignUpFee = 300,
                        DurationInMonths = 12,
                        DiscountRate = 20
                    });
                }

                // Genres
                if (context.Genre.Any())
                {
                    Console.WriteLine("Genres already seeded");
                }
                else
                {
                    context.Genre.AddRange(
                        new Genre
                        {
                            Id = 1,
                            Name = "Fantasy",
                        },
                        new Genre
                        {
                            Id = 2,
                            Name = "Horror"
                        });
                }

                // Customers
                if (context.Customers.Any())
                {
                    Console.WriteLine("Customers already seeded");
                }
                else
                {
                    context.Customers.AddRange(
                        new Customer
                        {
                            Name = "Michał Nowak",
                            Birthdate = new DateTime(1996, 3, 12),
                            HasNewsletterSubscribed = true,
                            MembershipTypeId = 1
                        },
                        new Customer
                        {
                            Name = "Jan Kowalski",
                            Birthdate = new DateTime(2000, 5, 11),
                            HasNewsletterSubscribed = false,
                            MembershipTypeId = 1
                        },
                        new Customer
                        {
                            Name = "Adam Frączek",
                            Birthdate = new DateTime(1987, 3, 9),
                            HasNewsletterSubscribed = false,
                            MembershipTypeId = 1,
                        });
                }

                // Books
                if (context.Books.Any())
                {
                    Console.WriteLine("Books already seeded");
                }
                else
                {
                    context.Books.AddRange(
                        new Book
                        {
                            ReleaseDate = new DateTime(1996, 3, 12),
                            AuthorName = "JK Rowler",
                            GenreId = 1,
                            Name = "Henry and Goblins of Furry",
                            NumberInStock = 20,
                        },
                        new Book
                        {
                            ReleaseDate = new DateTime(2000, 5, 11),
                            AuthorName = "JK Rowler",
                            GenreId = 1,
                            Name = "Henry and Goblins of Furry 2",
                            NumberInStock = 20,
                        },
                        new Book
                        {
                            ReleaseDate = new DateTime(1996, 3, 12),
                            AuthorName = "HP Craftlove",
                            GenreId = 2,
                            Name = "Call of Clutch",
                            NumberInStock = 13,
                        });
                }



                context.SaveChanges();
            }
        }
    }
}