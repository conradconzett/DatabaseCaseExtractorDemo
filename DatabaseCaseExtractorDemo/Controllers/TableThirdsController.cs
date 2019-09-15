
using DatabaseCaseExtractorDemo.Base;
using DatabaseCaseExtractorDemo.Model;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseCaseExtractorDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableThirdsController : BaseController<Table3>
    {
        public TableThirdsController(DatabaseContext context): base(context) { }
    }
}
