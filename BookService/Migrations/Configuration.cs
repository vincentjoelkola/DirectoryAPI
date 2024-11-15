namespace BookService.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BookService.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BookService.Models.BookServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BookService.Models.BookServiceContext context)
        {
            var telService = new ServiceLanguage() { Id = 1, Language = "Telugu" };
            var engService = new ServiceLanguage() { Id = 2, Language = "English" };

            context.ServiceLanguages.AddOrUpdate(x => x.Id,
                telService,
                engService,
                new ServiceLanguage() { Id = 3, Language = "Hindi" },
                new ServiceLanguage() { Id = 4, Language = "Tamil" },
                new ServiceLanguage() { Id = 5, Language = "Kannada" });

            List<ServiceLanguage> lstServices = new List<ServiceLanguage>();
            lstServices.Add(telService);
            lstServices.Add(engService);
            var sharonAssembly = new ChristianAssembly()
            {
                Id = 1,
                AssemblyName = "Sharon Christian Assembly",
                NoOfPersons = 80,
                WorshipTime = "10:00 - 12:00",
                EmailAddress = "vincent.kola@gmail.com",
                PermanantPhoneNo = "9703319144",
                ServiceLanguages = lstServices,
                Address1 = "45/34, St Francis High School",
                LandMark = "Opp Dayanidhi College",
                District = "Bangalore",
                City = "Bangalore",
                Country = "India",
                PinCode = "523228",
                State = "Karnataka"
            };

            context.ChristianAssemblies.AddOrUpdate(x => x.Id, sharonAssembly);

            Child child1 = new Child() { Id = 1, ChildName = "Jephnath Hudson", Age = 3, Gender = 1 };
            Child child2 = new Child() { Id = 2, ChildName = "Luke Jairus", Age = 1, Gender = 1 };

            List<Child> lstChildren = new List<Child>();
            lstChildren.Add(child1);
            lstChildren.Add(child2);

            context.Children.AddOrUpdate(x => x.Id, child1, child2);

            context.Evangelists.AddOrUpdate(x => x.Id,
                new Evangelist()
                {
                    Id = 1,
                    Name = "Vincent Joel Kola",
                    DOB = DateTime.Parse(DateTime.Now.ToString()),
                    EduQualification  = "B.Tech",
                    WifesName = "Dr.Huldah",
                    WifesDOB = DateTime.Parse(DateTime.Now.ToString()),
                    DateOfCommMinistry = DateTime.Parse(DateTime.Now.ToString()),
                    //CommdAssemblyId = 1,
                    AssemblyId = 1,
                    EmailAddress = "vincent.kola@gmail.com",
                    PermanantPhoneNo = "9703319144",
                    Address1 = "45/34, St Francis High School",
                    LandMark = "Opp Dayanidhi College",
                    District = "Bangalore",
                    City = "Bangalore",
                    Country = "India",
                    PinCode = "523228",
                    State = "Karnataka",
                    Children = lstChildren,
                    WhatsAppNo = "9703319144",

                });

            context.Authors.AddOrUpdate(x => x.Id,
                new Author() { Id = 1, Name = "Jane Austen" },
                new Author() { Id = 2, Name = "Charles Dickens" },
                new Author() { Id = 3, Name = "Miguel de Cervantes" }
                );

            context.Books.AddOrUpdate(x => x.Id,
                new Book()
                {
                    Id = 1,
                    Title = "Pride and Prejudice",
                    Year = 1813,
                    AuthorId = 1,
                    Price = 9.99M,
                    Genre = "Commedy of manners"
                },
                new Book()
                {
                    Id = 2,
                    Title = "Northanger Abbey",
                    Year = 1817,
                    AuthorId = 1,
                    Price = 12.95M,
                    Genre = "Gothic parody"
                },
                new Book()
                {
                    Id = 3,
                    Title = "David Copperfield",
                    Year = 1850,
                    AuthorId = 2,
                    Price = 15,
                    Genre = "Bildungsroman"
                },
                new Book()
                {
                    Id = 4,
                    Title = "Don Quixote",
                    Year = 1617,
                    AuthorId = 3,
                    Price = 8.95M,
                    Genre = "Picaresque"
                });
        }
    }
}
