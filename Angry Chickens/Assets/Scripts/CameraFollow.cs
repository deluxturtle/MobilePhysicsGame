using UnityEngine;
using System.Collections;



public class CameraFollow : MonoBehaviour
{

    public Transform targetFollow;

    public float xMargin = 1.5f;

    public float yMargin = 1.5f;

    public float xSmooth = 1.5f;

    public float ySmooth = 1.5f;

    private Vector2 maxXandY;

    private Vector2 minXandY;

    public GameObject cannon;


    void Awake()
    {
        if (targetFollow == null)
        {
            targetFollow = GameObject.FindWithTag("Player").transform;
            if(targetFollow == null)
                Debug.LogError("Player object not found. Please assign or tag your cannon");
        }

        if(cannon == null)
        {
            cannon = GameObject.Find("Cannon");
        }


        var backgroundBounds = GameObject.Find("Background").GetComponent<SpriteRenderer>().bounds;

        //Getting the bounds for the background in WORLD size
        foreach (GameObject background in GameObject.FindGameObjectsWithTag("Background"))
        {
            backgroundBounds.Encapsulate(background.GetComponent<SpriteRenderer>().bounds);
        }

        //get the viewable bounds of the camera in WORLD size
        var cameraTopLeft = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        var CameraBottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, 0f));

        minXandY.x = backgroundBounds.min.x - cameraTopLeft.x;
        maxXandY.x = backgroundBounds.max.x - CameraBottomRight.x;

        minXandY.y = backgroundBounds.min.y + CameraBottomRight.y;
        maxXandY.y = backgroundBounds.max.y + cameraTopLeft.y;
    }

    void LateUpdate()
    {
        float targetX = transform.position.x;
        float targetY = transform.position.y;

        if (CheckXMargin())
        {
            //Lerp between the current position and the player position, using xSmooth.
            targetX = Mathf.Lerp(transform.position.x, targetFollow.position.x, xSmooth * Time.fixedDeltaTime);
        }
        if (CheckYMargin())
        {
            targetY = Mathf.Lerp(transform.position.y, targetFollow.position.y, ySmooth * Time.fixedDeltaTime);
        }

        targetX = Mathf.Clamp(targetX, minXandY.x, maxXandY.x);
        targetY = Mathf.Clamp(targetY, minXandY.y, maxXandY.y);

        transform.position = new Vector3(targetX, targetY, -10);
    }

    public void FollowNew(Transform target)
    {
        targetFollow = target;
        CannonBall ball = target.GetComponent<CannonBall>();
        if (ball != null)
        {
            StartCoroutine("CheckForSleep", ball.GetComponent<Rigidbody2D>());
        }
    }

    IEnumerator CheckForSleep(Rigidbody2D rigid)
    {
        while (true)
        {
            if (rigid == null)
            {
                Invoke("ReturnToCannon", 1f);
                break;
            }
            if (rigid != targetFollow.GetComponent<Rigidbody2D>())
            {
                break;
            }
            else if (rigid.IsSleeping())
            {
                if(cannon != null)
                {
                    Invoke("ReturnToCannon", 1f);
                }
                break;
            }
            yield return null;
        }
        
    }

    void ReturnToCannon()
    {
        FollowNew(cannon.transform);
        cannon.GetComponent<Canon>().CheckForEnd();
    }


    /// <summary>
    /// Check if the player has moved near the edge of the camera bounds.
    /// </summary>
    /// <returns>If the player has moved near the X edge of the camera bounds.</returns>
    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - targetFollow.position.x) > xMargin;
    }

    bool CheckYMargin()
    {
        return Mathf.Abs(transform.position.y - targetFollow.position.y) > yMargin;
    }
}
