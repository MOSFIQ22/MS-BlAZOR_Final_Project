using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Blazor_FinalProject_1268474.Shared.DTO
{
    public class CourseDTO
    {
        [Key]
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

        public bool CanDelete { get; set; }
    }
}
