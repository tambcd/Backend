using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Common.Exceptions
{
    public class MISAException : Exception
    {
        string MessageError;
        IDictionary errors;
        /// <summary>
        /// thông báo Exception 
        /// @ create by : mf1270
        /// @ create day : 20/2/2023
        /// </summary>
        /// <param name="msg"> nội dùng thông báo </param>
        /// <param name="listMsg"> danh sách nội dùng thông báo</param>
        
        public MISAException(string msg = null, List<string> listMsg = null)
        {

            MessageError = msg;
            errors = new Dictionary<string, List<string>>();
            errors.Add("ListValidate", listMsg);

        }
        public override string Message => this.MessageError;
        public override IDictionary Data => this.errors;



    }
}
