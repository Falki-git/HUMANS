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

        public string GetRandomFirstName(Gender? gender)
        {

            if (gender == null || gender == Gender.Male || gender == Gender.Other)
            {
                if (MaleFirstNames == null || MaleFirstNames.Count == 0)
                    return string.Empty;

                return MaleFirstNames[UnityEngine.Random.Range(0, MaleFirstNames.Count)];
            }
            else
            {
                if (FemaleFirstNames == null || FemaleFirstNames.Count == 0)
                    return string.Empty;

                return FemaleFirstNames[UnityEngine.Random.Range(0, FemaleFirstNames.Count)];
            }
        }

        public string GetRandomLastName()
        {
            if (LastNames == null || LastNames.Count == 0)
                return string.Empty;

            return LastNames[UnityEngine.Random.Range(0, LastNames.Count)];
        }
    }
}
