using AutoCAD;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Library48
{
    public class Acad
    {
        public void Program()
        {
            var acad = PripojAcad48();
            acad.Visible = true;
        }

        /// <summary>
        /// Pripojení Aplikace Autocad
        /// </summary>
        /// <returns></returns>
        public AutoCAD.AcadApplication PripojAcad48()
        {
            AutoCAD.AcadApplication app = null;
            try
            {
                Process[] processlist = Process.GetProcesses();
                foreach (var item in processlist)
                {
                    //Console.WriteLine("item.ProcessName " + item.ProcessName);
                    Console.WriteLine("item.MainWindowTitle \t\t" + item.MainWindowTitle.ToString());
                    if (item.MainWindowTitle.Length < 20) continue;
                    Console.WriteLine("item.MainWindowTitle \t\t" + item.MainWindowTitle.Substring(0, 16).ToString());
                    if (item.MainWindowTitle.Substring(0, 16) == "Autodesk AutoCAD")
                    {
                        Console.WriteLine("Funguje bylo autodesk autocad 2019");
                        app = (AcadApplication)Marshal.GetActiveObject("AutoCAD.Application");
                        break;
                    }
                }
                if (app == null)
                {
                    //return new AutoCAD.AcadApplication();
                }
                return app;
            }
            catch (Exception)
            {
                app = new AutoCAD.AcadApplication();
                return app;
            }

        }
    }
}
