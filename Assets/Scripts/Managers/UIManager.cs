using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

    public void StartPressed(){
        GameEventManager.TriggerGameStart();
    }

    private void GameStart() {
        startText.enabled = false;
        titleText.enabled = false;
        controlsCanvas.SetActive(true);
    }

    private void GameOver() {
        gameOverText.enabled = true;
    }
}
