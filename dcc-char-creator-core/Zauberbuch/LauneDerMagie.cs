using System;
using System.Collections.Generic;
using System.Text;

namespace DccCharCreator.core.Zauberbuch
{
    public class LauneDerMagie
    {
        public int Wurf { get; set; }
        public string Beschreibung { get; set; } = string.Empty;

        private const string FileName = "launendermagie.xml";
        private static readonly Lazy<Dictionary<int, LauneDerMagie>> launen = new Lazy<Dictionary<int, LauneDerMagie>>(Load);
        public static LauneDerMagie Get(int wurf)
        {
            return launen.Value[wurf];
        }

        public static Dictionary<int, LauneDerMagie> Load()
        {
            return Serializer.Load<LauneDerMagie>(FileName, x => { }, x => x.Wurf);
        }
    }
}
