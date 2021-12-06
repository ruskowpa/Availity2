using Availity.DataLayer;
using Availity.Models.BusinessModels;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Availity.Business
{
    public class DataManager
    {
        public BusinessUser GetBusinessUserByUserId(string userId)
        {
            DataRepository dr = new DataRepository();
            return dr.GetBusinessUserByUserId(userId);
        }
        public List<BusinessUser> GetBusinessAllUsers()
        {
            DataRepository dr = new DataRepository();
            return dr.GetBusinessAllUsers();
        }
        public List<CSVFile> GetCSVFilesByUserId(string userId)
        {
            DataRepository dr = new DataRepository();
            return dr.GetCSVFilesByUserId(userId);
        }
        public List<InsuranceFile> GetInsuranceFilesByParentId(Guid parentFileId)
        {
            DataRepository dr = new DataRepository();
            return dr.GetInsuranceFilesByParentId(parentFileId);
        }
        public async Task<int> SaveCSVFileAsync(CSVFile file)
        {
            DataRepository dr = new DataRepository();
            return await dr.SaveCSVFileAsync(file);
        }
        public async Task<int> SaveEnrolleesAsync(List<Enrollee> enrollees)
        {
            DataRepository dr = new DataRepository();
            return await dr.SaveEnrolleesAsync(enrollees);
        }
        public async Task<int> SaveInsuranceFileAsync(List<InsuranceFile> insuranceFiles)
        {
            DataRepository dr = new DataRepository();
            return await dr.SaveInsuranceFileAsync(insuranceFiles);
        }
        public CSVFile GetCSVFileById(Guid fileId)
        {
            DataRepository dr = new DataRepository();
            return dr.GetCSVFileById(fileId);
        }
        public InsuranceFile GetInsuranceFileById(Guid fileId)
        {
            DataRepository dr = new DataRepository();
            return dr.GetInsuranceFileById(fileId);
        }
        public async Task<int> MarkUserAsReviewedAsync(string userId)
        {
            DataRepository dr = new DataRepository();
            return await dr.MarkUserAsReviewedAsync(userId);
        }
        public int GetEnrolleeCountForCSVFile(Guid fileId)
        {
            DataRepository dr = new DataRepository();
            return dr.GetEnrolleeCountForCSVFile(fileId);
        }
        public async Task<bool> ImportCSVAsync(HttpPostedFileBase files, string serverMappedPath, string userId)
        {
            bool retVal = false;
            DataRepository dr = new DataRepository();
            try
            {
                if (files != null && files.FileName != null)
                {
                    string fileName = Path.GetFileName(files?.FileName);

                    if (Path.GetExtension(fileName).ToUpper() != ".CSV")
                    {
                        return retVal;
                    }

                    var fileId = Guid.NewGuid();
                    Directory.CreateDirectory(serverMappedPath + fileId + "/");
                    var path = Path.Combine(serverMappedPath + fileId + "/",
                    Path.GetFileName(fileName));
                    files.SaveAs(path);

                    CSVFile dbFile = new CSVFile();
                    dbFile.FileID = fileId;
                    dbFile.FileName = files.FileName;
                    dbFile.FileContents = null;
                    dbFile.UploadedByUser = userId;
                    dbFile.FilePath = path;

                    await dr.SaveCSVFileAsync(dbFile);

                    //parse CSV
                    List<Enrollee> enrollees = new List<Enrollee>();
                    using (var reader = new StreamReader(path))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        csv.Context.RegisterClassMap<EnrolleeMap>();
                        enrollees = csv.GetRecords<Enrollee>()?.ToList() ?? new List<Enrollee>();
                    }

                    foreach (var enrollee in enrollees)
                    {
                        enrollee.CSVFileId = dbFile.FileID;
                        enrollee.Id = Guid.NewGuid();
                    }

                    await dr.SaveEnrolleesAsync(enrollees);
                    //create revised file 
                    //separate enrollees by insurance company in its own file.
                    Dictionary<string, List<Enrollee>> insuranceFiles = enrollees.GroupBy(x => x.InsuranceCompany).ToDictionary(g => g.Key, g => g.ToList());

                    //Additionally, sort the contents of each file by last and first name(ascending).
                    List<List<Enrollee>> insuranceEnrollees = new List<List<Enrollee>>();
                    foreach (var insuranceFile in insuranceFiles)
                    {
                        List<Enrollee> enrolleeList = insuranceFile.Value.OrderBy(x => x.LastName+x.FirstName).ToList();
                        //Lastly, if there are duplicate User Ids for the same Insurance Company, then only the record with the highest version should be included
                        List<string> conflictingUserIds = enrolleeList.GroupBy(x => x.UserId)
                                                         .Where(g => g.Count() > 1)
                                                         .Select(y => y.Key)
                                                         .ToList(); //returns conflicting userId(s).


                        //now we need to group on that userId and pick the largest.
                        foreach (var conflictingUserId in conflictingUserIds)
                        {
                            Enrollee keeper = enrolleeList.Where(x => x.UserId == conflictingUserId)?.OrderByDescending(x => x.UserId)?.FirstOrDefault();
                            enrolleeList.RemoveAll(x => x != keeper && x.UserId == conflictingUserId);
                        }

                        insuranceEnrollees.Add(enrolleeList);
                        }

                        List<InsuranceFile> insuranceFilesToSave = new List<InsuranceFile>();
                        // write the insurance files.
                        foreach (var currentInsuranceFileEnrollees in insuranceEnrollees)
                        {
                            var insuranceName = currentInsuranceFileEnrollees?.FirstOrDefault()?.InsuranceCompany ?? "";
                            var insuranceEnrolleeFileId = Guid.NewGuid();
                            using (var writer = new StreamWriter(serverMappedPath + fileId + "/" + insuranceName + ".csv"))
                            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                            {
                                csv.Context.RegisterClassMap<EnrolleeMap>();
                                csv.WriteRecords(currentInsuranceFileEnrollees);
                            }

                            // update the DB
                            InsuranceFile insFile = new InsuranceFile();
                            insFile.FileID = insuranceEnrolleeFileId;
                            insFile.FileName = insuranceName + ".csv";
                            insFile.ParentFileID = fileId;
                            insFile.FilePath = serverMappedPath + fileId + "/" + insuranceName + ".csv";
                            insuranceFilesToSave.Add(insFile);
                        }

                        await dr.SaveInsuranceFileAsync(insuranceFilesToSave);
                }

                retVal = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return retVal;
        }
    }
}