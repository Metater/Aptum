using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;
using AptumServer;
using AptumShared.Packets;
using AptumClient;
using Assets.Scripts.ClientHandler;

public class Aptum : MonoBehaviour
{
    public AptumClientListener listener;
    public NetManager client;

    public UIManager uiManager;
    public PlacementManager placementManager;
    public ColorDictionary colorDictionary;
    public BoardsManager boardsManager;
    public BoardHandler selfBoard;
    public BoardHandler otherBoard;

    public NetSendUpdateHandler netSendUpdateHandler;
    public GraphicsUpdateHandler graphicsUpdateHandler;
    public UISendUpdateHandler uiSendUpdateHandler;
    public NetworkUpdateHandler networkUpdateHandler;

    private void Start()
    {
        listener = new AptumClientListener(this);
        client = new NetManager(listener);
        listener.NetManager(client);

        netSendUpdateHandler = new NetSendUpdateHandler(this);
        graphicsUpdateHandler = new GraphicsUpdateHandler(this);
        uiSendUpdateHandler = new UISendUpdateHandler(this);
        networkUpdateHandler = new NetworkUpdateHandler(this);

        AptumClientManager.I.Init(netSendUpdateHandler, graphicsUpdateHandler, uiSendUpdateHandler, networkUpdateHandler);
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

}
