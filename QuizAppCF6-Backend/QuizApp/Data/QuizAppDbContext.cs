using Microsoft.EntityFrameworkCore;

namespace QuizApp.Data
{
    public class QuizAppDbContext : DbContext
    {
        public QuizAppDbContext() { }

        public QuizAppDbContext(DbContextOptions options) : base(options)
        {
        }

        //DbSet properties for each entity
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuizScore> QuizScores { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(e => e.UserRole).HasConversion<string>();

                entity.Property(e => e.InsertedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')"); 

                entity.Property(e => e.ModifiedAt)
               .ValueGeneratedOnAddOrUpdate()
               .HasDefaultValueSql("FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')"); 

                entity.HasIndex(e => e.Username, "IX_Users_Username").IsUnique();
                entity.HasIndex(e => e.Email, "IX_Users_Email").IsUnique();
            });

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.ToTable("Quizzes");

                entity.Property(e => e.InsertedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')"); 

                entity.Property(e => e.ModifiedAt)
               .ValueGeneratedOnAddOrUpdate()
               .HasDefaultValueSql("FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')"); 

                
                entity.HasMany(e => e.Questions)
                .WithOne(q => q.Quiz)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Questions");

                entity.Property(e => e.InsertedAt)
                      .ValueGeneratedOnAdd()
                      .HasDefaultValueSql("FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')"); 

                entity.Property(e => e.ModifiedAt)
                      .ValueGeneratedOnAddOrUpdate()
                      .HasDefaultValueSql("FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')"); 


                entity.HasOne(e => e.Quiz)
                .WithMany(q => q.Questions)
                .HasForeignKey(e => e.QuizId)
                .OnDelete(DeleteBehavior.Cascade); // Delete its questions when a quiz is deleted.
            });

            modelBuilder.Entity<QuizScore>(entity =>
            {

                entity.ToTable("QuizScores");

                entity.Property(e => e.InsertedAt)
                      .ValueGeneratedOnAdd()
                      .HasDefaultValueSql("FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')");

                entity.Property(e => e.ModifiedAt)
                      .ValueGeneratedOnAddOrUpdate()
                      .HasDefaultValueSql("FORMAT(SYSDATETIME(), 'yyyy-MM-dd HH:mm:ss')");

                entity.Property(e => e.QuizId).IsRequired();
                entity.Property(e => e.Score).IsRequired();

                // Relationship: Each score belongs to one user
                entity.HasOne(e => e.User)
                    .WithMany(u => u.Scores)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade); // Delete scores when a user is deleted.

                entity.HasOne(qs => qs.Quiz)
                .WithMany(q => q.QuizScores)
                .HasForeignKey(qs => qs.QuizId)
                .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
