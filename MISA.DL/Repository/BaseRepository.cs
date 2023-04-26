using MISA.DL.Interface;
using MySqlConnector;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using MISA.Common.QueryDatabase;
using MISA.Common.Entity;

namespace MISA.DL.Repository
{
    public class BaseRepository<MISAEntity> : IDisposable, IRepository<MISAEntity> where MISAEntity : class
    {

        protected MySqlConnection connection;
        public readonly String connectionString = "";
        private String className = "";


        public BaseRepository(IConfiguration configuration)
        {

            // Khởi tạo kết nối
            connectionString = configuration.GetConnectionString("MisaAppCon");
            connection = new MySqlConnection(connectionString);
            connection.Open();
            className = typeof(MISAEntity).Name;

        }

        /// <summary>
        /// Giải phóng bộ nhớ
        /// </summary>
        /// ceatedby : tvTam (04/08/2022)

        public void Dispose()
        {
            connection.Close();
            connection.Dispose();
        }


        public virtual IEnumerable<MISAEntity> GetAll()
        {
            string getAll = String.Format(StringQuery.GetAll, className);
            // khai bao sqlCommand
            var sqlcmd = getAll;

            // thực hiện lấy dữ liệu 
            var data = connection.Query<MISAEntity>(sql: sqlcmd);
            return data;
        }


        public MISAEntity GetById(Guid id)
        {
            var sqlcmd = $"SELECT * FROM {className} WHERE {className}_id = @Id";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@Id", id);
            var data = connection.QueryFirstOrDefault<MISAEntity>(sql: sqlcmd, param: dynamicParams);
            return data;
        }



        public virtual int Insert(MISAEntity mISAEntity)
        {
            using (var transaction = connection.BeginTransaction())
            {
                string insert = String.Format(StringQuery.Insert, className);

                var sqlcmd = insert;
                var rowsEffec = connection.Execute(sql: sqlcmd, param: mISAEntity, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                transaction.Commit();
                return rowsEffec;

            }

        }

        public bool checkEntityId(Guid EntityId)
        {
            var sqlcmd = $"SELECT * FROM {className} WHERE {className}_id = @Id";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@Id", EntityId);
            var data = connection.QueryFirstOrDefault<MISAEntity>(sql: sqlcmd, param: dynamicParams);
            if (data != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Update(MISAEntity entity)
        {
            string update = String.Format(StringQuery.Update, className);
            var sqlcmd = update;
            var rowsEffec = connection.Execute(sql: sqlcmd, param: entity, commandType: System.Data.CommandType.StoredProcedure);
            return rowsEffec;
        }

        public int Delete(Guid id)
        {
            using (var transaction = connection.BeginTransaction())
            {

                var sqlcmd = $"DELETE FROM {className} WHERE {className}_id = @id";
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);
                var rowsEffec = connection.Execute(sql: sqlcmd, param: parameters, transaction: transaction);
                transaction.Commit();
                return rowsEffec;
            }

        }

        public bool checkEntityCode(string entityCode, string emtityId)
        {
            var sqlcmd = $"SELECT * FROM {className} WHERE {className}_id != @Id and {className}Code = @Code";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@Id", emtityId);
            dynamicParams.Add("@Code", entityCode);
            var data = connection.QueryFirstOrDefault<MISAEntity>(sql: sqlcmd, param: dynamicParams);
            if (data != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int DeleteMany(List<Guid> ids)
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                var parameters = new DynamicParameters();
                parameters.Add("@id", ids);
                var sqlcmd = $"DELETE FROM {className} WHERE {className}_id in @id";
                var rowsEffec = connection.Execute(sql: sqlcmd, param: parameters, transaction: transaction);
                    if(rowsEffec == ids.Count){
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                        return 0;
                    }
                    
                return rowsEffec;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return 0;
                }
            }
        }

        public bool IsSameCode(string code,Guid? id )
        {
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@code", code);
            dynamicParams.Add("@id", id);
            var sqlcmd = $"SELECT * FROM {className} WHERE {className}_code = @code and {className}_id != @id" ;
            var data = connection.QueryFirstOrDefault<MISAEntity>(sql: sqlcmd, param: dynamicParams);
            if (data != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public string GetCodeNewfirst()
        {
            var sqlcmd = $"SELECT {className}_code FROM {className} ORDER BY created_date DESC LIMIT 1";
            var data = connection.QueryFirstOrDefault<string>(sql: sqlcmd);            
            return data;
            
        }

        public virtual string GetAutoCode(string? txt)
        {
            
            var dynamicParams = new DynamicParameters();
            if(txt == null)
            {
                txt = "TS";
            }
            txt = txt + "%";
            dynamicParams.Add("@txt", txt);
            var sqlcmd = $"SELECT SUBSTR({className}_code, 3) FROM {className} WHERE {className}_code LIKE @txt ORDER BY CAST(SUBSTR({className}_code, 3) AS SIGNED) DESC LIMIT 1";
            var data = connection.QueryFirstOrDefault<string>(sql: sqlcmd, param: dynamicParams);
            data = Regex.Replace(data, "[@,\\.\";'\\\\]", string.Empty);
            return (txt + (Int32.Parse(data) + 1).ToString()).Replace("%","");

        }

        public IEnumerable<MISAEntity> GetRecordActive(List<Guid> ids)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", ids);
            // khai bao sqlCommand
            var sqlcmd = $"SELECT * FROM {className} WHERE {className}_id not in  @Id";

            // thực hiện lấy dữ liệu 
            var data = connection.Query<MISAEntity>(sql: sqlcmd, param: parameters);
            return data;
        }

        public PagingRequest<MISAEntity> GetSreachBase(string codes, int pageNumber, int pageSize, string? txtSearch, Guid? idLicense)
        {
            if (txtSearch == null)
            {
                txtSearch = "";
            }
            if (codes == null || codes=="")
            {
                codes = "''";
            }
            
            var sqlcmd = $"proc_ft_{className}_active";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@txtSearch", txtSearch);
            dynamicParams.Add("@list_codes", codes);
            dynamicParams.Add("@PageNumber", pageNumber);
            dynamicParams.Add("@PageSize", pageSize);
            dynamicParams.Add("@id_license", idLicense);
            List<MISAEntity> datas = new List<MISAEntity>();
            List<Paging> paging = new List<Paging>();

            var data = connection.QueryMultiple(sql: sqlcmd, param: dynamicParams, commandType: System.Data.CommandType.StoredProcedure);

            datas = data.Read<MISAEntity>().ToList();
            paging = data.Read<Paging>().ToList();

            PagingRequest<MISAEntity> pagingRequest;
            if (datas.Count() > 0)
            {
                pagingRequest = new PagingRequest<MISAEntity>
                {
                    TotalPage = (int)Math.Ceiling(((double)paging[0].TotalRecord / (double)pageSize)),
                    TotalCost = paging[0].TotalCost,
                    TotalDepreciationValue = paging[0].TotalDepreciationValue,
                    TotalQuantity = paging[0].TotalQuantity,
                    TotalRecord = paging[0].TotalRecord,
                    CurrentPage = pageNumber,
                    CurrentPageRecords = pageSize,
                    Data = datas
                };
            }
            else
            {
                pagingRequest = new PagingRequest<MISAEntity>
                {
                    TotalPage = 0,
                    TotalQuantity = 0,
                    TotalDepreciationValue = 0,
                    TotalRecord = 0,
                    CurrentPage = 0,
                    CurrentPageRecords = 0,
                    Data = datas
                };

            }
            return pagingRequest;
        }

        public PagingRequest<MISAEntity> GetSelectItem(string codes, int pageNumber, int pageSize, string? txtSearch)
        {
            if (txtSearch == null)
            {
                txtSearch = "";
            }
            if (codes == null || codes.Trim() == "")
            {
                codes = "''";
            }
            var sqlcmd = $"proc_select_item_{className}";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@txtSearch", txtSearch);
            dynamicParams.Add("@list_codes", codes);
            dynamicParams.Add("@PageNumber", pageNumber);
            dynamicParams.Add("@PageSize", pageSize);
            List<MISAEntity> datas = new List<MISAEntity>();
            List<Paging> paging = new List<Paging>();

            var data = connection.QueryMultiple(sql: sqlcmd, param: dynamicParams, commandType: System.Data.CommandType.StoredProcedure);

            datas = data.Read<MISAEntity>().ToList();
            paging = data.Read<Paging>().ToList();

            PagingRequest<MISAEntity> pagingRequest;
            if (datas.Count() > 0)
            {
                pagingRequest = new PagingRequest<MISAEntity>
                {
                    TotalPage = (int)Math.Ceiling(((double)paging[0].TotalRecord / (double)pageSize)),
                    TotalCost = paging[0].TotalCost,
                    TotalDepreciationValue = paging[0].TotalDepreciationValue,
                    TotalQuantity = paging[0].TotalQuantity,
                    TotalRecord = paging[0].TotalRecord,
                    CurrentPage = pageNumber,
                    CurrentPageRecords = pageSize,
                    Data = datas
                };
            }
            else
            {
                pagingRequest = new PagingRequest<MISAEntity>
                {
                    TotalPage = 0,
                    TotalQuantity = 0,
                    TotalDepreciationValue = 0,
                    TotalRecord = 0,
                    CurrentPage = 0,
                    CurrentPageRecords = 0,
                    Data = datas
                };

            }
            return pagingRequest;
        }

        public MISAEntity GetByCode(string code)
        {
            var sqlcmd = $"SELECT * FROM {className} WHERE {className}_code = @code";
            var dynamicParams = new DynamicParameters();
            dynamicParams.Add("@code", code);
            var data = connection.QueryFirstOrDefault<MISAEntity>(sql: sqlcmd, param: dynamicParams);
            return data;
        }

        public int DeleteManyByCode(List<string> codes)
        {
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@codes", codes);
                    var sqlcmd = $"DELETE FROM {className} WHERE {className}_code in @codes";
                    var rowsEffec = connection.Execute(sql: sqlcmd, param: parameters, transaction: transaction);
                    if (rowsEffec == codes.Count)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                        return 0;
                    }

                    return rowsEffec;
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
