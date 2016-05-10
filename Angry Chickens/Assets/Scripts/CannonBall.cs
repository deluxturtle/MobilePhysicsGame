using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Andrew Seba
/// Description: Checks to see if cannonball is moving then if its stopped
/// check for end game.
/// </summary>
public class CannonBall : MonoBehaviour {

    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);
        if(screenPos.y > 1 || screenPos.y < 0|| screenPos.x > 1 || screenPos.x < 0)
        {
            GameObject.Find("Main Camera").GetComponent<CameraFollow>().targetFollow = GameObject.Find("Cannon").transform;
            Destroy(gameObject);
        }
    }
}
