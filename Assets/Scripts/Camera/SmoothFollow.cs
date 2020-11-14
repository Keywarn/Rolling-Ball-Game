using UnityEngine;

namespace UnityStandardAssets.Utility
{
    public class SmoothFollow : MonoBehaviour
    {

        // The target we are following
        [SerializeField]
        private GameObject player;
        // The distance in the x-z plane to the target
        [SerializeField]
        private Vector3 offset;

        // Use this for initialization
        void Start() { 
			offset = transform.position - player.transform.position;
		}

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = player.transform.position + offset;
        }
    }
}
