using Humans.Utilities;
using KSP.Game;
using KSP.Sim;
using KSP.Sim.impl;
using Newtonsoft.Json;
using UnityEngine;

namespace Humans
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Human
    {
        public Human() { }
        public Human(KerbalInfo kerbal, Culture culture)
        {
            KerbalUtility.SetKerbal(kerbal);

            Id = KerbalUtility.IGGuid;
            KerbalInfo = kerbal;

            // Male kerbals are recognized by having "*_M_*" for Head type and "*_M_*" for Eyes type
            // Female kerbals are recognized by having "*_F_*" for Head type and "*_F_*" for Eyes type
            if (KerbalUtility.Head.Contains("_M_"))
                Gender = Gender.Male;
            else
                Gender = Gender.Female;

            Nationality = culture.GetRandomNationality();

            var nation = CulturePresets.Instance.Nations.Find(n => n.Name == Nationality);

            FirstName = nation.GetRandomFirstName(Gender);
            var firstNameAttribute = new FirstNameAttribute();
            firstNameAttribute.ApplyAttribute(kerbal, FirstName);

            Surname = nation.GetRandomLastName();
            var surnameAttribute = new SurnameAttribute();
            surnameAttribute.ApplyAttribute(kerbal, Surname);

            var skinType = culture.GetRandomSkinColorType();
            SkinColor = HumanPresets.Instance.GetRandomSkinColor(skinType);
            var skinColorAttribute = new SkinColorAttribute();
            skinColorAttribute.ApplyAttribute(kerbal, (Color)SkinColor.Color);

            HairColor = HumanPresets.Instance.GetRandomHairColor();
            var hairColorAttribute = new HairColorAttribute();
            hairColorAttribute.ApplyAttribute(kerbal, HairColor.Color);

            //Utility.Roster._portraitRenderer.TakeKerbalPortrait(kerbal);

            /*
            NameKey = KerbalUtility.NameKey;
            FirstName = KerbalUtility.FirstName;
            Surname = KerbalUtility.Surname;
            //HumanType = KerbalUtility.HumanType;
            HairStyle = KerbalUtility.HairStyle;
            Helmet = KerbalUtility.Helmet;
            HairColor = KerbalUtility.HairColor;
            Eyes = KerbalUtility.Eyes;
            EyeHeight = KerbalUtility.EyeHeight;
            EyeSymmetry = KerbalUtility.EyeSymmetry;
            SkinColor = KerbalUtility.SkinColor;
            */

            // TODO

            //Head = KerbalUtility.Head;

        }

        [JsonProperty]
        public IGGuid Id { get; set; }
        public KerbalInfo KerbalInfo { get; set; }
        [JsonProperty]
        public Gender Gender { get; set; }
        [JsonProperty]
        public string Nationality { get; set; }


        [JsonProperty]
        public string NameKey { get; set; }
        [JsonProperty]
        public string FirstName { get; set; }
        [JsonProperty]
        public string Surname { get; set; }
        //public KerbalType HumanType { get; set; }
        [JsonProperty]
        public string HairStyle { get; set; }
        [JsonProperty]
        public string Helmet { get; set; }
        [JsonProperty]
        public HairColorPreset HairColor { get; set; }
        [JsonProperty]
        public EyesPreset Eyes { get; set; }
        [JsonProperty]
        public Single EyeHeight { get; set; }
        [JsonProperty]
        public Single EyeSymmetry { get; set; }
        [JsonProperty]
        public SkinColorPreset SkinColor { get; set; }


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
        public HeadPreset Head { get; set; }
        public string FacePaint { get; set; }
        public Single Radiation { get; set; }
        public Single Happiness { get; set; }
        public int Experience { get; set; }
        public string Biography { get; set; }



        /// <summary>
        /// If kerbal with this ID has been initialized with human parameters
        /// </summary>
        public bool IsHumanized { get; set; }

        public void Humanize(KerbalInfo kerbal)
        {
            var firstNameAttribute = new FirstNameAttribute();
            firstNameAttribute.ApplyAttribute(kerbal, FirstName);

            var surnameAttribute = new SurnameAttribute();
            surnameAttribute.ApplyAttribute(kerbal, Surname);

            var skinColorAttribute = new SkinColorAttribute();
            skinColorAttribute.ApplyAttribute(kerbal, (Color)SkinColor.Color);

            var hairColorAttribute = new HairColorAttribute();
            hairColorAttribute.ApplyAttribute(kerbal, HairColor.Color);

            //TODO other attributes
        }
    }
}
