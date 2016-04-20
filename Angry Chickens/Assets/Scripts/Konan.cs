using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Net;

/// <summary>
/// Author: Matt Gipson
/// Contact: Deadwynn@gmail.com
/// Domain: www.livingvalkyrie.net
/// 
/// Description: Konan 
/// </summary>
public class Konan : MonoBehaviour {
	#region Fields

	GameObject ballPrefab;

	[Header("Power")]
	public float power = 30;
	public float minPower, maxPower;

	//public float cannonBallRotation = 54f;

	[Header("Rotation")]
	public float cannonRotation = 49f;
	public float minRot, maxRot;
	public float rotationStep = 10f;

	[Header("Slider")]
	public Slider powerSlider;

	[Header("Ammo")]
	public int ammo;
	public Image ammoMask;

	[Header("Stars")]
	public int starCount;

	public int maxStars;

	#endregion

	void Start() {
		ballPrefab = Resources.Load<GameObject>("CannonBall");
		powerSlider.minValue = minPower;
		powerSlider.maxValue = maxPower;

		ammo = 5;

		maxStars = GameObject.FindGameObjectsWithTag("Star").Length;
	}

	void Update() {
		if (Input.GetKey(KeyCode.W)) {
			//transform.RotateAround(Vector3.forward, 10f);
			if (cannonRotation < maxRot) {
				//rotate Up
				cannonRotation += rotationStep;
				transform.Rotate(Vector3.forward * rotationStep);
			}
		}

		if (Input.GetKey(KeyCode.S)) {
			if (cannonRotation > minRot) {
				//rotate down
				cannonRotation -= rotationStep;
				transform.Rotate(Vector3.back * rotationStep);
			}
		}

		if (Input.GetKey(KeyCode.D)) {
			if (power <= maxPower) {
				power++;
			}
		}

		if (Input.GetKey(KeyCode.A)) {
			if (power >= minPower) {
				power--;
			}
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			Fire();
		}

		powerSlider.value = power;

		if (starCount <= 0 && ammo <= 0)
        {
			print("Game Won");
		}
        else if (starCount > 0 && ammo <= 0)
        {
			print("Game Lost");
		}
	}

	public void DestroyStar() {
		starCount++;
		if (starCount == maxStars) {
			print("you win");
		}
	}

	void Fire() {
		if (ammo >= 1) {
			ammo--;
			GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity) as GameObject;
			ball.transform.Rotate(0, 0, 54);
			ball.GetComponent<Rigidbody2D>().velocity = new Vector2(power * Mathf.Cos(cannonRotation * Mathf.Deg2Rad),
			                                                        power * Mathf.Sin(cannonRotation * Mathf.Deg2Rad));
			ammoMask.fillAmount = (ammo *  0.2f);
		}
	}
}