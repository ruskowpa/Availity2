using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Availity.Models.BusinessModels
{
    [Table("InsuranceFile")]
    public class InsuranceFile
    {
        [Key]
        public Guid FileID { get; set; }
        public string FileName { get; set; }
        public Guid ParentFileID { get; set; }
        public string FilePath { get; set; }
    }
}