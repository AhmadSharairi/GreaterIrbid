using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class IrbidContext : DbContext
    {
        public IrbidContext(DbContextOptions<IrbidContext> options) : base(options)
        {
        }

        public DbSet<NewsArticle> NewsArticles { get; set; }
        public DbSet<NewsImage> NewsImages { get; set; }
        public DbSet<EnvironmentImage> EnvironmentImages {   get; set; }
        public DbSet<AdviceImage> AdviceImages { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ComplaintType> ComplaintTypes { get; set; }
        public DbSet<CitizenSatisfaction> CitizenSatisfactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsArticle>()
              .HasMany(n => n.Images)
              .WithOne(i => i.NewsArticle)
              .HasForeignKey(i => i.NewsArticleId);


            modelBuilder.Entity<Complaint>()
                   .HasOne(c => c.ComplaintType)
                   .WithMany(ct => ct.Complaints)
                   .HasForeignKey(c => c.ComplaintTypeId)
                   .OnDelete(DeleteBehavior.Restrict); // Avoid cascading issues

            modelBuilder.Entity<Complaint>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Complaints)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading issues

            modelBuilder.Entity<ComplaintType>()
                .HasOne(ct => ct.Category)
                .WithMany(c => c.ComplaintTypes)
                .HasForeignKey(ct => ct.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading issues

            base.OnModelCreating(modelBuilder);




        }
    }
}
