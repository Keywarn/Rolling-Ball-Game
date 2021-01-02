using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rigid;
    

    [SerializeField]
    private float rollSpeed = 50;
    [SerializeField]
    private float reduceSpeed = 10;

    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private LayerMask ground;

    [SerializeField]
    private AudioSource rolling;

    private float rollInterval = 5f;
    private float rollPassed;

    // Start is called before the first frame update
    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
    }

    void Update(){       

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Check for ground
        if(Grounded()) {
            Vector3 floorNormal = GetFloorNormal();
            //Rolling Noise
            rollPassed += Time.deltaTime;
            if(rollPassed >= rollInterval / rigid.velocity.magnitude){
                rolling.Play();
                rollPassed = 0f;
            }
             // Slow down when no input recieved
            if (SimpleInput.GetAxis("Vertical") == 0.0f && SimpleInput.GetAxis("Horizontal") == 0.0f && rigid.velocity.magnitude > 0.0f){
                rigid.velocity = Vector3.Lerp(rigid.velocity, Vector3.zero, reduceSpeed * 0.1f * Time.deltaTime);
            }

            else {
                Vector3 forward = Vector3.Cross(mainCamera.transform.right, floorNormal);
                Vector3 forwardApply = forward * SimpleInput.GetAxis("Vertical");
                Vector3 rightApply = SimpleInput.GetAxis("Horizontal") * mainCamera.transform.right;
                //Zoom
                rigid.AddForce((forwardApply + rightApply) * rollSpeed);

                
            }
        }
    }
    
    public bool Grounded()
    {
        return Physics.CheckSphere(transform.position - (Vector3.up * 0.45f), 1, ground);
    }

    public Vector3 GetFloorNormal()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, ground))
        {
            return(hit.normal);
        }
        else return(Vector3.zero);
    }
}
