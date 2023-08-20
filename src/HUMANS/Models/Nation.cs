using UnityEngine;

namespace Humans
{
    public class Nation
    {
        public string Name { get; set; }
        public string FlagPath { get; set; }
        public List<string> FemaleFirstNames { get; set; }
        public List<string> MaleFirstNames { get; set; }
        public List<string> LastNames { get; set; }
        public override string ToString() => Name;

        public string GetRandomFirstName(Gender gender)
        {
            if (gender == Gender.Male || gender == Gender.Other)
            {
                return MaleFirstNames[UnityEngine.Random.Range(0, MaleFirstNames.Count)];
            }
            else
            {
                return FemaleFirstNames[UnityEngine.Random.Range(0, FemaleFirstNames.Count)];
            }
        }

        public string GetRandomLastName()
        {
            return LastNames[UnityEngine.Random.Range(0, LastNames.Count)];
        }
    }
}
