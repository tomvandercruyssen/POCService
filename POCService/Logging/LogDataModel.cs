using Microsoft.EntityFrameworkCore;
using SharedLib.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace POCService.Logging
{
    public class Log
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid LogId { get; set; } = new Guid();
        public int Query { get; set; }
        public int Time { get; set; }
        public string Database { get; set; }
        public int AmountOfRecords { get; set; }
    }
}
