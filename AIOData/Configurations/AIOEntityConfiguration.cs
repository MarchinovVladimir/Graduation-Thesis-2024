﻿using AIO.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AIO.Data.Configurations
{
    public class AIOEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(p => p.StartTime)
                .HasDefaultValueSql("GETDATE()");

            builder 
                .Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Agent)
                .WithMany(a => a.ProductsForSell)
                .HasForeignKey(p => p.AgentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(p => p.Buyer)
                .WithMany(b => b.ProductsBought)
                .HasForeignKey(p => p.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            builder
               .Property(p => p.Price)
               .HasPrecision(18, 2);

            builder
                .HasData(GenerateProducts());
        }

        private Product[] GenerateProducts()
        {
            ICollection<Product> products = new HashSet<Product>();

            Product product;

            product = new Product
            {
                Title = "Car",
                Description = "A car for sale",
                CategoryId = 1,
                ImageUrl = "https://images.unsplash.com/photo-1605559424843-9e4c228bf1c2?q=80&w=1000&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGNhcnN8ZW58MHx8MHx8fDA%3D",
                Price = 10000,
                AgentId = Guid.Parse("DB47B449-630E-4857-BC80-34A6C3E8E822"),
                BuyerId = Guid.Parse("AB6D096A-0CCC-49AE-2DB2-08DC32D4F58A"),
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddDays(7),
            };
            products.Add(product);

            product = new Product
            {
                Title = "Bike",
                Description = "A bike for sale",
                CategoryId = 2,
                ImageUrl = "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&dpr=1&w=500",
                Price = 100,
                AgentId = Guid.Parse("DB47B449-630E-4857-BC80-34A6C3E8E822"),
                BuyerId = Guid.Parse("AB6D096A-0CCC-49AE-2DB2-08DC32D4F58A"),               
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddDays(7),
            };
            products.Add(product);

            product = new Product
            {
                Title = "House",
                Description = "A house for sale",
                CategoryId = 3,
                ImageUrl = "https://www.bhg.com/thmb/H9VV9JNnKl-H1faFXnPlQfNprYw=/1799x0/filters:no_upscale():strip_icc()/white-modern-house-curved-patio-archway-c0a4a3b3-aa51b24d14d0464ea15d36e05aa85ac9.jpg",
                Price = 100000,
                AgentId = Guid.Parse("DB47B449-630E-4857-BC80-34A6C3E8E822"),
                BuyerId = null,               
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddDays(7),
            };
            products.Add(product);

            product = new Product
            {
                Title = "Phone",
                Description = "A phone for sale",
                CategoryId = 4,
                ImageUrl = "https://t3.ftcdn.net/jpg/03/20/87/00/360_F_320870096_CfJYzmZN5kUhFFkzkv17CKppGjqRBqSE.jpg",
                Price = 1000,
                AgentId = Guid.Parse("DB47B449-630E-4857-BC80-34A6C3E8E822"),
                BuyerId = Guid.Parse("AB6D096A-0CCC-49AE-2DB2-08DC32D4F58A"),             
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddDays(7),
            };

            return products.ToArray();
        }
    }
}
