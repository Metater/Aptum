using AptumShared.Enums;
using AptumShared.Packets;
using LiteNetLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AptumClient;

public class UIManager : MonoBehaviour
{
    public Aptum aptumClient;

    public UIState uiState;

    public List<GameObject> onWelcome = new List<GameObject>();
    public List<GameObject> onSelection = new List<GameObject>();
    public List<GameObject> onWaitingLobby = new List<GameObject>();
    public List<GameObject> onWaitingToStart = new List<GameObject>();
    public List<GameObject> onGame = new List<GameObject>();
    public List<GameObject> onGameOver = new List<GameObject>();

    public Text nameTextbox;
    public Text joinCodeTextbox;

    public Text inviteCodeText;
    public Text messagesText;
    private float messageDuration = 0;

    private void Start()
    {
        SetUIState(uiState);
    }

    private void Update()
    {
        FadeMessages();
    }

    private void FadeMessages()
    {
        if (messageDuration >= 0)
        {
            messageDuration -= Time.deltaTime;
            messagesText.gameObject.SetActive(true);
        }
        else
        {
            messagesText.gameObject.SetActive(false);
            messagesText.text = "";
        }
    }

    #region ButtonImplementations
    public void SingleplayerButton()
    {
        AptumClientManager.I.UIReceive.SingleplayerButton();
    }
    public void MultiplayerButton()
    {
        AptumClientManager.I.UIReceive.MultiplayerButton();
    }
    public void QuitButton()
    {
        AptumClientManager.I.UIReceive.QuitButton();
    }
    public void CreateLobbyButton()
    {
        AptumClientManager.I.UIReceive.CreateLobbyButton(nameTextbox.text);
    }
    public void JoinLobbyButton()
    {
        AptumClientManager.I.UIReceive.JoinLobbyButton(nameTextbox.text, joinCodeTextbox.text);
    }
    public void PlayAgainButton()
    {
        AptumClientManager.I.UIReceive.PlayAgainButton();
    }
    #endregion ButtonImplementations

    private void SetActive(List<GameObject> gos, bool value)
    {
        gos.ForEach((go) => go.SetActive(value));
    }
    public void SetUIState(UIState state)
    {
        uiState = state;

        SetActive(onWelcome, false);
        SetActive(onSelection, false);
        SetActive(onWaitingLobby, false);
        SetActive(onWaitingToStart, false);
        SetActive(onGame, false);
        SetActive(onGameOver, false);

        switch (uiState)
        {
            case UIState.Welcome:
                SetActive(onWelcome, true);
                break;
            case UIState.Selection:
                SetActive(onSelection, true);
                break;
            case UIState.WaitingLobby:
                SetActive(onWaitingLobby, true);
                break;
            case UIState.WaitingToStart:
                SetActive(onWaitingToStart, true);
                break;
            case UIState.Game:
                SetActive(onGame, true);
                break;
            case UIState.GameOver:
                SetActive(onGameOver, true);
                break;
        }
    }

    public void DisplayMessage(string message, float duration = 5)
    {
        messageDuration = duration;
        messagesText.text = message;
    }

    public void DisplayJoinCode(int joinCode)
    {
        inviteCodeText.text = "Code:\n" + joinCode;
    }
}
