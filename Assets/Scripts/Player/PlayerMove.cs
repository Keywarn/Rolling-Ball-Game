using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rigid;
    

    [SerializeField]
    private float rollSpeed = 50;

    [SerializeField]
    private GameObject mainCamera;
    [SerializeField]
    private LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        //Check for ground
        if(Grounded()) {
            Vector3 floorNormal = GetFloorNormal();
            
             // Slow down when no input recieved
            if (Input.GetAxis("Vertical") == 0.0f && Input.GetAxis("Horizontal") == 0.0f && rigid.velocity.magnitude > 0.0f){
                rigid.velocity = Vector3.Lerp(rigid.velocity, Vector3.zero, rollSpeed * 0.1f * Time.deltaTime);
            }

            else {
                Vector3 forward = Vector3.Cross(mainCamera.transform.right, floorNormal);
                Vector3 forwardApply = forward * Input.GetAxis("Vertical");
                Vector3 rightApply = Input.GetAxis("Horizontal") * mainCamera.transform.right;
                Debug.DrawLine(transform.position, transform.position + ((forwardApply + rightApply) * rollSpeed), Color.white, 0.5f);
                //Zoom
                rigid.AddForce((forwardApply + rightApply) * rollSpeed);
            }
        }
    }

    public bool Grounded()
    {
        return Physics.CheckSphere(transform.position - (Vector3.up * 0.5f), 1, ground);
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
