using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace MySQLWorker
{
    public class Execute
    {
        /// <summary>
        /// execute SELECT command
        /// </summary>
        /// <param name="select_what">what to select</param>
        /// <param name="select_from">select from</param>
        /// <param name="connection_path">path with connection</param>
        /// <returns></returns>
        public static string[] QuerySelect(in string select_what, in string select_from, in string connection_path) {
            List<string> result = new List<string>();

            using (SqlConnection connection = new SqlConnection(connection_path)) {
                connection.Open();

                string COMMAND = $"SELECT {select_what} FROM {select_from};";
                using (SqlCommand command = new SqlCommand(COMMAND, connection)) {
                    
                    SqlDataReader data = command.ExecuteReader();

                    result.Add(ReadTableColumnTitles(data));
                    result.AddRange(ReadTableRows(data).ToList());

                }
                connection.Close();
            }
            return result.ToArray();
        }

        //returns rows from table
        private static string[] ReadTableRows(in SqlDataReader data) {
            List<string> rows = new List<string>();

            while (data.Read()) {
                string temp = String.Empty;

                for (int i = 0; i < data.FieldCount; i++) {
                    temp += (data.GetValue(i) == DBNull.Value) ? "NULL," : $"{data.GetValue(i)},";
                }
                temp = temp.Remove(temp.Length - 1, 1);

                rows.Add(temp);
            }

            return rows.ToArray();
        }


        //returns column names from table
        private static string ReadTableColumnTitles(in SqlDataReader data) {
            string titles = String.Empty;

            for (int i = 0; i < data.FieldCount; i++) {
                titles += $"{data.GetName(i)},";
            }
            titles = titles.Remove(titles.Length - 1, 1);

            return titles;
        }


        /// <summary>
        /// execute INSERT command. Only one value
        /// </summary>
        /// <param name="table">your table</param>
        /// <param name="col_name">column title</param>
        /// <param name="value">insert value</param>
        /// <param name="connection_path">path with connection</param>
        public static void QueryInsertSingleValue(in string table, in string col_name, in string value, in string connection_path) {
            using (SqlConnection connection = new SqlConnection(connection_path)) {
                connection.Open();

                //"INSERT INTO [TableName]([Param]) VALUES (@param);"
                string COMMAND = $"INSERT INTO [{table}]([{col_name}]) VALUES (@value);";

                using (SqlCommand command = new SqlCommand(COMMAND, connection)) {
                    command.Parameters.AddWithValue("@value", value);

                    if (command.ExecuteNonQuery() < 0)
                        throw new Exception("insert error");
                }

                connection.Close();
            }
        }

        /// <summary>
        /// execute INSERT command. For all or a few values
        /// </summary>
        /// <param name="table">your table</param>
        /// <param name="col_names">column titles</param>
        /// <param name="values">insert values</param>
        /// <param name="connection_path">path with connection</param>
        public static void QueryInsertMultyValues(in string table, in string[] col_names, in string[] values, in string connection_path) {
            using (SqlConnection connection = new SqlConnection(connection_path)) {
                connection.Open();

                //"INSERT INTO [TableName]([Param]) VALUES (@param);"
                string COMMAND = $"INSERT INTO [{table}](";
                for (int i = 0; i < col_names.Length; i++) {
                    COMMAND += $"[{col_names[i]}], ";
                }
                COMMAND = COMMAND.Remove(COMMAND.Length - 2, 2);
                COMMAND += ") VALUES (";

                for (int i = 0; i < values.Length; i++) {
                    COMMAND += $"@value{i + 1}, ";
                }
                COMMAND = COMMAND.Remove(COMMAND.Length - 2, 2);
                COMMAND += ");";

                using (SqlCommand command = new SqlCommand(COMMAND, connection)) {
                    for (int i = 0; i < values.Length; i++) {
                        command.Parameters.AddWithValue($"@value{i + 1}", values[i]);
                    }

                    if (command.ExecuteNonQuery() < 0)
                        throw new Exception("insert error");
                }

                connection.Close();
            }
        }
    }
}
