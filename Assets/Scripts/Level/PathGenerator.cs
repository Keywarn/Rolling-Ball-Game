using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform pathPrefab;

    [SerializeField]
    private Vector3 startPos;

    [SerializeField]
    private int numPaths;
    [SerializeField]
    private int removeDelay;

    private Vector3 nextPos;
    private Queue<Transform> pathQueue;
    private int pathsFinished = 0;

    // Start is called before the first frame update
    void Start()
    {
        pathQueue = new Queue<Transform>(numPaths);
        nextPos = startPos;
        for (int i = 0; i < numPaths; i++) {
			CreatePath();
		}
    }

    void OnTriggerExit(Collider col){
        if (col.gameObject.tag == "pathEnd"){
            pathsFinished += 1;
            Destroy(col);
            if(pathsFinished == removeDelay){
                pathsFinished -= 1;
                Destroy(pathQueue.Dequeue().gameObject);
                CreatePath();
            }
        }
    }

    void CreatePath(){
        Transform path = (Transform)Instantiate(pathPrefab);
        path.localPosition = nextPos;
        nextPos = path.Find("End").transform.position;
        pathQueue.Enqueue(path);
    }
}
