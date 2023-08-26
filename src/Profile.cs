using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balance_History.src
{
    internal class Profile
    {
        public string Name { get; set; }
        public string DBName { get; set; }
        public Profile(string name) {
            Name = name;
            DBName = name + ".db";
        }
    }
}
