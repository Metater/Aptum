using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;
using AptumShared.Packets;

namespace AptumServer
{
    public class AptumClientListener : INetEventListener
    {
        private NetManager client;
        private AptumClient aptumClient;

        public NetPacketProcessor packetProcessor = new NetPacketProcessor();

        public AptumClientListener(AptumClient aptumClient)
        {
            this.aptumClient = aptumClient;
            packetProcessor.SubscribeReusable<DenyPacket, NetPeer>(OnDenyPacketReceived);
            packetProcessor.SubscribeReusable<CreatedLobbyPacket, NetPeer>(OnCreatedLobbyPacketReceived);
            packetProcessor.SubscribeReusable<JoinedLobbyPacket, NetPeer>(OnJoinedLobbyPacketReceived);
            packetProcessor.SubscribeReusable<UpdatePlayersPacket, NetPeer>(OnUpdatePlayersPacketReceived);
            packetProcessor.SubscribeReusable<StartGamePacket, NetPeer>(OnStartGamePacketReceived);
            packetProcessor.SubscribeReusable<PiecePlacedPacket, NetPeer>(OnPiecePlacedPacketReceived);
            packetProcessor.SubscribeReusable<GameEndedPacket, NetPeer>(OnGameEndedPacketReceived);
            packetProcessor.SubscribeReusable<PlayAgainPacket, NetPeer>(OnPlayAgainPacketReceived);
            packetProcessor.SubscribeReusable<LobbyClosePacket, NetPeer>(OnLobbyClosePacketReceived);
        }

        public void NetManager(NetManager client)
        {
            this.client = client;
        }

        public void OnConnectionRequest(ConnectionRequest request)
        {

        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {

        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {

        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            Debug.Log("[Client] received data. Processing...");
            packetProcessor.ReadAllPackets(reader, peer);
            reader.Recycle();
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {

        }

        public void OnPeerConnected(NetPeer peer)
        {
            aptumClient.Connected();
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            aptumClient.Connected(true);
            aptumClient.uiManager.SetUIState(UIManager.UIState.Welcome);
            aptumClient.uiManager.DisplayMessage("Disconnected from servers");
        }

        private void OnDenyPacketReceived(DenyPacket packet, NetPeer peer)
        {

        }
        private void OnCreatedLobbyPacketReceived(CreatedLobbyPacket packet, NetPeer peer)
        {
            Debug.Log("Created lobby, code is: " + packet.JoinCode);
            aptumClient.uiManager.CreatedLobby(packet.JoinCode);
        }
        private void OnJoinedLobbyPacketReceived(JoinedLobbyPacket packet, NetPeer peer)
        {
            aptumClient.uiManager.JoinedLobby();
        }
        private void OnUpdatePlayersPacketReceived(UpdatePlayersPacket packet, NetPeer peer)
        {
            foreach (string playerName in packet.PlayerNames)
            {
                Debug.Log("Player Acked: " + playerName);
            }
        }
        private void OnStartGamePacketReceived(StartGamePacket packet, NetPeer peer)
        {
            aptumClient.JoinedGame();
            aptumClient.placementManager.LoadSeed(packet.PieceGenerationSeed);
        }
        private void OnPiecePlacedPacketReceived(PiecePlacedPacket packet, NetPeer peer)
        {

        }
        private void OnGameEndedPacketReceived(GameEndedPacket packet, NetPeer peer)
        {

        }
        private void OnPlayAgainPacketReceived(PlayAgainPacket packet, NetPeer peer)
        {

        }
        private void OnLobbyClosePacketReceived(LobbyClosePacket packet, NetPeer peer)
        {

        }
    }
}
