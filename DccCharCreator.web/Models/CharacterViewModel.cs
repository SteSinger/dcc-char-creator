using DccCharCreator.core.CharacterData;

namespace DccCharCreator.web.Models
{
    public class CharacterViewModel
    {
        public Character[] Characters { get; set; }

        public int Seed { get; set; }
    }
}