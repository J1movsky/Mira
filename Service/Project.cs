using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mira.Service
{
    public class Project
    {   
        public const string Configs = "Project";
        public static string? ConnectionString { get; set; } = String.Empty;
        public static string? CompanyName { get; set; } = String.Empty;
        public static string? CompanyNumberPhone { get; set; } = String.Empty;
        public static string? CompanyNumberPhoneShort { get; set; } = String.Empty;
        public static string? CompanyEmail { get; set; } = String.Empty;
    }
}
