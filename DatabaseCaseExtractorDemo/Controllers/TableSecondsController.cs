using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseCaseExtractorDemo.Base;
using DatabaseCaseExtractorDemo.Model;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseCaseExtractorDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableSecondsController : BaseController<Table2> {
        public TableSecondsController(DatabaseContext context) : base(context) { }
    }
}
