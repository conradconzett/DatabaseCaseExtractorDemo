using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DatabaseCaseExtractor.Attributes;

namespace DatabaseCaseExtractorDemo.Model
{
    public class Table1
    {
        [Key]
        public Guid Id { get; set; }
        public string NameOne { get; set; }
        public DateTime DateOne { get; set; }
        public int IntOne { get; set; }

        // Table Seconds
        [DatabaseCaseExtractorInclude]
        public ICollection<Table2> TableSeconds { get; set; }
    }
}
