using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Blazor_FinalProject_1268474.Shared.Models
{
    public enum Result { pass = 1, fail }
    public class Course
    {
        public int CourseID { get; set; }
        [Required, StringLength(35), Display(Name = "Batch Name")]
        public string BatchName { get; set; } = default!;
        [Required, StringLength(45), Display(Name = "Course Name")]
        public string CourseName { get; set; } = default!;
        [Required, StringLength(90), Display(Name = "Course Desc")]
        public string CourseDesc { get; set; } = default!;
        [Required, StringLength(100), Display(Name = "Course Duration")]
        public string CourseDuration { get; set; } = default!;
        [Required, Column(TypeName = "date"), Display(Name = "Start Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date"), Display(Name = "End Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        [Display(Name = "Available")]
        public bool Available { get; set; }
        public virtual ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();
      

    }
    public class Exam
    {
        public int ExamID { get; set; }
        [Required, StringLength(50), Display(Name = "Exam Name")]
        public string ExamName { get; set; } = default!;

        [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal ExamFee { get; set; }
        public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();
    }
    public class ExamResult
    {
        [ForeignKey("Exam")]
        public int ExamID { get; set; }
        [ForeignKey("Trainee")]
        public int TraineeID { get; set; }
        [EnumDataType(typeof(Result))]
        public Result Result { get; set; }
        public Exam Exam { get; set; } = default!;
        public Trainee Trainee { get; set; } = default!;

    }
    public class Trainee
    {
        public int TraineeID { get; set; }
        [Required, StringLength(50), Display(Name = "Trainee Name")]
        public string TraineeName { get; set; } = default!;
        [Required, StringLength(70), Display(Name = "Trainee Address")]
        public string TraineeAddress { get; set; } = default!;
        [Required, StringLength(50), DataType(DataType.EmailAddress)]
        public string Email { get; set; } = default!;
        [Required, StringLength(150)]
        public string Picture { get; set; } = default!;
        [Display(Name = "Is Running")]
        public bool IsRunning { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Birth Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course? Course { get; set; } = default!;
        public virtual ICollection<ExamResult> ExamResults { get; set; } = new List<ExamResult>();

    }
    public class CourseDbContext : DbContext
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options) { }
        public DbSet<Course> Courses { get; set; } = default!;   
        public DbSet<ExamResult> ExamResults { get; set; } = default!;
        public DbSet<Exam> Exams { get; set; } = default!;
        public DbSet<Trainee> Trainees { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {       
            modelBuilder.Entity<ExamResult>().HasKey(ex => new { ex.ExamID, ex.TraineeID });
        }
    }
}
