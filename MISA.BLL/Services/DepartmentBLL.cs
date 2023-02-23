using MISA.BLL.Interface;
using MISA.Common.Entity;
using MISA.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BLL.Services
{
    public class DepartmentBLL : BaseBLL<department>, IDepartmentBLL
    {
        IDepartmentRepository IDepartmentRepository;
        public DepartmentBLL(IDepartmentRepository _IDepartmentRepository) : base(_IDepartmentRepository)
        {
            IDepartmentRepository = _IDepartmentRepository;

        }
    }
}
