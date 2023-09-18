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
        public Human(KerbalInfo kerbal, Culture culture)
        {
            KerbalUtility.SetKerbal(kerbal);

            Id = KerbalUtility.IGGuid;
            KerbalInfo = kerbal;

            // Male kerbals are recognized by having "*_M_*" for Head type and "*_M_*" for Eyes type
            // Female kerbals are recognized by having "*_F_*" for Head type and "*_F_*" for Eyes type
            Head = new HeadPreset { Name = KerbalUtility.Head, Gender = KerbalUtility.Head.Contains("_M_") ? Gender.Male : Gender.Female };

            Nationality = culture.GetRandomNationality();
            FirstName = Nation.GetRandomFirstName(Head.Gender);
            Surname = Nation.GetRandomLastName();
            KerbalType = KerbalUtility.KerbalType;
            NameKey = KerbalUtility.NameKey;

            // list presets
            // +Head
            var skinType = culture.GetRandomSkinColorType();
            SkinColor = HumanPresets.Instance.GetRandomSkinColor(skinType);
            HairStyle = KerbalUtility.HairStyle;
            Helmet = KerbalUtility.Helmet;
            HairColor = HumanPresets.Instance.GetRandomHairColor();
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

            // Colors
            TeamColor1 = KerbalUtility.TeamColor1;
            TeamColor2 = KerbalUtility.TeamColor2;

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

            // Freetext
            Biography = KerbalUtility.Biography;

            Humanize();
        }

        [JsonProperty]
        public IGGuid Id { get; set; }
        public KerbalInfo KerbalInfo { get; set; }
        [JsonProperty]
        public string Nationality { get; set; }
        public Nation Nation { get => CulturePresets.Instance.Nations.Find(n => n.Name == Nationality); }
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

        private static ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.Human");

        public void Humanize() => Humanize(KerbalInfo);
        public void Humanize(KerbalInfo kerbal)
        {
            if (KerbalInfo == null)
                KerbalInfo = kerbal;

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

            // HELMET DOESN'T WORK - TODO find out why
            //new HelmetAttribute().ApplyAttribute(kerbal, Helmet);

            // ALMOST WORKED
            kerbal._kerbalAttributes._fullName = $"{FirstName} {Surname}"; //KerbalUtility.FullName;
            //kerbal._nameKey = $"{FirstName} {Surname}";

            
            
            // TRYING TO MANIPULATE ORIGIN TYPE AND VETERAN STATUS - SUCCESS!
            new OriginTypeAttribute().ApplyAttribute(kerbal, "CUSTOM");
            //new IsVeteranAttribute().ApplyAttribute(kerbal, true);
            var customName = string.Join("_", $"{FirstName} {Surname}".Split(' ')).ToUpper();
            new RawCustomNameAttribute().ApplyAttribute(kerbal, customName);

            /*
            if ((bool)kerbal.Attributes.Attributes["ISVETERAN"].value != false)
            {
                var customName = string.Join("_", $"{FirstName} {Surname}".Split(' ')).ToUpper();
                new RawCustomNameAttribute().ApplyAttribute(kerbal, customName);

                kerbal._kerbalAttributes._fullName = $"{FirstName} {Surname}"; //KerbalUtility.FullName;
            }
            */

            //Utility.AllKerbals[10].Attributes.Attributes.Remove("RAW_CUSTOM_NAME");



            //Rename(FirstName, Surname);

            //kerbal.Attributes.CustomNameKey = string.Join("_", $"{FirstName} {Surname}".Split(' ')).ToUpper();

            IsHumanized = true;
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

        public int PreviousNation()
        {
            var index = CulturePresets.Instance.Nations.FindIndex(n => n == Nation);

            if (index == -1)
            {
                _logger.LogError($"Error retrieving Nation property {Nationality} for kerbal ID {Id}.");
                return index;
            }

            if (index > 0)
            {
                Nationality = CulturePresets.Instance.Nations[--index].Name;
            }

            return index;
        }

        public int NextNation()
        {
            var index = CulturePresets.Instance.Nations.FindIndex(n => n == Nation);

            if (index == -1)
            {
                _logger.LogError($"Error retrieving Nation property {Nationality} for kerbal ID {Id}.");
                return index;
            }

            if (index < CulturePresets.Instance.Nations.Count - 1)
            {
                Nationality = CulturePresets.Instance.Nations[++index].Name;
                Utility.SaveCampaigns();
            }

            return index;
        }
    }
}
