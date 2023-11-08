using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using XMLTabulka1.API;
using XMLTabulka1.Trida;

namespace XMLTabulka1
{
    public class SQL
    {
        /// <summary>
        /// ip adresa aktuálního počítače
        /// </summary>
        /// <returns></returns>
        public static string Podminka
        {
            get {
                //string MachineName = Environment.MachineName;
                //string Jmeno = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString();
                //if (Jmeno == "fe80::6521:3e9b:e592:b3df%11")
                if (Environment.MachineName == "W10177552")
                        return "10.55.1.100";
                return @"KANCELAR\SQLEXPRESS";
            }
        }

        public static string SQL3DPlant => @"ENCZ12\PLANT3D";

        /// <summary>
        /// Jakýkoli textový dotaz na databázi v SQL server, není definován nazev tabulky
        /// </summary>
        public static bool SQLConection(string Querry)
        {
            SqlConnection ConnectionString = new("Data Source=" + Podminka + ";Initial Catalog=master;Integrated Security=True;Pooling=False");
            SqlCommand cmd = new(Querry, ConnectionString);
            try
            {
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return true;
            }
            catch (Exception)
            {
                cmd.Connection.Close();
                return false;
            }
        }

        /// <summary>
        /// Jakýkoli textový dotaz "Querry" na databázi "Database" v SQL server
        /// </summary>
        public static void SQLConection(string Querry, string Database)
        {
            SqlConnection ConnectionString = new("Data Source=" + Podminka + ";Initial Catalog=" + Database + ";Integrated Security=True;Pooling=False");
            SqlCommand cmd = new(Querry, ConnectionString);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        /// <summary>
        /// Načete data z databaze SQL
        /// </summary>
        public static DataSet NactiSQL(string Querry)
        {
            DataSet Data = new();
            SqlConnection ConnectionString = new("Data Source=" + Podminka + ";Initial Catalog=master;Integrated Security=True;Pooling=False");
            SqlCommand cmd = new(Querry, ConnectionString);
            cmd.Connection.Open();
            SqlDataAdapter sqlda = new(cmd);
            sqlda.Fill(Data);
            //cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return Data;
        }

        /// <summary>
        /// Načete jmena databazi z SQL SQL3DPlant
        /// </summary>
        public static string[] Databaze()
        {
            DataSet Data = new();
            SqlConnection ConnectionString = new("Data Source=" + SQL3DPlant + ";Initial Catalog=master;Integrated Security=True;Pooling=False");
            string Querry = "SELECT name FROM master.dbo.sysdatabases WHERE name NOT IN('master', 'tempdb', 'model', 'msdb')";
            SqlCommand cmd = new(Querry, ConnectionString);
            cmd.Connection.Open();
            SqlDataAdapter sqlda = new(cmd);
            sqlda.Fill(Data);

            List<string> textlist = new();
            foreach (DataRow item in Data.Tables[0].Rows)
            {
                textlist.Add(item[0].ToString());
            }
            
            return textlist.ToArray();
        }

        /// <summary>
        /// Kontrola nových záznamů v SQL vůči Dbf
        /// </summary>
        public static async Task DataSqlAdd()
        {
            DbfDotazySQL sql = new();
            Console.Write("Načtení Tabulky z databaze TeZak.dbf ..." + Cesty.SouborDbf);
            DataTable dt = sql.HledejVse();
            Console.WriteLine(" OK --");

            Regex regex = new("'");
            foreach (DataRow dr in dt.Rows)
            {
                var global = dr["GLOBALID"].ToString();
                //bude použito RestApi
                var querry = await API.API.LoadAPI<TeZak>("api/TeZak/GLOBALID/" + global);
     
            }
        }

        /// <summary>
        /// Vytvoření databeze na SQl Serveru z databaze DBF. Podmínka je název počítače převeden na IP Adresu
        /// </summary>
        public static void DataSql()
        {
            //pouze jednou
            string Database = "TractebelTeZak";
            string Table = "TeZak";

            //Vytvoření databaze
            string QuerryNew = "CREATE DATABASE " + Database;  //the command that creates New database
            if (!SQLConection(QuerryNew))
            {
                //funguje
                try
                {
                    //smazat tabulku
                    SqlConnection ConnectionString2 = new("Data Source=" + Podminka + ";Initial Catalog= " + Database + " ;Integrated Security=True;Pooling=False");
                    SqlCommand oCommand2 = new("DROP TABLE [" + Table + "]", ConnectionString2);
                    Console.Write("Table delete ...");
                    oCommand2.Connection.Open();
                    oCommand2.ExecuteNonQuery();
                    oCommand2.Connection.Close();
                    Console.WriteLine(" OK --");
                }
                catch (Exception)
                { Console.WriteLine("CHYBA -- Pass / Přerušeno --"); }

                try
                {
                    //smazat databazi
                    SqlConnection ConnectionString1 = new("Data Source=" + Podminka + ";Integrated Security=True;Pooling=False");
                    SqlCommand oCommand1 = new("DROP DATABASE [" + Database + "]", ConnectionString1);
                    Console.Write("Database delete ...");
                    oCommand1.Connection.Open();
                    oCommand1.ExecuteNonQuery();
                    oCommand1.Connection.Close();
                    Console.WriteLine(" OK --");
                }
                catch (Exception)
                { Console.WriteLine("CHYBA -- Pass / Přerušeno --"); }
            }
            else 
            {
                Console.WriteLine("Databaze je OK");
            }

            DbfDotazySQL sql = new();
            Console.Write("Načtení Tabulky z databaze TeZak.dbf ..." + Cesty.SouborDbf);     
            DataTable dt = sql.HledejVse();
            Console.WriteLine(" OK --");
            //new Table
            //string strCreateColumns = "";
            //string strColumnList = ""; 
            //string strQuestionList = "";
            string strCreateColumns = "[APID] VarChar(20), ";// přidání Apid
            string strColumnList = string.Empty;
            string strQuestionList = string.Empty;
            Console.Write("Table create ...");
            
            foreach (DataColumn oColumn in dt.Columns)
            {
                strCreateColumns += "[" + oColumn.ColumnName + "] VarChar(100), ";
                strColumnList += "[" + oColumn.ColumnName + "],";
                strQuestionList += "?,";
            }
            strCreateColumns = strCreateColumns.Remove(strCreateColumns.Length - 2);
            strColumnList = strColumnList.Remove(strColumnList.Length - 1);
            strQuestionList = strQuestionList.Remove(strQuestionList.Length - 1);

            SqlConnection ConnectionString = new("Data Source=" + Podminka + ";Initial Catalog= " + Database + " ;Integrated Security=True;Pooling=False");
            SqlCommand oCommand = new("CREATE TABLE "+Table+" (ID INT IDENTITY(1,1) NOT NULL," + strCreateColumns + ")", ConnectionString);
            oCommand.Connection.Open();
            oCommand.ExecuteNonQuery();
            //oCommand.Connection.Close();
            Console.WriteLine(" OK --");

            Console.WriteLine("Table Add Data ...");
            //Get field names
            string sqlString = "INSERT INTO "+Table+" (";
            string valString = string.Empty;
            var sqlParams = new string[dt.Rows[0].ItemArray.Length];
            int count = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                sqlString += dc.ColumnName + ", ";
                valString += "@" + dc.ColumnName + ", ";
                sqlParams[count] = "@" + dc.ColumnName;
                count++;
            }
            //valString = valString.Substring(0, valString.Length - 2);
            //valString = valString[..^2]; vymazat dva poseldní znaky.
            valString = valString + "APID";
            //var sqlString1 = sqlString.Substring(0, sqlString.Length - 2) + ") VALUES ('" + valString + "')";
            var sqlString1 = sqlString + "APID" + ") VALUES ('" + valString + "')";

            int pocet = 0;
            oCommand.CommandText = sqlString1;
            //asi není dokončeno vložení jmen sloupcu do prvního radku 

            //vzor 
            Regex regex = new("'");
            foreach (DataRow dr in dt.Rows)
            {
                sqlString1 = "";
                valString = "";
                for (int i = 0; i < dr.ItemArray.Length; i++)
                {
                    //mazaní apostrofů
                    valString += "'" + regex.Replace(dr.ItemArray[i].ToString(), "") + "', ";
                }
                //valString = valString.Substring(0, valString.Length - 2);
                valString = valString + "'" +  Apid.Create() + "'";
                //sqlString1 = sqlString.Substring(0, sqlString.Length - 2) + ") VALUES (" + valString + ")";
                sqlString1 = sqlString + "APID" + ") VALUES (" + valString + ")";
                if (sqlString1 != null || sqlString1 == "")
                { 
                    oCommand.CommandText = sqlString1;
                    oCommand.ExecuteNonQuery();
                    Console.WriteLine("záznam "  + pocet++ + "... vytvořen.");
                }
            }
            ConnectionString.Close();
            Console.WriteLine("............Dokončeno");
        }

    }
}
