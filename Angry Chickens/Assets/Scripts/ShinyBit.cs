using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.net
/// 
/// Description: ShinyBit 
/// </summary>
public class ShinyBit : MonoBehaviour {


	#region Fields

	#endregion

	public void OnCollisionEnter2D( Collision2D collision ) {

		if (collision.gameObject.tag == "Floor")
        {
			GameObject.Find("Cannon").GetComponent<Konan>().DestroyStar();
            Destroy(gameObject);

        }

    }
}
