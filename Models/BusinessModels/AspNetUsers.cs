using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Availity.Models.BusinessModels
{
    [Table("AspNetUsers", Schema = "Test")]
    public class AspNetUsers
    {
        [Key]
        public string Id { get; set; }
        public bool IsReviewed { get; set; }
    }
}