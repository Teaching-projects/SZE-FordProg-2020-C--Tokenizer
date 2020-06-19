using System;
using System.IO;

namespace Fordprog_Tokenizer
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileName;
            do
            {
                FileName = setFileName();
                string readedText = "";
                OutputFileCreater creater = null;
                StreamReader sr = null;
                try
                {
                    FileName = FileName == "" ? "Program" : FileName;
                    sr = new StreamReader(FileName + ".cs");
                    creater = new OutputFileCreater(FileName);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nMegnyitás sikeres!\n------{0}_output.cs létrehozva!--------", FileName);
                    Console.ResetColor();
                    while (!sr.EndOfStream)
                    {
                        readedText += sr.ReadLine();
                    }
                    Lex lexer = new Lex("tokens.csv");
                    while (readedText != "")
                    {
                        readedText = readedText.Trim(' ', '\t');
                        string token = lexer.getNextElement(ref readedText);
                        Console.WriteLine(token);
                        creater.addItemToString(token);
                    }
                }
                catch (Exception)
                {
                    if (sr == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nFájl megnyitása sikertelen!");
                        Console.ResetColor();
                    }
                }
                if (creater != null)
                {
                    creater.fileCloser();
                }
                Console.WriteLine("\n");
            } while (!FileName.ToLower().Equals("exit"));
        }

        public static string setFileName()
        {
            Console.Write("Kilépéshez írja be, hogy EXIT!\nAdja meg a fájl nevét/elérési útját vagy üssön ENTER-t, hogy ezt a programot vizsgálja: ");
            string fileName = Console.ReadLine();
            return fileName;
        }

    }
}
