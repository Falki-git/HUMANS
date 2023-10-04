using KSP.Game;
using KSP.Sim;
using KSP.Sim.impl;
using UnityEngine;
using BepInEx.Logging;

namespace Humans.Utilities
{
    public static class KerbalUtility
    {
        public static void SetKerbal(KerbalInfo kerbalInfo) => _kerbalInfo = kerbalInfo;

        /// STRINGS ///

        public static IGGuid IGGuid => _kerbalInfo.Id;
        public static KerbalInfo KerbalInfo =>_kerbalInfo;
        public static string NameKey => _kerbalInfo.NameKey ?? "";
        public static string FirstName => _kerbalInfo.Attributes.FirstName ?? "";
        public static string Surname => _kerbalInfo.Attributes.Surname ?? "";
        public static string HairStyle => _kerbalInfo.Attributes.GetAttribute("HAIRSTYLE")?.ToString() ?? "";
        public static string Helmet => _kerbalInfo.Attributes.GetAttribute("HELMET")?.ToString() ?? "";
        public static string Eyes => _kerbalInfo.Attributes.GetAttribute("EYES")?.ToString() ?? "";
        public static string FullName => _kerbalInfo.Attributes.GetFullName();
        public static string FacialHair => _kerbalInfo.Attributes.GetAttribute("FACIALHAIR")?.ToString() ?? "";
        public static string FaceDecoration => _kerbalInfo.Attributes.GetAttribute("FACEDECORATION")?.ToString() ?? "";
        public static string Body => _kerbalInfo.Attributes.GetAttribute("BODY")?.ToString() ?? "";
        public static string Head => _kerbalInfo.Attributes.GetAttribute("HEAD")?.ToString() ?? "";
        public static string FacePaint => _kerbalInfo.Attributes.GetAttribute("FACEPAINT")?.ToString() ?? "";
        public static string Biography => _kerbalInfo.Attributes.GetAttribute("BIOGRAPHY")?.ToString() ?? "";

        /// SINGLES ///

        public static Single EyeHeight //{ get => (Single)_kerbalInfo.Attributes.GetAttribute("EYEHEIGHT"); }
        {
            get
            {
                string attributeValue = _kerbalInfo.Attributes.GetAttribute("EYEHEIGHT")?.ToString();

                return (attributeValue != null && Single.TryParse(attributeValue, out var toReturn)) ? toReturn: 0f;
            }
        }
        public static Single EyeSymmetry // => (Single)_kerbalInfo.Attributes.GetAttribute("EYESYMMETRY");
        {
            get
            {
                string attributeValue = _kerbalInfo.Attributes.GetAttribute("EYESYMMETRY")?.ToString();

                return (attributeValue != null && Single.TryParse(attributeValue, out var toReturn)) ? toReturn : 0f;
            }
        }
        public static Single Stupidity //{ get => Single.Parse(_kerbalInfo.Attributes.GetAttribute("STUPIDITY").ToString()); }
        {
            get
            {
                string attributeValue = _kerbalInfo.Attributes.GetAttribute("STUPIDITY")?.ToString();

                return (attributeValue != null && Single.TryParse(attributeValue, out var toReturn)) ? toReturn : 0f;
            }
        }
        public static Single Bravery //{ get => Single.Parse(_kerbalInfo.Attributes.GetAttribute("BRAVERY").ToString()); } ////////////////////////////////////
        {
            get
            {
                string attributeValue = _kerbalInfo.Attributes.GetAttribute("BRAVERY")?.ToString();

                return (attributeValue != null && Single.TryParse(attributeValue, out var toReturn)) ? toReturn : 0f;
            }
        }
        public static Single Constitution //{ get => Single.Parse(_kerbalInfo.Attributes.GetAttribute("CONSTITUTION").ToString()); }
        {
            get
            {
                string attributeValue = _kerbalInfo.Attributes.GetAttribute("CONSTITUTION")?.ToString();

                return (attributeValue != null && Single.TryParse(attributeValue, out var toReturn)) ? toReturn : 0f;
            }
        }
        public static Single Optimism //{ get => Single.Parse(_kerbalInfo.Attributes.GetAttribute("OPTIMISM").ToString()); }
        {
            get
            {
                string attributeValue = _kerbalInfo.Attributes.GetAttribute("OPTIMISM")?.ToString();

                return (attributeValue != null && Single.TryParse(attributeValue, out var toReturn)) ? toReturn : 0f;
            }
        }
        public static Single VoiceType //{ get => Single.Parse(_kerbalInfo.Attributes.GetAttribute("VOICETYPE").ToString()); }
        {
            get
            {
                string attributeValue = _kerbalInfo.Attributes.GetAttribute("VOICETYPE")?.ToString();

                return (attributeValue != null && Single.TryParse(attributeValue, out var toReturn)) ? toReturn : 0f;
            }
        }
        public static Single Radiation //{ get => Single.Parse(_kerbalInfo.Attributes.GetAttribute("RADIATION").ToString()); }
        {
            get
            {
                string attributeValue = _kerbalInfo.Attributes.GetAttribute("RADIATION")?.ToString();

                return (attributeValue != null && Single.TryParse(attributeValue, out var toReturn)) ? toReturn : 0f;
            }
        }
        public static Single Happiness //{ get => Single.Parse(_kerbalInfo.Attributes.GetAttribute("HAPPINESS").ToString()); }
        {
            get
            {
                string attributeValue = _kerbalInfo.Attributes.GetAttribute("HAPPINESS")?.ToString();

                return (attributeValue != null && Single.TryParse(attributeValue, out var toReturn)) ? toReturn : 0f;
            }
        }

        /// INTS ///

        public static int VoiceSelection //{ get => (int)_kerbalInfo.Attributes.GetAttribute("VOICESELECTION"); }
        {
            get
            {
                string attributeValue = _kerbalInfo.Attributes.GetAttribute("VOICESELECTION")?.ToString();

                return (attributeValue != null && int.TryParse(attributeValue, out var toReturn)) ? toReturn : 0;
            }
        }
        public static int Experience //{ get => int.Parse(_kerbalInfo.Attributes.GetAttribute("EXPERIENCE").ToString()); }
        {
            get
            {
                string attributeValue = _kerbalInfo.Attributes.GetAttribute("EXPERIENCE")?.ToString();

                return (attributeValue != null && int.TryParse(attributeValue, out var toReturn)) ? toReturn : 0;
            }
        }

        /// BOOLS ///

        public static bool IsVeteran //{ get => (bool)_kerbalInfo.Attributes.GetAttribute("ISVETERAN"); }
        {
            get
            {
                string attributeValue = _kerbalInfo.Attributes.GetAttribute("ISVETERAN")?.ToString();

                return (attributeValue != null && bool.TryParse(attributeValue, out var toReturn)) ? toReturn : false;
            }
        }

        /// COLORS ///

        public static Color SkinColor //{ get => (Color)_kerbalInfo.Attributes.GetAttribute("SKINCOLOR"); }
        {
            get
            {
                try
                {
                    return (Color)_kerbalInfo.Attributes.GetAttribute("SKINCOLOR");
                }
                catch { return Color.black; }
            }
        }
        public static Color HairColor
        {
            get
            {
                try
                {
                    return (Color)_kerbalInfo.Attributes.GetAttribute("HAIRCOLOR");
                }
                catch { return Color.black; }
            }
        }
        public static Color TeamColor1 //{ get => (Color)_kerbalInfo.Attributes.GetAttribute("TEAMCOLOR1"); }
        {
            get
            {
                try
                {
                    return (Color)_kerbalInfo.Attributes.GetAttribute("TEAMCOLOR1");
                }
                catch { return Color.red; }
            }
        }
        public static Color TeamColor2 //{ get => (Color)_kerbalInfo.Attributes.GetAttribute("TEAMCOLOR2"); }
        {
            get
            {
                try
                {
                    return (Color)_kerbalInfo.Attributes.GetAttribute("TEAMCOLOR2");
                }
                catch { return Color.white; }
            }
        }

        /// OTHER ///

        public static KerbalType KerbalType
        {
            //get => Enum.Parse(typeof(KSP.Sim.KerbalType), _kerbalInfo.Attributes.GetAttribute("TYPE").ToString());
            get
            {
                string attributeValue = _kerbalInfo.Attributes.GetAttribute("TYPE")?.ToString();

                return (attributeValue != null && Enum.TryParse(attributeValue, out KerbalType toReturn)) ? toReturn : KerbalType.CIRCLE;
            }
        }

        private static KerbalInfo _kerbalInfo;
        private static readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.KerbalUtility");

        public static void TakeKerbalPortraits(List<KerbalInfo> kerbals)
        {
            _logger.LogDebug($"Starting TakeKerbalPortraits. Number of kerbals: {kerbals.Count}.");
            //Utility.Roster._portraitRenderer._numberOfSimultaneousModels = kerbals.Count;
            //Utility.Roster._portraitRenderer._modelsInUse.Clear();

            foreach (var kerbal in kerbals)
            {
                //GameManager.Instance.Game.SessionManager.KerbalRosterManager._portraitRenderer.TakeKerbalPortrait(kerbal);
                TakeKerbalPortrait(kerbal);
            }
        }

        public static void TakeKerbalPortrait(KerbalInfo kerbal)
        {
            try
            {
                GameManager.Instance.Game.SessionManager.KerbalRosterManager._portraitRenderer.TakeKerbalPortrait(kerbal);
                _logger.LogDebug($"Successfuly create kerbal portrait for kerbal {kerbal.NameKey}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error taking kerbal portrait for kerbal {kerbal.NameKey}.\n" + ex);
            }
        }
    }
}
