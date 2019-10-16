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
            var berufWürfel = WürfelFactory.W100;
            var zeichenWürfel = WürfelFactory.W30;
            var attributWürfel = WürfelFactory._3W6;
            var trefferWürfel = WürfelFactory.W4;
            var handelsWarenWürfel = WürfelFactory.W24;
            var geldWürfel = WürfelFactory._5W12;

            do
            {
                var c = new Character(attributWürfel, berufWürfel, zeichenWürfel, trefferWürfel, geldWürfel, handelsWarenWürfel);
                Console.WriteLine(c);
            } while (Console.ReadKey().Key != ConsoleKey.Q);
        }
    }
}
