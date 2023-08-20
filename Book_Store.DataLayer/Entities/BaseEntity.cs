﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.DataLayer.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsDelete { get; set; }
    }
}
