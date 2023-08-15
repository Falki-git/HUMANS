using UnityEngine;

namespace Humans
{
    public class Culture
    {
        public CultureName Name; // American, European, Russian, East Asia, India...
        public Texture2D Picture;
        public Dictionary<Nation, int> NationalityWeights;
    }

    public class Nation
    {
        public NationName Name; // USA, Germany, France...
        public Texture2D Flag;
        public List<string> FirstNames;
        public List<string> LastNames;
    }

}
