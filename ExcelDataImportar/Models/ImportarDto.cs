using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ExcelDataImportar.Models
{
    public class ImportarDto
    {
        public IFormFile File { get; set; }
        public bool HasError { get; set; }
        public string Message { get; set; }
        public string FileName { get; set; }
        public AuditData AuditData { get; set; }
        public List<AuditData> AuditDataList { get; set; }
        public int TotalCount { get; set; }
        public List<string> Worksheets { get; set; }
        public string Worksheet { get; set; }
        public bool ForImport { get; set; }
    }
}
