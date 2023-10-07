using BepInEx.Logging;
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
        public Human(KerbalInfo kerbal)
        {
            KerbalUtility.SetKerbal(kerbal);
            Id = KerbalUtility.IGGuid;
        }

        [JsonProperty]
        public IGGuid Id { get; set; }
        public KerbalInfo KerbalInfo { get => Utility.AllKerbals.Find(k => k.Id == this.Id); }
        [JsonProperty]
        public string Nationality { get; set; }
        public Nation Nation { get => CultureNationPresets.Instance.Nations.Find(n => n.Name == Nationality); }
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
        public HairColor HairColor { get; set; }
        [JsonProperty]
        public string Eyes { get; set; }
        [JsonProperty]
        public Single EyeHeight { get; set; }
        [JsonProperty]
        public Single EyeSymmetry { get; set; }
        [JsonProperty]
        public SkinColor SkinColor { get; set; }
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
        public Head Head { get; set; }
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
        /// If kerbal with this ID has been initialized with custom attributes
        /// </summary>
        [JsonProperty]
        public bool HasCustomAttributesApplied { get; set; }

        private static ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.Human");

        public void InitializeUniversalAttributes()
        {
            KerbalUtility.SetKerbal(KerbalInfo);

            // Male kerbals are recognized by having "*_M_*" for Head type and "*_M_*" for Eyes type
            // Female kerbals are recognized by having "*_F_*" for Head type and "*_F_*" for Eyes type
            Head = new Head { Name = KerbalUtility.Head, Gender = KerbalUtility.Head.Contains("_M_") ? Gender.Male : Gender.Female };

            KerbalType = KerbalUtility.KerbalType;
            NameKey = KerbalUtility.NameKey;

            // list presets
            // +Head
            HairStyle = KerbalUtility.HairStyle;
            Helmet = KerbalUtility.Helmet;
            Eyes = KerbalUtility.Eyes;
            FacialHair = KerbalUtility.FacialHair;
            FaceDecoration = KerbalUtility.FaceDecoration;
            VoiceSelection = KerbalUtility.VoiceSelection;
            Body = KerbalUtility.Body;
            FacePaint = KerbalUtility.FacePaint;

            // Single 0 - 1 controls?
            EyeHeight = KerbalUtility.EyeHeight;
            EyeSymmetry = KerbalUtility.EyeSymmetry;
            Stupidity = KerbalUtility.Stupidity;
            Bravery = KerbalUtility.Bravery;

            // Floats
            Constitution = KerbalUtility.Constitution;
            Optimism = KerbalUtility.Optimism;
            VoiceType = KerbalUtility.VoiceType;
            Radiation = KerbalUtility.Radiation;
            Happiness = KerbalUtility.Happiness;

            // Bools
            IsVeteran = KerbalUtility.IsVeteran;

            // Ints
            Experience = KerbalUtility.Experience;

            /*temp*/
            // Freetext
            Biography = KerbalUtility.Biography;
            /*endtemp*/
        }

        public void InitializeHumanAttributes()
        {
            KerbalUtility.SetKerbal(KerbalInfo);
            var culture = Manager.Instance.LoadedCampaign.Culture;

            Nationality = culture.GetRandomNationality();
            FirstName = Nation.GetRandomFirstName(Head.Gender);
            Surname = Nation.GetRandomLastName();

            // list presets
            // +Head
            var skinType = culture.GetRandomSkinColorType();
            SkinColor = HumanPresets.Instance.GetRandomSkinColor(skinType);
            HairColor = HumanPresets.Instance.GetRandomHairColor();

            // Colors
            TeamColor1 = culture.SuitColor1;
            TeamColor2 = culture.SuitColor2;

            /*
            Biography = culture.GetRandomBiography() ?? KerbalUtility.Biography;
            Biography = Biography.Replace("<firstname>", FirstName);
            Biography = Biography.Replace("<lastname>", Surname);
            */

            if (!string.IsNullOrEmpty(KerbalUtility.FirstName))
                Biography = Biography.Replace(KerbalUtility.FirstName, FirstName);

            if (!string.IsNullOrEmpty(KerbalUtility.Surname))
                Biography = Biography.Replace(KerbalUtility.Surname, Surname);
        }

        public void InitializeKerbalAttributes()
        {
            KerbalUtility.SetKerbal(KerbalInfo);

            Nationality = CultureNationPresets.KERBALNATION;
            FirstName = KerbalUtility.FirstName;
            Surname = KerbalUtility.Surname;

            // list presets
            SkinColor = HumanPresets.Instance.SkinColors.Find(sc => sc.Color == KerbalUtility.SkinColor)
                ?? HumanPresets.Instance.GetRandomSkinColor("Kerbal")
                ?? HumanPresets.Instance.GetRandomSkinColor("Human")
                ?? HumanPresets.Instance.SkinColors.First();
            
            HairColor = HumanPresets.Instance.HairColors.Find(hc => hc.Color == KerbalUtility.HairColor)
                ?? HumanPresets.Instance.GetRandomHairColor()
                ?? HumanPresets.Instance.HairColors.First();
            
            // Colors
            TeamColor1 = KerbalUtility.TeamColor1;
            TeamColor2 = KerbalUtility.TeamColor2;

            // Freetext
            Biography = KerbalUtility.Biography;

            if (!string.IsNullOrEmpty(KerbalUtility.FirstName))
                Biography = Biography.Replace(KerbalUtility.FirstName, FirstName);

            if (!string.IsNullOrEmpty(KerbalUtility.Surname))
                Biography = Biography.Replace(KerbalUtility.Surname, Surname);
        }

        public void ApplyAllAtributes() => ApplyAllAttributes(KerbalInfo);
        public void ApplyAllAttributes(KerbalInfo kerbal)
        {
            KerbalUtility.SetKerbal(kerbal);

            new FirstNameAttribute().ApplyAttribute(kerbal, FirstName);
            new SurnameAttribute().ApplyAttribute(kerbal, Surname);
            new SkinColorAttribute().ApplyAttribute(kerbal, (Color)SkinColor.Color);
            new HairColorAttribute().ApplyAttribute(kerbal, HairColor.Color);
            new KerbalTypeAttribute().ApplyAttribute(kerbal, KerbalType);
            new HeadAttribute().ApplyAttribute(kerbal, Head.Name);
            new HairStyleAttribute().ApplyAttribute(kerbal, HairStyle);
            new EyesAttribute().ApplyAttribute(kerbal, Eyes);
            new EyeHeightAttribute().ApplyAttribute(kerbal, EyeHeight);
            new EyeSymmetryAttribute().ApplyAttribute(kerbal, EyeSymmetry);
            new FacialHairAttribute().ApplyAttribute(kerbal, FacialHair);
            new TeamColor1Attribute().ApplyAttribute(kerbal, TeamColor1);
            new TeamColor2Attribute().ApplyAttribute(kerbal, TeamColor2);
            new StupidityAttribute().ApplyAttribute(kerbal, Stupidity);
            new BraveryAttribute().ApplyAttribute(kerbal, Bravery);
            new ConstitutionAttribute().ApplyAttribute(kerbal, Constitution);
            new OptimismAttribute().ApplyAttribute(kerbal, Optimism);
            new IsVeteranAttribute().ApplyAttribute(kerbal, IsVeteran);
            new VoiceSelectionAttribute().ApplyAttribute(kerbal, VoiceSelection);
            new VoiceTypeAttribute().ApplyAttribute(kerbal, VoiceType);
            new BodyAttribute().ApplyAttribute(kerbal, Body);
            new FacePaintAttribute().ApplyAttribute(kerbal, FacePaint);
            new RadiationAttribute().ApplyAttribute(kerbal, Radiation);
            new HappinessAttribute().ApplyAttribute(kerbal, Happiness);
            new ExperienceAttribute().ApplyAttribute(kerbal, Experience);
            new BiographyAttribute().ApplyAttribute(kerbal, Biography);

            kerbal._kerbalAttributes._fullName = $"{FirstName} {Surname}";
            
            // Applying a CUSTOM value to ORIGINTYPE attribute is crucial for the game to generate portraits correctly
            new OriginTypeAttribute().ApplyAttribute(kerbal, "CUSTOM");
            var customName = string.Join("_", $"{FirstName} {Surname}".Split(' ')).ToUpper();
            new RawCustomNameAttribute().ApplyAttribute(kerbal, customName);

            HasCustomAttributesApplied = true;
        }

        public void Rename(string newFirstName, string newLastName)
        {
            FirstName = newFirstName;
            Surname = newLastName;
            
            new FirstNameAttribute().ApplyAttribute(KerbalInfo, FirstName);
            new SurnameAttribute().ApplyAttribute(KerbalInfo, Surname);
            new RawCustomNameAttribute().ApplyAttribute(KerbalInfo, string.Join("_", $"{newFirstName} {newLastName}".Split(' ')).ToUpper());

            KerbalInfo._kerbalAttributes._fullName = $"{FirstName} {Surname}";

            Utility.SaveCampaigns();

            //KerbalUtility.SetKerbal(KerbalInfo);
            //KerbalInfo._kerbalAttributes._fullName = KerbalUtility.FullName;

            /*
            var newAttrs = new KerbalAttributes(
            string.Join("_", $"{newFirstName} {newLastName}".Split(' ')).ToUpper(),
            newFirstName,
            newLastName
            );
            KerbalInfo.Attributes = newAttrs;
            if (GameManager.Instance.Game.Messages.TryCreateMessage(out KerbalAddedToRoster msg))
            {
                msg.Kerbal = KerbalInfo;
                GameManager.Instance.Game.Messages.Publish(msg);
            }
            */
        }

        public void ChangeSuitBaseColor(Color32 newColor)
        {
            TeamColor1 = newColor;
            new TeamColor1Attribute().ApplyAttribute(KerbalInfo, TeamColor1);
            Utility.SaveCampaigns();
            KerbalUtility.TakeKerbalPortrait(KerbalInfo);
        }

        public void ChangeSuitAccentColor(Color32 newColor)
        {
            TeamColor2 = newColor;
            new TeamColor1Attribute().ApplyAttribute(KerbalInfo, TeamColor2);
            Utility.SaveCampaigns();
            KerbalUtility.TakeKerbalPortrait(KerbalInfo);
        }
    }
}
