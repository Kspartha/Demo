﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usermngt.Entities
{
    public class TypeOfLiquor
    {
        public int type_of_liquor_id { get; set; }
        public string type_of_liquor_name { get; set; }
        public DateTime? created_on { get; set; }
        public string created_by { get; set; }
        public DateTime? updated_on { get; set; }
        public string updated_by { get; set; }
    }
}
