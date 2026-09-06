using Microsoft.EntityFrameworkCore;
using StudentAdministrationDatabase.Models;
using StudentAdministrationDatabase.SampleData;

namespace StudentAdministrationDatabase.Database;

/// <summary>
///     Represents the database context for the Student Administration system.
///     Provides access to the database entities and manages database operations.
/// </summary>
public class StudentAdministrationDbContext : DbContext
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="StudentAdministrationDbContext" /> class
    ///     using the specified options.
    /// </summary>
    /// <param name="options">
    ///     The options to configure the database context, including the connection string
    ///     and other database-related settings.
    /// </param>
    public StudentAdministrationDbContext(DbContextOptions<StudentAdministrationDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    ///     Gets or sets the collection of courses available in the student administration database.
    /// </summary>
    /// <value>
    ///     A <see cref="DbSet{TEntity}" /> representing the courses in the database.
    /// </value>
    public DbSet<Course> Courses { get; set; }

    /// <summary>
    ///     Gets or sets the <see cref="DbSet{TEntity}" /> representing the collection of degree programs in the database.
    /// </summary>
    /// <value>
    ///     A <see cref="DbSet{DegreeProgram}" /> that can be used to query and save instances of <see cref="DegreeProgram" />.
    /// </value>
    /// <remarks>
    ///     This property provides access to the degree programs stored in the database.
    ///     It is part of the Entity Framework Core context and enables CRUD operations on the <see cref="DegreeProgram" />
    ///     entities.
    /// </remarks>
    public DbSet<DegreeProgram> DegreePrograms { get; set; }

    /// <summary>
    ///     Gets or sets the <see cref="DbSet{TEntity}" /> representing the grades in the database.
    /// </summary>
    /// <value>
    ///     A <see cref="DbSet{TEntity}" /> of <see cref="Grade" /> entities, allowing CRUD operations
    ///     on grades assigned to students for specific courses.
    /// </value>
    public DbSet<Grade> Grades { get; set; }

    /// <summary>
    ///     Gets or sets the <see cref="DbSet{TEntity}" /> representing the collection of students in the database.
    /// </summary>
    /// <value>
    ///     A <see cref="DbSet{Student}" /> that can be used to query and save instances of <see cref="Student" />.
    /// </value>
    /// <remarks>
    ///     This property provides access to the student entities stored in the database.
    ///     It is used by Entity Framework Core to perform CRUD operations on the <see cref="Student" /> table.
    /// </remarks>
    public DbSet<Student> Students { get; set; }

    /// <summary>
    ///     Gets or sets the collection of universities in the Student Administration database.
    /// </summary>
    /// <value>
    ///     A <see cref="DbSet{TEntity}" /> representing the universities in the database.
    /// </value>
    public DbSet<University> Universities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().ToTable(nameof(Student));

        // A student number is a natural key and must be unique across all students.
        modelBuilder.Entity<Student>().HasIndex(s => s.StudentNumber).IsUnique();

        modelBuilder.Entity<Student>().HasOne(s => s.University).WithMany().HasForeignKey(s => s.UniversityId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Student>().HasMany(s => s.Grades).WithOne(g => g.Student).HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Grade>().HasOne(g => g.Course).WithMany().HasForeignKey(g => g.CourseId)
            .OnDelete(DeleteBehavior.Restrict);

        // A student can have at most one grade per course. This enforces at the database level the
        // "one grade per course" rule the application already assumes (create-or-update, credit math).
        modelBuilder.Entity<Grade>().HasIndex(g => new { g.StudentId, g.CourseId }).IsUnique();

        modelBuilder.Entity<Grade>().ToTable(nameof(Grade));

        modelBuilder.Entity<Course>().ToTable(nameof(Course)).HasOne(c => c.DegreeProgram).WithMany(dp => dp.Courses)
            .HasForeignKey(c => c.DegreeProgramId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DegreeProgram>().ToTable(nameof(DegreeProgram));
        modelBuilder.Entity<University>().ToTable(nameof(University));

        modelBuilder.Entity<Course>().HasData(SampleCourses.GetCourses());
        modelBuilder.Entity<Student>().HasData(SampleStudents.GetStudents());
        modelBuilder.Entity<DegreeProgram>().HasData(SampleDegreeProgram.GetDegreePrograms());
        modelBuilder.Entity<Grade>().HasData(SampleGrades.GetGrades());
        modelBuilder.Entity<University>().HasData(SampleUniversities.GetUniversities());
    }
}