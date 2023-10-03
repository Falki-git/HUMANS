using BepInEx.Logging;
using SpaceWarp.API.Assets;
using UnityEngine.UIElements;

namespace Humans
{
    public class Uxmls
    {
        public const string MAIN_GUI_PATH = "/humans/humans.uxml";
        public const string CULTURE_SELECT_PATH = "/humans/culture_select.uxml";

        public VisualTreeAsset MainGui;
        public VisualTreeAsset CultureSelect;

        private static Uxmls _instance;
        private static readonly ManualLogSource _logger = Logger.CreateLogSource("Humans.Uxmls");

        public static Uxmls Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Uxmls();

                return _instance;
            }
        }

        public Uxmls()
        {
            Initialize();
        }

        public void Initialize()
        {
            MainGui = LoadAsset($"{MAIN_GUI_PATH}");
            CultureSelect = LoadAsset($"{CULTURE_SELECT_PATH}");
        }

        private VisualTreeAsset LoadAsset(string path)
        {
            try
            {
                return AssetManager.GetAsset<VisualTreeAsset>($"{HumansPlugin.Instance.GUID}{path}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to load VisualTreeAsset at path \"{HumansPlugin.Instance.GUID}{path}\"\n" + ex.Message);
                return null;
            }
        }
    }
}
