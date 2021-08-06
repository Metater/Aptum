using AptumShared.Enums;
using AptumShared.Packets;
using LiteNetLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public AptumClient aptumClient;

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
    public Text opponentText;
    public Text selfScoreText;
    public Text otherScoreText;
    public Text messages;
    private float messageVisableFor = 0;

    public GameObject selfGrid;
    public GameObject otherGrid;

    private bool joiningLobby = false;
    private bool creatingLobby = false;


    public int selfScore = 0;
    public int otherScore = 0;

    public void AddSelfScore(int points)
    {
        selfScore += points;
        selfScoreText.text = "Score: " + selfScore;
    }

    public void AddOtherScore(int points)
    {
        otherScore += points;
        otherScoreText.text = "Score: " + otherScore;
    }

    private void Start()
    {
        SetUIState(uiState);
        selfScoreText.text = "Score: 0";
        otherScoreText.text = "Score: 0";
    }

    private void Update()
    {
        messageVisableFor -= Time.deltaTime;
        if (messageVisableFor >= 0) messages.gameObject.SetActive(true);
        else
        {
            messages.gameObject.SetActive(true);
            messages.text = "";
        }
    }

    #region ButtonImplementations
    public void PlayButton()
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
        messages.text = message;
    }
}
