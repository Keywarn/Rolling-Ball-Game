using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float offset;
    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.GameOver += GameOver;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player.GetComponent<PlayerMove>().Grounded()){
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y - offset, player.transform.position.z);
        }
    }

    void OnTriggerExit(Collider col){
        if (col.gameObject.tag == "Player"){
            GameEventManager.TriggerGameOver();
        }
    }

    void GameOver() {
        Destroy(this);
    }
}
