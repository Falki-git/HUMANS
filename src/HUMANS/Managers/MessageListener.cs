using BepInEx.Logging;
using KSP.Game;
using KSP.Messages;

namespace Humans
{
    public class MessageListener
    {
        private ManualLogSource _logger = BepInEx.Logging.Logger.CreateLogSource("Humans.MessageListener");
        private static MessageListener _instance;
        public MessageCenter MessageCenter => GameManager.Instance.Game.Messages;
        public GameStateConfiguration GameState => GameManager.Instance.Game.GlobalGameState.GetGameState();

        private MessageListener()
        { }

        public static MessageListener Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MessageListener();
                return _instance;
            }
        }

        public void SubscribeToMessages() => _ = Subscribe();

        private async Task Subscribe()
        {
            await Task.Delay(1000);

            // We're showing Culture select window when KSC is loaded and if campaign hasn't been initialized yet
            MessageCenter.Subscribe<KSCLoadedMessage>(OnKSCLoaded);
            _logger.LogInfo("Subscribed to KSCLoadedMessage.");

            // Show and Hide Culture select window
            MessageCenter.Subscribe<GameStateChangedMessage>(OnGameStateChanged);
            _logger.LogInfo("Subscribed to GameStateChangedMessage.");

            // Take kerbal portraits when game load is finished
            MessageCenter.Subscribe<GameLoadFinishedMessage>(OnGameLoadFinishedMessage);
            _logger.LogInfo("Subscribed to GameLoadFinishedMessage.");

            MessageCenter.Subscribe<KerbalAddedToRoster>(OnKerbalAddedToRoster);
            _logger.LogInfo("Subscribed to KerbalAddedToRoster.");
        }

        private void ResubscribeToMessages()
        {
            _logger.LogDebug("Resubscription triggered");

            try
            {
                MessageCenter.Unsubscribe<KSCLoadedMessage>(OnKSCLoaded);
            }
            catch { _logger.LogDebug("KSCLoadedMessage unsubscribe failed."); }
            try
            {
                MessageCenter.Unsubscribe<GameStateChangedMessage>(OnGameStateChanged);
            }
            catch { _logger.LogDebug("GameStateChangedMessage unsubscribe failed."); }

            try
            {
                MessageCenter.Unsubscribe<GameLoadFinishedMessage>(OnGameLoadFinishedMessage);
            }
            catch { _logger.LogDebug("GameLoadFinishedMessage unsubscribe failed."); }

            try
            {
                MessageCenter.Unsubscribe<KerbalAddedToRoster>(OnKerbalAddedToRoster);
            }
            catch { _logger.LogDebug("KerbalAddedToRoster unsubscribe failed."); }

            SubscribeToMessages();
        }

        private void OnKSCLoaded(MessageCenterMessage msg)
        {
            _logger.LogDebug("KSCLoadedMessage triggered.");
            Manager.Instance.OnKSCLoadedMessage(msg);
        }

        private void OnGameStateChanged(MessageCenterMessage obj)
        {
            var msg = obj as GameStateChangedMessage;
            _logger.LogDebug($"GameStateChangedMessage triggered. {msg.PreviousState} -> {msg.CurrentState}.");

            if (msg.CurrentState == KSP.Game.GameState.MainMenu)
                ResubscribeToMessages();
            else
                Manager.Instance.OnGameStateChangedMessage(msg);
        }

        private void OnGameLoadFinishedMessage(MessageCenterMessage message)
        {
            _logger.LogDebug("GameLoadFinishedMessage triggered.");
            Manager.Instance.OnGameLoadFinished(message);
        }

        private void OnKerbalAddedToRoster(MessageCenterMessage message)
        {
            var kerbalMsg = message as KerbalAddedToRoster;
            _logger.LogInfo($"OnKerbalAddedToRoster triggered. Kerbal name is {kerbalMsg.Kerbal?.NameKey}.");
            Manager.Instance.OnKerbalAddedToRoster(kerbalMsg);
        }
    }
}
