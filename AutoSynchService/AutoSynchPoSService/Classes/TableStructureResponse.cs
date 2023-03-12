﻿
using Microsoft.EntityFrameworkCore;

namespace AutoSynchPosService.Classes
{
    public class TableStructureResponse
    {       
        public List<string> dropQueries { get; set; }
        public List<string> createQueries { get; set; }
        public TableStructureResponse()
        {
            createQueries=new List<string>();
            dropQueries=new List<string>();
        }
        
    }
}
