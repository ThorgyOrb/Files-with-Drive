using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication3.Models;
using System.IO;
using System.Web;
using WebApplication3.Models;


namespace WebApplication3.Controllers
{
    

    public class HomeController : Controller
    {

        private readonly GoogleDriveFilesRepository _googleDriveFilesRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public HomeController(GoogleDriveFilesRepository googleDriveFilesRepository, IWebHostEnvironment hostingEnvironment)
        {
            _googleDriveFilesRepository = googleDriveFilesRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        
        [HttpGet]
        public ActionResult GetGoogleDriveFiles()
        {
            return View(GoogleDriveFilesRepository.GetDriveFiles());
        }
        
        [HttpPost]
        public ActionResult DeleteFile(GoogleDriveFiles file)
        {
            GoogleDriveFilesRepository.DeleteFile(file);
            return RedirectToAction("GetGoogleDriveFiles");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            await GoogleDriveFilesRepository.FileUploadAsync(file, _hostingEnvironment);
            return RedirectToAction("GetGoogleDriveFiles");
        }



        public IActionResult DownloadFile(string id)
        {
            string FilePath = _googleDriveFilesRepository.DownloadGoogleFile(id);

            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(FilePath, FileMode.Open))
            {
                stream.CopyTo(memoryStream);
            }
            memoryStream.Position = 0;

            return File(memoryStream, "application/zip", Path.GetFileName(FilePath));
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
