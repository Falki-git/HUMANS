﻿using BepInEx.Logging;
using UnityEngine;

namespace Humans
{
    public class Culture
    {
        public string Name { get; set; }
        public string PicturePath { get; set; }
        public Texture2D Picture { get; set; }
        public Dictionary<string, int> NationalityWeights { get; set; }
        public Dictionary<string, int> SkinColorTypeWeights { get; set; }
        public Color32 SuitColor1 { get; set; }
        public Color32 SuitColor2 { get; set; }
        public List<string> Biographies { get; set; }
        public override string ToString() => Name;

        private static ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.Culture");

        public string GetRandomNationality()
        {
            try
            {
                return GetRandom(NationalityWeights);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("Error generating random nationality. Weights are not properly defined.\n" + ex);
                return String.Empty;
            }
        }

        public string GetRandomSkinColorType()
        {
            try
            {
                return GetRandom(SkinColorTypeWeights);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError("Error generating random skin type. Weights are not properly defined.\n" + ex);
                return String.Empty;
            }
        }

        public string GetRandomBiography()
        {
            try
            {
                int randomValue = UnityEngine.Random.Range(0, Biographies.Count - 1);
                return Biographies[randomValue];
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting random culture's biography.\n" + ex);
                return null;
            }
        }

        private string GetRandom(Dictionary<string, int> weights)
        {
            int totalWeight = 0;
            foreach (var weight in weights.Values)
                totalWeight += weight;

            int randomValue = UnityEngine.Random.Range(0, totalWeight);

            foreach (var kvp in weights)
            {
                randomValue -= kvp.Value;
                if (randomValue < 0)
                {
                    return kvp.Key;
                }
            }

            // This should not happen if the weights are defined properly
            throw new InvalidOperationException();
        }
    }
}
