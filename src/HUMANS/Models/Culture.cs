namespace Humans
{
    public class Culture
    {
        public string Name { get; set; }
        public string PicturePath { get; set; }
        public Dictionary<string, int> NationalityWeights { get; set; }
        public override string ToString() => Name;
    }
}
