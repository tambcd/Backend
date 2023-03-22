
using MISA.BLL.Interface;
using MISA.Common.Entity;
using System;
using System.IO;

namespace MISA.WebApi.UnitTests
{
    internal class FakeAssetBl : IAssetBLL
    {
        public string AutoCodeSevices()
        {
            throw new NotImplementedException();
        }

        public int DeleteSevices(Guid id)
        {
            throw new NotImplementedException();
        }

        public Stream ExportAssets()
        {
            throw new NotImplementedException();
        }

        public int InsertSevices(fixed_asset entity)
        {
            throw new NotImplementedException();
        }

        public int UpdateSevices(fixed_asset entity)
        {
            throw new NotImplementedException();
        }

        public bool Validate(fixed_asset entity, string type)
        {
            throw new NotImplementedException();
        }
    }
}