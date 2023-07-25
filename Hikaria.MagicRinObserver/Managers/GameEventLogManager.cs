using System;
using System.Collections.Generic;
using CellMenu;
using Hikaria.MagicRinObserver.Ext;
using Hikaria.MagicRinObserver.Utils;
using UnityEngine;

namespace Hikaria.MagicRinObserver.Managers
{
    internal sealed class GameEventLogManager : MonoBehaviour
    {
        private void Awake()
        {
            Instance = this;
        }

        private void FixedUpdate()
        {
            if (_interval > 0f)
            {
                _interval -= Time.fixedDeltaTime;
                return;
            }
            if (_gameEventLogs.Count > 0)
            {
                string log;
                lock (_gameEventLogs)
                {
                    log = _gameEventLogs.Dequeue();
                }
                _playerLayerGameEventLog.AddLogItem(log, eGameEventChatLogType.GameEvent);
                _pageLoadoutGameEventLog.AddLogItem(log, eGameEventChatLogType.GameEvent);
                _interval = 0.5f;
            }
        }

        public static void AddLog(string log)
        {
            lock (Instance._gameEventLogs)
            {
                Instance._gameEventLogs.Enqueue(log);
            }
        }

        public static void AddLog(string[] logs)
        {
            foreach (string log in logs)
            {
                AddLog(log);
            }
        }

        public static void AddLogInSplit(string str, int len = 50)
        {
            if (len > 50)
            {
                len = 50;
            }
            AddLog(str.SplitByLength(len));
        }

        public static GameEventLogManager Instance { get; private set; }

        private Queue<string> _gameEventLogs = new();

        private float _interval;

        private PUI_GameEventLog _playerLayerGameEventLog
        {
            get
            {
                return GuiManager.PlayerLayer.m_gameEventLog;
            }
        }

        private PUI_GameEventLog _pageLoadoutGameEventLog
        {
            get
            {
                return CM_PageLoadout.Current.m_gameEventLog;
            }
        }
    }
}