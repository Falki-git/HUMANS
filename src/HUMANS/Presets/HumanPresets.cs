using System.Reflection;
using UnityEngine;

namespace Humans
{
    public class HumanPresets
    {
        private static HumanPresets _instance;

        public List<SkinColorPreset> SkinColors = new();
        public List<string> HairStyles = new();
        public List<string> Helmets = new();
        public List<HairColorPreset> HairColors = new();
        public List<EyesPreset> Eyes = new();
        public List<string> FacialHairs = new();
        public List<string> FaceDecorations = new();
        public List<int> VoiceSelection = new();
        public List<string> Bodies = new();
        public List<HeadPreset> Heads = new();
        public List<string> FacePaints = new();

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


        private HumanPresets() { }

        public static HumanPresets Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HumanPresets();

                return _instance;
            }
        }

        public void Initialize()
        {
            SkinColors = Utility.LoadPresets<List<SkinColorPreset>>(_skinColorsPath);
            HairStyles = Utility.LoadPresets<List<string>>(_hairStylesPath);
            Helmets = Utility.LoadPresets<List<string>>(_helmetsPath);
            HairColors = Utility.LoadPresets<List<HairColorPreset>>(_hairColorsPath);
            Eyes = Utility.LoadPresets<List<EyesPreset>>(_eyesPath);
            FacialHairs = Utility.LoadPresets<List<string>>(_facialHairPath);
            FaceDecorations = Utility.LoadPresets<List<string>>(_faceDecorationsPath);
            VoiceSelection = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Bodies = Utility.LoadPresets<List<string>>(_bodiesPath);
            Heads = Utility.LoadPresets<List<HeadPreset>>(_headsPath);
            FacePaints = Utility.LoadPresets<List<string>>(_facePaintsPath);
        }
    }

    public class SkinColorPreset
    {
        public string Type;
        public string Name;
        public Color32 Color;
    }

    public class HairColorPreset
    {
        public HairColorType Type;
        public string Name;
        public Color32 Color;
        public int Weight;
    }

    public class EyesPreset
    {
        public Gender Gender;
        public string Name;
    }

    public class HeadPreset
    {
        public Gender Gender;
        public string Name;
    }



}
