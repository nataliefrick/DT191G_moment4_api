using DT191G_moment4_api.Models;
using Microsoft.EntityFrameworkCore;

namespace DT191G_moment4_api.Data
{
    public class SongListContext : DbContext
    {
        // constructor
        public SongListContext(DbContextOptions<SongListContext> options) : base(options)
        {
        }

        public DbSet<Song> Song { get; set; }
        public DbSet<Category> Category { get; set; }

    }
}