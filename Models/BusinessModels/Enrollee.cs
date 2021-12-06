using CsvHelper.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Availity.Models.BusinessModels
{
    [Table("Enrollee")]
    public class Enrollee
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Version { get; set; }
        public string InsuranceCompany { get; set; }
        public Guid CSVFileId { get; set; }
    }

    public sealed class EnrolleeMap : ClassMap<Enrollee>
    {
        public EnrolleeMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.CSVFileId).Ignore();
            Map(m => m.Id).Ignore();
        }
    }
}