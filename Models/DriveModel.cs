using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp4.Models
{
    public class DriveModel
    {
        public string Name { get; set; }
        public DiskType Type { get; set; }
        public FileSystem FileSystem { get; set; }
        public int TotalSpace { get; set; }
        public int FreeSpace { get; set; }
    }

}
