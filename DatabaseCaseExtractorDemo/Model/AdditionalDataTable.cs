using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DatabaseCaseExtractor.Attributes;

namespace DatabaseCaseExtractorDemo.Model
{
    [AdditionalData]
    public class AdditionalDataTable
    {
        [Key]
        public Guid Id { get; set; }
        public string NameAdditional { get; set; }
    }
}
