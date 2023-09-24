﻿using BepInEx.Logging;
using KSP.Sim.impl;
using SpaceWarp.API.Assets;
using System.Reflection;
using UnityEngine;

namespace Humans
{
    public class CultureNationPresets
    {
        public List<Culture> Cultures = new();
        public List<Nation> Nations = new();

        private readonly string _culturesPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data", "culture_presets.json");
        private readonly string _nationsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data", "nation_presets.json");                
        private static CultureNationPresets _instance;
        private readonly ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.CulturePresets");

        private CultureNationPresets() { }
        public static CultureNationPresets Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CultureNationPresets();

                return _instance;
            }
        }

        public void Initialize()
        {
            Cultures = Utility.LoadPresets<List<Culture>>(_culturesPath);
            _logger.LogInfo($"Number of Culture presets: {Cultures.Count}");
            Nations = Utility.LoadPresets<List<Nation>>(_nationsPath);
            _logger.LogInfo($"Number of Nation presets: {Nations.Count}");

            // TODO cleaner way of doing this
            string flagPath;
            foreach (var nation in Nations)
            {
                try
                {
                    flagPath = $"{HumansPlugin.Instance.GUID}/images/{nation.FlagPath}";
                    nation.Flag = AssetManager.GetAsset<Texture2D>(flagPath);
                    _logger.LogInfo($"Successfully loaded {nation.Name}'s flag.");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error loading {nation.Name}'s flag from path \"{HumansPlugin.Instance.GUID}/images/{nation.FlagPath}\"\n" + ex);
                }
            }
            _logger.LogInfo($"Number of Nations with flags: {Nations.Where(n => n.Flag != null).Count()}");
        }

        public string PreviousNation(Human human)
        {
            var index = CultureNationPresets.Instance.Nations.FindIndex(n => n.Name == human.Nationality);

            if (index == -1)
            {
                _logger.LogError($"Error retrieving Nation property {human.Nationality} for kerbal ID {human.Id}.");
                return string.Empty;
            }

            if (index > 0)
            {
                return CultureNationPresets.Instance.Nations[--index].Name;
            }
            else
            {
                return string.Empty;
            }
        }

        public string NextNation(Human human)
        {
            var index = CultureNationPresets.Instance.Nations.FindIndex(n => n.Name == human.Nationality);

            if (index == -1)
            {
                _logger.LogError($"Error retrieving Nation property {human.Nationality} for kerbal ID {human.Id}.");
                return string.Empty;
            }

            if (index < CultureNationPresets.Instance.Nations.Count - 1)
            {
                return CultureNationPresets.Instance.Nations[++index].Name;                
            }

            return string.Empty;
        }
    }
}