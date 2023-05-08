﻿using Dapper;
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

        public int Insertlicense(license license, List<Guid>? ids)
            {
            
            var sqlcmdString = "";
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    license.license_id = Guid.NewGuid();
                    var sqlcmd = "proc_insert_license";
                    var rowsEffec = connection.Execute(sql: sqlcmd, param: license, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);

                    if (rowsEffec != 0)
                    {
                        if (ids != null || ids.Count() > 0)
                        {
                            foreach (var item in ids)
                            {
                                if (item == ids[ids.Count()-1])
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
                            if (data/2 == ids.Count())
                            {
                                transaction.Commit();
                                return data/2;

                            }                            
                        }
                    }
                    transaction.Rollback();
                    return 0;
                    
                }catch (Exception ex)
                {
                    transaction.Rollback();
                    return 0;

                }
            }

        }

        public int Updatelicense(EntityUpdateLicense updateLicense)
        {
            var sqlcmdString = "";
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var sqlcmd = "proc_update_license";
                    var rowsEffec = connection.Execute(sql: sqlcmd, param: updateLicense.license, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                    
                    if (rowsEffec != 0)
                    {
                        var parameters = new DynamicParameters();
                        
                        parameters.Add("@id", updateLicense.guidsDelete);
                        var sqlcmddelete = $"DELETE FROM license_detail WHERE fixed_asset_id in @id";
                        var res = connection.Execute(sql: sqlcmddelete, param: parameters, transaction: transaction);
                        var data = 0;

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
                            
                        }
                        if ((data / 2 == updateLicense.guidsUpdate.Count()||data==0) && (res == updateLicense.guidsDelete.Count()|| res==0))
                        {
                            transaction.Commit();
                            return data / 2 + res + rowsEffec;

                        }
                    }
                    transaction.Rollback();
                    return 0;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return 0;

                }
            }
        }
    }
}