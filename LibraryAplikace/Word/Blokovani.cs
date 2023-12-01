using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace LibraryAplikace.Word
{
    public class Blokovani
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool BlockInput([MarshalAs(UnmanagedType.Bool)] bool fBlockIt);

        public static void TestBlokování()
        {
            // Blokování myši a klávesnice
            BlockInput(true);

            Console.WriteLine("Myš a klávesnice jsou nyní blokovány.");

            // Počkejte na nějakou dobu (např. 5 sekund)
            Thread.Sleep(5000);
            
            // Odemknutí myši a klávesnice
            BlockInput(false);

            Console.WriteLine("Myš a klávesnice jsou nyní odemknuté.");
        }

        public static void Zapnuto()
        {
            // Blokování myši a klávesnice
            BlockInput(true);

        }
        public static void Vypnuto()
        {
            // Odemknutí myši a klávesnice
            BlockInput(false);

        }

    }
}
