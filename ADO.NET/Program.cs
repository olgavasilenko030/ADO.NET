﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Configuration;

namespace ADO.NET
{
	internal class Program
	{
		static void Main(string[] args)
		{


			////////
			//1) бЕРЕМ СТРОКУ ПОДКЛЮЧЕНИЯ
			const int PADDING = 30;
			//const string CONNECTION_STRING =
			//	"Data Source=(localdb)\\ProjectModels;" +
			//	"Initial Catalog=Movies;" +
			//	"Integrated Security=True;" +
			//	"Connect Timeout=30;" +
			//	"Encrypt=False;" +
			//	"TrustServerCertificate=False;" +
			//	"ApplicationIntent=ReadWrite;" +
			//	"MultiSubnetFailover=False";
			string CONNECTION_STRING =  ConfigurationManager.ConnectionStrings["Movies_PV_319"].ConnectionString;
				
			Console.WriteLine(CONNECTION_STRING);
			//2)Создаем подключение к серверу
			SqlConnection connection = new SqlConnection(CONNECTION_STRING);
			//На даннй момент подключение является закрытым, мы его не открывали, а только создали

			//3) Создаем команду, которую нужно выполнить на сервере:

			string cmd = "SELECT title,release_date,FORMATMESSAGE(N'%s %s',first_name, last_name) FROM Movies,Directors WHERE director=director_id";
			SqlCommand command = new SqlCommand(cmd, connection);

			//4) Получаем результаты выполнения команды
			connection.Open();
			SqlDataReader reader = command.ExecuteReader();

			//5)Обрабатываем результаты запроса

			if (reader.HasRows)
			{
				Console.WriteLine("================================================================================================");
				for (int i = 0; i < reader.FieldCount; i++)
					Console.Write(reader.GetName(i).PadRight(PADDING));
				Console.WriteLine();
				Console.WriteLine("===============================================================================================");
				while (reader.Read())
				{
					//Console.WriteLine($"{reader[0].ToString().PadRight(15)}{reader[2].ToString().PadRight(15)}{reader[1].ToString().PadRight(15)}");
					for (int i = 0; i < reader.FieldCount; i++)
					{
						Console.Write(reader[i].ToString().PadRight(PADDING));
					}
					Console.WriteLine();
				}
			}
			//6)Закрываем SqlDataReader :
			reader.Close();
			connection.Close();

			Console.Read();
		}
	}
}
