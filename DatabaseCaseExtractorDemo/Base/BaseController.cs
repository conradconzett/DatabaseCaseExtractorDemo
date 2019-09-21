using DatabaseCaseExtractor;
using DatabaseCaseExtractor.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
            List<T> tempList = _context.Set<T>().AsQueryable().ToList();
            //string tempRes = Console.ReadLine();
            return Ok(tempList);
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
        public ActionResult Import(ExportResult importData)
        {
            ExportImportService<T> tempService = new ExportImportService<T>(_context);
            tempService.SetImportResult(importData);
            return Ok();
        }

        [HttpPost]
        [Route("statements")]
        public ActionResult<List<LogEntry>> Statements(ExportResult[] dataSets)
        {
            ExportImportService<T> tempService = new ExportImportService<T>(_context);
            return Ok(tempService.ExportSQLScripts(dataSets[0], dataSets[1]));
        }
    }
}
