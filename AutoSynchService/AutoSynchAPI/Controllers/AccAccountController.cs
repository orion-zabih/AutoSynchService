using AutoSynchAPI.Classes;
using AutoSynchAPI.Models;
using AutoSynchService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutoSynchAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccAccountController : ControllerBase
    {
        // GET: api/<AccAccountController>
        [HttpGet]
        public IActionResult GetAccounts()
        {
            Models.DataResponse responseObj = new Models.DataResponse();
            try
            {
                using (Entities dbContext = new Entities())
                {
                    responseObj.accAccounts = dbContext.AccAccount.ToList();
                    if (responseObj.accAccounts != null && responseObj.accAccounts.Count!=0)
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
        

        // GET api/<AccAccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccAccountController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccAccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccAccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
