using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Availity.Models.BusinessModels
{
    [Table("CSVFile")]
    public class CSVFile
    {
        [Key]
        public Guid FileID { get; set; }
        public string FileName { get; set; }
        public string FileContents { get; set; }
        public string UploadedByUser { get; set; }
        public string FilePath { get; set; }
        [NotMapped]
        public int EnrolleeCount { get; set; }
    }
}