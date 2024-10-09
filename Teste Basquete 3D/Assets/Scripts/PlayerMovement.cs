using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Thiago Grossi esteve aqui...

    public Rigidbody body;

    public SpriteRenderer spriteRenderer;

    public float walkSpeed;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

        //set walk based on direction
        body.velocity = direction * walkSpeed;
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

}
