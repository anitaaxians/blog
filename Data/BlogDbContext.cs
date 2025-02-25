using BlogManagementAPI.Data.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using Microsoft.AspNetCore.Identity;


namespace BlogManagementAPI.Data
{
    public class BlogDbContext: IdentityDbContext<IdentityUser>
    {
        public BlogDbContext(DbContextOptions <BlogDbContext>options) : base(options) { }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Blog> Blog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Blogs)
                .WithOne(b => b.User)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
     
            }


    }
}
