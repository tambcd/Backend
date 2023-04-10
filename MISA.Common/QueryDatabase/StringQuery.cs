using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.QueryDatabase
{
    public static class StringQuery
    {
        public static string GetAll = "SELECT * FROM {0}";
        public static string GetById = "SELECT * FROM {0} WHERE {1}_id = @Id}";
        public static string Insert = "proc_insert_{0}";
        public static string Update = "proc_update_{0}";
       
    }
}
