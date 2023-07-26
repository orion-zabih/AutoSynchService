﻿using AutoSynchSqlServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoSynchAPI.Models
{
    public class TableStructureResponse
    {
        public List<AutoSynchSqlServer.CustomModels.TableStructure> tableStructures { get; set; }
        public List<string> dropQueries { get; set; }
        public List<string> createQueries { get; set; }
        public TableStructureResponse()
        {
            createQueries=new List<string>();
            dropQueries=new List<string>();
            tableStructures=new List<AutoSynchSqlServer.CustomModels.TableStructure>();
        }
        
    }
}
