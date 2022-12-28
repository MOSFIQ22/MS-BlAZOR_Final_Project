using Blazor_FinalProject_1268474.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blazor_FinalProject_1268474.Shared.DTO
{
    public class TraineeDTO
    {
        [Key]
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
        [StringLength(60), Display(Name = "Course Name")]
        public string CourseName { get; set; } = default!;
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; } = default!;
        
    }
}
