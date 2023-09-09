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
            Head = new HeadPreset { Name = KerbalUtility.Head, Gender = KerbalUtility.Head.Contains("_M_") ? Gender.Male : Gender.Female };
            Gender = Head.Gender;

            Nationality = culture.GetRandomNationality();

            var nation = CulturePresets.Instance.Nations.Find(n => n.Name == Nationality);

            FirstName = nation.GetRandomFirstName(Gender);
            Surname = nation.GetRandomLastName();
            KerbalType = KerbalUtility.KerbalType;
            NameKey = KerbalUtility.NameKey;

            var skinType = culture.GetRandomSkinColorType();
            SkinColor = HumanPresets.Instance.GetRandomSkinColor(skinType);
            HairColor = HumanPresets.Instance.GetRandomHairColor();
            HairStyle = KerbalUtility.HairStyle;
            Helmet = KerbalUtility.Helmet;
            Eyes = KerbalUtility.Eyes;
            EyeHeight = KerbalUtility.EyeHeight;
            EyeSymmetry = KerbalUtility.EyeSymmetry;
            FacialHair = KerbalUtility.FacialHair;
            FaceDecoration = KerbalUtility.FaceDecoration;
            TeamColor1 = KerbalUtility.TeamColor1;
            TeamColor2 = KerbalUtility.TeamColor2;
            Stupidity = KerbalUtility.Stupidity;
            Bravery = KerbalUtility.Bravery;
            Constitution = KerbalUtility.Constitution;
            Optimism = KerbalUtility.Optimism;
            IsVeteran = KerbalUtility.IsVeteran;
            VoiceSelection = KerbalUtility.VoiceSelection;
            VoiceType = KerbalUtility.VoiceType;
            Body = KerbalUtility.Body;
            FacePaint = KerbalUtility.FacePaint;
            Radiation = KerbalUtility.Radiation;
            Happiness = KerbalUtility.Happiness;
            Experience = KerbalUtility.Experience;
            Biography = KerbalUtility.Biography;

            //HumanType = KerbalUtility.HumanType;


            // TODO

            Humanize();
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
        [JsonProperty]
        public KerbalType KerbalType { get; set; }
        [JsonProperty]
        public string HairStyle { get; set; }
        [JsonProperty]
        public string Helmet { get; set; }
        [JsonProperty]
        public HairColorPreset HairColor { get; set; }
        [JsonProperty]
        public string Eyes { get; set; }
        [JsonProperty]
        public Single EyeHeight { get; set; }
        [JsonProperty]
        public Single EyeSymmetry { get; set; }
        [JsonProperty]
        public SkinColorPreset SkinColor { get; set; }

        [JsonProperty]
        public string FacialHair { get; set; }
        [JsonProperty]
        public string FaceDecoration { get; set; }
        [JsonProperty]
        public Color TeamColor1 { get; set; }
        [JsonProperty]
        public Color TeamColor2 { get; set; }
        [JsonProperty]
        public Single Stupidity { get; set; }
        [JsonProperty]
        public Single Bravery { get; set; }
        [JsonProperty]
        public Single Constitution { get; set; }
        [JsonProperty]
        public Single Optimism { get; set; }


        [JsonProperty]
        public bool IsVeteran { get; set; }
        [JsonProperty]
        public int VoiceSelection { get; set; }
        [JsonProperty]
        public Single VoiceType { get; set; }
        [JsonProperty]
        public string Body { get; set; }
        [JsonProperty]
        public HeadPreset Head { get; set; }
        [JsonProperty]
        public string FacePaint { get; set; }
        [JsonProperty]        
        public Single Radiation { get; set; }
        [JsonProperty]
        public Single Happiness { get; set; }
        [JsonProperty]
        public int Experience { get; set; }
        [JsonProperty]
        public string Biography { get; set; }



        /// <summary>
        /// If kerbal with this ID has been initialized with human parameters
        /// </summary>
        public bool IsHumanized { get; set; }

        public void Humanize() => Humanize(KerbalInfo);
        public void Humanize(KerbalInfo kerbal)
        {
            
            KerbalUtility.SetKerbal(kerbal);

            var firstNameAttribute = new FirstNameAttribute();
            firstNameAttribute.ApplyAttribute(kerbal, FirstName);

            var surnameAttribute = new SurnameAttribute();
            surnameAttribute.ApplyAttribute(kerbal, Surname);

            var skinColorAttribute = new SkinColorAttribute();
            skinColorAttribute.ApplyAttribute(kerbal, (Color)SkinColor.Color);

            var hairColorAttribute = new HairColorAttribute();
            hairColorAttribute.ApplyAttribute(kerbal, HairColor.Color);

            //KerbalUtility.FullName = $"{FirstName} {Surname}";
            kerbal._kerbalAttributes._fullName = KerbalUtility.FullName; // = kerbal.Attributes.GetFullName();

            //TODO other attributes
        }
    }
}
