using System.Data;

namespace XMLTabulka1
{
    public class LinqDotazy
    {
        public DataTable Test(DataTable table)
        {
            IEnumerable<DataRow> xxx = table.Rows.Cast<DataRow>()
                //.Select(jeden => jeden.Field<string>("C_UKOL"))
                //.Where(qwe => qwe == "0320")
                .Where(qwe => qwe.Field<string>("C_UKOL") == "0320")
                .Distinct()
                .OrderByDescending(d => d.Field<string>("C_UKOL"))
                //.OrderByDescending(d => d)
                ;
            if (xxx.Count() < 1) return null;
            DataTable data = xxx.CopyToDataTable();
            data.TableName = "cestina";
            return data;
        }

        /// <summary>
        /// Vybere jeden sloupec
        /// </summary>
        public string[] Slopec(DataTable table, VyberSloupec vyber)
        {
            IEnumerable<string?>? xx = table.Rows.Cast<DataRow>()
            .Select(qwe => qwe.Field<string>(vyber.ToString()))
            .Distinct(); //bez opakovani
            string[] data = xx.ToArray();

            string?[] xxx = table.Rows.Cast<DataRow>()
            .Select(qwe => qwe.Field<string>(vyber.ToString()))
            .Distinct() //bez opakovani
            .ToArray();

            return xxx;
        }

        /// <summary>
        /// Vybere jeden sloupec
        /// </summary>
        public string[] Slopec1(DataTable table, VyberSloupec vyber)
        {
            IEnumerable<string?>? xxx = table.AsEnumerable()
            .Select(qwe => qwe.Field<string>(vyber.ToString()))
            .Distinct()  //bez opakovani
            .OrderBy(qwe => qwe); //setrideni dle velikosti

            string[] data = xxx.ToArray();
            return data;
        }

        public DataTable Test4(DataTable table)
        {
            //List<(string, string)> data = new List<(string, string)>();

            IEnumerable<DataRow> e = table.AsEnumerable();

            IEnumerable<DataRow> rad = table.Rows.Cast<DataRow>();

            //IEnumerable<DataRow> query = table.Rows.Cast<DataRow>()
            var query = table.AsEnumerable()
                    .Where(xx => xx.Field<string>(VyberSloupec.C_UKOL.ToString()) == "0200")
                    .Select(qwe => new
                    {
                        d = qwe.Field<string>("DIL"),
                        c = qwe.Field<string>("C_UKOL"),
                    })
                    //.Select(qwe => new
                    //    {
                    //     DIL = qwe.Field<string>("DIL"),
                    //     CUKOL = qwe.Field<string>(VyberSloupec.C_UKOL.ToString())
                    //    })
                    .Distinct()
                    ;

            //DataTable zxf = query.CopyToDataTable();
            foreach (var item in query)
            {
                Console.WriteLine("Test4: " + item.c + "," + item.d);
            }
            Console.WriteLine("Continuos Press Key ....");
            Console.ReadKey(true);

            DataTable zxf = new();
            zxf.TableName = "POkus";
            return zxf;
        }

        /// <summary>
        /// Vybere zadaný sloupec neopakujícíse 
        /// </summary>
        public DataTable Test5(DataTable table, VyberSloupec vyber)
        {
            IEnumerable<DataRow> xxx = table.Rows.Cast<DataRow>()
                .GroupBy(dr => dr.Field<string>(vyber.ToString())) //seskupit podle sloupce
                                                                   //.Where(dr => dr.Count() > 1)
                                                                   //.Select(g => g.FirstOrDefault())
                .Select(g => g.FirstOrDefault()) //vybrat první
                .Distinct() //vyhodit opakovaní
                            //.OrderBy(d => d.Field<string>(vyber.ToString())) //setrídit
                .OrderByDescending(d => d.Field<string>(vyber.ToString())) //setrídit
                ;
            if (xxx == null) return null;
            DataTable data = xxx.CopyToDataTable();
            data.TableName = "cestina";
            return data;
        }

        public DataTable Test6(DataTable table, VyberSloupec vyber)
        {
            IEnumerable<DataRow> xxx = table.Rows.Cast<DataRow>()
                .GroupBy(dr => dr.Field<string>(vyber.ToString()))
                //.Where(dr => dr.Count() > 1)
                .Select(g => g.FirstOrDefault())
                //.Select(g => g.FirstOrDefault())
                //.Distinct()
                //.OrderBy(d => d.Field<string>(vyber.ToString()))
                .OrderByDescending(d => d.Field<string>(vyber.ToString())) //setrídit
                ;
            if (xxx == null) return null;
            DataTable data = xxx.CopyToDataTable();
            data.TableName = "cestina";
            return data;
        }

    }
}
