using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AptumClient.Interfaces;
using AptumShared.Packets;
using LiteNetLib;

namespace Assets.Scripts.ClientHandler
{
    public class NetSendUpdateHandler : INetSendUpdate
    {
        private Aptum aptum;

        public NetSendUpdateHandler(Aptum aptum)
        {
            this.aptum = aptum;
        }

        public void Send(RequestCreateLobbyPacket packet)
        {
            aptum.client.FirstPeer.Send(aptum.listener.packetProcessor.Write(packet), DeliveryMethod.ReliableOrdered);
        }

        public void Send(RequestJoinLobbyPacket packet)
        {
            aptum.client.FirstPeer.Send(aptum.listener.packetProcessor.Write(packet), DeliveryMethod.ReliableOrdered);
        }

        public void Send(RequestStartGamePacket packet)
        {
            aptum.client.FirstPeer.Send(aptum.listener.packetProcessor.Write(packet), DeliveryMethod.ReliableOrdered);
        }

        public void Send(RequestPlacePiecePacket packet)
        {
            aptum.client.FirstPeer.Send(aptum.listener.packetProcessor.Write(packet), DeliveryMethod.ReliableOrdered);
        }

        public void Send(RequestPlayAgainPacket packet)
        {
            aptum.client.FirstPeer.Send(aptum.listener.packetProcessor.Write(packet), DeliveryMethod.ReliableOrdered);
        }
    }
}
