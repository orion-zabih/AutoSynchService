﻿using System;
using System.Collections.Generic;

namespace AutoSynchSqlServer.Models
{
    public partial class ResFoodToAddonsMapping
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int AddonId { get; set; }
    }
}
