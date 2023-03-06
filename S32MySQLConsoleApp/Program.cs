using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using MySql.Data.MySqlClient;

namespace ProductMasterImporter
{
	class Program
	{
		static async Task Main(string[] args)
		{
			// Amazon S3 の認証情報を設定します
			var s3Config = new AmazonS3Config
			{
				RegionEndpoint = RegionEndpoint.USEast1,
				// 以下は、必要に応じて設定してください
				//  Credentials = new Amazon.Runtime.BasicAWSCredentials("accessKeyId", "secretAccessKey")
			};
			var s3Client = new AmazonS3Client(s3Config);

			// Amazon S3 バケットから CSV ファイルをダウンロードします
			var getObjectRequest = new GetObjectRequest
			{
				BucketName = "s3-bucket-name",
				Key = "product_master.csv"
			};
			using var getObjectResponse = await s3Client.GetObjectAsync(getObjectRequest);
			using var streamReader = new StreamReader(getObjectResponse.ResponseStream);
			var csvData = await streamReader.ReadToEndAsync();

			// AWS Aurora データベースへの接続情報を設定します
			var connectionStringBuilder = new MySqlConnectionStringBuilder
			{
				Server = "database-host-name",
				Database = "database-name",
				UserID = "database-user-id",
				Password = "database-password"
			};
			using var connection = new MySqlConnection(connectionStringBuilder.ConnectionString);

			// 商品マスタテーブルに CSV ファイルのデータを挿入します
			var insertCommand = new MySqlCommand
			{
				Connection = connection,
				CommandText = "INSERT INTO product_master (product_code, product_name, price) VALUES (@product_code, @product_name, @price);"
			};
			insertCommand.Parameters.Add("@product_code", MySqlDbType.VarChar);
			insertCommand.Parameters.Add("@product_name", MySqlDbType.VarChar);
			insertCommand.Parameters.Add("@price", MySqlDbType.Int32);
			await connection.OpenAsync();
			foreach (var line in csvData.Split('\n'))
			{
				var values = line.Split(',');
				if (values.Length == 3)
				{
					insertCommand.Parameters["@product_code"].Value = values[0];
					insertCommand.Parameters["@product_name"].Value = values[1];
					insertCommand.Parameters["@price"].Value = int.Parse(values[2]);
					await insertCommand.ExecuteNonQueryAsync();
				}
			}
			await connection.CloseAsync();
		}
	}
}
