using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Common.Entity;
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

        public EntityReturn Insertlicense(license license, List<Guid>? ids)
        {
            EntityReturn entityReturn = new EntityReturn();
            var sqlcmdString = "";
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    license.license_id = Guid.NewGuid();
                    var sqlcmd = "proc_insert_license";
                    var rowsEffec = connection.Execute(sql: sqlcmd, param: license, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                    if (rowsEffec == 1)
                    {
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
                                var parameters = new DynamicParameters();
                                parameters.Add("@id", ids);
                                var sqlcmdupdateActiveAcsset = $"UPDATE fixed_asset fa SET  active = 1 WHERE fixed_asset_id in @id";
                                var rowsUpdate = connection.Execute(sql: sqlcmdupdateActiveAcsset, param: parameters, transaction: transaction);
                                if (rowsUpdate == ids.Count)
                                {
                                    transaction.Commit();
                                    entityReturn.statusCode = 201;
                                    return entityReturn;
                                }

                            }
                        }
                    }
                    entityReturn.statusCode = 400;
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

        public EntityReturn Updatelicense(EntityUpdateLicense updateLicense)
        {
            EntityReturn entityReturn = new EntityReturn();
            var sqlcmdString = "";
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var sqlcmd = "proc_update_license";
                    var rowsEffec = connection.Execute(sql: sqlcmd, param: updateLicense.license, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                    if (rowsEffec == 1)
                    {
                        var data = 0;
                        var res = 0;
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
                                entityReturn.statusCode = 201;
                                return entityReturn;
                            }
                            

                        }
                    }
                    entityReturn.statusCode = 400;
                    transaction.Rollback();
                    return entityReturn;

                }
                catch (Exception ex)
                {
                    entityReturn.statusCode = 500;
                    entityReturn.devMsg = ex.Message;
                    transaction.Rollback();
                    return entityReturn;

                }
            }
        }
    }
}
