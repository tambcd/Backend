﻿using MISA.DL.Interface;
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

        public bool checkEntityCode(string entityCode, Guid emtityId)
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

        public int deleteMany(List<Guid> ids)
        {
            using (var transaction = connection.BeginTransaction())
            {

                var sqlcmd = $"DELETE FROM {className} WHERE {className}_id in @id";
                var parameters = new DynamicParameters();
                parameters.Add("@id", ids);
                var rowsEffec = connection.Execute(sql: sqlcmd, param: parameters, transaction: transaction);
                transaction.Commit();
                return rowsEffec;
            }
        }
    }

    }
