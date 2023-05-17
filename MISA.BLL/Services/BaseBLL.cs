using MISA.BLL.Interface;
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
        protected EntityReturn entityReturn = new EntityReturn();
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
        public EntityReturn InsertSevices(MISAEntity entity)
        {
            // check validate chung
            var isValid = Validate(entity, (int)MisaEnum.Insert);
            // check validate custom
            isValidCustom = ValidateCusrtom(entity);
            if (isValid && isValidCustom)
            {
                var res = repository.Insert(entity);
                if(res != 0)
                {
                    entityReturn.statusCode = (int)MisaEnum.OK;
                    return entityReturn;
                }
            }

            entityReturn.statusCode = (int)MisaEnum.ErrorServe;
            return entityReturn;


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
        /// <returns>đối tượng chưá trang thái thành công || lỗi </returns>
        public EntityReturn UpdateSevices(MISAEntity entity)
        {
            // check validate chung
            bool isValid = Validate(entity, (int)MisaEnum.Update);
            // check validate custom
            isValidCustom = ValidateCusrtom(entity);
            if (isValid && isValidCustom)
            {
                var res = repository.Update(entity);
                if (res!=0)
                {
                    entityReturn.statusCode = (int)MisaEnum.OK;
                     return entityReturn;
                }
            }
           
                entityReturn.statusCode = (int)MisaEnum.ErrorInput;            
                return entityReturn;
        }

        /// <summary>
        /// thực hiện chuẩn hóa dữ liệu
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="entity">thông tin đối tường</param>
        /// <param name="typeVailde">iểu thêm hay sửa </param>
        /// <returns>true || fasle</returns>
        public bool Validate(MISAEntity entity, int typeVailde)
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
                    entityReturn.codeError.Add((int)MisaEnum.ErrorCodeEmpty);
                    entityReturn.titleError.Add($"{propName} {Common.CommonResource.GetResoureString("EmptyCheck")}");
                }
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

            return repository.GetAutoCode(data.Substring(0, 2).ToString());

        }

        /// <summary>
        /// xóa nhiểu đối tượng 
        /// </summary>
        /// <param name="guids">danh sách id tài sản cần xóa</param>
        /// <returns>đối tượng chưá trang thái thành công || lỗi </returns>
        public EntityReturn DeleteManyService(List<Guid> guids)
        {
            if (ValidateCustomDelete(guids))
            {
                return repository.DeleteMany(guids);
            }
            else
            {
                entityReturn.codeError.Add((int)MisaEnum.ErrorInput);
                return entityReturn;
            }
        }
        /// <summary>
        /// validate riêng của mỗi thực thể 
        /// @created by : tvTam
        /// @create day : 1/3/2023
        /// </summary>
        /// <param name="entity"> thực thể cần validate</param>
        /// <returns>bool</returns>
        protected virtual bool ValidateCustomDelete(List<Guid> guids)
        {
            return true;
        }

    }
}
