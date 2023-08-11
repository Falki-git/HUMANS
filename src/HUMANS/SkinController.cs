using UnityEngine;

namespace Humans
{
    public class SkinController
    {
        private static SkinController _instance;

        public List<HumanSkinColor> Presets = new();

        public SkinController()
        {
            InitializeColors();
        }

        public static SkinController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SkinController();

                return _instance;
            }
        }
        private void InitializeColors()
        {
            ///// REAL /////

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Real,
                Name = "Russet",
                Color = new(141, 85, 36, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Real,
                Name = "Peru",
                Color = new(198, 134, 66, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Real,
                Name = "Fawn",
                Color = new(224, 172, 105, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Real,
                Name = "Mellow Apricot",
                Color = new(241, 194, 125, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Real,
                Name = "Navajo White",
                Color = new(255, 219, 172, 255)
            });

            ///// FAIR /////
            /*
            Presets.Add(new SkinColor() // REMOVE
            {
                Type = SkinType.Fair,
                Name = "#f2efee",
                Color = new(242,239,238, 255)
            });

            Presets.Add(new SkinColor() // REMOVE
            {
                Type = SkinType.Fair,
                Name = "#efe6dd",
                Color = new(239, 230, 221, 255)
            });
            */

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Fair,
                Name = "#e8d3c5",
                Color = new(232, 211, 197, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Fair,
                Name = "#d7b6a5",
                Color = new(215, 182, 165, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Fair,
                Name = "#9f7967",
                Color = new(159, 121, 103, 255)
            });


            ///// DARK /////

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Dark,
                Name = "#70361c",
                Color = new(112, 54, 28, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Dark,
                Name = "#714937",
                Color = new(113, 73, 55, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Dark,
                Name = "#65371e",
                Color = new(101, 55, 30, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Dark,
                Name = "#492816",
                Color = new(73, 40, 22, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Dark,
                Name = "#321b0f",
                Color = new(50, 27, 15, 255)
            });


            ///// CAUCASIAN /////

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Caucasian,
                Name = "#ffcd94",
                Color = new(255, 205, 148, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Caucasian,
                Name = "#ffe0bd",
                Color = new(255, 224, 189, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Caucasian,
                Name = "#eac086",
                Color = new(234, 192, 134, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Caucasian,
                Name = "#ffe39f",
                Color = new(255, 227, 159, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Caucasian,
                Name = "#ffad60",
                Color = new(255, 173, 96, 255)
            });


            ///// INDIAN /////

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Indian,
                Name = "#bf9169",
                Color = new(191, 145, 105, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Indian,
                Name = "#8c644d",
                Color = new(140, 100, 77, 255)
            });

            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Indian,
                Name = "#593123",
                Color = new(89, 49, 35, 255)
            });

            /////////////////////////////////////
            //// https://www.schemecolor.com ////
            /////////////////////////////////////

            // https://www.schemecolor.com/beautiful-touch.php
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.BeautifulTouch,
                Name = "Café Au Lait",
                Color = new(179, 121, 89, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.BeautifulTouch,
                Name = "Antique Brass",
                Color = new(212, 152, 120, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.BeautifulTouch,
                Name = "Tumbleweed",
                Color = new(232, 175, 149, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.BeautifulTouch,
                Name = "Desert Sand",
                Color = new(235, 191, 174, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.BeautifulTouch,
                Name = "Champagne Pink",
                Color = new(242, 214, 204, 255)
            });

            // https://www.schemecolor.com/ebony-skin.php
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Ebony,
                Name = "Royal Brown",
                Color = new(84, 55, 52, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Ebony,
                Name = "Rose Ebony",
                Color = new(102, 70, 63, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Ebony,
                Name = "Quincy",
                Color = new(112, 83, 77, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Ebony,
                Name = "Deep Taupe",
                Color = new(125, 99, 91, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Ebony,
                Name = "Shadow",
                Color = new(138, 112, 106, 255)
            });

            // https://www.schemecolor.com/normal-and-tan-skin-colors.php
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.NormalAndTan,
                Name = "Unbleached Silk",
                Color = new(248, 224, 200, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.NormalAndTan,
                Name = "Crayola's Tan",
                Color = new(213, 156, 110, 255)
            });

            // https://www.schemecolor.com/natural-skin-tones.php
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Natural,
                Name = "Fawn",
                Color = new(219, 169, 116, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Natural,
                Name = "Crayola's Gold",
                Color = new(227, 192, 139, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Natural,
                Name = "Dutch White",
                Color = new(241, 217, 183, 255)
            });
            /*
            Presets.Add(new SkinColor()
            {
                Type = SkinType.Natural,
                Name = "Lemon Meringue",
                Color = new(246, 229, 196, 255)
            });
            */

            // https://www.schemecolor.com/asian-skin.php
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Asian,
                Name = "Pale Pink",
                Color = new(249, 222, 215, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Asian,
                Name = "Baby Pink",
                Color = new(236, 201, 189, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Asian,
                Name = "Pastel Pink",
                Color = new(232, 179, 162, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Asian,
                Name = "Desert Sand",
                Color = new(240, 194, 170, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Asian,
                Name = "Dutch White",
                Color = new(240, 211, 192, 255)
            });

            // https://www.schemecolor.com/black-guy-skin-colors.php
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Black,
                Name = "Root Beer",
                Color = new(38, 7, 1, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Black,
                Name = "Black Bean",
                Color = new(61, 12, 2, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Black,
                Name = "Burnt Umber",
                Color = new(132, 55, 34, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Black,
                Name = "Brown Sugar",
                Color = new(175, 110, 81, 255)
            });
            Presets.Add(new HumanSkinColor()
            {
                Type = SkinType.Black,
                Name = "Antique Brass",
                Color = new(198, 144, 118, 255)
            });

        }
    }

    public class HumanSkinColor
    {
        public SkinType Type;
        public string Name;
        public Color32 Color;
    }
}
