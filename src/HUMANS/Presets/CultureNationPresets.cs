﻿using BepInEx.Logging;
using KSP.Game;
using SpaceWarp.API.Assets;
using System.Reflection;
using UnityEngine;

namespace Humans
{
    public class CultureNationPresets
    {
        public const string KERBALNATION = "Kerbal";
        public const string KERBALCULTURE = "Kerbalkind";

        public List<Culture> Cultures = new();
        public List<Nation> Nations = new();

        private readonly string _culturesPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data", "culture_presets.json");
        private readonly string _nationsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data", "nation_presets.json");
        private static CultureNationPresets _instance;
        private readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.CulturePresets");

        private CultureNationPresets() { }
        public static CultureNationPresets Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CultureNationPresets();

                return _instance;
            }
        }

        public void Initialize()
        {
            Cultures = Utility.LoadPresets<List<Culture>>(_culturesPath);
            _logger.LogInfo($"Number of Culture presets: {Cultures.Count}");
            Nations = Utility.LoadPresets<List<Nation>>(_nationsPath);
            _logger.LogInfo($"Number of Nation presets: {Nations.Count}");

            // TODO cleaner way of doing this
            string flagPath;
            foreach (var nation in Nations)
            {
                try
                {
                    flagPath = $"{HumansPlugin.Instance.GUID}/images/{nation.FlagPath}";
                    nation.Flag = AssetManager.GetAsset<Texture2D>(flagPath);
                    _logger.LogInfo($"Successfully loaded {nation.Name}'s flag.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error loading {nation.Name}'s flag from path \"{HumansPlugin.Instance.GUID}/images/{nation.FlagPath}\"\n" + ex);
                }
            }
            _logger.LogInfo($"Number of Nations with flags: {Nations.Where(n => n.Flag != null).Count()}");

            string picturePath;
            foreach (var culture in Cultures)
            {
                try
                {
                    picturePath = $"{HumansPlugin.Instance.GUID}/images/{culture.PicturePath}";
                    culture.Picture = AssetManager.GetAsset<Texture2D>(picturePath);
                    _logger.LogInfo($"Successfully loaded {culture.Name} picture.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error loading {culture.Name} picture from path \"{HumansPlugin.Instance.GUID}/images/{culture.PicturePath}\"\n" + ex);
                }
            }

            InitializeKerbalCulture();
        }

        private void InitializeKerbalCulture()
        {
            var kerbalNation = new Nation
            {
                Name = KERBALNATION,
            };

            Nations.Add(kerbalNation);

            var nationalityWeights = new Dictionary<string, int>
            {
                { KERBALNATION, 100 }
            };

            var skinColorWeights = new Dictionary<string, int>
            {
                { KERBALNATION, 100 }
            };

            var kerbalCulture = new Culture
            {
                Name = KERBALCULTURE,
                NationalityWeights = nationalityWeights,
                SkinColorTypeWeights = skinColorWeights
            };

            try
            {
                var picturePath = $"{HumansPlugin.Instance.GUID}/images/culture_pictures/{KERBALCULTURE}.png";
                kerbalCulture.Picture = AssetManager.GetAsset<Texture2D>(picturePath);
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.LogError($"Error loading Kerbalkind picture from path \"{HumansPlugin.Instance.GUID}/images/culture_pictures/{KERBALCULTURE}.png\"\n" + ex);
            }

            Cultures.Add(kerbalCulture);
        }

        public void LoadDefaultKerbalFlag()
        {
            GameManager.Instance.Assets.LoadByLabel("flag_AGY_Default", sprite =>
            {
                var kerbalNation = Nations.Find(n => n.Name == KERBALNATION);
                kerbalNation.Flag = sprite?.texture ?? new Texture2D(1, 1);
            }
            , delegate (IList<Sprite> _) { });
        }

        public void PreviousNation(Human human)
        {
            var index = Nations.FindIndex(n => n.Name == human.Nationality);

            if (index == -1)
            {
                _logger.LogError($"Error retrieving Nation property {human.Nationality} for kerbal ID {human.Id}.");
                return;
            }

            if (index > 0)
            {
                human.Nationality = Nations[--index].Name;
                Utility.SaveCampaigns();
            }
        }

        public void NextNation(Human human)
        {
            var index = Nations.FindIndex(n => n.Name == human.Nationality);

            if (index == -1)
            {
                _logger.LogError($"Error retrieving Nation property {human.Nationality} for kerbal ID {human.Id}.");
                return;
            }

            if (index < Nations.Count - 1)
            {
                human.Nationality = Nations[++index].Name;
                Utility.SaveCampaigns();
            }
        }
    }
}
