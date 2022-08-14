using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelDataImportar.Models
{
    public class ExcelDataReaderBll
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public ExcelDataReaderBll(IWebHostEnvironment env, AppDbContext context)
        {
            _env = env;
            _context = context;
        }
        public async Task<bool> CheckIfFileAlreadyUploaded(ImportarDto importarDto)
        {
            bool isExist = false;
            AuditMasterData master = await _context.ExcelMasterDatas.FirstOrDefaultAsync(x => x.FileName == importarDto.File.FileName);
            if (master != null)
            {
                isExist = true;
            }

            return isExist;

        }

        public async Task<string> Import(ImportarDto importarDto)
        {
            try
            {
                var audits = _context.AuditData.ToList();
                if (audits.Count > 0)
                {
                    _context.AuditData.RemoveRange(audits);
                    await _context.SaveChangesAsync();
                }
                _context.AuditData.AddRange(importarDto.AuditDataList);
                await _context.SaveChangesAsync();

                AuditMasterData auditMasterData = new();

                auditMasterData.CreatedAt = DateTimeOffset.Now;
                auditMasterData.ExcelId = importarDto.AuditDataList[1].ExcelId;
                auditMasterData.UserId = 1;
                auditMasterData.FileName = importarDto.FileName;
                _context.ExcelMasterDatas.AddRange(auditMasterData);
                await _context.SaveChangesAsync();
                importarDto.Message = "Excel data imported successfully";
            }
            catch (Exception ex)
            {
                importarDto.Message = ex.Message;
                importarDto.HasError = true;
            }

            return importarDto.Message;
        }
        public async Task<ImportarDto> ProcessExcelData(ImportarDto importarDto)
        {
            var file = importarDto.File;
            //string uploadsFolder = Path.Combine(_env.WebRootPath, "files");
            //string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            //string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            //await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
            List<AuditData> list = new();
            try
            {
                var audits = _context.AuditData.ToList();
                if (audits.Count > 0)
                {
                    _context.AuditData.RemoveRange(audits);
                    await _context.SaveChangesAsync();
                }
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    string excelId = Guid.NewGuid().ToString();
                    using (var package = new ExcelPackage(stream))
                    {
                        var firstSheet = package.Workbook.Worksheets["Sheet1"];
                        var rowCount = firstSheet.Dimension.Rows;
                        int colCount = firstSheet.Dimension.End.Column;

                        for (int row = 2; row <= rowCount; row++) // start from to 2 omit header
                        {
                            AuditData auditData = new AuditData();
                            
                            for (int col = 1; col <= colCount; col++)
                            {
                                if(col == 1)
                                    auditData.Col1 
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 2)
                                    auditData.Col2 
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 3)
                                    auditData.Col3
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 4)
                                    auditData.Col4
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 5)
                                    auditData.Col5 
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 6)
                                    auditData.Col6
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 7)
                                    auditData.Col7
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 8)
                                    auditData.Col8
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 9)
                                    auditData.Col9
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 10)
                                    auditData.Col10
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 11)
                                    auditData.Col11
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 12)
                                    auditData.Col12
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 13)
                                    auditData.Col13
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 14)
                                    auditData.Col14
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 15)
                                    auditData.Col15
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                if(col == 16)
                                    auditData.Col16
                                        = firstSheet.Cells[row, col].Value?.ToString().Trim();
                            }
                            auditData.ExcelId = excelId;
                            auditData.UserId = 1;
                            list.Add(auditData);
                        }

                        if(list.Count > 0)
                        {
                            importarDto.AuditDataList = list;
                            _context.AuditData.AddRange(list);
                            await _context.SaveChangesAsync();

                            AuditMasterData auditMasterData = new();

                            auditMasterData.CreatedAt = DateTimeOffset.Now;
                            auditMasterData.ExcelId = excelId;
                            auditMasterData.UserId = 1;
                            auditMasterData.FileName = file.FileName;
                            _context.ExcelMasterDatas.AddRange(auditMasterData);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                importarDto.Message = ex.Message;
                importarDto.HasError = true;
            }
            return importarDto;
        }


        public async Task<ImportarDto> ExtractExcelData(ImportarDto importarDto)
        {
            var file = importarDto.File;
            List<AuditData> list = new();
            importarDto.Worksheets = new();
            try
            {
                var audits = _context.AuditData.ToList();
                if (audits.Count > 0)
                {
                    _context.AuditData.RemoveRange(audits);
                    await _context.SaveChangesAsync();
                }
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    string excelId = Guid.NewGuid().ToString();
                    using (var package = new ExcelPackage(stream))
                    {
                        var firstSheet = importarDto.Worksheet != null
                                ? package.Workbook.Worksheets.FirstOrDefault(x=>x.Name.ToLower() == importarDto.Worksheet.ToLower())
                                : package.Workbook.Worksheets["sheet1"];

                        if (package.Workbook.Worksheets.Count > 0 || firstSheet == null)
                        {
                            foreach (ExcelWorksheet sheet in package.Workbook.Worksheets)
                            {
                                importarDto.Worksheets.Add(sheet.Name);
                            }
                            
                            var rowCount = firstSheet.Dimension.Rows;
                            int colCount = firstSheet.Dimension.End.Column;

                            for (int row = 2; row <= rowCount; row++) // start from to 2 omit header
                            {
                                AuditData auditData = new AuditData();

                                for (int col = 1; col <= colCount; col++)
                                {
                                    if (col == 1)
                                        auditData.Col1
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 2)
                                        auditData.Col2
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 3)
                                        auditData.Col3
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 4)
                                        auditData.Col4
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 5)
                                        auditData.Col5
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 6)
                                        auditData.Col6
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 7)
                                        auditData.Col7
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 8)
                                        auditData.Col8
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 9)
                                        auditData.Col9
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 10)
                                        auditData.Col10
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 11)
                                        auditData.Col11
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 12)
                                        auditData.Col12
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 13)
                                        auditData.Col13
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 14)
                                        auditData.Col14
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 15)
                                        auditData.Col15
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                    if (col == 16)
                                        auditData.Col16
                                            = firstSheet.Cells[row, col].Value?.ToString().Trim();
                                }
                                auditData.ExcelId = excelId;
                                auditData.UserId = 1;
                                list.Add(auditData);
                            }
                            if (list.Count > 0)
                            {
                                importarDto.AuditDataList = list;
                            }
                        }
                        else
                        {
                            importarDto.Message = "Excel dosen't contain default sheet Sheet1";
                            importarDto.HasError = true;
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                importarDto.Message = ex.Message;
                importarDto.HasError = true;
            }
            return importarDto;
        }
    }
}
