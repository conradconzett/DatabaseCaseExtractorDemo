using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DatabaseCaseExtractor.Models;
using Newtonsoft.Json;

namespace ExportHandler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private ExportDatabaseContext _context;
        public ExportController(ExportDatabaseContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<List<ExportModel>> Get()
        {
            return _context.Exports.Where(c => c.Done == null).ToList();
        }

        [HttpPost]
        public ActionResult<ExportModel> ExportFile(ExportLayout layout)
        {
            // Save exportlayout for local export
            ExportModel toSaveExport = new ExportModel();
            toSaveExport.ExportLayout = JsonConvert.SerializeObject(layout);
            _context.Exports.Add(toSaveExport);
            _context.SaveChanges();
            return Ok(toSaveExport);
        }
        [HttpDelete]
        public ActionResult<string> Delete(Guid id)
        {
            // Save exportlayout for local export
            ExportModel model = _context.Exports.Find(id);
            _context.Remove(model);
            _context.SaveChanges();
            return Ok("ok");
        }
    }
}
