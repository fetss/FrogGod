using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMent : MonoBehaviour
{
    public float movementSpeed = 3;
    public float jumpSpeed = 5;
    //CharacterController characterController;
    Vector3 velocity;
    Vector3 Gravity = new Vector3(0, -9.81f, 0);
    float gravityScale = 1;

    Rigidbody rid;

    public bool moveable = true;

    [SerializeField] bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rid = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (!moveable)
        {
            return;
        }

        Quaternion xQuaternionChange = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 10, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Vector3.Cross(-transform.right, Gravity), -Gravity), 0.1f) * xQuaternionChange;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!moveable)
        {
            return;
        }


        float x = Input.GetAxis("Horizontal") * movementSpeed;
        float y = Input.GetAxis("Vertical") * movementSpeed;

        transform.position += (transform.right * x + transform.forward * y) * Time.deltaTime;

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.2f, LayerMask.GetMask("Ground"));
        if (isGrounded)
        {
            velocity = transform.up * Input.GetAxis("Jump") * jumpSpeed;
        }
        
        velocity += Gravity * gravityScale * Time.fixedDeltaTime;
        rid.velocity = velocity;

        if (Input.GetAxis("Jump") > 0)
        {
            gravityScale = 0.7f;
        }
        else
        {
            gravityScale = 1;
        }
    }

    

}
