using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labss
{
    [Serializable]
    public class ProjectData
    {
        public List<string> TableNames { get; set; } = new List<string>();
        public string SelectedTable { get; set; }
        public string ConnectionString { get; set; }
    }
}
