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
                importarDto.Message = "Please select excel file";
            }
            else
            {
                bool isAlreadyUploaded = await _excelDataReaderBll.CheckIfFileAlreadyUploaded(importarDto);
                if (isAlreadyUploaded)
                {
                    importarDto.HasError = true;
                    importarDto.Message = $"{importarDto.File.FileName} already uploaded.";
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
                            importarDto.Message = "Please select excel file";
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
                importarDto.Message = "Please select excel file";
            }
            else
            {
                bool isAlreadyUploaded = await _excelDataReaderBll.CheckIfFileAlreadyUploaded(importarDto);
                if (isAlreadyUploaded)
                {
                    importarDto.HasError = true;
                    importarDto.Message = $"{importarDto.File.FileName} already uploaded.";
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
                            importarDto.Message = "Please select excel file";
                        }
                        else
                        {
                            importarDto.FileName = fileName;
                            importarDto = await _excelDataReaderBll.ExtractExcelData(importarDto);
                        }
                    }
                }
            }
            return Json(importarDto);
        }

        [HttpPost]
        public async Task<IActionResult> PreviewOrImportExcel(string importarDto1)
        {
            if (importarDto1 != null)
            {
                ImportarDto importarDto = JsonConvert.DeserializeObject<ImportarDto>(importarDto1);
                if (importarDto.ForImport)
                {
                    importarDto.Message = await _excelDataReaderBll.Import(importarDto);
                    return PartialView("~/views/home/_PreviewExcel.cshtml", importarDto.AuditDataList);
                }
                else
                {
                    return PartialView("~/views/home/_PreviewExcel.cshtml", importarDto.AuditDataList);
                }
                
            }
            else
            {
                return PartialView("~/views/home/_PreviewExcel.cshtml", null);
            }
            
        }
    }
}
