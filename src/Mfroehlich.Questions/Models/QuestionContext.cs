using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Mfroehlich.Questions.Models
{
    public class QuestionContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }

        private string file;

        public QuestionContext(string file) {
            this.file = file;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) {
            options.UseSqlite($"Filename={ Path.GetFullPath(this.file) }");
        }
    }
}