using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DatabaseCaseExtractor.Attributes;

namespace DatabaseCaseExtractorDemo.Model
{
    public class Table3
    {
        [Key]
        public string Id { get; set; }
        public string NameThird { get; set; }
        public DateTime DateThird { get; set; }
        public int IntThird { get; set; }

        // Table One
        public int TableSecondId { get; set; }

        [DatabaseCaseExtractorInclude]
        [ForeignKey("TableSecondId")]
        public Table2 TableSecond { get; set; }

    }
}
