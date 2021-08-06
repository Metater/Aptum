using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;
using AptumServer;
using AptumShared.Packets;

public class AptumClient : MonoBehaviour
{
    public bool IsConnected { get; private set; } = false;
    public bool IsInGame { get; private set; } = false;
    public int CurrentJoinCode { get; private set; } = -1;

    public AptumClientListener listener;
    public NetManager client;

    public UIManager uiManager;
    public PlacementManager placementManager;
    public ColorDictionary colorDictionary;
    public BoardHandler selfBoard;
    public BoardHandler otherBoard;

    private void Start()
    {
        listener = new AptumClientListener(this);
        client = new NetManager(listener);
        listener.NetManager(client);
        client.Start();
        client.Connect("192.168.1.92", 12733, "Aptum");
    }

    private void Update()
    {
        client.PollEvents();
    }

    public void Send(byte[] data, DeliveryMethod deliveryMethod)
    {
        client.FirstPeer.Send(data, deliveryMethod);
    }

    public NetPacketProcessor GetProcessor()
    {
        return listener.packetProcessor;
    }

    #region SetAccessors
    public void Connected(bool disconnected = false)
    {
        if (!IsConnected && !disconnected) IsConnected = true;
        else if (IsConnected && disconnected) IsConnected = false;
        else if (!IsConnected && disconnected) throw new System.Exception("Already disconnected!");
        else throw new System.Exception("Already connected!");
    }
    public void JoinedGame()
    {
        if (!IsInGame) IsInGame = true;
        else throw new System.Exception("Already in game!");
    }
    public void SetCurrentJoinCode(int joinCode)
    {
        CurrentJoinCode = joinCode;
    }
    #endregion SetAccessors

}
