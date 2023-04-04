using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mira.Service
{
    public class Config
    {   
        public const string Project = "Project";
        public static string ConnectionString { get; set; } = String.Empty;
        public static string CompanyName { get; set; } = String.Empty;
        public static string CompanyNumberPhone { get; set; } = String.Empty;
        public static string CompanyNumberPhoneShort { get; set; } = String.Empty;
        public static string CompanyEmail { get; set; } = String.Empty;
    }
}
