using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameUI gameUI;    
    public List<Movement> players;
    public List<Movement> remainingPlayers;

    public int loserScore = 0;
    public System.Action onReset;
   // public System.Action Dead;

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
        gameUI.UpdateImage();

        //CheckWin();
        CheckDied();
    }

    private void CheckDied()
    {
       for(int i=0;i<players.Count;i++)
        {
            if (players[i].healthPoints <=0)
            {
                players[i].healthPoints = 0;
                remainingPlayers.Remove(players[i]);
                players[i].isDead = true;
                players[i].Dead();
            }
        }
        CheckWin();
    }

    private void CheckWin()
    {       
        if(remainingPlayers.Count==1)
        {
            int WINNERId = players[0].isDead != true ? 1 : players[1].isDead != true ? 2 : players[2].isDead != true ? 3 : players[3].isDead != true ? 4 : 0;            
                gameUI.OnGameEnds(WINNERId); //Se manda el jugador que gano
                onReset?.Invoke();            
        }

    }

    private void OnStartGame()
    {
        //Reinicia vidas, el ScoreUI, y resetea toda la lista de RemainingPlayers
        players[0].healthPoints = 20;
        players[1].healthPoints = 20;
        players[2].healthPoints = 20;
        players[3].healthPoints = 20;
        
        gameUI.UpdateScores(players[0].healthPoints, players[1].healthPoints, players[2].healthPoints, players[3].healthPoints);
        
        remainingPlayers.Clear();
        
        remainingPlayers.Add(players[0]);
        remainingPlayers.Add(players[1]);
        remainingPlayers.Add(players[2]);
        remainingPlayers.Add(players[3]);
    }

}
