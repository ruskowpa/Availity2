using Availity.Business;
using Availity.Models.BusinessModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Availity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId() ?? "";

            DataManager dm = new DataManager();
            BusinessUser currentBusinessUser = dm.GetBusinessUserByUserId(currentUserId);
            ViewData["currentUser"] = currentBusinessUser;

            if (currentBusinessUser?.RoleName == "Admin")
            {
                List<BusinessUser> allUsers = dm.GetBusinessAllUsers();
                ViewData["allUsers"] = allUsers;
            }

            return View();
        }

        [Authorize]
        public ActionResult EnrolleeData()
        {
            var userId = User.Identity.GetUserId() ?? "";
            List<CSVFile> userFiles = new List<CSVFile>();
            List<InsuranceFile> userInsurnaceFiles = new List<InsuranceFile>();

            if (userId != null && userId != "")
            {
                DataManager dm = new DataManager();

                userFiles = dm.GetCSVFilesByUserId(userId);

                foreach (var file in userFiles)
                {
                    file.EnrolleeCount = dm.GetEnrolleeCountForCSVFile(file.FileID);
                    List<InsuranceFile> currentFileChildren = dm.GetInsuranceFilesByParentId(file.FileID);
                    userInsurnaceFiles = userInsurnaceFiles.Union(currentFileChildren).ToList();
                }
            }

            ViewData["userFiles"] = userFiles;
            ViewData["userInsurnaceFiles"] = userInsurnaceFiles;
            return View();
        }

        [Authorize]
        [HttpPost]
        [ActionName("ImportCSV")]
        public async Task<ActionResult> ImportCSVAsync(HttpPostedFileBase files)
        {
            if (files == null || files.ContentLength == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            string serverMappedPath = Server.MapPath("~/Content/ImportedFiles/");
            string userId = User.Identity.GetUserId() ?? "";
            DataManager dm = new DataManager();
            if (await dm.ImportCSVAsync(files, serverMappedPath, userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }

        [Authorize]
        public ActionResult GetCSVFile(string fileId)
        {
            if (fileId == null)
            {
                return null;
            }

            DataManager dm = new DataManager();
            Guid fileIdGuid = new Guid(fileId);
            CSVFile file = dm.GetCSVFileById(fileIdGuid);

            byte[] fileBytes = System.IO.File.ReadAllBytes(file.FilePath);
            string fileName = file.FileName;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [Authorize]
        public FileContentResult GetInsuranceFiles(string fileId)
        {
            if (fileId == null)
            {
                return null;
            }

            DataManager dm = new DataManager();
            var fileIdGuid = new Guid(fileId);
            var file = dm.GetInsuranceFileById(fileIdGuid);
            byte[] fileBytes = System.IO.File.ReadAllBytes(file.FilePath);
            string fileName = file.FileName;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [Authorize]
        [HttpPost]
        [ActionName("MarkUserAsReviewed")]
        public async Task<ActionResult> MarkUserAsReviewedAsync(string userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }

            try
            {
                DataManager dm = new DataManager();
                await dm.MarkUserAsReviewedAsync(userId);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
                throw;
            }
        }
    }
}