using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R50_M10_Class_07_Work_01.Shared.DTO
{
    public class ImageUploadResponse
    {
        public string FileName { get; set; } = default!;
        public string StoredFileName { get; set; } = default!;
    }
}
