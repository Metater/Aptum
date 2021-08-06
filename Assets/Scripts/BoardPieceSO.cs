using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AptumShared.Enums;

[CreateAssetMenu(fileName = "Unnamed Piece", menuName = "Aptum/New Board Piece", order = 1)]
public class BoardPieceSO : ScriptableObject
{
    public PieceType pieceType;
    public List<Vector2Int> cells = new List<Vector2Int>();
}