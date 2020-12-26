using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace satisfaction_review.Models
{
    public class ReviewContext : DbContext
    {
        public ReviewContext(DbContextOptions<ReviewContext> options)
         :base(options)
        {}
        
        public DbSet<Review> Reviews {get; set;}
    }
}