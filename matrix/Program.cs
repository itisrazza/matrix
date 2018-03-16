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
                characterSelection = new char[] { 'ｱ', 'ｲ', 'ｳ', 'ｴ', 'ｵ', // No consonant
                                                  'ｶ', 'ｷ', 'ｸ', 'ｹ', 'ｺ', // k
                                                  'ｻ', 'ｼ', 'ｽ', 'ｾ', 'ｿ', // s
                                                  'ﾀ', 'ﾁ', 'ﾂ', 'ﾃ', 'ﾄ', // t
                                                  'ﾅ', 'ﾆ', 'ﾇ', 'ﾈ', 'ﾉ', // n
                                                  'ﾊ', 'ﾋ', 'ﾌ', 'ﾍ', 'ﾎ', // h
                                                  'ﾏ', 'ﾐ', 'ﾑ', 'ﾒ', 'ﾓ', // m
                                                  'ﾔ',      'ﾕ',      'ﾖ', // y
                                                  'ﾜ',                'ｦ', // w
                                                  'ﾝ',                     // Miscellaneous
                                                  '0', '1', '2', '3', '4', // Numbers
                                                  '5', '6', '7', '8', '9'};
            else
                // Boo! Get your mind out of the gutter, Microsoft!
                characterSelection = new char[] { 'A', 'B', 'C', 'D', 'E', 'F',
                                                  'G', 'H', 'I', 'J', 'K', 'L',
                                                  'M', 'N', 'O', 'P', 'Q', 'R',
                                                  'S', 'T', 'U', 'V', 'W', 'X',
                                                  'Y', 'Z',
                                                  '0', '1', '2', '3', '4',
                                                  '5', '6', '7', '8', '9'};

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
                        Console.ResetColor();
                        Console.Clear();
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

                // See if it has been written to before or not, also if the line is full
                if (lines.ContainsKey(rndLine) &&
                    lines[rndLine] < Console.WindowHeight - 1) // Also, check if line is full
                {
                    // The line has been declared
                    lines[rndLine]++; // Increment line
                    Console.CursorLeft = rndLine;       // Set the cursor
                    Console.CursorTop = lines[rndLine];
                    Console.Write(characterSelection[rnd.Next(characterSelection.Length)]); // Draw the char
                }
                else if (lines.ContainsKey(rndLine) && lines[rndLine] >= Console.WindowHeight - 1)
                {
                    // Line is full, reset counter and try again
                    lines.Remove(rndLine);
                    continue;
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
                //Thread.Sleep(2);
            }
        }
    }
}
