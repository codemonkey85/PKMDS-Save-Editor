﻿#pragma warning disable CA1416 // Validate platform compatibility
namespace PKMDS_Save_Editor;

internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main(string[] args)
    {
        var filePath = string.Empty;
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        if (args.Length > 0)
        {
            filePath = args[0];
        }
        Application.Run(new frmMain(filePath));
    }
}
#pragma warning restore CA1416 // Validate platform compatibility
