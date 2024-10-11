using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int scorePlayer1,scorePlayer2,scorePlayer3,scorePlayer4; //Setear a 20 en el Inspector
    public ScoreText scoreText1,scoreText2,scoreText3,scoreText4;
    //Cada vez que un gol toque una portería con un ID similar al jugador se le resta un punto a ese jugador 
    public void OnScoreZoneReached(int id)
    {
        if (id == 1)
            scorePlayer1--;
        if(id==2)
            scorePlayer2--;
        if(id==3)
            scorePlayer3--;
        if(id==4)
            scorePlayer4--;

        UpdateScores();
    }

    private void UpdateScores()
    {
        scoreText1.SetScore(scorePlayer1);
        scoreText2.SetScore(scorePlayer2);
        scoreText3.SetScore(scorePlayer3);
        scoreText4.SetScore(scorePlayer4);
    }
}
