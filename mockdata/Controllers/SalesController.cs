using Microsoft.AspNetCore.Mvc;
using mockdata.Models;
using mockdata.Services;

namespace mockdata.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesController : ControllerBase
{
    private readonly MockDataService _mockDataService;

    public SalesController(MockDataService mockDataService)
    {
        _mockDataService = mockDataService;
    }

    [HttpGet("getallsales")]
    public ActionResult<IReadOnlyList<Sales>> GetAllSales()
    {
        return Ok(_mockDataService.GetAllSales());
    }

    [HttpGet("getsalesbycid/{cid:int}")]
    public ActionResult<Sales> GetSalesByCid(int cid)
    {
        var sale = _mockDataService.GetSalesByCid(cid);
        return sale is null ? NotFound() : Ok(sale);
    }

    [HttpPost("createmockdata")]
    public ActionResult<IReadOnlyList<Sales>> CreateMockData()
    {
        return Ok(_mockDataService.CreateMockData());
    }
}
