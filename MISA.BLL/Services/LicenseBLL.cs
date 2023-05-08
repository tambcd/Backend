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
        protected bool ValidateCusrtom(List<Guid> idsAsset, license license)
        {
            isValidCustom = true;
            if (idsAsset == null || idsAsset.Count() == 0)
            {
                listMsgEr.Add(Common.CommonResource.GetResoureString("ListIdAsset"));
                isValidCustom= false;
            }   
            if (licenseRepository.IsSameCode(license.license_code, license.license_id))
            {
                isValidCustom = false;
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
        public int InsertLicense(license license, List<Guid> idsAsset)
        {
            base.listMsgEr.Clear();
            var b = ValidateCusrtom(idsAsset, license);
            var a = base.Validate(license, (int)MisaEnum.Insert);
            if (a && b)
            {
                
                return licenseRepository.Insertlicense(license, idsAsset);
            }
            else
            {
                throw new MISAException(Common.CommonResource.GetResoureString("InvalidInput"), listMsgEr);
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
                listMsgEr.Add($" {Common.CommonResource.GetResoureString("LicenseCode")} {entity.license_code} {Common.CommonResource.GetResoureString("SameCode")}");
            }
            return isValidCustom;
        }

        public int UpdateLicense(EntityUpdateLicense updateLicense)
        {
            base.listMsgEr.Clear();
            var a = base.Validate(updateLicense.license, (int)MisaEnum.Insert);
            var b = ValidateCusrtom(updateLicense.license);
            if (a && b)
            {

                return licenseRepository.Updatelicense(updateLicense);
            }
            else
            {
                throw new MISAException(Common.CommonResource.GetResoureString("InvalidInput"), listMsgEr);
            }
        }
        

    }
}
