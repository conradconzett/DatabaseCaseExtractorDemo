using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExportHandler
{
    public class ExportModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Done { get; set; }
        public string ExportLayout { get; set; }
    }
}
