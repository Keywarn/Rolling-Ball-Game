using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private float startTimer;

    [SerializeField]
    private float maxTimer;

    [SerializeField]
    private float timerGain;

    private float liveTime;
    private float time;

    private bool playing;
    [SerializeField]
    private AudioSource collect;
    
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
    }

    void GameStart() {
        liveTime = startTimer;
        playing = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(playing){
            time += Time.deltaTime;
            if (liveTime > 0){
                liveTime -= Time.deltaTime;
            }
            else{
                GameEventManager.TriggerGameOver();
            }
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "pickup"){
            Destroy(col.gameObject);
            collect.Play();
            liveTime = Mathf.Min(liveTime + timerGain, maxTimer);
        }
    }

    void GameOver() {
        playing = false;
        print(time);
        //Update score
    }

    void OnDestroy(){
        GameEventManager.GameStart -= GameStart;
        GameEventManager.GameOver -= GameOver;
    }
}
