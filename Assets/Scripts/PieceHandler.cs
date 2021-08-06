using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AptumShared.Enums;

public class PieceHandler : MonoBehaviour
{
    public List<SpriteRenderer> cells = new List<SpriteRenderer>();

    public PieceType type;
    public int color;

    public void SetColor(Color color)
    {
        foreach (SpriteRenderer sr in cells)
        {
            sr.material.color = color;
        }
    }
}
