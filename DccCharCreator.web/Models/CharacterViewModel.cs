using DccCharCreator.core.CharacterData;

namespace DccCharCreator.web.Models
{
    public class CharacterViewModel
    {
        public Charakter[] Characters { get; set; }

        public int Seed { get; set; }
    }
}