﻿//using Microsoft.VisualBasic;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Diagnostics.Metrics;
//using System.Linq;
//using System.Reflection.Metadata;
//using System.Runtime.Intrinsics.X86;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;

//namespace XMLTabulka1
//{
//    public class SQL
//    {
//        /// <summary>
//        /// ip adresa aktuálního počítače podle jména počítače
//        /// </summary>
//        public static string Podminka
//        {
//            get {
//                string MachineName = Environment.MachineName;
//                //string Jmeno = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString();
//                //if (Jmeno == "fe80::6521:3e9b:e592:b3df%11")
//                if (Environment.MachineName == "W10177552")
//                        return "10.55.1.100";
//                return @"KANCELAR\SQLEXPRESS";
//            }
//        }

//        public static string SQL3DPlant => @"ENCZ12\PLANT3D";

//        /// <summary>
//        /// Jakýkoli textový dotaz na databázi v SQL server, není definován nazev tabulky
//        /// </summary>
//        /// <param name="Querry"></param>
//        public bool SQLConection(string Querry)
//        {
//            SqlConnection ConnectionString = new SqlConnection("Data Source=" + Podminka + ";Initial Catalog=master;Integrated Security=True;Pooling=False");
//            SqlCommand cmd = new SqlCommand(Querry, ConnectionString);
//            try
//            {
//                cmd.Connection.Open();
//                cmd.ExecuteNonQuery();
//                cmd.Connection.Close();
//                return true;
//            }
//            catch (Exception)
//            {
//                cmd.Connection.Close();
//                return false;
//            }
//        }

//        /// <summary>
//        /// Jakýkoli textový dotaz "Querry" na databázi "Database" v SQL server
//        /// </summary>
//        public void SQLConection(string Querry, string Database)
//        {
//            SqlConnection ConnectionString = new SqlConnection("Data Source=" + Podminka + ";Initial Catalog=" + Database + ";Integrated Security=True;Pooling=False");
//            SqlCommand cmd = new SqlCommand(Querry, ConnectionString);
//            cmd.Connection.Open();
//            cmd.ExecuteNonQuery();
//            cmd.Connection.Close();
//        }

//        /// <summary>
//        /// Načete data z databaze SQL
//        /// </summary>
//        public DataSet NactiSQL(string Querry)
//        {
//            DataSet Data = new();
//            SqlConnection ConnectionString = new SqlConnection("Data Source=" + Podminka + ";Initial Catalog=master;Integrated Security=True;Pooling=False");
//            SqlCommand cmd = new SqlCommand(Querry, ConnectionString);
//            cmd.Connection.Open();
//            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
//            sqlda.Fill(Data);
//            //cmd.ExecuteNonQuery();
//            cmd.Connection.Close();
//            return Data;
//        }

//        /// <summary>
//        /// Načete jmena databazi z SQL SQL3DPlant
//        /// </summary>
//        public string[] Databaze()
//        {
//            DataSet Data = new();
//            SqlConnection ConnectionString = new SqlConnection("Data Source=" + SQL3DPlant + ";Initial Catalog=master;Integrated Security=True;Pooling=False");
//            string Querry = "SELECT name FROM master.dbo.sysdatabases WHERE name NOT IN('master', 'tempdb', 'model', 'msdb')";
//            SqlCommand cmd = new SqlCommand(Querry, ConnectionString);
//            cmd.Connection.Open();
//            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
//            sqlda.Fill(Data);

//            List<string> textlist = new();
//            foreach (DataRow item in Data.Tables[0].Rows)
//            {
//                textlist.Add(item[0].ToString());
//            }
            
//            return textlist.ToArray();
//        }

//        /// <summary>
//        /// Vytvoření kopie DBF na SQl Serveru
//        /// </summary>
//        public async void DataSql()
//        {
//            //pouze jednou

//            string Database = "TractebelTeZak";
//            string Table = "TeZak";

//            //Vytvoření databaze
//            string QuerryNew = "CREATE DATABASE " + Database;  //the command that creates New database
//            if (!SQLConection(QuerryNew))
//            {
//                //funguje
//                try
//                {
//                    //smazat tabulku
//                    SqlConnection ConnectionString2 = new SqlConnection("Data Source=" + Podminka + ";Initial Catalog= " + Database + " ;Integrated Security=True;Pooling=False");
//                    SqlCommand oCommand2 = new SqlCommand("DROP TABLE ["+Table+"]", ConnectionString2);

//                    oCommand2.Connection.Open();
//                    oCommand2.ExecuteNonQuery();
//                    Console.WriteLine("Table delete -- OK --");
//                    oCommand2.Connection.Close();
//                }
//                catch (Exception)
//                { Console.WriteLine("CHYBA Table delete -- Pass --"); }

//                try
//                {
//                    //smazat databazi
//                    SqlConnection ConnectionString1 = new SqlConnection("Data Source=" + Podminka + ";Integrated Security=True;Pooling=False");
//                    SqlCommand oCommand1 = new SqlCommand("DROP DATABASE [" + Database + "]", ConnectionString1);

//                    oCommand1.Connection.Open();
//                    oCommand1.ExecuteNonQuery();
//                    Console.WriteLine("Database delete -- OK --");
//                    oCommand1.Connection.Close();
//                }
//                catch (Exception)
//                { Console.WriteLine("CHYBA Database delete -- Pass --"); }
//                return;
//            }

//            SQLDotazy sql = new();
//            DataTable dt = sql.HledejVse();

//            //new Table
//            string strCreateColumns = "";
//            string strColumnList = "";
//            string strQuestionList = "";
//            foreach (DataColumn oColumn in dt.Columns)
//            {
//                strCreateColumns += "[" + oColumn.ColumnName + "] VarChar(100), ";
//                strColumnList += "[" + oColumn.ColumnName + "],";
//                strQuestionList += "?,";
//            }
//            strCreateColumns = strCreateColumns.Remove(strCreateColumns.Length - 2);
//            strColumnList = strColumnList.Remove(strColumnList.Length - 1);
//            strQuestionList = strQuestionList.Remove(strQuestionList.Length - 1);

//            SqlConnection ConnectionString = new SqlConnection("Data Source=" + Podminka + ";Initial Catalog= " + Database + " ;Integrated Security=True;Pooling=False");
//            SqlCommand oCommand = new SqlCommand("CREATE TABLE "+Table+" (ID INT IDENTITY(1,1) NOT NULL," + strCreateColumns + ")", ConnectionString);
//            oCommand.Connection.Open();
//            oCommand.ExecuteNonQuery();

//            //Get field names
//            string sqlString = "INSERT INTO "+Table+" (";
//            string valString = "";
//            var sqlParams = new string[dt.Rows[0].ItemArray.Count()];
//            int count = 0;
//            foreach (DataColumn dc in dt.Columns)
//            {
//                sqlString += dc.ColumnName + ", ";
//                valString += "@" + dc.ColumnName + ", ";
//                sqlParams[count] = "@" + dc.ColumnName;
//                count++;
//            }
//            valString = valString.Substring(0, valString.Length - 2);
//            var sqlString1 = sqlString.Substring(0, sqlString.Length - 2) + ") VALUES ('" + valString + "')";

//            int pocet = 0;
//            oCommand.CommandText = sqlString1;
//            //vzor 
//            Regex regex = new Regex("'");
//            foreach (DataRow dr in dt.Rows)
//            {
//                sqlString1 = "";
//                valString = "";
//                for (int i = 0; i < dr.ItemArray.Count(); i++)
//                {
//                    //mazaní apostrofů
//                    valString += "'" + regex.Replace(dr.ItemArray[i].ToString(), "") + "', ";
//                }
//                valString = valString.Substring(0, valString.Length - 2);
//                sqlString1 = sqlString.Substring(0, sqlString.Length - 2) + ") VALUES (" + valString + ")";
//                if (sqlString1 != null || sqlString1 == "")
//                { 
//                    oCommand.CommandText = sqlString1;
//                    oCommand.ExecuteNonQuery();
//                    Console.WriteLine(pocet++);
//                }
//            }
//            ConnectionString.Close();
//          }

//    }
//}