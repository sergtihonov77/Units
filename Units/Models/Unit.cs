using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Units.Models
{
    public class Unit
    {
        public string Title { get; set; }
        public List<Employee> Employees { get; set; }
    }
}