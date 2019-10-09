using DccCharCreator.core;
using DccCharCreator.core.Dice;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace dcc_char_creator_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var berufWürfel = DiceFactory.W100;
            var zeichenWürfel = DiceFactory.W30;
            var attributWürfel = DiceFactory._3W6;
            var trefferWürfel = DiceFactory.W4;
            
            var geldWürfel = DiceFactory._5W12;

            while (Console.ReadKey().Key != ConsoleKey.Q)
            {
                var c = new Character(attributWürfel, berufWürfel, zeichenWürfel, trefferWürfel, geldWürfel);
                Console.WriteLine(c);
            }
        }
    }
}
