using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Thiago Grossi esteve aqui...

    Rigidbody rb;
    public SpriteRenderer spriteRenderer;

    Vector3 direction;
    public float walkSpeed, jumpForce;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
    }

    void Movement()
    {
        if(isGrounded)
        {
            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

            //set walk based on direction
            rb.velocity = new Vector3(direction.x * walkSpeed, rb.velocity.y, direction.z * walkSpeed);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void GetSpriteDirection()
    {
        //Colocar as animações aqui conforme sua direção

        if (direction.z > 0)//north
        {
            if (Mathf.Abs(direction.x) > 0)//east or west
            {

            }
            else//neutral x
            {

            }
        }
        else if (direction.z < 0)//south
        {
            if (Mathf.Abs(direction.x) > 0)//east or west
            {

            }
            else//neutral x
            {

            }
        }
        else //neutrol
        {
            if (Mathf.Abs(direction.x) > 0)//east or west
            {

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Floor")
        {
            isGrounded = false;
        }
    }
}
