using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common
{
    public static class CommonResource
    {
        public static string LaguageCode = "VN";

        public static string? GetResoureString(string keyResoure)
        {
            var langCode = CommonResource.LaguageCode;
            var resoureString = $"{langCode}_{keyResoure}";
            return MISA.Common.Resource.Resource.ResourceManager.GetString(resoureString);
        }
    }
}
