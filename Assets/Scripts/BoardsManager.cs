using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> boardLayouts = new List<GameObject>();

    [SerializeField] private BoardHandler board1;
    [SerializeField] private List<BoardHandler> boards2 = new List<BoardHandler>();
    [SerializeField] private List<BoardHandler> boards3 = new List<BoardHandler>();
    [SerializeField] private List<BoardHandler> boards4 = new List<BoardHandler>();
    [SerializeField] private List<BoardHandler> boards5 = new List<BoardHandler>();

    private void Start()
    {
        foreach (GameObject boardLayout in boardLayouts)
            boardLayout.SetActive(false);
    }

    private void Update()
    {
        
    }

    public BoardHandler GetBoard(int layout, int index)
    {
        return layout switch
        {
            1 => board1,
            2 => boards2[index],
            3 => boards3[index],
            4 => boards4[index],
            5 => boards5[index],
            _ => throw new System.Exception($"Unknown board layout: {layout}!"),
        };
    }

    public void ArrangeBoards(int count)
    {
        boardLayouts[count - 1].SetActive(true);
    }
}
