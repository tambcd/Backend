﻿using MISA.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DL.Interface
{
    public interface ILicenseRepository: IRepository<license>
    {
        /// <summary>
        /// thêm chứng từ 
        /// </summary>
        /// <param name="license">chứng từ </param>
        /// <param name="ids">danh sách id </param>
        /// <returns>thất bại 0 || thành công số bản ghi được thêm </returns>
        public int Insertlicense(license license, List<Guid> ids);

    }
}
