using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    [SerializeField]
    private Transform[] pathPrefabs;

    [SerializeField]
    private Transform startPos;

    [SerializeField]
    private int numPaths;
    [SerializeField]
    private int removeDelay;

    private Transform nextPos;
    private Queue<Transform> pathQueue;
    private int paths = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameEventManager.GameStart += GameStart;
    }

    void GameStart() {
        pathQueue = new Queue<Transform>(numPaths);
        nextPos = startPos;
        for (int i = 0; i < numPaths; i++) {
			CreatePath();
		}
    }

    void OnTriggerExit(Collider col){
        if (col.gameObject.name == "End"){
            int finished = int.Parse(col.gameObject.transform.root.gameObject.name);
            Destroy(col);

            while(int.Parse(pathQueue.Peek().gameObject.name) < finished) {
                Destroy(pathQueue.Dequeue().gameObject);
                CreatePath();
            }
        }
    }

    void CreatePath(){
        Transform selectedPath = pathPrefabs[Random.Range(0, pathPrefabs.Length)];
        
        Transform path = (Transform)Instantiate(selectedPath, nextPos.position, Quaternion.Inverse(selectedPath.GetChild(0).Find("Start").rotation) * nextPos.rotation);
        path.gameObject.name = paths.ToString();
        paths ++;
        nextPos = path.GetChild(0).Find("End").transform;
        pathQueue.Enqueue(path);
    }

    void OnDestroy(){
        GameEventManager.GameStart -= GameStart;
    }
}
