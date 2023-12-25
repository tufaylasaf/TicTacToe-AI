using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TicTacToe : MonoBehaviour
{
    Dictionary<int, string> board = new Dictionary<int, string>(){
        {1 , " "},{2 , " "},{3 , " "},
        {4 , " "},{5 , " "},{6 , " "},
        {7 , " "},{8 , " "},{9 , " "}
    };

    [SerializeField]
    GameObject[] TileObj;

    [SerializeField]
    GameObject Cross, Circle;
    [SerializeField]
    Text winnerTxt;

    string player = "X", comp = "O";

    public bool playersTurn = true;

    bool gameOver = false;
    [SerializeField]
    int positions = 0;

    private void Start()
    {
        winnerTxt.enabled = true;
    }

    private void Update()
    {
        if (!playersTurn)
        {
            int bestScore = -800;
            int bestMove = 0;
            // positions = 0;

            for (int i = 1; i <= board.Count; i++)
            {
                if (board[i] == " ")
                {
                    positions++;
                    board[i] = comp;
                    int score = MiniMax(board, false);
                    board[i] = " ";

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = i;
                    }
                }
            }
            if (!CheckWin(player) && !CheckWin(comp) && !CheckDraw()) InsertLetter(bestMove);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            for (int i = 1; i <= board.Count; i++)
            {
                board[i] = " ";
                if (TileObj[i - 1].transform.childCount > 0) Destroy(TileObj[i - 1].transform.GetChild(0).gameObject);
                positions = 0;
                playersTurn = true;
                gameOver = false;
                winnerTxt.GetComponent<Animator>().SetTrigger("Restart");
            }
        }
    }

    void PrintDictionary()
    {
        Debug.Log("|" + "1 : " + board[1] + "|" + "2 : " + board[2] + "|" + "3 : " + board[3] + "|");
        Debug.Log("|" + "4 : " + board[4] + "|" + "5 : " + board[5] + "|" + "6 : " + board[6] + "|");
        Debug.Log("|" + "7 : " + board[7] + "|" + "8 : " + board[8] + "|" + "9 : " + board[9] + "|");
    }

    int MiniMax(Dictionary<int, string> board, bool isMaximizer)
    {
        if (CheckWin(comp)) return 10;
        else if (CheckWin(player)) return -10;
        else if (CheckDraw()) return 0;

        if (isMaximizer)
        {
            int bestScore = -100;

            for (int i = 1; i <= board.Count; i++)
            {
                if (board[i] == " ")
                {
                    positions++;
                    board[i] = comp;
                    int score = MiniMax(board, false);
                    board[i] = " ";

                    if (score > bestScore)
                    {
                        bestScore = score;
                    }
                }
            }
            return bestScore;
        }
        else
        {
            int bestScore = 100;

            for (int i = 1; i <= board.Count; i++)
            {
                if (board[i] == " ")
                {
                    positions++;
                    board[i] = player;
                    int score = MiniMax(board, true);
                    board[i] = " ";

                    if (score < bestScore)
                    {
                        bestScore = score;
                    }
                }
            }
            return bestScore;
        }
    }

    bool SpaceIsFree(int pos) { return (board[pos] == " ") ? true : false; }

    public void InsertLetter(int pos)
    {
        if (SpaceIsFree(pos) && !gameOver)
        {
            string letter = playersTurn ? player : comp;
            board[pos] = letter;

            if (letter == "X") Instantiate(Cross, TileObj[pos - 1].transform.position, Quaternion.identity, TileObj[pos - 1].transform);
            else Instantiate(Circle, TileObj[pos - 1].transform.position, Quaternion.identity, TileObj[pos - 1].transform);

            if (CheckWin(letter))
            {
                gameOver = true;
                winnerTxt.enabled = true;
                winnerTxt.text = (((letter == "X") ? "Player" : "Computer") + " wins!");
                winnerTxt.GetComponent<Animator>().SetTrigger("Win");
            }
            else if (CheckDraw())
            {
                gameOver = true;
                winnerTxt.enabled = true;
                winnerTxt.text = "Draw!";
                winnerTxt.GetComponent<Animator>().SetTrigger("Win");
            }

            playersTurn = !playersTurn;
        }
    }

    bool CheckDraw()
    {
        foreach (var key in board.Keys)
        {
            if (board[key] == " ")
            {
                return false;
            }
        }
        return true;
    }

    bool CheckWin(string x)
    {
        //Horizontal
        for (int i = 1; i <= 3; i++)
        {
            if (board[i] == x && board[i + 3] == x && board[i + 6] == x)
            {
                return true;
            }
        }

        //Vertical
        for (int i = 1; i <= 9; i += 3)
        {
            if (board[i] == x && board[i + 1] == x && board[i + 2] == x)
            {
                return true;
            }
        }

        //Two Diagonals
        if (board[1] == x && board[5] == x && board[9] == x)
        {
            return true;
        }
        if (board[3] == x && board[5] == x && board[7] == x)
        {
            return true;
        }

        return false;
    }
}
