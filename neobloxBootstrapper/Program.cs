﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace neobloxBootstrapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            logoConsole();

            Console.WriteLine(@"@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@(//(((((&@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@(///((((%&@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@...........@@@@@@@@@@@@@@@@@@@@@@@@@@@@@((//(((((&@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@,............&@@@@@@@@@@@@@@@@@@@@@@@@@@#(//(((((&@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@*,............,%@@@@@@@@@@@@@@@@@@@@@@@@#(/((((((&@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@/,............,,,%@@@@@@@@@@@@@@@@@@@@@@@(/((((((&&@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@,............,,,,,%@@@@@@@@@@@@@@@@@@@@@(((((((((&@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@,...........,,,,,,,,(@@@@@@@@@@@@@@@@@@@(((((((((&@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@*,.......%@/,,,,,,,,,,*@@@@@@@@@@@@@@@@@#((((((((&@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@*,.......%@@@(*,,,,,,,***@@@@@@@@@@@@@@@@((((((((&@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@#,.......%@@@@@@*,,,,,*****&@@@@@@@@@@@@@((((((((&&@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@,.......(&@@@@@@@/,,,*******%@@@@@@@@@@@#((((((((&@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@*........%@@@@@@@@@/,*********&@@@@@@@@@#((((((((&@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@*,.......%@@@@@@@@@@@/********//#@@@@@@@##(((((((&@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@**.......%@@@@@@@@@@@@@(******/////@@@@@@#(((((((&&@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@*.......%%@@@@@@@@@@@@@@#/***///////@@@@#((((((((&@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@*........%@@@@@@@@@@@@@@@@@///////////@@#((((((((&@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@*,.......%@@@@@@@@@@@@@@@@@@@(/////////((((((((((&@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@*,.......%@@@@@@@@@@@@@@@@@@@@@(///////((((((((((&@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@/*.......%@@@@@@@@@@@@@@@@@@@@@@@(////(((((((((((&&@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@*.......%%@@@@@@@@@@@@@@@@@@@@@@@@#(/((((((((((((&@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@*.......,%@@@@@@@@@@@@@@@@@@@@@@@@@@%((((((((((((&@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@*,......,%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@((((((((((&@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@/*.....,,%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@*....,,%%@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@");

            Thread.Sleep(2000);

            // Console managment

            void errorDump(Exception err)
            {
                if (!File.Exists("err.txt"))
                {
                    File.Create("err.txt").Close();
                }

                File.WriteAllText("err.txt", err.ToString());

                Console.WriteLine("Wrote error to err.txt! Please make sure to open a GitHub issue on the NeobloxExecutor/Neoblox repository with the error!");
                Console.WriteLine("Press any key to open the GitHub repository issues page...");
                Console.ReadLine();
                System.Diagnostics.Process.Start("https://github.com/NeobloxExecutor/neoblox/issues");
            }

            void greenConsole()
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            void redConsole()
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }

            void logoConsole()
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }

            void infoConsole()
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }

            void normalConsole()
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            void space()
            {
                Console.WriteLine("");
            }

            space();

            infoConsole();

            Console.WriteLine("Would you like to download Monaco Neoblox? (Made for normal computers) Or Scintilla Neoblox? (Made for lower end computers)");

            space();

            Console.WriteLine("Enter \"1\" for Monaco Neoblox. Enter \"2\" for Scintilla Neoblox");

            space();

            string decision = Console.ReadLine();

            if (decision == "1")
            {
                using (var client = new WebClient())
                {
                    try
                    {
                        space();

                        greenConsole();
                        Console.WriteLine("Downloading Monaco Neoblox...");
                        space();
                        normalConsole();

                        if (File.Exists("Neoblox.zip") != false)
                        {
                            redConsole();
                            Console.WriteLine("Neoblox.zip is already downloaded in this directory!");
                            space();
                            normalConsole();
                        }

                        if (File.Exists("Neoblox.zip") == false)
                        {
                            greenConsole();
                            client.DownloadFile("https://neoblox.is-from.space/r/Neoblox.zip", "Neoblox.zip");
                            Thread.Sleep(2000);
                            Console.WriteLine("Successfully downloaded Neoblox!");
                            normalConsole();

                            space();
                        }
                    }
                    catch (Exception ex)
                    {
                        space();

                        redConsole();
                        Console.WriteLine("Ran into an error in the archive download phase");

                        errorDump(ex);

                        System.Environment.Exit(1);
                    }

                    try
                    {
                        greenConsole();
                        Console.WriteLine("Extracting Neoblox.zip....");

                        Thread.Sleep(5000);

                        string neobloxArchive = $"{AppDomain.CurrentDomain.BaseDirectory}\\Neoblox.zip";

                        ZipFile.ExtractToDirectory(neobloxArchive, AppDomain.CurrentDomain.BaseDirectory);

                        File.Delete(neobloxArchive);

                        space();

                        Console.WriteLine("Successfully extracted Neoblox.zip!");

                        Thread.Sleep(1000);

                        System.Diagnostics.Process.Start($"{AppDomain.CurrentDomain.BaseDirectory}\\Neoblox");
                    }

                    catch (Exception ex)
                    {
                        space();

                        redConsole();
                        Console.WriteLine("Ran into an error in the archive extraction phase");

                        errorDump(ex);

                        System.Environment.Exit(1);
                    }
                }
            }

            if (decision == "2")
            {
                using (var client = new WebClient())
                {
                    try
                    {
                        space();

                        greenConsole();
                        Console.WriteLine("Downloading Scintilla Neoblox...");
                        space();
                        normalConsole();

                        if (File.Exists("Scintilla Neoblox.zip") != false)
                        {
                            redConsole();
                            Console.WriteLine("Scintilla Neoblox.zip is already downloaded in this directory!");
                            space();
                            normalConsole();
                        }

                        if (File.Exists("Scintilla Neoblox.zip") == false)
                        {
                            greenConsole();
                            client.DownloadFile("https://neoblox.is-from.space/r/lightweightNeoblox.zip", "Scintilla Neoblox.zip");
                            Thread.Sleep(2000);
                            Console.WriteLine("Successfully downloaded Scintilla Neoblox!");
                            normalConsole();

                            space();
                        }
                    }
                    catch (Exception ex)
                    {
                        space();

                        redConsole();
                        Console.WriteLine("Ran into an error in the download phase");

                        errorDump(ex);

                        System.Environment.Exit(1);
                    }

                    try
                    {
                        greenConsole();
                        Console.WriteLine("Extracting Scintilla Neoblox.zip....");

                        Thread.Sleep(5000);

                        string neobloxArchive = $"{AppDomain.CurrentDomain.BaseDirectory}\\Scintilla Neoblox.zip";

                        ZipFile.ExtractToDirectory(neobloxArchive, AppDomain.CurrentDomain.BaseDirectory);

                        File.Delete(neobloxArchive);

                        space();

                        Console.WriteLine("Successfully extracted Neoblox.zip!");

                        Thread.Sleep(1000);

                        System.Diagnostics.Process.Start($"{AppDomain.CurrentDomain.BaseDirectory}\\lightweightNeoblox");
                    }

                    catch (Exception ex)
                    {
                        space();

                        redConsole();
                        Console.WriteLine("Ran into an error in the archive extraction phase");

                        errorDump(ex);

                        System.Environment.Exit(1);
                    }
                }
            }
        }
    }
}
