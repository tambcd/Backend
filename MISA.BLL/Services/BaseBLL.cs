using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.Common.Exceptions;
using MISA.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BLL.Services
{
    public class BaseBLL<MISAEntity> : IBaseBLL<MISAEntity> where MISAEntity : class
    {

        /// <summary>
        /// biến check validate custom 
        /// </summary>
        protected bool isValidCustom = true;
        /// <summary>
        /// danh sách lỗi 
        /// </summary>
        protected List<string> listMsgEr = new List<string>();

        /// <summary>
        ///  đối tượng DAL gửi resquest 
        /// </summary>
        IRepository<MISAEntity> repository;
        public BaseBLL(IRepository<MISAEntity> _repository)
        {
            repository = _repository;
        }

        public int InsertSevices(MISAEntity entity)
        {
            // check validate chung
            var isValid = Validate(entity);
            // check validate custom
            isValidCustom = ValidateCusrtom(entity);
            if (isValid && isValidCustom)
            {
                var res = repository.Insert(entity);
                return res;
            }
            else
            {
                throw new MISAException(Common.CommonResource.GetResoureString("InvalidInput"), listMsgEr);
            }
        }

        public int DeleteSevices(Guid id)
        {
            throw new NotImplementedException();
        }


        public int UpdateSevices(MISAEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool Validate(MISAEntity entity)
        {
            var isValid = true;
            // validate chung 

            // kiểm tra buộc nhập 

            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propName = property.Name;
                var value = property.GetValue(entity);
                var arrProNameDisplay = property.GetCustomAttributes(typeof(PropNameDisplay), false).FirstOrDefault();


                // nếu dữ liệu trống hoặc bằng null 
                if (property.IsDefined(typeof(MISARequired), false) && (value == null || value.ToString() == String.Empty))
                {
                    isValid = false;
                    propName = (arrProNameDisplay as PropNameDisplay).PropName;
                    listMsgEr.Add($"{propName} {Common.CommonResource.GetResoureString("EmptyCheck")}");
                }

            }
            return isValid; ;
        }
        protected virtual bool ValidateCusrtom(MISAEntity entity)
        {

            return true;
        }


       


    }
}
