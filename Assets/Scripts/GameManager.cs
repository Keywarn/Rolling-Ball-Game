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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreText.text = score.ToString("000");
        
    }

    public void EndGame() {
        Debug.Log(PlayerPrefs.GetInt("Highscore"));
        if(score > PlayerPrefs.GetInt("Highscore")){
            PlayerPrefs.SetInt("Highscore", score);
            Debug.Log(PlayerPrefs.GetInt("Highscore").ToString("000"));
        }
        SceneManager.LoadScene("Level");
    }
}
