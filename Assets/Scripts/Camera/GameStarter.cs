using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private AudioSource play;

    [SerializeField]
    private Vector3 endPos;
    // Start is called before the first frame update

    private AsyncOperation loadingOperation;
    private Vector3 startPos;

    

    private bool done;
    private bool active;
    private float progress = 0;
    void Start()
    {
        startPos = transform.position;
        done = false;
        active = false;
        text.text = PlayerPrefs.GetInt("Highscore").ToString("000");
    }

    // Update is called once per frame
    void Update()
    {
        if(active) {
            progress += Time.deltaTime;

            Vector3 interpolatedPosition = Vector3.Lerp(startPos, endPos, progress/2f);
            transform.position = interpolatedPosition;
            if(progress >= 2f){
                done = true;
            }
        }
    }

    public void StartGame(){
        play.Play();
        active = true;
        StartCoroutine(LoadScene());

    }

    IEnumerator LoadScene()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Level");
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f && done)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
