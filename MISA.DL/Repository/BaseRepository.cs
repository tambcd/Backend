using MISA.DL.Interface;
using MySqlConnector;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

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
            connection.Dispose(); ;
        }

        public int Delete(Guid id)
        {
            throw new NotImplementedException();
        }

       

        public virtual IEnumerable<MISAEntity> GetAll()
        {
            // khai bao sqlCommand
            var sqlcmd = $"SELECT * FROM View_GetAll{className}";

            // thực hiện lấy dữ liệu 
            var data = connection.Query<MISAEntity>(sql: sqlcmd);
            return data;
        }

        public MISAEntity GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public int Insert(MISAEntity entiy)
        {
            using (var transaction = connection.BeginTransaction())
            {
                var sqlcmd = $"Proc_Insert{className}";
                var rowsEffec = connection.Execute(sql: sqlcmd, param: entiy, transaction: transaction, commandType: System.Data.CommandType.StoredProcedure);
                transaction.Commit();
                return rowsEffec;

            }
        }

        public int Update(MISAEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
