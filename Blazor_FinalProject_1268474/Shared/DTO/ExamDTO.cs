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
    public class ExamDTO
    {
        public int ExamID { get; set; }
        [Required, StringLength(50), Display(Name = "Exam Name")]
        public string ExamName { get; set; } = default!;

        [Required, Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:0.00}")]
        public decimal ExamFee { get; set; }
        public virtual ICollection<ExamResultDTO> ExamResults { get; set; } = new List<ExamResultDTO>();

    }
}
