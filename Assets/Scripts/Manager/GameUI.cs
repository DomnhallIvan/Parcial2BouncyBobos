using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public ScoreText scoreText1, scoreText2, scoreText3, scoreText4;
    [SerializeField] private Movement[] players;
    [SerializeField] private List<Image> playerImage;
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private TextMeshProUGUI winText;

    public System.Action onStartGame;




    public void UpdateScores(int scorePlayer1, int scorePlayer2, int scorePlayer3, int scorePlayer4)
    {
        scoreText1.SetScore(scorePlayer1);
        scoreText2.SetScore(scorePlayer2);
        scoreText3.SetScore(scorePlayer3);
        scoreText4.SetScore(scorePlayer4);
        
    }

    public void UpdateImage()
    {
        if (players[0].healthPoints <= 10)
        {
            playerImage[0].sprite = players[0].sadFace;
        }
        else if (players[1].healthPoints <= 10)
        {
            playerImage[1].sprite = players[1].sadFace;
        }
        if (players[2].healthPoints <= 10)
        {
            playerImage[2].sprite = players[2].sadFace;
        }
        if (players[3].healthPoints <= 10)
        {
            playerImage[3].sprite = players[3].sadFace;
        }
    }

    public void ResetImage()
    {
        playerImage[0].sprite = players[0].happyFace;
        playerImage[1].sprite = players[1].happyFace;
        playerImage[2].sprite = players[2].happyFace;
        playerImage[3].sprite = players[3].happyFace;
    }

    //SI le da clcik el evento onStartGame se invoca
    public void OnStartGameButtonClicked()
    {
        menuCanvas.enabled=false;
        ResetImage();
        onStartGame?.Invoke();
    }

    public void OnGameEnds(int winerId)
    {
        menuCanvas.enabled = true;
        winText.text = $"Player {winerId} wins!";
    }

}
