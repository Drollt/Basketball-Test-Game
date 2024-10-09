using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //Thiago Grossi esteve aqui...

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowMovement();
    }

    void FollowMovement()
    {
        transform.eulerAngles = new Vector3 (((target.transform.position.z * -1) + 20.5f), (target.transform.position.x), this.transform.rotation.z);
    }
}
