using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balance_History.src
{
    [Serializable]
    public class SaveJsonToDisk
    {
        public List<string> Categories {  get; set; }
        public SaveJsonToDisk() { }
    }
}
