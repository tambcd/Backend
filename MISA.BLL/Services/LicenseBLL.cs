using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.Common.Enum;
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

        public int InsertLicense(license license, List<Guid> idsAsset)
        {
            base.listMsgEr.Clear();
            if (base.Validate(license, (int)MisaEnum.Insert))
            {
                
                return licenseRepository.Insertlicense(license, idsAsset);
            }
            else
            {
                return 0;
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
    }
}
