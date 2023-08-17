using Newtonsoft.Json;
using UnityEngine;

namespace Humans
{
    //[JsonObject(MemberSerialization.OptIn)]
    public class Culture
    {
        //[JsonProperty]
        public CultureName Name { get; set; } // American, European, Russian, East Asia, India...
        public string PicturePath { get; set; }
        public Dictionary<NationName, int> NationalityWeights { get; set; }
    }

    public class Nation
    {
        public NationName Name { get; set; } // USA, Germany, France...
        public string FlagPath { get; set; }
        //public List<FirstName> FirstNames { get; set; }
        public List<string> FemaleFirstNames { get; set; }
        public List<string> MaleFirstNames { get; set; }
        public List<string> LastNames { get; set; }

        public override string ToString()
        {
            return Name.ToString();
        }
    }

    /*
    public struct FirstName
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
    }
    */

}
