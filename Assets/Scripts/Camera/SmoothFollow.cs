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
		[SerializeField]
        private float height;

		[SerializeField]
		private float maxVerticalAngle;
		[SerializeField]
		private float maxHorizontalAngle;
		[SerializeField]
		private float tiltSpeed;

		[SerializeField]
    	private bool useFloorNormal;

		private float initialXRotation;


		private Rigidbody rigid;

        // Use this for initialization
        void Start() { 
			rigid = player.GetComponent<Rigidbody>();
			initialXRotation = transform.eulerAngles.x;
		}

		void Update() {
			if(!player.GetComponent<PlayerMove>().flying){
				CameraTilt();
			}
			else {
				CameraTilt();
			}
		}

		void CameraTilt()
    	{
			// Rotate camera container along the x axis when tilting the joystick up or down to give a forward and back tilt effect.
			// The further up the joystick is the higher the angle for target rotation will be and vice versa.
			float scaledVerticalTilt = initialXRotation - (SimpleInput.GetAxis("Vertical") * maxVerticalAngle);

			// Using floor normal adjust the rotation of the camera's x axis at rest.
			float angleBetweenFloorNormal = useFloorNormal ? Vector3.SignedAngle(Vector3.up, player.GetComponent<PlayerMove>().GetFloorNormal(), transform.right) : 0.0f;

			Quaternion targetXRotation = Quaternion.Euler(scaledVerticalTilt + angleBetweenFloorNormal, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetXRotation, tiltSpeed * Time.deltaTime);

			// Rotate camera along the z axis when tilting the joystick left or right to give a left and right tilt effect.
			// The further right the joystick is the higher the angle for target rotation will be and vice versa.
			float scaledHorizontalTilt = SimpleInput.GetAxis("Horizontal") * maxHorizontalAngle;

			Quaternion targetZRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, scaledHorizontalTilt);

			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetZRotation, tiltSpeed * Time.deltaTime);
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
			transform.position += Vector3.up * height;
			//transform.LookAt(new Vector3(player.transform.position.x,transform.position.y,player.transform.position.z));
			//transform.LookAt(player.transform.position);
        }
    }
}
