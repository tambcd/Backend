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
    public class CostCourceBLL : BaseBLL<cost_source>, ICostSourceBLL
    {
        ICostCourceRepository iCostCourceRepository;

        public CostCourceBLL(ICostCourceRepository _repository) : base(_repository)
        {
            iCostCourceRepository = _repository;
        }
    }
}
