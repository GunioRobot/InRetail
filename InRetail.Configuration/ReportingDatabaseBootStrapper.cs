using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using InRetail.Reporting.Dto;
using InRetail.Reporting.Infrastructure;

namespace InRetail.Configuration
{
    public class ReportingDatabaseBootStrapper
    {
        public static string dataBaseFile = "InRetailReportingDataBase.db3";
        private readonly SqlCreateBuilder _sqlCreateBuilder = new SqlCreateBuilder();
        private readonly List<Type> _dtos = new List<Type>
        {
            typeof(ProductReport), 
            typeof(CategoryReport), 
        };

        public static void BootStrap()
        {
            new ReportingDatabaseBootStrapper().CreateDatabaseSchemaIfNeeded();
        }

        public void ReCreateDatabaseSchema()
        {
            if (File.Exists(dataBaseFile))
                File.Delete(dataBaseFile);

            DoCreateDatabaseSchema();
        }

        public void CreateDatabaseSchemaIfNeeded()
        {
            if (File.Exists(dataBaseFile))
                return;

            DoCreateDatabaseSchema();
        }

        private void DoCreateDatabaseSchema()
        {
            SQLiteConnection.CreateFile(dataBaseFile);

            var sqLiteConnection = new SQLiteConnection(string.Format("Data Source={0}", dataBaseFile));

            sqLiteConnection.Open();

            using (DbTransaction dbTrans = sqLiteConnection.BeginTransaction())
            {
                using (DbCommand sqLiteCommand = sqLiteConnection.CreateCommand())
                {
                    foreach (var dto in _dtos)
                    {
                        sqLiteCommand.CommandText = _sqlCreateBuilder.CreateSqlCreateStatementFromDto(dto);
                        sqLiteCommand.ExecuteNonQuery();
                    }
                }
                dbTrans.Commit();
            }

            sqLiteConnection.Close();
        }
    }
}