using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Star : MonoBehaviour {

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Floor")
        {
            GameObject.Find("Cannon").GetComponent<Canon>().DestroyStar();
            Destroy(gameObject);

        }

    }
}
