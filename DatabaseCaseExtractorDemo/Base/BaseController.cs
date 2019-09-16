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
            // tempService.BeforeSaveChangesEvent += TempService_BeforeSaveChangesEvent;
            // tempService.AfterSaveChangesEvent += TempService_AfterSaveChangesEvent;
            // this.TempService_BeforeSaveChangesEvent(null, null);
            bool res = tempService.SetImportResult(importData);
            _context.SaveChanges();
            // this.TempService_AfterSaveChangesEvent(null, null);
            return Ok(res);
        }

        private void TempService_AfterSaveChangesEvent(object sender, DatabaseCaseExtractor.EventArgs.AfterSaveChangeEventArgs e)
        {
            try
            {
                _context.Database.ExecuteSqlCommand((string)$"SET IDENTITY_INSERT TableSeconds OFF");
            }
            catch (Exception)
            {

            }
        }

        private void TempService_BeforeSaveChangesEvent(object sender, DatabaseCaseExtractor.EventArgs.BeforeSaveChangeEventArgs e)
        {
            try
            {
                _context.Database.ExecuteSqlCommand((string)$"SET IDENTITY_INSERT TableSeconds ON");
            }
            catch (Exception)
            {

            }
        }
    }
}
