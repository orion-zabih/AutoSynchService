using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchSqlServer.CustomModels
{
    public class TableStructure
    {
        public string ColumnName { get; set; }
        public string TableName { get; set; }
        public string ColumnDefault { get; set; }
        public string IsNullable { get; set; }
        public string IsPrimaryKey { get; set; }
        public string DataType { get; set; }
        public string CharacterMaximumLength { get; set; }
        public TableStructure()
        {
            TableName=ColumnName=ColumnDefault=IsNullable=DataType=CharacterMaximumLength= IsPrimaryKey = string.Empty;
        }
    }
}
