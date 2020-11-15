using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score;

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private GameObject UICanvas;

    [SerializeField]
    private GameObject EndGameCanvas;

    [SerializeField]
    private Text scoreResult;

    [SerializeField]
    private GameObject newHighText;
    [SerializeField]
    private Text highText;
    [SerializeField]
    private Text msgText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreText.text = score.ToString("000");
        
    }

    public void EndGame(bool win) {
        bool newHigh = false;
        string msg = "You Win!";
        if(!win){
            score = 0;
            msg = "You Lose!";
        }
        if(score > PlayerPrefs.GetInt("Highscore")){
            PlayerPrefs.SetInt("Highscore", score);
            newHigh = true;
        }
        UICanvas.SetActive(false);

        scoreResult.text = "score - " + score.ToString("000");
        if (newHigh) {
            newHighText.SetActive(true);
        }
        highText.text = "highscore - " + PlayerPrefs.GetInt("Highscore").ToString("000");
        msgText.text = msg;
        EndGameCanvas.SetActive(true);

    }

    public void LoadAgain() {
        SceneManager.LoadScene("Level");
    }
    public void LoadMenu() {
        SceneManager.LoadScene("Menu");
    }
}

