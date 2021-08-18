using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AptumClient.Interfaces;
using AptumShared.Structs;
using AptumClient;

namespace Assets.Scripts.ClientHandler
{
    public class GraphicsUpdateHandler : IGraphicsUpdate
    {
        private Aptum aptum;

        public GraphicsUpdateHandler(Aptum aptum)
        {
            this.aptum = aptum;
        }

        public void ClearLine(int boardIndex)
        {
            throw new NotImplementedException();
        }

        public void PlacePiece(int boardIndex, Piece piece, (int, int) pos)
        {
            aptum.boardsManager.GetBoard(AptumClientManager.I.AptumClientState.boardLayout, boardIndex).PlacePiece(piece, pos, aptum.colorDictionary.colors[(int)piece.color]);
        }

        public void WipeBoard(int boardIndex)
        {
            //aptum.boardsManager.GetBoard(AptumClientManager.I.AptumClientState.boardLayout, boardIndex).
        }
    }
}
