using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DatabaseCaseExtractor.Attributes;

namespace DatabaseCaseExtractorDemo.Model
{
    public class Table2
    {
        [Key]
        public int Id { get; set; }
        public string NameSecond { get; set; }
        public DateTime DateSecond { get; set; }
        public int IntSecond { get; set; }

        // Table One
        public Guid TableOneId { get; set; }

        [DatabaseCaseExtractorInclude]
        public Table1 TableOne { get; set; }

        // Table Thirds
        [DatabaseCaseExtractorInclude]
        public ICollection<Table3> TableThirds { get; set; }
    }
}
