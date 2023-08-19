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
        public List<string> Eyes = new();
        public List<string> FacialHairs = new();
        public List<string> FaceDecorations = new();
        public List<int> VoiceSelection = new();
        public List<string> Bodies = new();
        public List<string> Heads = new();
        public List<string> FacePaints = new();

        private readonly string _skinColorsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data", "skin_color_presets.json");

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
            HairStyles.Add("HAIR_F_01");
            HairStyles.Add("HAIR_F_02");
            HairStyles.Add("HAIR_F_03");
            HairStyles.Add("HAIR_F_04");
            HairStyles.Add("HAIR_F_05");
            HairStyles.Add("HAIR_F_06");
            HairStyles.Add("HAIR_F_07");
            HairStyles.Add("HAIR_F_08");
            HairStyles.Add("HAIR_F_09");
            HairStyles.Add("HAIR_F_10");
            HairStyles.Add("HAIR_M_01");
            HairStyles.Add("HAIR_M_02");
            HairStyles.Add("HAIR_M_03");
            HairStyles.Add("HAIR_M_04");
            HairStyles.Add("HAIR_M_05");
            HairStyles.Add("HAIR_M_06");
            HairStyles.Add("HAIR_M_07");
            HairStyles.Add("HAIR_M_08");
            HairStyles.Add("HAIR_M_09");
            HairStyles.Add("HAIR_M_10");
        }

        private void InitializeHelmets()
        {
            Helmets.Add("HELM_SPACESUIT_01");
        }

        private void InitializeHairColors()
        {
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Dark Brown",
                Color = new(85, 72, 63, 255),
                Weight = 3
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Black",
                Color = new(22, 21, 20, 255),
                Weight = 3
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Dark Blue",
                Color = new(38, 41, 58, 255),
                Weight = 3
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Dark Red",
                Color = new(70, 37, 35, 255),
                Weight = 3
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Dark Green",
                Color = new(70, 66, 35, 255),
                Weight = 3
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Orange",
                Color = new(243, 141, 40, 255),
                Weight = 1
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Gray",
                Color = new(204, 204, 204, 255),
                Weight = 1
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Brown",
                Color = new(132, 80, 38, 255),
                Weight = 1
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Golden",
                Color = new(219, 213, 148, 255),
                Weight = 1
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "White",
                Color = new(247, 247, 247, 255),
                Weight = 1
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Ultramarine",
                Color = new(0, 60, 125, 255),
                Weight = 1
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Sepia",
                Color = new(255, 188, 115, 255),
                Weight = 1
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Light Brown",
                Color = new(152, 124, 104, 255),
                Weight = 1
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Dijon",
                Color = new(183, 171, 101, 255),
                Weight = 1
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Light Orange",
                Color = new(233, 159, 65, 255),
                Weight = 1
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Blond",
                Color = new(255, 211, 122, 255),
                Weight = 1
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Graying blond",
                Color = new(152, 134, 98, 255),
                Weight = 1
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Canary Blond",
                Color = new(255, 196, 79, 255),
                Weight = 1
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Dirty Blond",
                Color = new(195, 168, 115, 255),
                Weight = 1
            });
            HairColors.Add(new HairColorPreset()
            {
                Type = HairColorType.Kerbal,
                Name = "Light Blond",
                Color = new(233, 200, 134, 255),
                Weight = 1
            });
        }

        private void InitializeEyes()
        {
            Eyes.Add("EYES_F_01");
            Eyes.Add("EYES_M_01");
        }

        private void InitializeFacialHairs()
        {
            FacialHairs.Add("TIMC_FACIALHAIR");
        }

        private void InitializeFaceDecorations()
        {
            FaceDecorations.Add("DECO_FACE_GLASSES_AVIATORS_01");
            FaceDecorations.Add("DECO_FACE_GLASSES_CATEYE_01");
            FaceDecorations.Add("DECO_FACE_GLASSES_CIRCULAR_01");
            FaceDecorations.Add("DECO_FACE_GLASSES_NASA60S_01");
            FaceDecorations.Add("DECO_FACE_GLASSES_NERDY_01");
            FaceDecorations.Add("DECO_FACE_GLASSES_RIMLESS_01");
            FaceDecorations.Add("DECO_FACE_GLASSES_WIDE_01");
            FaceDecorations.Add("DECO_FACE_GLASSES_WIDE_02");
            FaceDecorations.Add("DECO_FACE_SAFETY_01");
            FaceDecorations.Add("DECO_FACE_SAFETY_02");
            FaceDecorations.Add("DECO_FACE_SAFETY_03");
        }

        private void InitializeVoiceSelection()
        {
            VoiceSelection = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        }

        private void InitializeBodies()
        {
            Bodies.Add("BODY_SPACESUIT_01");
            Bodies.Add("BODY_GROUNDCREW_01");
            Bodies.Add("BODY_JUMPSUIT_01");
            Bodies.Add("BODY_JUMPSUIT_02");
            Bodies.Add("BODY_SCIENTIST_01");
            Bodies.Add("BODY_WORKER_01");
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


}
