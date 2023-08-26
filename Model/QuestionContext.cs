using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace simplequiz.Model
{
    public class QuestionContext : DbContext
    {
        public QuestionContext(DbContextOptions<QuestionContext> options) : base(options)
        {
        }
        public DbSet<Questionaire> Questionaire { get; set; }
        public DbSet<Choice> Choice { get; set; }
    }
}
