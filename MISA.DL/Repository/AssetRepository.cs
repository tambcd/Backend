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
        /// lấy danh sách tài sản theo số chứng từ
        /// </summary>
        /// <param name="idLicense"> id chứng từ </param>
        /// <returns>danh sách tài sản</returns>
        public IEnumerable<fixed_asset> GetByLicense(Guid idLicense)
        {
            var sqlcmd = $"proc_ft_license_detail";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@id_license", idLicense);

            var data = connection.Query<fixed_asset>(sql: sqlcmd, param: dynamicParams, commandType: System.Data.CommandType.StoredProcedure);

            return data;
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
        public PagingRequest<fixed_asset> GetFilter(int pageNumber, int pageSize, string? txtSearch , Guid? DepartmentId ,Guid? AssetCategoryId)
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
            PagingRequest<fixed_asset> pagingRequest;
            if (fixed_assets.Count() > 0)
            {
                pagingRequest = new PagingRequest<fixed_asset>
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
                pagingRequest = new PagingRequest<fixed_asset>
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
                if(rowsEffec == assets.Count())
                {
                transaction.Commit();
                    return rowsEffec;

                }
                transaction.Rollback();
            return rowsEffec;
            }
            
        }

        public int UpdateCost(Guid id, Guid idLicense,double cost, string new_cost)
        {
            if (new_cost == null)
            {
                new_cost = "NST:0";
            }                      
                      
                var sqlcmd = $"proc_update_cost";
                var dynamicParams = new DynamicParameters();
                dynamicParams.Add("@v_cost", cost);
                dynamicParams.Add("@v_new_cost", new_cost);
                dynamicParams.Add("@v_fixed_asset_id", id);               
                dynamicParams.Add("@v_license_id", idLicense);               
                var rowsEffec = connection.Execute(sql: sqlcmd, param: dynamicParams , commandType: System.Data.CommandType.StoredProcedure);
      
                return rowsEffec;
        }

        public List<fixed_asset> AssetLicense(List<Guid> ids)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", ids);
            var sqlcmd = $"select fa.fixed_asset_code,l.license_code FROM fixed_asset fa INNER JOIN license_detail ld ON fa.fixed_asset_id = ld.fixed_asset_id INNER JOIN license l ON ld.license_id = l.license_id WHERE fa.fixed_asset_id in @id";
            var data = connection.Query<fixed_asset>(sql: sqlcmd, param: parameters).ToList();
           
            return data ;
        }
    }
}
