using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    Color mouseEnter;
    [SerializeField]
    Color mouseExit;

    SpriteRenderer sr;

    GameObject obj;
    GameObject pieceObj;

    TicTacToe game;

    public int pos;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<TicTacToe>();
    }

    private void OnMouseOver()
    {
        sr.color = mouseEnter;
    }

    private void OnMouseExit()
    {
        sr.color = mouseExit;
    }

    private void OnMouseDown()
    {
        game.InsertLetter(pos);
    }
}
