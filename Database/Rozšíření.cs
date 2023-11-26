using System.Data;

namespace Database
{
    public static class Rozšíření
    {
        public static void Vypis(this DataTable data)
        {
            foreach (DataRow item in data.Rows)
            {
                foreach (DataColumn col in data.Columns)
                {
                    Console.Write(item[col.ColumnName].ToString() + ", ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Continuos Press Key ....");
            Console.ReadKey(true);
        }

        public static void Vypis(this Dictionary<string, string> data)
        {
            foreach (KeyValuePair<string, string> item in data)
            {
                Console.WriteLine(item.Key + item.Value);
            }
            Console.WriteLine("Continuos Press Key ....");
            Console.ReadKey(true);
        }

        public static void Vypis(this string[] data)
        {
            foreach (string item in data)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Continuos Press Key ....");
            Console.ReadKey(true);
        }
    }
}