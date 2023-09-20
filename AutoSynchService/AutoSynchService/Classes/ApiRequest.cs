using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService.Classes
{
    internal class ApiRequest
    {
        internal string BranchId { get; set; }
        internal List<ApiRequestData> RequestDatas { get; set; }
        public ApiRequest()
        {
            RequestDatas = new List<ApiRequestData>();
            BranchId = string.Empty;
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
