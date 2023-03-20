using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.Common.Exceptions;
using MISA.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var isValid = Validate(entity , "insert");
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
            var listMsgEr = new List<string>();

            // gọi đến DAL gửi resquest 
            var res = repository.Delete(id);
            return res;
        }


        public int UpdateSevices(MISAEntity entity)
        {
            // check validate chung
            var isValid = Validate(entity,"update");
            // check validate custom
            isValidCustom = ValidateCusrtom(entity);
            if (isValid && isValidCustom)
            {
                var res = repository.Update(entity);
                return res;
            }
            else
            {
                throw new MISAException(Common.CommonResource.GetResoureString("InvalidInput"), listMsgEr);
            }
        }

        public bool Validate(MISAEntity entity , string typeVailde)
        {
            
            var isValid = true;
            
                var properties = entity.GetType().GetProperties();
                Guid identity;


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
                    if (property.IsDefined(typeof(MISANumberBig), false) )
                    {
                        if (value.ToString()[1] == '.')
                        {
                            if(Int32.Parse(value.ToString().Split('+')[1]) >= 17)
                            {
                                isValid = false;
                                propName = (arrProNameDisplay as PropNameDisplay).PropName;
                                listMsgEr.Add($"{propName} {Common.CommonResource.GetResoureString("ValueNotType")}");
                            }
                        }
                    else if(value.ToString().Length > 15)
                        {
                            isValid = false;
                            propName = (arrProNameDisplay as PropNameDisplay).PropName;
                            listMsgEr.Add($"{propName} {Common.CommonResource.GetResoureString("ValueNotType")}");
                        }

                       
                    }
                
               
             
            }

            // validate chung 
            return isValid;
            // kiểm tra buộc nhập 


        }
        protected virtual bool ValidateCusrtom(MISAEntity entity)
        {

            return true;
        }

        public string AutoCodeSevices()
        {
            string data = repository.getCodeNewfirst();        

             return repository.getAutoCode(data.Substring(0, 2).ToString()) ;
            
        }
    }
}
