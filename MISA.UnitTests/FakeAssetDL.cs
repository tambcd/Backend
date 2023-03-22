using MISA.Common.Entity;
using MISA.DL.Interface;
using System;
using System.Collections.Generic;

namespace MISA.WebApi.UnitTests
{
    internal class FakeAssetDL: IAssetRepository
    {      

        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public int deleteMany(List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<fixed_asset> GetAll()
        {
            throw new NotImplementedException();
        }

        public string getAutoCode(string txt)
        {
            throw new NotImplementedException();
        }

        public fixed_asset GetById(Guid id)
        {
            return new fixed_asset
            {
                fixed_asset_id = new Guid("075c791c-9db6-4370-9825-152968fb0e24"),
                fixed_asset_code = "TS001",
                fixed_asset_name = "IPhone"
            };
        }

        public string getCodeNewfirst()
        {
            throw new NotImplementedException();
        }

        public PagingRequest GetFilter(int pageNumber, int pageSize, string? txtSearch, Guid? DepartmentId, Guid? AssetCategoryId)
        {
            throw new NotImplementedException();
        }

        public int Insert(fixed_asset entiy)
        {
            throw new NotImplementedException();
        }

        public bool isSameCode(string code, Guid? id)
        {
            throw new NotImplementedException();
        }

        public int Update(fixed_asset entity)
        {
            throw new NotImplementedException();
        }
    }
}