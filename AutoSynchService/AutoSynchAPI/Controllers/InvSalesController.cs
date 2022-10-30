using AutoSynchAPI.Classes;
using AutoSynchAPI.Models;
using AutoSynchService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoSynchAPI.Controllers
{
    public class InvSalesController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public InvSalesController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetSaleDetails")]
        public IActionResult GetSaleDetails()
        {
            Models.DataResponse responseObj = new Models.DataResponse();
            try
            {
                using (Entities dbContext = new Entities())
                {
                    responseObj.invSaleDetails = dbContext.InvSaleDetail.ToList();
                    if (responseObj.invSaleDetails != null && responseObj.invSaleDetails.Count != 0)
                    {
                        responseObj.Response.Code = ApplicationResponse.SUCCESS_CODE;
                        responseObj.Response.Message = ApplicationResponse.SUCCESS_MESSAGE;
                        return Ok(responseObj);
                    }
                    else
                    {
                        responseObj.Response.Code = ApplicationResponse.NOT_EXISTS_CODE;
                        responseObj.Response.Message = ApplicationResponse.NOT_EXISTS_MESSAGE;
                        return NotFound(responseObj);
                    }
                }
            }
            catch (Exception ex)
            {
                responseObj.Response.Code = ApplicationResponse.GENERIC_ERROR_CODE;
                responseObj.Response.Message = ApplicationResponse.GENERIC_ERROR_MESSAGE;
                return BadRequest(responseObj);
            }
        }
        // POST api/<InvSalesController>
        [HttpPost]
        public void PostSaleDetails([FromBody] DataResponse dataResponse)
        {
        }
    }

}
