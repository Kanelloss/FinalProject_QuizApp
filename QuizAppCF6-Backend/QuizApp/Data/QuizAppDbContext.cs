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

        //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    var entries = ChangeTracker.Entries()
        //        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        //    foreach (var entry in entries)
        //    {
        //        if (entry.Entity is Quiz quiz)
        //        {
        //            if (entry.State == EntityState.Modified)
        //                quiz.ModifiedAt = DateTime.UtcNow;

        //            if (entry.State == EntityState.Added)
        //                quiz.InsertedAt = DateTime.UtcNow;
        //        }

        //        if (entry.Entity is Question question)
        //        {
        //            if (entry.State == EntityState.Modified)
        //                question.ModifiedAt = DateTime.UtcNow;

        //            if (entry.State == EntityState.Added)
        //                question.InsertedAt = DateTime.UtcNow;
        //        }

        //        if (entry.Entity is QuizScore score)
        //        {
        //            if (entry.State == EntityState.Modified)
        //                score.ModifiedAt = DateTime.UtcNow;

        //            if (entry.State == EntityState.Added)
        //                score.InsertedAt = DateTime.UtcNow;
        //        }

        //        if (entry.Entity is User user)
        //        {
        //            if (entry.State == EntityState.Modified)
        //                user.ModifiedAt = DateTime.UtcNow;

        //            if (entry.State == EntityState.Added)
        //                user.InsertedAt = DateTime.UtcNow;
        //        }
        //    }

        //    return await base.SaveChangesAsync(cancellationToken);
        //}
    }
}
