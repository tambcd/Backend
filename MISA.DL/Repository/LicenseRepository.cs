using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Common.Entity;
using MISA.Common.Enum;
using MISA.Common.QueryDatabase;
using MISA.DL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.DL.Repository
{
    public class LicenseRepository : BaseRepository<license>, ILicenseRepository
    {
        public LicenseRepository(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Thực hiện thêm  chứng từ và chi tiết và cập nhập trạng thái tài sản 
        /// created : tvtam - 10/5/2023
        /// </summary>
        /// <param name="license">đối tượng chứng từ </param>
        /// <param name="ids"> danh sách khóa chính tài sản ghi tăng</param>
        /// <returns>số bản ghi thêm mới </returns>
        public EntityReturn Insertlicense(license license, List<Guid>? ids)
        {
            EntityReturn entityReturn = new EntityReturn();
            var sqlcmdString = "";
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // thêm chứng từ 
                    license.license_id = Guid.NewGuid();
                    var sqlcmd = "proc_insert_license";
                    var rowsEffec = connection.Execute(sql: sqlcmd, param: license, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                    if (rowsEffec == 1)
                    {
                        // thêm chi tiết chứng từ và tài sản 
                        if (ids != null || ids.Count() > 0)
                        {
                            foreach (var item in ids)
                            {
                                if (item == ids[ids.Count() - 1])
                                {
                                    sqlcmdString += $"(UUID(), '{item}', 'tvtam', NOW(), 'tvtam', NOW(), '{license.license_id}')";
                                }
                                else
                                {
                                    sqlcmdString += $"(UUID(), '{item}', 'tvtam', NOW(), 'tvtam', NOW(), '{license.license_id}')" + ",";

                                }

                            }
                            var sqlcmdInsert = $"proc_insert_many_license_detail";
                            var dynamicParams = new DynamicParameters();
                            dynamicParams.Add("@values_insert", sqlcmdString);

                            var data = connection.Execute(sql: sqlcmdInsert, param: dynamicParams, commandType: System.Data.CommandType.StoredProcedure, transaction: transaction);
                            if (data == ids.Count())
                            {
                                // cập nhập trạng thái cho sản phẩm đã sử dụng 
                                var parameters = new DynamicParameters();
                                parameters.Add("@id", ids);
                                var sqlcmdupdateActiveAcsset = $"UPDATE fixed_asset fa SET  active = 1 WHERE fixed_asset_id in @id";
                                var rowsUpdate = connection.Execute(sql: sqlcmdupdateActiveAcsset, param: parameters, transaction: transaction);
                                if (rowsUpdate == ids.Count)
                                {
                                    transaction.Commit();
                                    entityReturn.statusCode = (int)MisaEnum.Success;
                                    return entityReturn;
                                }

                            }
                        }
                    }
                    entityReturn.statusCode = (int)MisaEnum.ErrorInput;
                    transaction.Rollback();
                    return entityReturn;

                }
                catch (Exception ex)
                {   
                    entityReturn.devMsg = ex.Message;
                    transaction.Rollback();
                    return entityReturn;

                }
            }

        }
        /// <summary>
        /// Cập nhập trạng thái chưa sử dụng cho tài sản 
        /// /// created : tvtam - 10/5/2023
        /// </summary>
        /// <param name="ids">danh sách id tài sản </param>
        /// <returns>số bản ghi cập nhập</returns>

        public int UpdateActiveAsset(List<Guid> ids)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", ids);
            var sqlcmd = $"UPDATE fixed_asset fa SET active = 0,fa.modified_date = NOW() WHERE fixed_asset_id  IN(SELECT ld.fixed_asset_id FROM  license_detail ld INNER JOIN license l ON ld.license_id = l.license_id WHERE l.license_id IN @id) ;";
            var data = connection.Execute(sql: sqlcmd, param: parameters);
            return data;
        }
        /// <summary>
        /// Sửa chứng từ
        //// created : tvtam - 10/5/2023
        /// </summary>
        /// <param name="updateLicense"> đối tươngj chứng từ , sanh sách id tài sản  xóa, thêm ở bảng chi tiết  </param>
        /// <returns>số bản ghi được cập nhập</returns>
        public EntityReturn Updatelicense(EntityUpdateLicense updateLicense)
        {
            EntityReturn entityReturn = new EntityReturn();
            var sqlcmdString = "";
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    // Sửa chứng từ 
                    var sqlcmd = "proc_update_license";
                    var rowsEffec = connection.Execute(sql: sqlcmd, param: updateLicense.license, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                    if (rowsEffec == 1)
                    {
                        var data = 0;
                        var res = 0;
                        // sửa chi tiết (xóa và cập nhập trạng thái tài sản  )
                        if (updateLicense.guidsDelete != null && updateLicense.guidsDelete.Count() > 0)
                        {
                            var parameters = new DynamicParameters();
                            parameters.Add("@id", updateLicense.guidsDelete);
                            var sqlcmddelete = $"DELETE FROM license_detail WHERE fixed_asset_id in @id";
                            res = connection.Execute(sql: sqlcmddelete, param: parameters, transaction: transaction);
                            if (res == updateLicense.guidsDelete.Count)
                            {
                                parameters.Add("@id", updateLicense.guidsDelete);
                                var sqlcmdupdateActiveAcsset = $"UPDATE fixed_asset fa SET  active = 0 WHERE fixed_asset_id in @id";
                                var rowsUpdate = connection.Execute(sql: sqlcmdupdateActiveAcsset, param: parameters, transaction: transaction);

                            }
                        }

                        // sửa chi tiết (thêm và cập nhập trạng thái tài sản  )
                        if (updateLicense.guidsUpdate != null && updateLicense.guidsUpdate.Count() > 0)
                        {
                            foreach (var item in updateLicense.guidsUpdate)
                            {
                                if (item == updateLicense.guidsUpdate[updateLicense.guidsUpdate.Count() - 1])
                                {
                                    sqlcmdString += $"(UUID(), '{item}', 'tvtam', NOW(), 'tvtam', NOW(), '{updateLicense.license.license_id}')";
                                }
                                else
                                {
                                    sqlcmdString += $"(UUID(), '{item}', 'tvtam', NOW(), 'tvtam', NOW(), '{updateLicense.license.license_id}')" + ",";

                                }

                            }
                            var sqlcmdInsert = $"proc_insert_many_license_detail";
                            var dynamicParams = new DynamicParameters();
                            dynamicParams.Add("@values_insert", sqlcmdString);
                             data = connection.Execute(sql: sqlcmdInsert, param: dynamicParams, commandType: System.Data.CommandType.StoredProcedure, transaction: transaction);
                            if (data == updateLicense.guidsUpdate.Count())
                            {
                                var parameters = new DynamicParameters();
                                parameters.Add("@id", updateLicense.guidsUpdate);
                                var sqlcmdupdateActiveAcsset = $"UPDATE fixed_asset fa SET  active = 1 WHERE fixed_asset_id in @id";
                                var rowsUpdate = connection.Execute(sql: sqlcmdupdateActiveAcsset, param: parameters, transaction: transaction);

                            }
                        }
                        if ((data == updateLicense.guidsUpdate.Count() || data == 0) && (res == updateLicense.guidsDelete.Count() || res == 0))
                        {
                            var sqlcmdUpdateCostLicense = $"proc_update_sum_cost_license";
                            var dynamicParams = new DynamicParameters();
                            dynamicParams.Add("@id_license", updateLicense.license.license_id);
                            data = connection.Execute(sql: sqlcmdUpdateCostLicense, param: dynamicParams, commandType: System.Data.CommandType.StoredProcedure, transaction: transaction);
                            if (data != 0)
                            {
                                transaction.Commit();
                                entityReturn.statusCode = (int)MisaEnum.Success;
                                return entityReturn;
                            }
                            

                        }
                    }
                    entityReturn.statusCode = (int)MisaEnum.ErrorInput;
                    transaction.Rollback();
                    return entityReturn;

                }
                catch (Exception ex)
                {
                    entityReturn.statusCode = (int)MisaEnum.ErrorServe;
                    entityReturn.devMsg = ex.Message;
                    transaction.Rollback();
                    return entityReturn;

                }
            }
        }
    }
}
