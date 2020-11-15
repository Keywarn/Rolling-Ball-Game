using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyAnim : MonoBehaviour
{

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform target;

    [SerializeField]
    private Transform mainCamera;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private GameObject player;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerMove>().Grounded() && !player.GetComponent<PlayerMove>().flying){
            transform.position = target.position + offset;
            transform.rotation = mainCamera.transform.rotation;
        }
        else if(!player.GetComponent<PlayerMove>().Grounded() && !player.GetComponent<PlayerMove>().flying){
            transform.position = target.position;
            transform.rotation = player.transform.rotation;
        }
        else if(player.GetComponent<PlayerMove>().flying) {
            transform.position = target.position + offset;
            transform.rotation = player.transform.rotation;
        }

    }

}
