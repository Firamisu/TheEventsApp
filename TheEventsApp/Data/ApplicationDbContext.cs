using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheEventsApp.Models;

namespace TheEventsApp;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    

        modelBuilder.Entity<ApplicationUser>()
           .Property(x => x.FirstName).HasMaxLength(255);
        modelBuilder.Entity<ApplicationUser>()
           .Property(x => x.LastName).HasMaxLength(255);

    

        modelBuilder.Entity<Event>()
            .HasOne(e => e.Organizer).WithMany(u => u.OrganizedEvents).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Event>()
            .HasMany(e => e.Participants).WithMany(u => u.ParticipatedEvents);
           
            
           
    }





    public DbSet<Event> Events { get; set; }

}