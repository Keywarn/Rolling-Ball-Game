using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAudioCollision : MonoBehaviour
{
    [SerializeField]
    private GameObject trigger;

    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    private bool onlyOnce;
    private bool done;

    // Start is called before the first frame update
    void Start()
    {
        if(onlyOnce){
            done = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
  {
        if (col.gameObject == trigger && !done){
            Debug.Log("Playing Wind");
            audio.Play();
            if(onlyOnce){
                done = true;
            }
        }
    }
}
