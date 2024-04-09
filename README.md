To create admin:
- Create an account through register button.( admin@admin.bg ).
- Next you must become a Seller.
- In the Program.cs in the builder.Services.AddDefaultIdentity<ApplicationUser>() is added Role as ".AddRoles<IdentityRole<Guid>>()". Pay attention ".AddRoles<IdentityRole<Guid>>()" must be added before ".AddEntityFrameworkStores<AIODbContext>()".
- An extension method "IApplicationBuilder SeedAdministrator(this IApplicationBuilder app, string email)" declared for the IApplicationBuilder interface. It extends the functionality of the IApplicationBuilder interface, allowing you to call this method on an instance of IApplicationBuilder
- The extention method  SeedAdministrator(this IApplicationBuilder app, string email) is called in Program.cs after authentication and authorization middlewares:
- If there is no role assign to a seller with the given email it will be seeded if there is the role assigment will be skiped.
  
 
