using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace matrix
{
    class Program
    {
        static char[] characterSelection;
        static Random rnd = new Random();


        static void Main(string[] args)
        {
            // Hello world
            Console.WriteLine("Hello World!");

            // Check the terminal codepage (Windows, dunno about Linux/macOS tho)
            if (Console.OutputEncoding.CodePage == 65001) // Is this Unicode?
                //                        カタカナ
                // Yay. Display Japanese (Katakana) symbols
                characterSelection = new char[] { 'ア', 'イ', 'ウ', 'エ', 'オ', // No consonant
                                                  'カ', 'キ', 'ク', 'ケ', 'コ', // k
                                                  'サ', 'シ', 'ス', 'セ', 'ソ', // s
                                                  'タ', 'チ', 'ク', 'ケ', 'コ', // t
                                                  'ナ', 'ニ', 'ク', 'ケ', 'コ', // n
                                                  'ハ', 'ヒ', 'ク', 'ケ', 'コ', // h
                                                  'マ', 'ミ', 'ク', 'ケ', 'コ', // m
                                                  'ヤ',     'ユ',     'ヨ', // y
                                                  'ワ',               'ヲ', // w
                                                  'ン'               }; // Miscellaneous
            else
                // Boo! Get your mind out of the gutter, Microsoft!
                characterSelection = new char[] { 'A', 'B', 'C', 'D', 'E', 'F',
                                                  'G', 'H', 'I', 'J', 'K', 'L',
                                                  'M', 'N', 'O', 'P', 'Q', 'R',
                                                  'S', 'T', 'U', 'V', 'W', 'X',
                                                  'Y', 'Z'                   };

            // Disable handling Ctrl+C, the application will handle it
            // (in order to no leave the colour screwed for following applications)
            Console.TreatControlCAsInput = true;

            // Create matrix drawing thread
            var thread = new Thread(Matrix);
            thread.Start();

            while (true)
            {
                // Handle Ctrl+C if that is on the way in
                if (Console.KeyAvailable)
                {
                    // A key is, let's fetch it
                    var input = Console.ReadKey(true);
                    if (input.KeyChar == 3) // If the character is ASCII 3 [ETX]
                    {
                        // It is Ctrl+C, do something
                        //Thread.CurrentThread.Abort();
                        Environment.Exit(0);
                    }
                }
            }
        }

        static void Matrix()
        {
            // Change the terminal a bit
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.CursorVisible = false;

            Dictionary<int, int> lines = new Dictionary<int, int>();

            // Do it until the thread breaks
            while (true)
            {
                // Pick a random line
                var rndLine = rnd.Next(Console.WindowWidth);

                // See if it has been written to before or not
                if (lines.ContainsKey(rndLine))
                {
                    // The line has been declared
                    lines[rndLine]++; // Increment line
                    Console.CursorLeft = rndLine;       // Set the cursor
                    Console.CursorTop = lines[rndLine];
                    Console.Write(characterSelection[rnd.Next(characterSelection.Length)]); // Draw the char
                }
                else
                {
                    // It hasn't been added yet.
                    lines.Add(rndLine, 0); // Add line to array, start from top
                    Console.CursorLeft = rndLine;       // Set the cursor
                    Console.CursorTop = lines[rndLine];
                    Console.Write(characterSelection[rnd.Next(characterSelection.Length)]); // Draw the char
                }

                // Wait a bit
                Thread.Sleep(10);
            }
        }
    }
}
