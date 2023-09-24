using BepInEx.Logging;
using Humans.Utilities;
using Newtonsoft.Json;
using System.Reflection;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Humans
{
    public class HumanPresets
    {
        private HumanPresets() { }
        private static HumanPresets _instance;
        public static HumanPresets Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HumanPresets();

                return _instance;
            }
        }

        public List<SkinColor> SkinColors;
        public List<string> HairStyles;
        public List<string> Helmets;
        public List<HairColor> HairColors;
        public List<string> Eyes;
        public List<string> FacialHairs;
        public List<string> FaceDecorations;
        public List<int> VoiceSelection;
        public List<string> Bodies;
        public List<Head> Heads;
        public List<string> FacePaints;

        private static ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.HumanPresets");
        private readonly string _baseDataPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data");
        private string _skinColorsPath => Path.Combine(_baseDataPath, "skin_color_presets.json");
        private string _hairStylesPath => Path.Combine(_baseDataPath, "hair_style_presets.json");
        private string _helmetsPath => Path.Combine(_baseDataPath, "helmet_presets.json");
        private string _eyesPath => Path.Combine(_baseDataPath, "eyes_presets.json");
        private string _hairColorsPath => Path.Combine(_baseDataPath, "hair_color_presets.json");
        private string _faceDecorationsPath => Path.Combine(_baseDataPath, "face_decoration_presets.json");
        private string _facialHairPath => Path.Combine(_baseDataPath, "facial_hair_presets.json");
        private string _bodiesPath => Path.Combine(_baseDataPath, "body_presets.json");
        private string _headsPath => Path.Combine(_baseDataPath, "head_presets.json");
        private string _facePaintsPath => Path.Combine(_baseDataPath, "face_paint_presets.json");

        public void Initialize()
        {
            SkinColors = Utility.LoadPresets<List<SkinColor>>(_skinColorsPath);
            HairStyles = Utility.LoadPresets<List<string>>(_hairStylesPath);
            Helmets = Utility.LoadPresets<List<string>>(_helmetsPath);
            HairColors = Utility.LoadPresets<List<HairColor>>(_hairColorsPath);
            Eyes = Utility.LoadPresets<List<string>>(_eyesPath);
            FacialHairs = Utility.LoadPresets<List<string>>(_facialHairPath);
            FaceDecorations = Utility.LoadPresets<List<string>>(_faceDecorationsPath);
            VoiceSelection = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Bodies = Utility.LoadPresets<List<string>>(_bodiesPath);
            Heads = Utility.LoadPresets<List<Head>>(_headsPath);
            FacePaints = Utility.LoadPresets<List<string>>(_facePaintsPath);
        }

        public SkinColor GetRandomSkinColor(string skinType)
        {
            var skinsWithSkinType = SkinColors.FindAll(s => s.Type == skinType);

            if (skinsWithSkinType.Count == 0)
            {
                _logger.LogError($"Error retrieving skins for skin type '{skinType}'");
                return null;
            }

            return skinsWithSkinType[Random.Range(0, skinsWithSkinType.Count())];
        }

        public HairColor GetRandomHairColor()
        {
            int totalWeight = 0;
            foreach (var weight in HairColors.Select(hc => hc.Weight).ToList())
                totalWeight += weight;

            int randomValue = Random.Range(0, totalWeight + 1);

            foreach (var hc in HairColors)
            {
                randomValue -= hc.Weight;
                if (randomValue < 0)
                {
                    return hc;
                }
            }

            _logger.LogError("Error generating random skin color. Weights are not properly defined.");
            return null;
        }

        public void PreviousSkinColor(Human human)
        {
            var index = SkinColors.FindIndex(c => c == human.SkinColor);

            if (index == -1)
            {
                _logger.LogError($"Error retrieving SkinColor property {human.SkinColor?.Type ?? "n/a"} : {human.SkinColor?.Name ?? "n/a"} for kerbal ID {human.Id}.");
                return;
            }

            if (index > 0)
            {
                human.SkinColor = SkinColors[--index];
                new SkinColorAttribute().ApplyAttribute(human.KerbalInfo, human.SkinColor.Color);
                KerbalUtility.TakeKerbalPortrait(human.KerbalInfo);
            }
        }

        public void NextSkinColor(Human human)
        {
            var index = SkinColors.FindIndex(c => c == human.SkinColor);

            if (index == -1)
            {
                _logger.LogError($"Error retrieving SkinColor property {human.SkinColor?.Type ?? "n/a"} : {human.SkinColor?.Name ?? "n/a"} for kerbal ID {human.Id}.");
            }

            if (index < SkinColors.Count - 1)
            {
                human.SkinColor = SkinColors[++index];
                new SkinColorAttribute().ApplyAttribute(human.KerbalInfo, human.SkinColor.Color);
                KerbalUtility.TakeKerbalPortrait(human.KerbalInfo);
            }
        }
    }
}
