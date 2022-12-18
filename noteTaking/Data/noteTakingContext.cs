using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using noteTaking.Models;

namespace noteTaking.Data
{
    public class noteTakingContext : DbContext
    {
        public noteTakingContext (DbContextOptions<noteTakingContext> options)
            : base(options)
        {
        }

        public DbSet<noteTaking.Models.Posts> Posts { get; set; } = default!;

        public DbSet<noteTaking.Models.Category> Category { get; set; }

        public DbSet<noteTaking.Models.Comments> Comments { get; set; }
    }
}
