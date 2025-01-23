using Microsoft.EntityFrameworkCore;
using HireStreamDotNetProject.Models;

namespace HireStreamDotNetProject.Data {
    public class ApplicationDbContext: DbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}