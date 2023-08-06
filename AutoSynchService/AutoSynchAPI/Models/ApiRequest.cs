using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchPosService.Classes
{
    internal class ApiRequest
    {
        internal string BranchId { get; set; }
        internal string LocalDatabase { get; set; }
        internal List<ApiRequestData> RequestDatas { get; set; }
        public ApiRequest()
        {
            RequestDatas = new List<ApiRequestData>();
            BranchId = "0";
            LocalDatabase = "SqlServer";
        }
    }
    internal class ApiRequestData
    {
        internal string TableName { get; set; }
        internal List<string> Columns { get; set; }
        public ApiRequestData()
        {
            Columns = new List<string>();
            TableName = string.Empty;
        }
    }

}
