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

        public void PlacePiece(int boardIndex, Piece piece, (int, int) pos)
        {
            int boardLayout = AptumClientManager.I.State.boardLayout;
            BoardHandler board = aptum.boardsManager.GetBoard(boardLayout, boardIndex);
            board.PlacePiece(piece.cellOffsets, pos, aptum.colorDictionary.colors[(int)piece.color]);
        }

        public void WipeLine(int boardIndex, int index, bool horizontal)
        {
            aptum.boardsManager.GetBoard(AptumClientManager.I.State.boardLayout, boardIndex).WipeLine(index, horizontal);
        }

        public void WipeBoard(int boardIndex)
        {
            bool horizontal = UnityEngine.Random.Range(0, 2) == 1;
            aptum.boardsManager.GetBoard(AptumClientManager.I.State.boardLayout, boardIndex).WipeBlock(0, 8, horizontal);
        }
    }
}
