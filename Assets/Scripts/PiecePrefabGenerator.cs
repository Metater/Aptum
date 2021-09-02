using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AptumShared.Enums;
using AptumShared.Structs;
using AptumShared;

public class PiecePrefabGenerator : MonoBehaviour
{
    [SerializeField] private Transform piecePrefabs;

    private Dictionary<PieceType, GameObject> cachedPiecePrefabs = new Dictionary<PieceType, GameObject>();

    public GameObject GetPiecePrefab(PieceType pieceType)
    {
        if (cachedPiecePrefabs.TryGetValue(pieceType, out GameObject prefab))
        {
            return prefab;
        }
        return FormPiecePrefab(pieceType);
    }

    private GameObject FormPiecePrefab(PieceType pieceType)
    {
        Piece piece = PieceDictionary.GetPiece(pieceType);
        // It has to be known where the root pos is
        return null;
    }
}
