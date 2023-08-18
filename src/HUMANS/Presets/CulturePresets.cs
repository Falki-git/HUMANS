using System.Reflection;

namespace Humans
{
    public class CulturePresets
    {
        private CulturePresets() { }

        public List<Culture> Cultures = new();
        public List<Nation> Nations = new();

        private readonly string _culturesPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data", "culture_presets.json");
        private readonly string _nationsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data", "nation_presets.json");

        private static CulturePresets _instance;
        public static CulturePresets Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CulturePresets();

                return _instance;
            }
        }

        public void Initialize()
        {
            Cultures = Utility.LoadPresets<List<Culture>>(_culturesPath);
            Nations = Utility.LoadPresets<List<Nation>>(_nationsPath);

            //InitializeNations();
            //InitializeCultures();
        }

        /*
        private void InitializeNations()
        {
            Nations.Add(new Nation
            {
                Name = NationName.USA, //TODO loading of firstname and lastnames from an external files. In fact, load everything from an external json file
                FemaleFirstNames = new List<string> { "Anna", "Ellie"},
                MaleFirstNames = new List<string> { "Joel", "Peter" },
                LastNames = new List<string> { "Anderson", "Smith", "Ramsey", "Pascal" },
                FlagPath = "some path" //TODO nation flags
            });

            Nations.Add(new Nation
            {
                Name = NationName.Canada,
                FemaleFirstNames = new List<string> { "FirstName1", "FirstName2", "FirstName3", "FirstName4"},
                MaleFirstNames = new List<string> { "FirstName5", "FirstName6", "FirstName7", "FirstName8" },
                LastNames = new List<string> { "LastName1", "LastName2", "LastName3", "LastName4" },
                FlagPath = "some path"
            });
        }

        private void InitializeCultures()
        {
            Cultures.Add(new Culture
            {
                Name = CultureName.American,
                PicturePath = "some path", // TODO picture of a culture,
                NationalityWeights = new Dictionary<NationName, int>
                {
                    { Nations.Find(n => n.Name == NationName.USA).Name, 10 },
                    { Nations.Find(n => n.Name == NationName.Canada).Name, 1 }
                },
            });

            Cultures.Add(new Culture
            {
                Name = CultureName.European,
                PicturePath = "some path", // TODO picture of a culture,
                NationalityWeights = new Dictionary<NationName, int> { } //TODO European nations
            });

            Cultures.Add(new Culture
            {
                Name = CultureName.Asian,
                PicturePath = "some path", // TODO picture of a culture,
                NationalityWeights = new Dictionary<NationName, int> { } //TODO Asians nations
            });
        }
        */
    }
}
