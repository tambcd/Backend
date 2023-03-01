using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Common.Entity;
using MISA.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DL.Repository
{
    public class AssetRepository : BaseRepository<fixed_asset>, IAssetRepository
    {
        public AssetRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public PagingRequest GetFilter(int pageNumber, int pageSize, string? txtSearch , Guid? DepartmentId ,Guid? AssetCategoryId)
        {
            if (txtSearch == null)
            {
                txtSearch = "";
            }
            // khai bao sqlCommand
            var sqlcmd = $"proc_ft_fixed_asset";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@pageNumber", pageNumber);
            dynamicParams.Add("@pageSize", pageSize);
            dynamicParams.Add("@txtSearch", txtSearch);
            dynamicParams.Add("@DepartmentId", DepartmentId);
            dynamicParams.Add("@AssetCategoryId", AssetCategoryId);

            // thực hiện lấy dữ liệu 

            List<fixed_asset> fixed_assets = new List<fixed_asset>();
            fixed_assets = connection.Query<fixed_asset>(sql: sqlcmd, param: dynamicParams, commandType: System.Data.CommandType.StoredProcedure).ToList();
            PagingRequest pagingRequest;
            if (fixed_assets.Count() > 0)
            {
                pagingRequest = new PagingRequest
                {
                    TotalPage = (int)Math.Ceiling(((double)fixed_assets[0].TotalRecord / (double)pageSize))
               ,
                    TotalRecord = (int)fixed_assets[0].TotalRecord,
                    CurrentPage = pageNumber,
                    CurrentPageRecords = pageSize
               ,
                    Data = fixed_assets
                };
            }
            else
            {
                pagingRequest = new PagingRequest
                {
                    TotalPage = 0,
                    TotalRecord = 0,
                    CurrentPage = 0,
                    CurrentPageRecords = 0 ,
                    Data = fixed_assets
                };

            }


            return pagingRequest;
        }

        public string GetNewCodeAssets()
        {
            var sqlcmd = $"SELECT SUBSTR(fixed_asset_code, 3) FROM fixed_asset WHERE fixed_asset_code LIKE 'TS%'ORDER BY CAST(SUBSTR(fixed_asset_code, 3) AS SIGNED) DESC LIMIT 1";
            var data = connection.QueryFirstOrDefault<string>(sql: sqlcmd);
            return data;
        }
    }
}
