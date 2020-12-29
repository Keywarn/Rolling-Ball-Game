using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform pathPrefab;
    [SerializeField]
    private int numPaths;

    [SerializeField]
    private Vector3 startPos;

    private Vector3 nextPos;
    private Queue<Transform> pathQueue;
    private int pathsFinished = 0;

    // Start is called before the first frame update
    void Start()
    {
        pathQueue = new Queue<Transform>(numPaths);
        nextPos = startPos;
        for (int i = 0; i < numPaths; i++) {
			Transform path = (Transform)Instantiate(pathPrefab);
			path.localPosition = nextPos;
			nextPos = path.Find("End").transform.position;
            pathQueue.Enqueue(path);
		}
    }

    void OnTriggerExit(Collider col){
        if (col.gameObject.tag == "pathEnd"){
            pathsFinished += 1;
            Destroy(col);
            Debug.Log(pathsFinished);
        }
    }
}
