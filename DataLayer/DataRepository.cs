using Availity.Models.BusinessModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Availity.DataLayer
{
    public class DataRepository
    {
        public BusinessUser GetBusinessUserByUserId(string userId)
        {
            BusinessDataContext db = new BusinessDataContext();
            SqlParameter Id = new SqlParameter("@UserID", userId);
            List<BusinessUser> userResponse = db.Database.SqlQuery<BusinessUser>("GetBusinessUser @UserID", Id)?.ToList();
            return userResponse?.FirstOrDefault() ?? new BusinessUser();
        }
        public List<BusinessUser> GetBusinessAllUsers()
        {
            BusinessDataContext db = new BusinessDataContext();
            return db.Database.SqlQuery<BusinessUser>("GetAllBusinessUsers")?.ToList();
        }
        public List<CSVFile> GetCSVFilesByUserId(string userId)
        {
            BusinessDataContext db = new BusinessDataContext();
            List<CSVFile> userFiles = new List<CSVFile>();
            return db.CSVFiles?.Where(x => x.UploadedByUser == userId)?.ToList() ?? new List<CSVFile>();
        }
        public List<InsuranceFile> GetInsuranceFilesByParentId(Guid parentFileId)
        {
            BusinessDataContext db = new BusinessDataContext();
            return db.InsuranceFiles?.Where(x => x.ParentFileID == parentFileId)?.ToList() ?? new List<InsuranceFile>();
        }
        public async Task<int> SaveCSVFileAsync(CSVFile file)
        {
            BusinessDataContext db = new BusinessDataContext();
            db.CSVFiles.Add(file);
            return await db.SaveChangesAsync();
        }
        public async Task<int> SaveEnrolleesAsync(List<Enrollee> enrollees)
        {
            BusinessDataContext db = new BusinessDataContext();
            foreach (var enrollee in enrollees)
            {
                db.Enrollees.Add(enrollee);
            }
            return await db.SaveChangesAsync();
        }
        public async Task<int> SaveInsuranceFileAsync(List<InsuranceFile> insuranceFiles)
        {
            BusinessDataContext db = new BusinessDataContext();
            foreach (var insuranceFile in insuranceFiles)
            {
                db.InsuranceFiles.Add(insuranceFile);
            }
            return await db.SaveChangesAsync();
        }
        public CSVFile GetCSVFileById(Guid fileId)
        {
            BusinessDataContext db = new BusinessDataContext();
            return db.CSVFiles?.Where(x => x.FileID == fileId)?.FirstOrDefault() ?? new CSVFile();
        }
        public InsuranceFile GetInsuranceFileById(Guid fileId)
        {
            BusinessDataContext db = new BusinessDataContext();
            return db.InsuranceFiles?.Where(x => x.FileID == fileId)?.FirstOrDefault() ?? new InsuranceFile();
        }
        public async Task<int> MarkUserAsReviewedAsync(string userId)
        {
            BusinessDataContext db = new BusinessDataContext();
            AspNetUsers user = db.AspNetUsers?.Where(x => x.Id == userId)?.FirstOrDefault() ?? new AspNetUsers();
            user.IsReviewed = true;
            return await db.SaveChangesAsync();
        }

        public int GetEnrolleeCountForCSVFile(Guid fileId)
        {
            BusinessDataContext db = new BusinessDataContext();
            return db.Enrollees?.Where(x => x.CSVFileId == fileId)?.Count() ?? 0;
        }
    }
}