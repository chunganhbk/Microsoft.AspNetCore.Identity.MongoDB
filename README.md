# Microsoft.AspNetCore.Identity.MongoDB
MongoDB data store adaptor for ASP.NET Core Identity, which allows you to build ASP.NET Core web applications, including membership, login, and user data. With this library, you can store your user's membership related data on MongoDB.
# Use
    var database =  new MongoClient("mongodb://localhost:27017").GetDatabase("databaseName");
    services.AddIdentity<IdentityUser, IdentityRole>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            })
           .RegisterMongoStores<IdentityUser, IdentityRole>(database).AddDefaultTokenProviders();
