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
        public static string NameKey { get => _kerbalInfo.NameKey; }
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

        private static KerbalInfo _kerbalInfo;


        // TODO other attributes



        public static string Head { get => (string)_kerbalInfo.Attributes.GetAttribute("HEAD"); }

        public static void TakeKerbalPortraits(List<KerbalInfo> kerbals)
        {
            foreach (var kerbal in kerbals)
            {
                GameManager.Instance.Game.SessionManager.KerbalRosterManager._portraitRenderer.TakeKerbalPortrait(kerbal);
            }
        }

        public static void TakeKerbalPortrait(KerbalInfo kerbal)
        {
            GameManager.Instance.Game.SessionManager.KerbalRosterManager._portraitRenderer.TakeKerbalPortrait(kerbal);
        }
    }
}
