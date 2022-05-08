using CoreWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebApi.Data
{
    public class ContactsAPIDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public ContactsAPIDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
