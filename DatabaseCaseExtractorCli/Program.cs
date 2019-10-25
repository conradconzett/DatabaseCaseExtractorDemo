using CommandLine;
using DatabaseCaseExtractor;
using DatabaseCaseExtractor.Interfaces;
using DatabaseCaseExtractor.Logger;
using DatabaseCaseExtractor.Models;
using DatabaseCaseExtractorDemo.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DatabaseCaseExtractorCli
{
    class Program
    {
        public static readonly ILoggerFactory loggerFactory = new LoggerFactory(new[] {
              new DatabaseCaseExtractorLoggerProvider()
        });
        static void Main(string[] args)
        {
            /*args = new string[] {
                "--applychanges",
                "--ModelName",
                "Table1",
                "--WorkingFile",
                @"C:\Users\Coni\Downloads\output.json",
                "--SecondFile", @"C:\Users\Coni\Downloads\second.json",
                "--StatementsOutput", @"C:\Users\Coni\Downloads\output.json"
            };*/
            Parser.Default.ParseArguments<Options>(args)
            .WithParsed<Options>(o =>
            {
                DbContextOptionsBuilder dbContextOptionBuilder = new DbContextOptionsBuilder();
                dbContextOptionBuilder.EnableSensitiveDataLogging(true);
                dbContextOptionBuilder.UseLoggerFactory(loggerFactory);
                dbContextOptionBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=DatabaseCaseExtractorDb;Integrated Security=SSPI;");
                DatabaseContext context = new DatabaseContext(dbContextOptionBuilder.Options);

                if (o.ModelName == "") new Exception("We need a model-name for an export");
                if (o.ExportProcess)
                {
                    if (o.WorkingFile == "") new Exception("We need a working-file for an export");

                    ExportLayout exportLayout = new ExportLayout();
                    exportLayout.EntityName = o.ModelName;
                    if (o.PrimaryKeyValue != "")
                    {
                        exportLayout.EntityPrimaryValue = o.PrimaryKeyValue;
                    }
                    ExportResult exportResult = ((IExportImportService)GetExportImportService(context, exportLayout.EntityName))
                        .GetExportResult(exportLayout);
                    File.WriteAllText(o.WorkingFile,
                        JsonConvert.SerializeObject(
                            exportResult,
                            Formatting.Indented,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }
                        )
                    );
                }
                else if (o.ImportProcess)
                {
                    if (o.WorkingFile == "") new Exception("We need a working-file for an import");
                    ExportResult exportResult = JsonConvert.DeserializeObject<ExportResult>(File.ReadAllText(o.WorkingFile));
                    ((IExportImportService)GetExportImportService(context, o.ModelName))
                        .SetImportResult(exportResult);
                }
                else if (o.StatementProcess)
                {
                    if (o.WorkingFile == "") new Exception("We need a working-file for a statetment");
                    if (o.SecondFile == "") new Exception("We need a second-file for an  statetment");
                    List<LogEntry> entries = ((IExportImportService)GetExportImportService(context, o.ModelName))
                        .ExportSQLScripts(
                            JsonConvert.DeserializeObject<ExportResult>(File.ReadAllText(o.WorkingFile)),
                            JsonConvert.DeserializeObject<ExportResult>(File.ReadAllText(o.SecondFile))
                        );
                    File.WriteAllText(o.StatementsOutput, JsonConvert.SerializeObject(entries));
                }
                else if (o.ApplyChanges)
                {
                    List<LogEntry> entries = JsonConvert.DeserializeObject<List<LogEntry>>(File.ReadAllText(o.WorkingFile));
                    foreach(LogEntry entry in entries)
                    {
                        context.Database.ExecuteSqlCommand(entry.Command, entry.Parameters.Values.ToArray().Reverse());
                    }
                }
            });
        }

        private static object GetExportImportService(DatabaseContext context, string modelName)
        {
            var properties = context.GetType().GetProperties();

            PropertyInfo setType = properties.Where(p => p.PropertyType.IsGenericType &&
                p.PropertyType.GetGenericArguments()[0].Name.ToUpper().Contains(modelName.ToUpper())).FirstOrDefault();

            var addionalInstance = typeof(ExportImportService<>).MakeGenericType(setType.PropertyType.GetGenericArguments()[0]);
            object subExportLayout = Activator.CreateInstance(addionalInstance, new object[] { context });
            return subExportLayout;
        }
    }

    class Options
    {
        [Option("export", Default = false)]
        public bool ExportProcess { get; set; } = false;
        [Option("import", Default = false)]
        public bool ImportProcess { get; set; } = false;
        [Option("statements", Default = false)]
        public bool StatementProcess { get; set; } = false;
        [Option("applychanges", Default = false)]
        public bool ApplyChanges { get; set; } = false;


        [Option("ModelName", Required = true)]
        public string ModelName { get; set; }

        [Option("PrimaryKeyValue", Default = "")]
        public string PrimaryKeyValue { get; set; }

        [Option("WorkingFile", Default = "")]
        public string WorkingFile { get; set; }
        [Option("SecondFile", Default = "")]
        public string SecondFile { get; set; }
        [Option("StatementsOutput", Default = "")]
        public string StatementsOutput { get; set; }

    }
}
