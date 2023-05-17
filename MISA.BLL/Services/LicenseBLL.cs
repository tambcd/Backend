using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.Common.Enum;
using MISA.Common.Exceptions;
using MISA.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BLL.Services
{
    public class LicenseBLL : BaseBLL<license>, ILicenseBLL
    {
        ILicenseRepository licenseRepository;
        public LicenseBLL(ILicenseRepository _repository) : base(_repository)
        {
            licenseRepository = _repository;
        }

        /// <summary>
        /// validate insert chung tu
        /// @created by : tvTam
        /// @create day : 26/04/2023
        /// </summary>
        /// <param name="idsAsset"> danh sach id tai san </param>
        /// <returns>   
        ///   true : hợp lệ 
        ///   false ; không hợp lệ 
        /// </returns>
        protected bool ValidateCustom(List<Guid> idsAsset, license license)
        {
            isValidCustom = true;
            if (idsAsset == null || idsAsset.Count() == 0)
            {
                entityReturn.codeError.Add((int)MisaEnum.ErrorCodelistEmpty); 
                listMsgEr.Add(Common.CommonResource.GetResoureString("ListIdAsset"));
                isValidCustom= false;
            }   
            if (licenseRepository.IsSameCode(license.license_code, license.license_id))
            {
                isValidCustom = false;
                entityReturn.codeError.Add((int)MisaEnum.ErrorCodeSameCode);
                listMsgEr.Add($" {Common.CommonResource.GetResoureString("license_code")} {license.license_code} {Common.CommonResource.GetResoureString("SameCode")}");
            }
            return isValidCustom;
        }


        /// <summary>
        /// thêm chứng từ và thêm danh sách tài sản thuộc chứng từ 
        /// </summary>
        /// <param name="license"></param>
        /// <param name="idsAsset"></param>
        /// <returns></returns>
        public EntityReturn InsertLicense(license license, List<Guid> idsAsset)
        {
             var isValidateCustom = ValidateCustom(idsAsset, license);
            var isBaseValidate = base.Validate(license, (int)MisaEnum.Insert);
            if (isValidateCustom && isBaseValidate)
            {
                base.entityReturn.statusCode = 201;
                return licenseRepository.Insertlicense(license, idsAsset);
            }
            else
            {
                base.entityReturn.statusCode = 400;
                return entityReturn;
            }

        }

        /// <summary>
        /// validate insert cho tài sản
        /// @created by : tvTam
        /// @create day : 16/4/2023
        /// </summary>
        /// <param name="entity"> chúng từ </param>
        /// <returns>   
        ///   true : hợp lệ 
        ///   false ; không hợp lệ 
        /// </returns>
        protected override bool ValidateCusrtom(license entity)
        {
         
            // trung mã 
            if (licenseRepository.IsSameCode(entity.license_code, entity.license_id))
            {
                isValidCustom = false;
                entityReturn.codeError.Add((int)MisaEnum.ErrorCodeSameCode);
                entityReturn.titleError.Add($" {Common.CommonResource.GetResoureString("LicenseCode")} {entity.license_code} {Common.CommonResource.GetResoureString("SameCode")}");
            }
            return isValidCustom;
        }

        public EntityReturn UpdateLicense(EntityUpdateLicense updateLicense)
        {
            base.listMsgEr.Clear();
            var isValidate = base.Validate(updateLicense.license, (int)MisaEnum.Insert);
            var isValidateCusrtom = ValidateCusrtom(updateLicense.license);
            if (isValidate && isValidateCusrtom)
            {
                base.entityReturn.statusCode = 201;
                return licenseRepository.Updatelicense(updateLicense);
            }
            else
            {
                base.entityReturn.statusCode = 400;
                return entityReturn;
            }
        }
        protected override bool ValidateCustomDelete(List<Guid> guids)
        {
            if (licenseRepository.UpdateActiveAsset(guids) !=0 ){
                return true;
            }
            base.entityReturn.statusCode = 400;
            base.entityReturn.titleError.Add(Common.CommonResource.GetResoureString("NotUpdateAsset"));
            return false;
        }

    }
}
