using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private float startTimer;

    public float time;

    private bool playing;
    [SerializeField]
    private AudioSource collect;
    
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.GameStart += GameStart;
    }

    void GameStart() {
        time = startTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time > 0){
            time -= Time.deltaTime;
        }
        else{
            GameEventManager.TriggerGameOver();
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "pickup"){
            Debug.Log("PICKUP");
            Destroy(col.gameObject);
            collect.Play();
        }
    }

        void OnDestroy(){
        GameEventManager.GameStart -= GameStart;
    }
}
