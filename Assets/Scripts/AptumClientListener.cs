using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using LiteNetLib;
using LiteNetLib.Utils;
using AptumShared.Packets;
using AptumShared.Enums;

namespace AptumServer
{
    public class AptumClientListener : INetEventListener
    {
        private NetManager client;
        private Aptum aptum;

        public NetPacketProcessor packetProcessor = new NetPacketProcessor();

        public AptumClientListener(Aptum aptum)
        {
            this.aptum = aptum;
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
            packetProcessor.ReadAllPackets(reader, peer);
            reader.Recycle();
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {

        }

        public void OnPeerConnected(NetPeer peer)
        {
            Debug.Log("[Client] Connected.");
            //aptumClient.Connected();
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Debug.Log("[Client] Disconnected.");
            //aptumClient.Connected(true);
            //aptumClient.uiManager.SetUIState(UIManager.UIState.Welcome);
            //aptumClient.uiManager.DisplayMessage("Disconnected from servers");
        }

        private void OnDenyPacketReceived(DenyPacket packet, NetPeer peer)
        {
            Debug.Log($"[Client] Deny packet received, {((DenyReason)packet.DenyBitField)} denied.");
        }
        private void OnCreatedLobbyPacketReceived(CreatedLobbyPacket packet, NetPeer peer)
        {
            Debug.Log($"[Client] Created lobby packet received, the join code is {packet.JoinCode}.");
            //Debug.Log("Created lobby, code is: " + packet.JoinCode);
            //aptumClient.uiManager.CreatedLobby(packet.JoinCode);
        }
        private void OnJoinedLobbyPacketReceived(JoinedLobbyPacket packet, NetPeer peer)
        {
            Debug.Log("[Client] Joined lobby packet received.");
            //aptumClient.uiManager.JoinedLobby();
        }
        private void OnUpdatePlayersPacketReceived(UpdatePlayersPacket packet, NetPeer peer)
        {
            Debug.Log("[Client] Update player packet received.");
            /*
            foreach (string playerName in packet.PlayerNames)
            {
                Debug.Log("Player Acked: " + playerName);
            }
            */
        }
        private void OnStartGamePacketReceived(StartGamePacket packet, NetPeer peer)
        {
            Debug.Log("[Client] Start game packet received.");
            //aptumClient.JoinedGame();
            //aptumClient.placementManager.LoadSeed(packet.PieceGenerationSeed);
        }
        private void OnPiecePlacedPacketReceived(PiecePlacedPacket packet, NetPeer peer)
        {
            Debug.Log("[Client] Piece placed packet received.");
        }
        private void OnGameEndedPacketReceived(GameEndedPacket packet, NetPeer peer)
        {
            Debug.Log("[Client] Game ended packet received.");
        }
        private void OnPlayAgainPacketReceived(PlayAgainPacket packet, NetPeer peer)
        {
            Debug.Log("[Client] Play again packet received.");
        }
        private void OnLobbyClosePacketReceived(LobbyClosePacket packet, NetPeer peer)
        {
            Debug.Log("[Client] Lobby close packet received.");
        }
    }
}
