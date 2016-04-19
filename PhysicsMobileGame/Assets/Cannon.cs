using UnityEngine;
using System.Collections;
using System;

public class Cannon : MonoBehaviour {

    GameObject cannonBall;
    [Header("Power Settings")]
    public float powerX = 30.0f;
    public float powerY = 30.0f;

    public float rotateSpeed = 5f;
    


    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("up"))
        {
            if (powerX <= 100)
                powerX++;
        }

        if (Input.GetKeyDown("down"))
        {
            if (powerX >= 0)
                powerX--;
        }

        if (Input.GetKey("right"))
        {
            if (transform.rotation.eulerAngles.z > 0)
                transform.Rotate(0, 0, -rotateSpeed);
        }


        if (Input.GetKey("left"))
        {
            if(transform.rotation.eulerAngles.z < 40)
            transform.Rotate(0, 0, rotateSpeed);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Cannonballs();
        }
	}

    private void Cannonballs()
    {
        GameObject cannonballInstance;
        cannonballInstance = Instantiate( Resources.Load("CannonBall"), transform.position, Quaternion.identity) as GameObject;
        cannonballInstance.transform.Rotate(0, 0, 54);
        cannonballInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(powerX, powerY);
    }
}
