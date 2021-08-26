using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payments.Infrastructure.Data.Entities
{
    [Table("log_message")]
    public class LogMessage
    {
        [Column("id"), Key]
        public int Id { get; set; }

        [Column("severity")]
        public string Severity { get; set; }

        [Column("msg_code")]
        public string Code { get; set; }

        [Column("msg_text")]
        public string Text { get; set; }

        [Column("source_package")]
        public string SourcePackage { get; set; }

        [Column("msg_datetime")]
        public DateTime Date { get; set; }
    }
}