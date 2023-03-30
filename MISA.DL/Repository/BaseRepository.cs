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
            // khai bao sqlCommand
            var sqlcmd = $"SELECT * FROM {className}";

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



        public int Insert(MISAEntity mISAEntity)
        {
            using (var transaction = connection.BeginTransaction())
            {
                var sqlcmd = $"proc_insert_{className}";
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
            var sqlcmd = $"proc_update_{className}";
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

                var sqlcmd = $"DELETE FROM {className} WHERE {className}_id in @id";
                var parameters = new DynamicParameters();
                parameters.Add("@id", ids);
                var rowsEffec = connection.Execute(sql: sqlcmd, param: parameters, transaction: transaction);
                transaction.Commit();
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
                return false;
            }
            else
            {
                return true;
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

            return (txt + (Int32.Parse(data) + 1).ToString()).Replace("%","");

        }
    }

    }
