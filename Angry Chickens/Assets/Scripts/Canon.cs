using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// Author: Andrew Seba
/// Description: Canon controller and win condition.
/// </summary>
public class Canon : MonoBehaviour
{
    [Header("Power Settings")]
    public float power = 30.0f;
    public float minPower, maxPower;

    [Header("Rotation")]
    public float rotateSpeed = 5f;
    public float minRotation, maxRotation;

    [Header("Slider")]
    public Slider powerSlider;
    [Range(0.1f, 1f)]
    public float slideSpeed = 0.5f;

    [Header("Ammo")]
    public int ammo;
    public Image ammoMask;

    [HideInInspector]
    public int starCount;
    [HideInInspector]
    public int maxStars;

    private float cannonRotation = 49f;
    GameObject cannonBall;
    CameraFollow cameraFollow;
    SaveManager saveManager;
    bool ended = false;

    // Use this for initialization
    void Start()
    {
        GameObject SaveManagerObj;
        if (SaveManagerObj  = GameObject.Find("SaveManager"))
        {
            saveManager = SaveManagerObj.GetComponent<SaveManager>();
        }
        else
        {
            Debug.LogWarning("Couldn't Find Save Manager Save Disabled.");
        }
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        cannonBall = Resources.Load<GameObject>("CannonBall");
        maxStars = GameObject.FindGameObjectsWithTag("Star").Length;

        Debug.Log("You need to get " + maxStars + " star(s) to achieve 100% rating.");

        powerSlider.minValue = minPower;
        powerSlider.maxValue = maxPower;

        SetAmmoGraphic();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up"))
        {
            if (power <= 100)
                power += slideSpeed;
        }

        if (Input.GetKey("down"))
        {
            if (power >= 0)
                power -= slideSpeed;
        }

        powerSlider.value = power;

        if (Input.GetKey("right"))
        {
            if (cannonRotation > minRotation)
            {
                cannonRotation -= rotateSpeed;
                transform.Rotate(0, 0, -rotateSpeed);
            }
        }


        if (Input.GetKey("left"))
        {
            if (cannonRotation < maxRotation)
            {
                cannonRotation += rotateSpeed;
                transform.Rotate(0, 0, rotateSpeed);
            }
        }


        if (Input.GetButtonDown("Jump"))
        {
            Cannonballs();
        }

        
    }

    /// <summary>
    /// Gets win and loss condition.
    /// </summary>
    public void CheckForEnd()
    {
        if (ended)
            return;
        
        if (starCount > 0 && ammo <= 0 || starCount == maxStars)
        {
            ended = true;
            Debug.Log("Game Won");
            float percentWin = starCount / maxStars;

            if(saveManager != null)
                saveManager.SetLevelStars(percentWin);
        }
        else if (starCount <= 0 && ammo <= 0)
        {
            ended = true;
            Debug.Log("Game Lost");
        }
    }

    public void DestroyStar()
    {
        starCount++;
        if(starCount == maxStars)
        {
            Debug.Log("--Max Stars Reached--");
        }

        CheckForEnd();
    }

    /// <summary>
    /// Fires the cannon ball.
    /// </summary>
    private void Cannonballs()
    {
        if(ammo >= 1)
        {
            ammo--;
            GameObject cannonballInstance;
            cannonballInstance = Instantiate(cannonBall, transform.position, Quaternion.identity) as GameObject;
            cannonballInstance.transform.Rotate(0, 0, 54);
            cannonballInstance.GetComponent<Rigidbody2D>().velocity = 
                new Vector2(power * Mathf.Cos(cannonRotation * Mathf.Deg2Rad),
                            power * Mathf.Sin(cannonRotation * Mathf.Deg2Rad));

            SetAmmoGraphic();
            cameraFollow.FollowNew(cannonballInstance.transform);
        }
    }

    void SetAmmoGraphic()
    {
        ammoMask.fillAmount = (ammo * 0.2f);
    }
}
