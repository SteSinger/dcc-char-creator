using DccCharCreator.core.CharacterData;
using DccCharCreator.core.Würfel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DccCharCreator.core
{
    public static class CharacterFactory
    {
        public static Character Default()
        {
            var berufWürfel = WürfelFactory.W100;
            var zeichenWürfel = WürfelFactory.W30;
            var attributWürfel = WürfelFactory._3W6;
            var trefferWürfel = WürfelFactory.W4;
            var handelsWarenWürfel = WürfelFactory.W24;
            var geldWürfel = WürfelFactory._5W12;

            return new Character(attributWürfel, berufWürfel, zeichenWürfel, trefferWürfel, geldWürfel, handelsWarenWürfel);            
        }
    }
}
