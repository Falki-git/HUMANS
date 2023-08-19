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
            ///// REAL /////
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Real,
                Name = "Russet",
                Color = new(141, 85, 36, 255)
            });

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Real,
                Name = "Peru",
                Color = new(198, 134, 66, 255)
            });

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Real,
                Name = "Fawn",
                Color = new(224, 172, 105, 255)
            });

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Real,
                Name = "Mellow Apricot",
                Color = new(241, 194, 125, 255)
            });

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Real,
                Name = "Navajo White",
                Color = new(255, 219, 172, 255)
            });

            ///// FAIR /////
            /*
            HumanPresets.Add(new SkinColor() // REMOVE
            {
                Type = SkinType.Fair,
                Name = "#f2efee",
                Color = new(242,239,238, 255)
            });

            HumanPresets.Add(new SkinColor() // REMOVE
            {
                Type = SkinType.Fair,
                Name = "#efe6dd",
                Color = new(239, 230, 221, 255)
            });
            */

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Fair,
                Name = "#e8d3c5",
                Color = new(232, 211, 197, 255)
            });

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Fair,
                Name = "#d7b6a5",
                Color = new(215, 182, 165, 255)
            });

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Fair,
                Name = "#9f7967",
                Color = new(159, 121, 103, 255)
            });


            ///// DARK /////

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Dark,
                Name = "#70361c",
                Color = new(112, 54, 28, 255)
            });

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Dark,
                Name = "#714937",
                Color = new(113, 73, 55, 255)
            });

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Dark,
                Name = "#65371e",
                Color = new(101, 55, 30, 255)
            });

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Dark,
                Name = "#492816",
                Color = new(73, 40, 22, 255)
            });

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Dark,
                Name = "#321b0f",
                Color = new(50, 27, 15, 255)
            });


            ///// CAUCASIAN /////

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Caucasian,
                Name = "#ffcd94",
                Color = new(255, 205, 148, 255)
            });
            /*
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Caucasian,
                Name = "#ffe0bd",
                Color = new(255, 224, 189, 255)
            });
            */

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Caucasian,
                Name = "#eac086",
                Color = new(234, 192, 134, 255)
            });

            /*
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Caucasian,
                Name = "#ffe39f",
                Color = new(255, 227, 159, 255)
            });
            */

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Caucasian,
                Name = "#ffad60",
                Color = new(255, 173, 96, 255)
            });


            ///// INDIAN /////

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Indian,
                Name = "#bf9169",
                Color = new(191, 145, 105, 255)
            });

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Indian,
                Name = "#8c644d",
                Color = new(140, 100, 77, 255)
            });

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Indian,
                Name = "#593123",
                Color = new(89, 49, 35, 255)
            });

            /////////////////////////////////////
            //// https://www.schemecolor.com ////
            /////////////////////////////////////

            // https://www.schemecolor.com/beautiful-touch.php
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.BeautifulTouch,
                Name = "Café Au Lait",
                Color = new(179, 121, 89, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.BeautifulTouch,
                Name = "Antique Brass",
                Color = new(212, 152, 120, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.BeautifulTouch,
                Name = "Tumbleweed",
                Color = new(232, 175, 149, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.BeautifulTouch,
                Name = "Desert Sand",
                Color = new(235, 191, 174, 255)
            });
            /*
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.BeautifulTouch,
                Name = "Champagne Pink",
                Color = new(242, 214, 204, 255)
            });
            */

            // https://www.schemecolor.com/ebony-skin.php
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Ebony,
                Name = "Royal Brown",
                Color = new(84, 55, 52, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Ebony,
                Name = "Rose Ebony",
                Color = new(102, 70, 63, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Ebony,
                Name = "Quincy",
                Color = new(112, 83, 77, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Ebony,
                Name = "Deep Taupe",
                Color = new(125, 99, 91, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Ebony,
                Name = "Shadow",
                Color = new(138, 112, 106, 255)
            });

            // https://www.schemecolor.com/normal-and-tan-skin-colors.php
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.NormalAndTan,
                Name = "Unbleached Silk",
                Color = new(248, 224, 200, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.NormalAndTan,
                Name = "Crayola's Tan",
                Color = new(213, 156, 110, 255)
            });

            // https://www.schemecolor.com/natural-skin-tones.php
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Natural,
                Name = "Fawn",
                Color = new(219, 169, 116, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Natural,
                Name = "Crayola's Gold",
                Color = new(227, 192, 139, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Natural,
                Name = "Dutch White",
                Color = new(241, 217, 183, 255)
            });
            /*
            HumanPresets.Add(new SkinColor()
            {
                Type = SkinType.Natural,
                Name = "Lemon Meringue",
                Color = new(246, 229, 196, 255)
            });
            */

            // https://www.schemecolor.com/asian-skin.php
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Asian,
                Name = "Pale Pink",
                Color = new(249, 222, 215, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Asian,
                Name = "Baby Pink",
                Color = new(236, 201, 189, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Asian,
                Name = "Pastel Pink",
                Color = new(232, 179, 162, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Asian,
                Name = "Desert Sand",
                Color = new(240, 194, 170, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Asian,
                Name = "Dutch White",
                Color = new(240, 211, 192, 255)
            });

            // https://www.schemecolor.com/black-guy-skin-colors.php
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Black,
                Name = "Root Beer",
                Color = new(38, 7, 1, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Black,
                Name = "Black Bean",
                Color = new(61, 12, 2, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Black,
                Name = "Burnt Umber",
                Color = new(132, 55, 34, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Black,
                Name = "Brown Sugar",
                Color = new(175, 110, 81, 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Black,
                Name = "Antique Brass",
                Color = new(198, 144, 118, 255)
            });

            /////////////////////////////////////
            ////         STOCK KERBAL        ////
            /////////////////////////////////////

            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Kerbal,
                Name = "1",
                Color = new((byte)(0.7960784 * 255f), (byte)(0.854902 * 255f), (byte)(0.5058824 * 255f), 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Kerbal,
                Name = "2",
                Color = new((byte)(0.7294118 * 255f), (byte)(0.854902 * 255f), (byte)(0.3333333 * 255f), 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Kerbal,
                Name = "3",
                Color = new((byte)(0.5356948 * 255f), (byte)(0.6176471 * 255f), (byte)(0.2770329 * 255f), 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Kerbal,
                Name = "4",
                Color = new((byte)(0.4438164 * 255f), (byte)(0.6176471 * 255f), (byte)(0.2770329 * 255f), 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Kerbal,
                Name = "5",
                Color = new((byte)(0.5805274 * 255f), (byte)(0.7794118 * 255f), (byte)(0.3897059 * 255f), 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Kerbal,
                Name = "6",
                Color = new((byte)(0.6000865 * 255f), (byte)(0.8529412 * 255f), (byte)(0.3574827 * 255f), 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Kerbal,
                Name = "7",
                Color = new((byte)(0.5926172 * 255f), (byte)(0.6911765 * 255f), (byte)(0.3150952 * 255f), 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Kerbal,
                Name = "8",
                Color = new((byte)(0.7459081 * 255f), (byte)(0.8602941 * 255f), (byte)(0.4238214 * 255f), 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Kerbal,
                Name = "9",
                Color = new((byte)(0.4851151 * 255f), (byte)(0.5735294 * 255f), (byte)(0.2361592 * 255f), 255)
            });
            SkinColors.Add(new SkinColorPreset()
            {
                Type = SkinType.Kerbal,
                Name = "10",
                Color = new((byte)(0.6157909 * 255f), (byte)(0.7573529 * 255f), (byte)(0.2171821 * 255f), 255)
            });
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
        public SkinType Type;
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
