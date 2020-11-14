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
        private float distance;


		private Rigidbody rigid;

        // Use this for initialization
        void Start() { 
			rigid = player.GetComponent<Rigidbody>();
		}

        // Update is called once per frame
        void LateUpdate()
        {
            // Get forward vector minus the y component
			Vector3 vectorA = new Vector3(transform.forward.x, 0.0f, transform.forward.z);

			// Get target's velocity vector minus the y component
			Vector3 vectorB = new Vector3(rigid.velocity.x, 0.0f, rigid.velocity.z);

			// Find the angle between vectorA and vectorB
			float rotateAngle = Vector3.SignedAngle(vectorA.normalized, vectorB.normalized, Vector3.up);

			// Get the target's speed (maginitude) without the y component
			// Only set speed factor when vector A and B are almost facing the same direction
			float speedFactor = Vector3.Dot(vectorA, vectorB) > 0.0f ? vectorB.magnitude : 1.0f;

			// Rotate towards the angle between vectorA and vectorB
			// Use speedFactor so camera doesn't rotatate at a constant speed
			// Limit speedFactor to be between 1 and 2
			transform.Rotate(Vector3.up, rotateAngle * Mathf.Clamp(speedFactor, 1.0f, 2.0f) * Time.deltaTime);

			// Position the camera behind target at a distance of offset
			transform.position = player.transform.position - (transform.forward * distance);
			transform.position += Vector3.up * 3f;
			transform.LookAt(new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z));
        }
    }
}
