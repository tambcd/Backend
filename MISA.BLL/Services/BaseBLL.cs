﻿using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.Common.Enum;
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
        /// <summary>
        /// Thự hiện thêm đối tượng
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="entity">thông tin đối tường</param>
        /// <returns></returns>
        /// <exception cref="MISAException">khi gặp lỗi trả về</exception>
        public int InsertSevices(MISAEntity entity)
        {
            // check validate chung
            var isValid = Validate(entity , (int)MisaEnum.Insert);
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
        /// <summary>
        /// thực hiện xóa 1 bản ghi 
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="id">id bản ghi cần xóa </param>
        /// <returns></returns>
        public int DeleteSevices(Guid id)
        {
            var listMsgEr = new List<string>();

            // gọi đến DAL gửi resquest 
            var res = repository.Delete(id);
            return res;
        }

        /// <summary>
        /// thực hiện xửa 1 đối  tường 
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="entity">thông tin đối tượng </param>
        /// <returns></returns>
        /// <exception cref="MISAException">trả về lỗi </exception>
        public int UpdateSevices(MISAEntity entity)
        {
            // check validate chung
            bool isValid = Validate(entity, (int)MisaEnum.Update);
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

        /// <summary>
        /// thực hiện chuẩn hóa dữ liệu
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="entity">thông tin đối tường</param>
        /// <param name="typeVailde">iểu thêm hay sửa </param>
        /// <returns>true || fasle</returns>
        public bool Validate(MISAEntity entity , int typeVailde)
        {
            
            var isValid = true;            
                var properties = entity.GetType().GetProperties();
            // kiểm tra dữ liệu dựa vào Attribute tự định nghĩa 

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
                    // dữ liệu lớn 
                    /*if (property.IsDefined(typeof(MISANumberBig), false) )
                    {
                    if ((int)value == 0)
                    {
                        isValid = false;
                        propName = (arrProNameDisplay as PropNameDisplay).PropName;
                        listMsgEr.Add($"{propName} {Common.CommonResource.GetResoureString("PleaseEnter")}");
                    }
                    else if (value.ToString()[1] == '.')
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
=                    }    */      
            }

            // validate chung 
            return isValid;


        }
        /// <summary>
        /// validate riêng của mỗi thực thể 
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="entity"> thực thể cần validate</param>
        /// <returns>bool</returns>
        protected virtual bool ValidateCusrtom(MISAEntity entity)
        {

            return true;
        }
        /// <summary>
        /// sinh ra mã tự động
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <returns>mã mới (string)</returns>
        public string AutoCodeSevices()
        {
            string data = repository.GetCodeNewfirst();        

             return repository.GetAutoCode(data.Substring(0, 2).ToString()) ;
            
        }
    }
}
