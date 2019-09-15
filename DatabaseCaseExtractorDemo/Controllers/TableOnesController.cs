using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseCaseExtractor;
using DatabaseCaseExtractor.Models;
using DatabaseCaseExtractorDemo.Base;
using DatabaseCaseExtractorDemo.Model;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseCaseExtractorDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableOnesController : BaseController<Table1>
    {
        public TableOnesController(DatabaseContext context) : base(context) { }


    }
}
