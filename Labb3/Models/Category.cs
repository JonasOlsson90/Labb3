using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3.Models
{
    public class Category
    {
        public string CategoryName { get; set; }
        public bool IsSelected { get; set; }

        public Category(string name)
        {
            CategoryName = name;
        }
    }
}
