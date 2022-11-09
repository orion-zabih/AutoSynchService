
namespace AutoSynchService.Models
{
    public class SysTablesResponse
    {
        public List<SysControllesGroup> sysControllesGroups { get; set; }
        public List<SysExecptionLogging> sysExecptionLoggings { get; set; }
        public List<SysFeature> sysFeatures { get; set; }
        public List<SysOrgFormsMapping> sysOrgFormsMappings { get; set; }
        public List<SysForm> sysForms { get; set; }
        public List<SysOrgModulesMapping> sysOrgModulesMappings { get; set; }
        public List<SysLayout> sysLayouts { get; set; }
        public List<SysModule> sysModules { get; set; }
        public List<SysModuleFormsMapping> sysModuleFormsMappings { get; set; }
        public List<SysSystem> syssystems { get; set; }
        public List<SysWeekDay> sysWeekDays { get; set; }
        public List<SysMonthName> sysMonthNames { get; set; }
        public List<SysYear> sysYears { get; set; }
        public List<SysLableContent> sysLableContents { get; set; }
        public List<SysHtml> sysHtmls { get; set; }
        public List<SysInvTypeWiseControll> sysInvTypeWiseControlls { get; set; }
        public SysTablesResponse()
        {
            sysControllesGroups = new List<SysControllesGroup>();
            sysExecptionLoggings = new List<SysExecptionLogging>();
            sysFeatures = new List<SysFeature>();
            sysOrgFormsMappings = new List<SysOrgFormsMapping>();
            sysForms = new List<SysForm>();
            sysOrgModulesMappings = new List<SysOrgModulesMapping>();
            sysLayouts = new List<SysLayout>();
            sysModules = new List<SysModule>();
            sysModuleFormsMappings = new List<SysModuleFormsMapping>();
            syssystems = new List<SysSystem>();
            sysWeekDays = new List<SysWeekDay>();
            sysMonthNames = new List<SysMonthName>();
            sysYears = new List<SysYear>();
            sysLableContents = new List<SysLableContent>();
            sysHtmls = new List<SysHtml>();
            sysInvTypeWiseControlls = new List<SysInvTypeWiseControll>();
        }
    }
}
