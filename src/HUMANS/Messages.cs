﻿using BepInEx.Logging;
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
            MessageCenter.Subscribe<KSCLoadedMessage>(new Action<MessageCenterMessage>(this.OnKSCLoadedMessage));
        }

        private void OnKSCLoadedMessage(MessageCenterMessage obj)
        {
            //TODO check if campaign is initialized. If not, raise the screen for mode selection
        }
    }
}
