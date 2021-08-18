using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AptumClient.Interfaces;
using AptumShared.Enums;
using UnityEngine;

namespace Assets.Scripts.ClientHandler
{
    public class UISendUpdateHandler : IUISendUpdate
    {
        private Aptum aptum;

        public UISendUpdateHandler(Aptum aptum)
        {
            this.aptum = aptum;
        }

        public void DisplayMessage(string message, float duration)
        {
            aptum.uiManager.DisplayMessage(message, duration);
        }

        public void DisplayJoinCode(int joinCode)
        {
            aptum.uiManager.DisplayJoinCode(joinCode);
        }

        public void SetUIState(UIState uiState)
        {
            aptum.uiManager.SetUIState(uiState);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
