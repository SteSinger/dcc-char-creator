using DccCharCreator.core.CharacterData;
using DccCharCreator.core.Würfel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DccCharCreator.core
{
    public class CharacterFactory
    {
        private readonly Random _random;

        public CharacterFactory(Random random)
        {
            _random = random;
        }

        public CharacterData.Charakter Default()
        {
            var würfel = new WürfelFactory(_random);
            var berufWürfel = würfel.W100;
            var zeichenWürfel = würfel.W30;
            var attributWürfel = würfel._3W6;
            var trefferWürfel = würfel.W4;
            var handelsWarenWürfel = würfel.W24;
            var geldWürfel = würfel._5W12;

            return new CharacterData.Charakter(attributWürfel, berufWürfel, zeichenWürfel, trefferWürfel, geldWürfel, handelsWarenWürfel);
        }
    }
}
