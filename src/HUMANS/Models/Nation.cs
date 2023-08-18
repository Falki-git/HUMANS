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
    }
}
