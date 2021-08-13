using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AptumShared.Utils;
using AptumShared.Enums;

public class BoardHandler : MonoBehaviour
{
    public Aptum aptumClient;

    public Grid grid;
    [SerializeField] private GameObject cellPrefab;

    public SpriteRenderer[,] Board { private set; get; } = new SpriteRenderer[8, 8];
    public bool[,] BoardBool { private set; get; } = new bool[8, 8];

    [SerializeField] private List<BoardPieceSO> pieces = new List<BoardPieceSO>();


    private void Start()
    {
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                SpriteRenderer cell = AddCell(x, y, Color.white);
                cell.gameObject.SetActive(false);
                Board[x, y] = cell;
                BoardBool[x, y] = false;
            }
        }
    }

    private void Update()
    {

    }

    private SpriteRenderer AddCell(int x, int y, Color color)
    {
        GameObject cell = Instantiate(cellPrefab, GetCellCenterWorldPos(new Vector3Int(x, y, 0)), Quaternion.identity, grid.gameObject.transform);
        SpriteRenderer sr = cell.GetComponent<SpriteRenderer>();
        sr.material.color = color;
        return sr;
    }

    private Vector3 GetCellCenterWorldPos(Vector3Int pos)
    {
        Vector3 cellSpacing = grid.CellToWorld(Vector3Int.one) - grid.CellToWorld(Vector3Int.zero);
        Vector3 worldPos = grid.CellToWorld(pos + new Vector3Int(-4, -4, 0));
        worldPos += cellSpacing / 2f;
        worldPos.z = 0;
        return worldPos;
    }

    public void PlaceCell(int x, int y, Color color)
    {
        SpriteRenderer cell = Board[x, y];
        cell.material.color = color;
        cell.gameObject.SetActive(true);
        BoardBool[x, y] = true;
    }
    public void RemoveCell(int x, int y)
    {
        SpriteRenderer cell = Board[x, y];
        cell.gameObject.SetActive(false);
        BoardBool[x, y] = false;
    }

    // Returns is line full
    public bool CheckLine(int index, bool horizontal)
    {
        if (horizontal) // x changes, y constant
        {
            for (int i = 0; i < 8; i++)
            {
                if (!BoardBool[i, index]) return false;
            }
        }
        else // y changes, x constant
        {
            for (int i = 0; i < 8; i++)
            {
                if (!BoardBool[index, i]) return false;
            }
        }
        return true;
    }

    public void WipeLine(int index, bool horizontal)
    {
        if (horizontal) // x changes, y constant
        {
            for (int i = 0; i < 8; i++)
            {
                BoardBool[i, index] = false;
                RemoveCell(i, index);
            }
        }
        else // y changes, x constant
        {
            for (int i = 0; i < 8; i++)
            {
                BoardBool[index, i] = false;
                RemoveCell(index, i);
            }
        }
    }

    public int TryWipe()
    {
        int wipes = 0;
        for (int i = 0; i < 8; i++)
        {
            if (CheckLine(i, true))
            {
                wipes++;
                WipeLine(i, true);
            }
            if (CheckLine(i, false))
            {
                wipes++;
                WipeLine(i, false);
            }
        }
        return wipes;
    }

    public bool CheckCellEmptyAndValid(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x > 7 || pos.y < 0 || pos.y > 7) return false;
        return !BoardBool[pos.x, pos.y];
    }

    public bool CheckPieceFit(Vector2Int pos, List<Vector2Int> cells)
    {
        foreach (Vector2Int cellOffset in cells)
        {
            Vector2Int offsetPos = pos + cellOffset;
            Debug.Log("LL: " + offsetPos);
            if (!CheckCellEmptyAndValid(offsetPos))
            {
                return false;
            }
        }
        return true;
    }

    public void PlacePiece(Vector2Int pos, List<Vector2Int> cells, Color color)
    {
        foreach (Vector2Int cellOffset in cells)
        {
            Vector2Int offsetPos = pos + cellOffset;
            PlaceCell(offsetPos.x, offsetPos.y, color);
        }
        int wipes = TryWipe();

        if (!CheckPieceFitOnBoard(cells))
        {
            aptumClient.uiManager.SetUIState(UIManager.UIState.GameOver);
            aptumClient.uiManager.DisplayMessage("Game Over!");
        }
    }

    public bool CheckPieceFitOnBoard(List<Vector2Int> piece)
    {
        bool fits = false;
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                if (CheckPieceFit(new Vector2Int(x, y), piece))
                {
                    fits = true;
                }
            }
        }
        return fits;
    }

    public BoardPieceSO GetBoardPieceSO(PieceType pieceType)
    {
        foreach (BoardPieceSO boardPieceSO in pieces)
        {
            if (boardPieceSO.pieceType == pieceType) return boardPieceSO;
        }
        return null;
    }
}
