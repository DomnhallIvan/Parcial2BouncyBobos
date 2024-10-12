using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameUI gameUI;    
    public Movement[] players;

    public int loserScore = 0;
    public System.Action onReset;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            gameUI.onStartGame += OnStartGame;
        }
    }


    //Cada vez que un gol toque una portería con un ID similar al jugador se le resta un punto a ese jugador 
    public void OnScoreZoneReached(int id)
    {
        if (id == 1)
            players[0].healthPoints--;
        if(id==2)
            players[1].healthPoints--;
        if(id==3)
            players[2].healthPoints--;
        if (id==4)
            players[3].healthPoints--;

        gameUI.UpdateScores(players[0].healthPoints, players[1].healthPoints, 
            players[2].healthPoints, players[3].healthPoints);

        //CheckWin();
        CheckDied();
    }

    private void CheckDied()
    {
        if (players[0].healthPoints==0)
        {
            players[0].healthPoints+=0;
            players[0].isDead = true;

        }else if (players[1].healthPoints==0)
        {
            players[1].healthPoints += 0;
            players[1].isDead = true;
        }else if (players[2].healthPoints==0)
        {
            players[2].healthPoints += 0;
            players[2].isDead = true;
        }else if (players[3].healthPoints==0)
        {
            players[3].healthPoints += 0;
            players[3].isDead = true;
        }
    }

    private void CheckWin()
    {
        int LoserId = players[0].healthPoints == loserScore ? 1 : players[1].healthPoints == loserScore ? 2 : players[2].healthPoints == loserScore?3: players[3].healthPoints == loserScore?4:0;

        if(LoserId!=0)
        {
            //we have a loser!

            //gameUI.OnGameEnds(LoserId);
        }
        else
        {
            onReset?.Invoke();
        }
    }

    private void OnStartGame()
    {
        players[0].healthPoints = 20;
        players[1].healthPoints = 20;
        players[2].healthPoints = 20;
        players[3].healthPoints = 20;
        gameUI.UpdateScores(players[0].healthPoints, players[1].healthPoints, players[2].healthPoints, players[3].healthPoints);
    }

}
