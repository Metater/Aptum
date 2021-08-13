using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AptumClient.Interfaces;
using AptumShared.Structs;

namespace Assets.Scripts.ClientHandler
{
    public class GraphicsUpdateHandler : IGraphicsUpdate
    {
        private Aptum aptum;

        public GraphicsUpdateHandler(Aptum aptum)
        {
            this.aptum = aptum;
        }

        public void ClearLine(int boardId)
        {
            throw new NotImplementedException();
        }

        public void PlacePiece(int boardId, Piece piece, (int, int) pos)
        {
            throw new NotImplementedException();
        }

        public void WipeBoard(int boardId)
        {
            throw new NotImplementedException();
        }
    }
}
