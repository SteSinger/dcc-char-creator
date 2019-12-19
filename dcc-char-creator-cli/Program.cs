using DccCharCreator.core.CharacterData;
using DccCharCreator.core.Würfel;
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
            var würfel = new WürfelFactory(new Random());
            var berufWürfel = würfel.W100;
            var zeichenWürfel = würfel.W30;
            var attributWürfel = würfel._3W6;
            var trefferWürfel = würfel.W4;
            var handelsWarenWürfel = würfel.W24;
            var geldWürfel = würfel._5W12;

            do
            {
                var c = new Character(attributWürfel, berufWürfel, zeichenWürfel, trefferWürfel, geldWürfel, handelsWarenWürfel);
                Console.WriteLine(c);
            } while (Console.ReadKey().Key != ConsoleKey.Q);
        }
    }
}
