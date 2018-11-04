using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MovePlayer : MonoBehaviour {
    //Speed Modifier
    [SerializeField] [Range(3, 7)] private float Speed;
    private float speedIncreaseLastTick;
    private float speedTime = 13;
    private float speedTimeLeft;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;

    //Player stats
    public Transform player;
    private int score, moveSpace, position;

    //Movement measurement
    private float time;
    private float maxDubbleTapTime, swipeDistanceX, swipeDistanceY, dragDistance, angle, delta;
    private Vector3 offset, currentPosition, movePosition, fp, lp;
    private Rigidbody rb;
    private Quaternion targetRotation;
    private int tapCount, laneNum, x, y, z;
    private bool changedJump = false;

    public string controlLocked = "n";
    public Animator animator;
    public Animation animation;

    // Use this for initialization
    void Start () {
        moveSpace = 8;
        position = 0;
        dragDistance = Screen.height * 15 / 100;
        tapCount = 0; //double tap = down
        targetRotation = transform.rotation;
        currentPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        movePosition = new Vector3(0, 0, 1);
        ResetValue();
        ResetGame();
    }

    private void FinishGame()
    {
        
    }

    private void ResetGame()
    {
        laneNum = 0;
        score = 0;
        time = 0;
        position = 0;
        //winText.text = "";
    }

    // Update is called once per frame
    void Update() {

        time += Time.deltaTime;
        currentPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        // movePosition = new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + 1);
        if (Input.touchCount > 0)
        {
            Touch touchZero = Input.GetTouch(0);

            if (touchZero.phase == TouchPhase.Began)
            {
                fp = touchZero.position;
                lp = touchZero.position;
            }
            if (touchZero.phase == TouchPhase.Moved)
            {
                lp = touchZero.position; // update last position
                swipeDistanceX = Mathf.Abs((lp.x - fp.x));
                swipeDistanceY = Mathf.Abs((lp.y - fp.y));
               
            }
            if (touchZero.phase == TouchPhase.Ended)
            {
                lp = touchZero.position;
                angle = Mathf.Atan2((lp.x - fp.x), (lp.y - fp.y)) * 57.2957795f;
                if (angle > 150 || angle < -150 && swipeDistanceY > 40)
                {
                    //SWIPE UP
                    y = moveSpace;
                }
                if (angle > 60 && angle < 120 && swipeDistanceX > 40 && laneNum>=0 && (controlLocked == "n"))
                {
                    //SWIPE RIGHT
                    laneNum -= 1;
                    x = -moveSpace;
                    controlLocked = "y";
                }
                if (angle < -60 && angle > -120 && swipeDistanceX > 40 && laneNum<1 && (controlLocked == "n"))
                {
                    //SWIPE LEFT
                    laneNum += 1;
                    x = moveSpace;
                    controlLocked = "y";
                }
                if (angle < 40 && angle > -40 && swipeDistanceY > 40)
                {
                    //SWIPE DOWN
                    y = moveSpace;
                }

            }
        }

        movePosition = new Vector3(x , y, z);
        transform.position += movePosition * Speed * Time.deltaTime;
        ResetValue();
    }

    public void stopMovePlayer()
    {
        animator.SetTrigger("isDeath");
        this.enabled = false; 
    }

    public void jumpMovePlayer()
    {
        //animator.SetTrigger("isJumping");
        if (changedJump == false){
            animator.SetTrigger("isJumping");
            changedJump = true;
            WaitJump();
            
            animator.ResetTrigger("isJumping");
            Debug.Log("reset");
        }
        
    }

    private IEnumerator WaitJump()
    {
        yield return new WaitForSeconds(5);
        
    }

    void ResetValue()
    {
        x = 0;
        y = 0;
        z = 1;
        controlLocked = "n";
    }

    void Direction(int typ)
    {
        switch (typ)
        {
            case 0:
                StartCoroutine(RotatePlayer(Vector3.up * 90, 0.8f));
                break;
            case 1:
                StartCoroutine(RotatePlayer(Vector3.up * -90, 0.8f));
                break;
            default:
                break;
        }
    }

    IEnumerator RotatePlayer(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
    }

}
