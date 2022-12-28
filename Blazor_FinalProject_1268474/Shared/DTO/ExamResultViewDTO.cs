using Blazor_FinalProject_1268474.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_FinalProject_1268474.Shared.DTO
{
    public class ExamResultViewDTO
    {
        public int ExamID { get; set; }
        public int TraineeID { get; set; }
        public string TraineeName { get; set; } = default!;
        [Required]
        public Result Result { get; set; }
        
    }
}
