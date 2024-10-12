using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public ScoreText scoreText1, scoreText2, scoreText3, scoreText4;
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

    //SI le da clcik el evento onStartGame se invoca
    public void OnStartGameButtonClicked()
    {
        menuCanvas.enabled=false;
        onStartGame?.Invoke();
    }

    public void OnGameEnds(int winerId)
    {
        menuCanvas.enabled = true;
        winText.text = $"Player {winerId} wins!";
    }

}
