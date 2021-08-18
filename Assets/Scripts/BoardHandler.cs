using System.Collections.Generic;
using UnityEngine;

public class BoardHandler : MonoBehaviour
{
    public Aptum aptumClient;

    public Grid grid;
    [SerializeField] private GameObject cellPrefab;

    public SpriteRenderer[,] Board { private set; get; } = new SpriteRenderer[8, 8];

    private void Start()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                SpriteRenderer cell = AddCell(x, y, Color.white);
                cell.gameObject.SetActive(false);
                Board[x, y] = cell;
            }
        }
    }

    private Vector3 GetCellCenterWorldPos(Vector3Int pos)
    {
        Vector3 cellSpacing = grid.CellToWorld(Vector3Int.one) - grid.CellToWorld(Vector3Int.zero);
        Vector3 worldPos = grid.CellToWorld(pos + new Vector3Int(-4, -4, 0));
        worldPos += cellSpacing / 2f;
        worldPos.z = 0;
        return worldPos;
    }

    private SpriteRenderer AddCell(int x, int y, Color color)
    {
        GameObject cell = Instantiate(cellPrefab, GetCellCenterWorldPos(new Vector3Int(x, y, 0)), Quaternion.identity, grid.gameObject.transform);
        SpriteRenderer sr = cell.GetComponent<SpriteRenderer>();
        sr.material.color = color;
        return sr;
    }

    public void PlaceCell(int x, int y, Color color)
    {
        SpriteRenderer cell = Board[x, y];
        cell.material.color = color;
        cell.gameObject.SetActive(true);
    }
    public void RemoveCell(int x, int y)
    {
        SpriteRenderer cell = Board[x, y];
        cell.gameObject.SetActive(false);
    }

    public void PlacePiece(List<(int, int)> cellOffsets, (int, int) pos, Color color)
    {
        foreach ((int, int) cellOffset in cellOffsets)
        {
            (int, int) offsetPos = (pos.Item1 + cellOffset.Item1, pos.Item2 + cellOffset.Item2);
            PlaceCell(offsetPos.Item1, offsetPos.Item2, color);
        }
    }

    public void WipeLine(int index, bool horizontal)
    {
        // Also do animation and sound here later
        if (horizontal)
        {
            for (int i = 0; i < 8; i++)
                RemoveCell(i, index);
        }
        else
        {
            for (int i = 0; i < 8; i++)
                RemoveCell(index, i);
        }
    }

    public void WipeBlock(int startIndex, int length, bool horizontal)
    {
        // Could alternate direction of for loop, queue animations
        for (int i = 0; i < length; i++)
            WipeLine(startIndex + i, horizontal);
    }
}
