﻿using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;

Console.WriteLine("xxxxxxxxxxx");

[DllImport("kernel32.dll")]
static extern IntPtr GetConsoleWindow();

[DllImport("user32.dll")]
static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

const int SW_HIDE = 0;
const int SW_SHOW = 5;

var handle = GetConsoleWindow();
// Hide
ShowWindow(handle, SW_HIDE);
try
{
    //cesta nastavena podle uživatele do AppData/roaming
	string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
	string StartSoubor = Path.Combine(AppData, "TeZak", @"WFForm.exe");
    Console.WriteLine(StartSoubor);
    Process.Start(StartSoubor);
	Environment.Exit(0);
}
catch (Exception)
{
    Environment.Exit(0);
}