using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AptumShared.Enums;

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
        return null;
    }
}
