﻿using AutoCAD;
using Podpora;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using static System.Net.Mime.MediaTypeNames;

namespace LibraryAplikace
{
    public class Acad
    {
        public void Program()
        {
            //var acad =  PripojAcad();
            var acad = OpenAcad();
            if (acad != null) acad.Visible = true;
        }



        static AutoCAD.AcadApplication OpenAcad()
        {
            try
            {
                return (AutoCAD.AcadApplication)Marshal2.GetActiveObject("Autocad.Application");
            }
            catch (Exception)
            {
                return new AutoCAD.AcadApplication();
                //throw;
            }
        }



        /// <summary>
        /// Pripojení Aplikace Autocad
        /// </summary>
        /// <returns></returns>
        public AutoCAD.AcadApplication PripojAcad()
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
                    if (item.MainWindowTitle.Substring(0,16) == "Autodesk AutoCAD")
                    {
                        Console.WriteLine("Funguje bylo autodesk autocad 2019");
                        //acAppComObj = (AcadApplication)Marshal.GetActiveObject("AutoCAD.Application");

                        //var ttt = Marshal.GetComObjectData(item.Handle);
                        //app = Marshal.GetObjectForIUnknown(item.Handle) as AutoCAD.AcadApplication;

                        //app = CType(GetObject(, "Autocad.Application"), AcadApplication)

                        //AutoCAD.AcadApplication acadapp = (AutoCAD.AcadApplication)Application.AcadApplication;

                        //app = Marshal.GetObjectForIUnknown(item.MainWindowHandle);
                        //app = Marshal.GetObjectForIUnknown(item.MainWindowHandle);
                        //AutoCAD.AcadApplication obj = Marshal.ReleaseComObject(("Autocad.Application") as AutoCAD.AcadApplication; 
                        //app = (AutoCAD.AcadApplication)item; 

                        //https://stackoverflow.com/questions/58010510/no-definition-found-for-getactiveobject-from-system-runtime-interopservices-mars
                        string filename = @"C:\Users\Martin\Documents\rr.dwg";
                        var ttt = Marshal.BindToMoniker(filename);

                        AutoCAD.AcadDocument ddd = (AutoCAD.AcadDocument)ttt;
                        
                        var sss = ddd.Name;
                        break;
                    }
                }
                if (app == null)
                {
                    return new AutoCAD.AcadApplication();
                }
                return app;
            }
            catch (Exception)
            {
                app = new AutoCAD.AcadApplication();
                return app;
            }

        }

        /// <summary>
        /// Otevre výkres acadu
        /// </summary>
        /// <param name="cesta"></param>
        /// <returns></returns>
        public AutoCAD.AcadDocument OtevriAcad(string cesta)
        {
            if (File.Exists(cesta))
                return PripojAcad().Documents.Open(cesta);
            return null;
        }


    }
}