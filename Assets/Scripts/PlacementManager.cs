using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AptumShared.Utils;
using AptumShared.Enums;
using AptumShared.Structs;

public class PlacementManager : MonoBehaviour
{
    public AptumClient aptumClient;

    public List<GameObject> placementPoints = new List<GameObject>();

    private PieceGenerator pieceGenerator;
    private int nextPieceIndex = 3;
    private bool dragging = false;
    private int draggedSlot = 0;

    private bool seedLoaded = false;

    public List<GameObject> piecePrefabs = new List<GameObject>();

    public GameObject[] pieces = new GameObject[3];

    private void Start()
    {
        LoadSeed(1);
    }

    private void Update()
    {
        //if (!seedLoaded || !aptumClient.connected) return;
        if ((Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
        {
            Debug.Log("E321312");
            //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            for (int i = 0; i < 3; i++)
            {
                Debug.Log("E");
                GameObject placementPoint = placementPoints[i];
                float dist = Vector2.Distance(pos, placementPoint.transform.position);
                if (dist < 1f)
                {
                    dragging = true;
                    draggedSlot = i;
                    Debug.Log("E");
                }
            }
        }

        pieces[draggedSlot].transform.localScale = new Vector3(0.2f, 0.2f);

        if (dragging)
        {
            if ((Input.touchCount == 1 && Input.touches[0].phase != TouchPhase.Ended) || Input.GetMouseButton(0))
            {
                //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                pieces[draggedSlot].transform.position = pos;
                pieces[draggedSlot].transform.localScale = new Vector3(0.5f, 0.5f);
            }
            else
            {
                pieces[draggedSlot].transform.localScale = new Vector3(0.2f, 0.2f);
                dragging = false;
                PieceHandler pieceHandler = pieces[draggedSlot].GetComponent<PieceHandler>();
                BoardPieceSO s = aptumClient.selfBoard.GetBoardPieceSO(pieceHandler.type);
                Vector3Int boardPos = aptumClient.selfBoard.grid.WorldToCell(pieceHandler.cells[0].transform.position);
                Vector2Int boardPosv2 = new Vector2Int(boardPos.x + 4, boardPos.y + 4);
                Debug.Log("eeeeeeeeeeeeee: " + boardPosv2);
                if (aptumClient.selfBoard.CheckPieceFit(boardPosv2, s.cells))
                {
                    aptumClient.selfBoard.PlacePiece(boardPosv2, s.cells, aptumClient.colorDictionary.colors[pieceHandler.color]);
                    Destroy(pieces[draggedSlot]);
                    Piece pieceS = pieceGenerator.GetPieceAtIndex(nextPieceIndex);
                    (int, int) piece = ((int)pieceS.piece, (int)pieceS.color);
                    pieces[draggedSlot] = Instantiate(piecePrefabs[piece.Item1], placementPoints[draggedSlot].transform.position, Quaternion.identity);
                    PieceHandler newPieceHandler = pieces[draggedSlot].GetComponent<PieceHandler>();
                    newPieceHandler.SetColor(aptumClient.colorDictionary.colors[piece.Item2]);
                    newPieceHandler.color = piece.Item2;
                    nextPieceIndex++;
                    Debug.Log("Ed123231");
                }
                else
                {
                    dragging = false;
                    pieces[draggedSlot].transform.position = placementPoints[draggedSlot].transform.position;
                    pieces[draggedSlot].transform.localScale = new Vector3(0.2f, 0.2f);
                    Debug.Log("stop");
                }
                pieces[draggedSlot].transform.position = placementPoints[draggedSlot].transform.position;
            }
        }
    }

    public void LoadSeed(int seed)
    {
        pieceGenerator = new PieceGenerator(seed);
        seedLoaded = true;
        for (int i = 0; i < 3; i++)
        {
            Piece pieceS = pieceGenerator.GetPieceAtIndex(nextPieceIndex);
            (int, int) piece = ((int)pieceS.piece, (int)pieceS.color);
            pieces[i] = Instantiate(piecePrefabs[piece.Item1], placementPoints[i].transform.position, Quaternion.identity);
            PieceHandler pieceHandler = pieces[i].GetComponent<PieceHandler>();
            pieceHandler.SetColor(aptumClient.colorDictionary.colors[piece.Item2]);
            pieceHandler.color = piece.Item2;
            pieces[i].transform.localScale = new Vector3(0.2f, 0.2f);
        }
    }
}
