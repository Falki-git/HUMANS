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

        public static IGGuid IGGuid { get => _kerbalInfo.Id; }
        public static KerbalInfo KerbalInfo { get => _kerbalInfo; }
        public static string NameKey { get => _kerbalInfo.NameKey; set => _kerbalInfo._nameKey = value; }
        public static string FirstName { get => _kerbalInfo.Attributes.FirstName; }
        public static string Surname { get => _kerbalInfo.Attributes.Surname; }
        public static KerbalType HumanType { get => (KerbalType)_kerbalInfo.Attributes.GetAttribute("TYPE"); }
        public static string HairStyle { get => (string)_kerbalInfo.Attributes.GetAttribute("HAIRSTYLE"); }
        public static string Helmet { get => (string)_kerbalInfo.Attributes.GetAttribute("HELMET"); }
        public static Color HairColor { get => (Color)_kerbalInfo.Attributes.GetAttribute("HAIRCOLOR"); }
        public static string Eyes { get => (string)_kerbalInfo.Attributes.GetAttribute("EYES"); }
        public static Single EyeHeight { get => (Single)_kerbalInfo.Attributes.GetAttribute("EYEHEIGHT"); }
        public static Single EyeSymmetry { get => (Single)_kerbalInfo.Attributes.GetAttribute("EYESYMMETRY"); }
        public static Color SkinColor { get => (Color)_kerbalInfo.Attributes.GetAttribute("SKINCOLOR"); }
        public static string FullName { get => _kerbalInfo.Attributes._fullName; set => _kerbalInfo._kerbalAttributes._fullName = value; }

        private static KerbalInfo _kerbalInfo;
        private static readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.KerbalUtility");


        // TODO other attributes



        public static string Head { get => (string)_kerbalInfo.Attributes.GetAttribute("HEAD"); }

        public static void TakeKerbalPortraits(List<KerbalInfo> kerbals)
        {
            _logger.LogDebug($"Starting TakeKerbalPortraits. Number of kerbals: {kerbals.Count}.");

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
