using ExcelDataImportar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelDataImportar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExcelDataReaderBll _excelDataReaderBll;

        public HomeController(ILogger<HomeController> logger, ExcelDataReaderBll excelDataReaderBll)
        {
            _logger = logger;
            _excelDataReaderBll = excelDataReaderBll;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExcelImportar(ImportarDto importarDto)
        {
            if(importarDto.File == null)
            {
                importarDto.HasError = true;
                importarDto.ErrorText = "Please select excel file";
            }
            else
            {
                bool isAlreadyUploaded = await _excelDataReaderBll.CheckIfFileAlreadyUploaded(importarDto);
                if (isAlreadyUploaded)
                {
                    importarDto.HasError = true;
                    importarDto.ErrorText = $"{importarDto.File.FileName} already uploaded.";
                }
                else
                {
                    var file = importarDto.File;
                    var name = file.Name;
                    var fileName = file.FileName;
                    var spFileName = file.FileName.Split('.');
                    if (spFileName.Length == 2)
                    {
                        string ext = spFileName[1];
                        if (ext != "xls" && ext != "xlsx")
                        {
                            importarDto.HasError = true;
                            importarDto.ErrorText = "Please select excel file";
                        }
                        else
                        {
                            importarDto = await _excelDataReaderBll.ProcessExcelData(importarDto);
                        }
                    }
                }
            }
            return View(importarDto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> ExcelImportarAjax()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ExcelImportarAjax(ImportarDto importarDto)
        {
            if (importarDto.File == null)
            {
                importarDto.HasError = true;
                importarDto.ErrorText = "Please select excel file";
            }
            else
            {
                bool isAlreadyUploaded = await _excelDataReaderBll.CheckIfFileAlreadyUploaded(importarDto);
                if (isAlreadyUploaded)
                {
                    importarDto.HasError = true;
                    importarDto.ErrorText = $"{importarDto.File.FileName} already uploaded.";
                }
                else
                {
                    var file = importarDto.File;
                    var name = file.Name;
                    var fileName = file.FileName;
                    var spFileName = file.FileName.Split('.');
                    if (spFileName.Length == 2)
                    {
                        string ext = spFileName[1];
                        if (ext != "xls" && ext != "xlsx")
                        {
                            importarDto.HasError = true;
                            importarDto.ErrorText = "Please select excel file";
                        }
                        else
                        {
                            importarDto = await _excelDataReaderBll.ExtractExcelData(importarDto);
                        }
                    }
                }
            }
            return Json(importarDto);
        }

        [HttpPost]
        public async Task<IActionResult> PreviewExcel(string importarDto1)
        {
            if (importarDto1 != null)
            {
                ImportarDto importar = JsonConvert.DeserializeObject<ImportarDto>(importarDto1);
                return PartialView("~/views/home/_PreviewExcel.cshtml", importar.AuditDataList);
            }
            else
            {
                return PartialView("~/views/home/_PreviewExcel.cshtml", null);
            }
            
        }
    }
}
