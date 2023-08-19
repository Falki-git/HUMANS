﻿using System.Reflection;
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
        public List<string> Heads = new();
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
            InitializeSkinColors();
            InitializeHairStyles();
            InitializeHelmets();
            InitializeHairColors();
            InitializeEyes();
            InitializeFacialHairs();
            InitializeFaceDecorations();
            InitializeVoiceSelection();
            InitializeBodies();
            InitializeHeads();
            InitializeFacePaints();
        }


        private void InitializeSkinColors()
        {
            SkinColors = Utility.LoadPresets<List<SkinColorPreset>>(_skinColorsPath);
        }

        private void InitializeHairStyles()
        {
            HairStyles = Utility.LoadPresets<List<string>>(_hairStylesPath);
        }

        private void InitializeHelmets()
        {
            Helmets = Utility.LoadPresets<List<string>>(_helmetsPath);
        }

        private void InitializeHairColors()
        {
            HairColors = Utility.LoadPresets<List<HairColorPreset>>(_hairColorsPath);
        }

        private void InitializeEyes()
        {
            Eyes = Utility.LoadPresets<List<EyesPreset>>(_eyesPath);
        }

        private void InitializeFacialHairs()
        {
            FacialHairs = Utility.LoadPresets<List<string>>(_facialHairPath);
        }

        private void InitializeFaceDecorations()
        {
            FaceDecorations = Utility.LoadPresets<List<string>>(_faceDecorationsPath);
        }

        private void InitializeVoiceSelection()
        {
            VoiceSelection = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }

        private void InitializeBodies()
        {
            Bodies = Utility.LoadPresets<List<string>>(_bodiesPath);
        }

        private void InitializeHeads()
        {
            Heads.Add("HEAD_F_01");
            Heads.Add("HEAD_M_01");
        }

        private void InitializeFacePaints()
        {
            FacePaints.Add("FRECKLES");
            FacePaints.Add("DARKENED UNDER-EYE");
            FacePaints.Add("LIGHT MAKEUP");
            FacePaints.Add("HEAVY MAKEUP");
            FacePaints.Add("MIME");
            FacePaints.Add("CLOWN");
            FacePaints.Add("EYE SCAR");
            FacePaints.Add("WHISKER MARKS");
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


}
