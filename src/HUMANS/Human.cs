using KSP.Game;
using KSP.Sim;
using KSP.Sim.impl;
using UnityEngine;

namespace Humans
{
    public class Human
    {
        public IGGuid Id { get; set; }
        public KerbalInfo KerbalInfo { get; set; }
        public Gender Gender { get; set; }
        public NationName Nationality { get; set; }


        public string Name { get; set; }
        public string Surname { get; set; }
        public KerbalType HumanType { get; set; }
        public string HairStyle { get; set; }
        public string Helmet { get; set; }
        public string HairColor { get; set; }
        public string Eyes { get; set; }
        public Single EyeHeight { get; set; }
        public Single EyeSymmetry { get; set; }
        public Color SkinColor { get; set; }
        public string FacialHair { get; set; }
        public string FaceDecoration { get; set; }
        public Color TeamColor1 { get; set; }
        public Color TeamColor2 { get; set; }
        public Single Stupidity { get; set; }
        public Single Bravery { get; set; }
        public Single Constitution { get; set; }
        public Single Optimism { get; set; }
        public bool IsVeteran { get; set; }
        public int VoiceSelection { get; set; }
        public Single VoiceType { get; set; }
        public string Body { get; set; }
        public string Head { get; set; }
        public string FacePaint { get; set; }
        public Single Radiation { get; set; }
        public Single Happiness { get; set; }
        public int Experience { get; set; }
        public string Biography { get; set; }



        /// <summary>
        /// If kerbal with this ID has been initialized with human parameters
        /// </summary>
        public bool IsHumanized { get; set; }

        public void Humanize()
        {
            // TODO Humanize default kerbals - assign them a skin color, hair color, names, etc.
        }
    }
}
