using Assessment.Report.API.Models;
using Assessment.Report.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assessment.Report.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ReportController : ControllerBase
    {
        private readonly ReportDbContext _context;
        private readonly RabbitMQPublisher _rabbitMQPublisher;
        public ReportController(ReportDbContext context, RabbitMQPublisher rabbitMQPublisher)
        {
            _context = context;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpGet]
        public async Task<IActionResult> CreateReportExcel()
        {
            var fileName = $"report-excel-{Guid.NewGuid().ToString().Substring(1, 10)}";
            Models.Report report = new()
            {
                FileName = fileName,
                Status = FileStatus.Creating,
            };
            await _context.Report.AddAsync(report);
            await _context.SaveChangesAsync();
            _rabbitMQPublisher.Publish(new ReportMessage() { ReportId = report.ReportId });
            return Ok(report);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _context.Report.ToListAsync();
            return Ok(reports);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file, int reportId)
        {
            if (file is not { Length: > 0 }) return BadRequest();

            Models.Report report = await _context.Report.FirstAsync(report => report.ReportId == reportId);
            string filePath = report.FileName + Path.GetExtension(file.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", filePath);

            using FileStream stream = new(path, FileMode.Create);
            await file.CopyToAsync(stream);

            report.CreatedDate = DateTime.Now;
            report.FilePath = filePath;
            report.Status = Models.FileStatus.Completed;

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
