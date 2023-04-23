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
    public class LicenseDetailRepository : BaseRepository<license_detail>, ILicenseDetailRepository
    {
        public LicenseDetailRepository(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Thêm danh sách chứng từ tài sản 
        /// </summary>
        /// <param name="id">id chứng từ </param>
        /// <param name="ids">danh sách id tài sản </param>
        /// <returns></returns>
        public int InsertManyDetail(Guid id, List<Guid> ids)
        {
            var sqlcmdString = "";
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (var item in ids)
                    {
                        if (item == ids[ids.Count()])
                        {
                            sqlcmdString += $"(UUID(), '{item}', 'tvtam', NOW(), 'tvtam', NOW(), '{id}')";
                        }
                        else
                        {
                            sqlcmdString += $"(UUID(), '{item}', 'tvtam', NOW(), 'tvtam', NOW(), '{id}')" + ",";

                        }

                    }
                    var sqlcmd = $"proc_insert_many_license_detail";
                    var dynamicParams = new DynamicParameters();
                    dynamicParams.Add("@values_insert", sqlcmdString);

                    var data = connection.Execute(sql: sqlcmd, param: dynamicParams, commandType: System.Data.CommandType.StoredProcedure, transaction: transaction);
                    if (data - 1 == ids.Count())
                    {
                        transaction.Commit();
                        return data - 1;

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
