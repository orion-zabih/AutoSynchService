using AutoSynchAPI.Classes;
using AutoSynchAPI.Models;
using AutoSynchSqlServer.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AutoSynchAPI.Controllers
{
    [Route("api/AccAccount")]
    [ApiController]
    public class AccAccountController : ControllerBase
    {
        // GET: api/<AccAccountController>
        [Route("GetAccounts")]
        [HttpGet]
        public IActionResult GetAccounts()
        {
            Models.DataResponse responseObj = new Models.DataResponse();
            try
            {
                using (Entities dbContext = new Entities())
                {
                    responseObj.invProduct = dbContext.InvProduct.Where(g => g.BranchId == 1).ToList();
                    if (responseObj.invProduct != null && responseObj.invProduct.Count!=0)
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


        //GET api/<AccAccountController>/5

        [Route("GetAccounts/{id?}")]
        [HttpGet]
        public string GetAccAccount(int id)
        {
            return "value";
        }

        // POST api/<AccAccountController>
        //[HttpPost]
        //public async Task<IActionResult<DataResponse>> PostAccAccount( DataResponse dataResponse)
        //{

        //}

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
