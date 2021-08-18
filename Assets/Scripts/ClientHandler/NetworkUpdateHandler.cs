using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AptumClient.Interfaces;

namespace Assets.Scripts.ClientHandler
{
    public class NetworkUpdateHandler : INetworkUpdate
    {
        private Aptum aptum;

        public NetworkUpdateHandler(Aptum aptum)
        {
            this.aptum = aptum;
        }

        public void Connect(string address, int port, string key)
        {
            aptum.client.Start();
            aptum.client.Connect(address, port, key);
        }
    }
}
