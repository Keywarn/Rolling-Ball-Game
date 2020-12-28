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

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startPos;
        for (int i = 0; i < numPaths; i++) {
			Transform obj = (Transform)Instantiate(pathPrefab);
			obj.localPosition = nextPos;
			nextPos = obj.Find("End").transform.position;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
