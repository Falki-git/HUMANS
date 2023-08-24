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

        public void SubscribeToMessages()
        {
            // We're showing Culture select window when KSC is loaded and if campaign hasn't been initialized yet
            MessageCenter.Subscribe<KSCLoadedMessage>(new Action<MessageCenterMessage>(msg =>
            {
                _logger.LogDebug("KSCLoadedMessage triggered.");
                Manager.Instance.OnKSCLoadedMessage(msg);
            }));

            MessageCenter.Subscribe<GameStateChangedMessage>(new Action<MessageCenterMessage>(obj =>
            {
                var msg = obj as GameStateChangedMessage;
                _logger.LogDebug($"GameStateChangedMessage triggered. {msg.PreviousState} -> {msg.CurrentState}.");
                Manager.Instance.OnGameStateChangedMessage(msg);
            }));
        }
    }
}
