using System;

namespace ExcelDataImportar.Models
{
    public class AuditMasterData
    {
        public int Id { get; set; }
        public string ExcelId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string FileName { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
