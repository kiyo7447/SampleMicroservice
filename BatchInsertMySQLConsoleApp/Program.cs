using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;

namespace BatchInsertExample
{
	class Program
	{
		static void Main(string[] args)
		{
			// データベース接続情報の設定
			string connectionString = "server=localhost;user=root;password=password;database=mydb";

			// データの読み取り
			List<string[]> data = ReadDataFromCsv("data.csv");

			// バッチ挿入の設定
			int batchSize = 1000;

			// バッチ挿入の実行
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				using (MySqlCommand command = new MySqlCommand())
				{
					command.Connection = connection;
					command.CommandType = CommandType.Text;
					command.CommandText = "INSERT INTO products (name, price) VALUES (@name, @price)";

					for (int i = 0; i < data.Count; i += batchSize)
					{
						int count = Math.Min(batchSize, data.Count - i);
						for (int j = 0; j < count; j++)
						{
							command.Parameters.AddWithValue("@name" + j, data[i + j][0]);
							command.Parameters.AddWithValue("@price" + j, int.Parse(data[i + j][1]));
						}
						command.ExecuteNonQuery();
						command.Parameters.Clear();
					}
				}
				connection.Close();
			}
		}

		static List<string[]> ReadDataFromCsv(string fileName)
		{
			List<string[]> data = new List<string[]>();
			using (StreamReader reader = new StreamReader(fileName))
			{
				while (!reader.EndOfStream)
				{
					string line = reader.ReadLine();
					string[] values = line.Split(',');
					data.Add(values);
				}
			}
			return data;
		}
	}
}
