using DatabaseCaseExtractor;
using DatabaseCaseExtractor.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseCaseExtractorDemo.Base
{
    public class BaseController<T>: Controller
        where T: class, new()
    {
        protected readonly DatabaseContext _context;

        public BaseController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<T>> Get()
        {
            return Ok(_context.Set<T>().AsQueryable().ToList());
        }

        [HttpPost]
        public void Post(T table)
        {
            _context.Set<T>().Add(table);
            _context.SaveChanges();
        }

        [HttpPost]
        [Route("export")]
        public ActionResult<ExportResult> Export(ExportLayout layout)
        {
            ExportImportService<T> tempService = new ExportImportService<T>(_context);
            var export = tempService.GetExportResult(layout);
            return Ok(export);
        }

        [HttpPost]
        [Route("import")]
        public ActionResult<bool> Import(ExportResult importData)
        {
            ExportImportService<T> tempService = new ExportImportService<T>(_context);
            return Ok(tempService.SetImportResult(importData));
        }
    }
}
