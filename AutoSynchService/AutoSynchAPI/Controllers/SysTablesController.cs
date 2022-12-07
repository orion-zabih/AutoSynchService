using AutoSynchService.Models;
using Microsoft.AspNetCore.Mvc;

namespace AutoSynchAPI.Controllers
{

    [Route("api/SysTables")]
    [ApiController]
    public class SysTablesController : ControllerBase
    {
        private readonly ILogger<SysTablesController> _logger;

        public SysTablesController(ILogger<SysTablesController> logger)
        {
            _logger = logger;
        }
        [Route("GetSysTables")]
        [HttpGet]
        public IActionResult GetSysTables()
        {
            Models.SysTablesResponse responseObj = new Models.SysTablesResponse();
            try
            {
                using (Entities dbContext = new Entities())
                {//branch id in appsettings
                    //org//all
                    //inv//all
                    //usr//all
                    //accfiscalyear, 
                    responseObj.sysControllesGroups = dbContext.SysControllesGroup.ToList();
                    responseObj.sysExecptionLoggings = dbContext.SysExecptionLogging.ToList();
                    responseObj.sysFeatures = dbContext.SysFeature.ToList();
                    responseObj.sysOrgFormsMappings = dbContext.SysOrgFormsMapping.ToList();
                    responseObj.sysForms = dbContext.SysForm.ToList();
                    responseObj.sysOrgModulesMappings = dbContext.SysOrgModulesMapping.ToList();
                    responseObj.sysLayouts = dbContext.SysLayout.ToList();
                    responseObj.sysModules = dbContext.SysModule.ToList();
                    responseObj.sysModuleFormsMappings = dbContext.SysModuleFormsMapping.ToList();
                    responseObj.syssystems = dbContext.SysSystem.ToList();
                    responseObj.sysWeekDays = dbContext.SysWeekDay.ToList();
                    responseObj.sysMonthNames = dbContext.SysMonthName.ToList();
                    responseObj.sysYears = dbContext.SysYear.ToList();
                    responseObj.sysLableContents = dbContext.SysLableContent.ToList();
                    responseObj.sysHtmls = dbContext.SysHtml.ToList();
                    responseObj.sysInvTypeWiseControlls = dbContext.SysInvTypeWiseControll.ToList();
                    if (responseObj.sysLayouts != null)
                    {
                        //responseObj.Response.Code = ApplicationResponse.SUCCESS_CODE;
                        //responseObj.Response.Message = ApplicationResponse.SUCCESS_MESSAGE;
                        return Ok(responseObj);
                    }
                    else
                    {
                        //responseObj.Response.Code = ApplicationResponse.NOT_EXISTS_CODE;
                        //responseObj.Response.Message = ApplicationResponse.NOT_EXISTS_MESSAGE;
                        return NotFound(responseObj);
                    }
                }
            }
            catch (Exception ex)
            {
                //responseObj.Response.Code = ApplicationResponse.GENERIC_ERROR_CODE;
                //responseObj.Response.Message = ApplicationResponse.GENERIC_ERROR_MESSAGE;
                return BadRequest(responseObj);
            }
        }
    }
}
