using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AptumClient.Interfaces;
using AptumShared.Packets;

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
            throw new NotImplementedException();
        }

        public void Send(RequestJoinLobbyPacket packet)
        {
            throw new NotImplementedException();
        }

        public void Send(RequestStartGamePacket packet)
        {
            throw new NotImplementedException();
        }

        public void Send(RequestPlacePiecePacket packet)
        {
            throw new NotImplementedException();
        }

        public void Send(RequestPlayAgainPacket packet)
        {
            throw new NotImplementedException();
        }
    }
}
