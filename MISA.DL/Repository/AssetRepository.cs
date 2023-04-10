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
        /// <summary>
        /// Lấy dữ liệu theo phân trang
        /// @created by : tvTam
        /// @create day : 19/3/2023
        /// </summary>
        /// <param name="pageNumber">số trang </param>
        /// <param name="pageSize">số bản ghi trên 1 trang</param>
        /// <param name="txtSearch">từ khóa tìm kiếm </param>
        /// <param name="DepartmentId">mã phòng ban</param>
        /// <param name="AssetCategoryId">mã loại sản phẩm</param>
        /// <returns>danh sách bản ghi</returns>
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
                    TotalPage = (int)Math.Ceiling(((double)fixed_assets[0].TotalRecord / (double)pageSize)),
                    TotalCost = fixed_assets[0].totalCost,
                    TotalDepreciationValue = fixed_assets[0].TotalDepreciationValue,
                    TotalQuantity = fixed_assets[0].TotalQuantity,               
                    TotalRecord = (int)fixed_assets[0].TotalRecord,
                    CurrentPage = pageNumber,
                    CurrentPageRecords = pageSize,
                    Data = fixed_assets
                };
            }
            else
            {
                pagingRequest = new PagingRequest
                {
                    TotalPage = 0,
                    TotalQuantity=0,
                    TotalDepreciationValue = 0,
                    TotalRecord = 0,
                    CurrentPage = 0,
                    CurrentPageRecords = 0 ,
                    Data = fixed_assets
                };

            }
            return pagingRequest;
        }
        /// <summary>
        /// lọc danh sách
        /// @created by : tvTam
        /// @create day : 19/3/2023
        /// </summary>
        /// <param name="txtSearch">từ khóa </param>
        /// <param name="DepartmentId">id phòng ban</param>
        /// <param name="AssetCategoryId">id loại sản phẩm </param>
        /// <returns></returns>
        public IEnumerable<fixed_asset> Getpage(string? txtSearch, Guid? DepartmentId, Guid? AssetCategoryId)
        {
            if(txtSearch == null)
            {
                txtSearch = "";
            }
            var sqlcmd = $"proc_paging";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@txtSearch", txtSearch);
            dynamicParams.Add("@DepartmentId", DepartmentId);
            dynamicParams.Add("@AssetCategoryId", AssetCategoryId);

            var page = connection.Query<fixed_asset>(sql: sqlcmd, param: dynamicParams, commandType: System.Data.CommandType.StoredProcedure);

            return page;

        }
        /// <summary>
        /// thực hiện nhập khẩu
        /// @created by : tvTam
        /// @create day : 19/3/2023
        /// </summary>
        /// <param name="assets">tệp danh sách tài sản</param>
        /// <returns>số bản ghi thành công</returns>
        public int Import(List<fixed_asset> assets)
        {
            var rowsEffec = 0;
            using (var transaction = connection.BeginTransaction())
            {
                var sqlcmd = $"proc_insert_fixed_asset";
                foreach (var asset in assets)
                {
                    rowsEffec += connection.Execute(sql: sqlcmd, param: asset, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                }
                transaction.Commit();

            }
            return rowsEffec;
        }
    }
}
