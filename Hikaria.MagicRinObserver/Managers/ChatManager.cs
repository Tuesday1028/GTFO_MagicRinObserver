using Hikaria.MagicRinObserver.Ext;
using Player;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Hikaria.MagicRinObserver.Managers
{
    internal sealed class ChatManager : MonoBehaviour
    {
        private void Awake()
        {
            Instance = this;
        }

        private void FixedUpdate()
        {
            if (queue.Count != 0)
            {
                lock (queue)
                {
                    Speak(queue.Dequeue());
                }
            }
        }

        public static void AddQueueInSeparate(string str, int len = 50)
        {
            if (len > 50)
            {
                len = 50;
            }
            AddQueue(str.SplitByLength(len));
        }

        public static void AddQueue(string msg)
        {
            if (msg.Length > 50)
            {
                AddQueue(msg.SplitByLength(50));
            }
            else
            {
                lock (Instance.queue)
                {
                    Instance.queue.Enqueue(msg);
                }

            }
        }

        public static void AddQueue(string[] msgs)
        {
            foreach (string msg in msgs)
            {
                AddQueue(msg);
            }
        }

        public static void ClearQueue()
        {
            Instance.queue.Clear();
        }

        public static void Speak(string text)
        {
            PlayerChatManager.WantToSentTextMessage(PlayerManager.GetLocalPlayerAgent(), text, null);
        }

        private readonly Queue<string> queue = new();

        public static ChatManager Instance { get; private set; }
    }
}
