﻿using ClosedXML.Excel;
using RegistanFerghanaLC.Domain.Enums;
using RegistanFerghanaLC.Service.Dtos.FileViewModels;
using RegistanFerghanaLC.Service.Dtos.Students;
using RegistanFerghanaLC.Service.Dtos.Teachers;
using RegistanFerghanaLC.Service.Interfaces.Files;

namespace RegistanFerghanaLC.Service.Services.Files
{
    public class ExcelService : IExcelService
    {
        private readonly IFileService _fileService;
        public ExcelService(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<List<StudentRegisterDto>> ReadStudentFileAsync(FileModeldto filemodel)
        {
            string fullPath = await _fileService.CreateFile(filemodel);

            List<StudentRegisterDto> dtos = new();
            using (IXLWorkbook workbook = new XLWorkbook(fullPath, XLEventTracking.Disabled))
            {
                IXLWorksheet worksheet = workbook.Worksheets.First();
                IXLRow headers = worksheet.FirstRow();
                if (headers.Cell("A").Value.ToString() != "" ||
                    headers.Cell("B").Value.ToString() != "name" ||
                    headers.Cell("C").Value.ToString() != "surname" ||
                    headers.Cell("D").Value.ToString() != "phone" ||
                    headers.Cell("E").Value.ToString() != "password" ||
                    headers.Cell("F").Value.ToString() != "birthdate" ||
                    headers.Cell("G").Value.ToString() != "subject" ||
                    headers.Cell("H").Value.ToString() != "level"
                    )
                    throw new Exception("Invalid excel table");
                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                {
                    StudentRegisterDto dto = new StudentRegisterDto();
                    dto.FirstName = row.Cell("B").Value.ToString();
                    dto.LastName = row.Cell("C").Value.ToString();
                    dto.PhoneNumber = row.Cell("D").Value.ToString();
                    dto.Password = row.Cell("E").Value.ToString();
                    dto.BirthDate = DateTime.Parse(row.Cell("F").Value.ToString());
                    dto.Subject = row.Cell("G").Value.ToString();
                    Enum.TryParse(row.Cell("H").Value.ToString(), out EnglishLevel level);
                    dto.StudentLevel = level;
                    dtos.Add(dto);
                }
            }
            await _fileService.DeleteFileAsync(fullPath);
            return dtos;
        }

        public async Task<List<TeacherRegisterDto>> ReadTeacherFileAsync(FileModeldto filemodel)
        {
            string fullPath = await _fileService.CreateFile(filemodel);

            List<TeacherRegisterDto> dtos = new();
            using (IXLWorkbook workbook = new XLWorkbook(fullPath, XLEventTracking.Disabled))
            {
                IXLWorksheet worksheet = workbook.Worksheets.First();
                IXLRow headers = worksheet.FirstRow();
                if (headers.Cell("A").Value.ToString() != "" ||
                    headers.Cell("B").Value.ToString() != "name" ||
                    headers.Cell("C").Value.ToString() != "surname" ||
                    headers.Cell("D").Value.ToString() != "phone" ||
                    headers.Cell("E").Value.ToString() != "password" ||
                    headers.Cell("F").Value.ToString() != "birthdate" ||
                    headers.Cell("G").Value.ToString() != "subject" ||
                    headers.Cell("H").Value.ToString() != "level"
                    )
                    throw new Exception("Invalid excel table");
                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                {
                    TeacherRegisterDto dto = new TeacherRegisterDto();
                    dto.FirstName = row.Cell("B").Value.ToString();
                    dto.LastName = row.Cell("C").Value.ToString();
                    dto.PhoneNumber = row.Cell("D").Value.ToString();
                    dto.Password = row.Cell("E").Value.ToString();
                    dto.BirthDate = DateTime.Parse(row.Cell("F").Value.ToString());
                    dto.Subject = row.Cell("G").Value.ToString();
                    dtos.Add(dto);
                }
            }
            await _fileService.DeleteFileAsync(fullPath);
            return dtos;
        }
    }
}
