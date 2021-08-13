using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AptumClient.Interfaces;

namespace Assets.Scripts.ClientHandler
{
    public class UIWriteUpdateHandler : IUIWriteUpdate
    {
        private Aptum aptum;

        public UIWriteUpdateHandler(Aptum aptum)
        {
            this.aptum = aptum;
        }
    }
}
