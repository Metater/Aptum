using AptumShared.Enums;
using AptumShared.Packets;
using LiteNetLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Aptum aptumClient;

    public enum UIState
    {
        Welcome,
        Selection,
        WaitingLobby,
        WaitingToStart,
        Game,
        GameOver
    }

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
    private float messageVisableFor = 0;

    public GameObject selfGrid;
    public GameObject otherGrid;

    private bool joiningLobby = false;
    private bool creatingLobby = false;

    private void Start()
    {
        SetUIState(uiState);
    }

    private void Update()
    {
        if (messageVisableFor >= 0)
        {
            messageVisableFor -= Time.deltaTime;
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
        SetUIState(UIState.Game);
    }
    public void MultiplayerButton()
    {
        if (!aptumClient.IsConnected)
        {
            DisplayMessage("Could not connect to servers");
            return;
        }
        SetUIState(UIState.Selection);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void CreateLobbyButton()
    {
        if (joiningLobby || creatingLobby)
        {
            DisplayMessage("Already connecting to lobby");
            return;
        }
        if (nameTextbox.text == "")
        {
            DisplayMessage("Enter your name");
            return;
        }
        if (nameTextbox.text.Length > 16)
        {
            DisplayMessage("Name is too long");
            return;
        }

        RequestCreateLobbyPacket requestCreateLobbyPacket = new RequestCreateLobbyPacket
        { LeaderName = nameTextbox.text };
        aptumClient.Send(aptumClient.GetProcessor().Write(requestCreateLobbyPacket), DeliveryMethod.ReliableOrdered);

        creatingLobby = true;
        DisplayMessage("Creating Lobby...");
    }
    public void JoinLobbyButton()
    {
        if (joiningLobby || creatingLobby)
        {
            DisplayMessage("Already connecting to lobby");
            return;
        }
        if (nameTextbox.text == "")
        {
            DisplayMessage("Enter your name");
            return;
        }
        if (nameTextbox.text.Length > 16)
        {
            DisplayMessage("Name is too long");
            return;
        }

        if (int.TryParse(joinCodeTextbox.text, out int joinCode))
        {
            if (joinCode < 0 || joinCode > 1000)
            {
                DisplayMessage("Failed to parse join code");
                return;
            }

            RequestJoinLobbyPacket requestJoinLobbyPacket = new RequestJoinLobbyPacket
            { Name = nameTextbox.text, JoinCode = joinCode };
            aptumClient.Send(aptumClient.GetProcessor().Write(requestJoinLobbyPacket), DeliveryMethod.ReliableOrdered);

            joiningLobby = true;
            DisplayMessage("Joining Lobby...");
            aptumClient.SetCurrentJoinCode(joinCode);
        }
        else
        {
            DisplayMessage("Failed to parse join code");
            return;
        }
    }
    public void PlayAgainButton()
    {
        SetUIState(UIState.Game);
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                aptumClient.selfBoard.RemoveCell(x, y);
            }
        }
    }
    #endregion ButtonImplementations

    #region StateUpdates
    public void CreatedLobby(int inviteCode)
    {
        SetUIState(UIState.WaitingLobby);
        inviteCodeText.text = "Code:\n" + inviteCode;
    }
    public void JoinedLobby()
    {
        SetUIState(UIState.WaitingLobby);
        inviteCodeText.text = "Code:\n" + aptumClient.CurrentJoinCode;
    }
    #endregion StateUpdates

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
    private void SetActive(List<GameObject> gos, bool value)
    {
        gos.ForEach((go) => go.SetActive(value));
    }

    public void DisplayMessage(string message, float messageVisibleFor = 5)
    {
        this.messageVisableFor = messageVisibleFor;
        messagesText.text = message;
    }
}
