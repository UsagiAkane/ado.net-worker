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
        public static object[,] QuerySelect(in string select_what, in string select_from, in string connection_path)
        {
            object[,] result;

            //подключение к бд
            using (SqlConnection connection = new SqlConnection(connection_path)) {
                connection.Open();

                //команда для выполнения
                string COMMAND = $"SELECT {select_what} FROM {select_from};";
                using (SqlCommand command = new SqlCommand(COMMAND, connection)) {
                    //получает данные
                    SqlDataReader data = command.ExecuteReader();
                    result = ReadData(data);
                }
                connection.Close();
            }

            return result;
        }

        //returns rows from table
        private static object[,] ReadData(in SqlDataReader data)
        {
            object[,] rows = new string[1, data.FieldCount];

            //строка с полями
            for (int i = 0; i < data.FieldCount; i++) {
                rows[rows.GetLength(0) - 1, i] = data.GetName(i);
            }
            //строки с данными
            while (data.Read()) {
                rows = ResizeArray(in rows, rows.GetLength(0) + 1, rows.GetLength(1));
                for (int i = 0; i < data.FieldCount; i++) {
                    rows[rows.GetLength(0) - 1, i] = (data.GetValue(i) == DBNull.Value) ? "NULL" : data.GetValue(i);
                }
            }
            return rows;
        }

        static T[,] ResizeArray<T>(in T[,] original, int rows, int cols)
        {
            T[,] newArray = new T[rows, cols];
            int minRows = Math.Min(rows, original.GetLength(0));
            int minCols = Math.Min(cols, original.GetLength(1));

            for (int i = 0; i < minRows; i++)
                for (int j = 0; j < minCols; j++)
                    newArray[i, j] = original[i, j];
            return newArray;
        }



        // execute INSERT command. Only one value
        public static void QueryInsertSingleValue(in string table, in string col_name, in string value, in string connection_path)
        {
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

        // execute INSERT command. For all or a few values
        public static void QueryInsertMultyValues(in string table, in string[] col_names, in string[] values, in string connection_path)
        {
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
