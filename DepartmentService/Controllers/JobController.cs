using Dapper;
using DepartmentService.Entities;
using DepartmentService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DepartmentService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly GmailScannerService _gmailScannerService;
        public JobController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("JobConnection");
            _gmailScannerService = new GmailScannerService();
        }
        
        [HttpGet]
        public IActionResult GetAllJobs(int page = 1,  int pageSize = 50)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var offset = (page - 1) * pageSize;
                    var sql = @"SELECT * FROM Stg_Data_Raw 
                        ORDER BY Nganh -- đổi cột Id nếu cần
                        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
                    var result = connection.Query(sql, new { Offset = offset, PageSize = pageSize });
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Có lỗi xảy ra: {ex.Message}");
            }
        }
        [HttpGet("cv/scan")]
        public async Task<ActionResult<List<AppliedCv>>> Scan()
        {
            var results = await _gmailScannerService.ScanAsync();
            return Ok(results);
        }

    }
}
