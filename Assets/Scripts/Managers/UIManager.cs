using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text gameOverText, startText, titleText;

    [SerializeField]
    private GameObject controlsCanvas;

    // Start is called before the first frame update

    void Start() {
        GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;

        gameOverText.enabled = false;
    }

    public void StartPressed(){
        GameEventManager.TriggerGameStart();
    }

    public void RestartPressed(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GameStart() {
        startText.enabled = false;
        titleText.enabled = false;
        controlsCanvas.SetActive(true);
    }

    private void GameOver() {
        gameOverText.enabled = true;
    }

    void OnDestroy(){
        GameEventManager.GameStart -= GameStart;
		GameEventManager.GameOver -= GameOver;
    }
}
