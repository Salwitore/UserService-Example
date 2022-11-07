using Data.EntityClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Data.ContextClasses

{
    public class MasterContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<UserTicket> UserTickets { get; set; }

        
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySql("Server=127.0.0.1;Port=3306;Database=Service;Uid=root;Pwd=root;CharSet=utf8;", ServerVersion.AutoDetect("Server=127.0.0.1;Port=3306;Database=Service;Uid=root;Pwd=root;CharSet=utf8;"));
            optionsBuilder.UseMySql("Server=127.0.0.1;Port=3306;Database=Service;Uid=root;Pwd=root;CharSet=utf8;", ServerVersion.AutoDetect("Server=127.0.0.1;Port=3306;Database=Service;Uid=root;Pwd=root;CharSet=utf8;"));
        }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //User ile Ticketı Many-to-Many yaptığım kısım.
            modelBuilder.Entity<UserTicket>().HasKey(ut => new { ut.UserId, ut.TicketId });

            modelBuilder.Entity<UserTicket>()
                .HasOne<User>(ut => ut.User)
                .WithMany(u => u.UserTickets)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<UserTicket>()
                .HasOne<Ticket>(ut => ut.Ticket)
                .WithMany(u => u.UserTickets)
                .HasForeignKey(ut => ut.TicketId);

            //shadow property
            modelBuilder.Entity<Ticket>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<User>().Property<DateTime>("CreatedDate");
            modelBuilder.Entity<UserTicket>().Property<DateTime>("CreatedDate");


        }
        public override int SaveChanges()//User eklerken CreatedDate propertysi olmadığı için hata alıyorum.
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entityEntry in entries)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Property("CreatedDate").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

    }
}
