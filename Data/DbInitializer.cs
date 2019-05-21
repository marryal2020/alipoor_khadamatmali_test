using System;
using System.Linq;
using alipoor_test.Models;

namespace alipoor_test.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DBContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any Persons.
            if (context.Persons.Any())
            {
                return;   // DB has been seeded
            }

            var persons = new Person[]
            {
                new Person {NationalID=1, FirstMidName = "Carson",   LastName = "Alexander",
                    BirthDate = DateTime.Parse("2010-09-01") },
                new Person { NationalID=2,FirstMidName = "Meredith", LastName = "Alonso",
                    BirthDate = DateTime.Parse("2012-09-01") },
                new Person { NationalID=3,FirstMidName = "Arturo",   LastName = "Anand",
                    BirthDate = DateTime.Parse("2013-09-01") },


            };

            foreach (Person s in persons)
            {
                context.Persons.Add(s);
            }
            context.SaveChanges();


            var adresses = new Address[]
            {
                new Address {AddressID = 1050, AddressLine = "a1",
                    NationalID =1
                },
                new Address {AddressID = 4022, AddressLine = "a2",
                    NationalID =1
                },
                new Address {AddressID = 4041, AddressLine = "a3",
                    NationalID = 2
                },
                new Address {AddressID = 1045, AddressLine = "a4",
                    NationalID = 2
                },
                new Address {AddressID = 3141, AddressLine = "a5",
                    NationalID =3
                },
                new Address {AddressID = 2021, AddressLine = "a6",
                    NationalID = 3
                },

            };

            foreach (Address c in adresses)
            {
                context.Addresses.Add(c);
            }
            context.SaveChanges();



        }
    }
}