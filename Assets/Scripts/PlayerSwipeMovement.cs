using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwipeMovement : MonoBehaviour
{
    [SerializeField] float borderValue;
    [SerializeField] float speed;
    [SerializeField] float jumpHeight;
    [SerializeField] float jumpLimiter;
    [SerializeField] float fallSpeed;
    [SerializeField] float jumpPushDelay;
    [SerializeField] GameObject myAttack;
    [SerializeField] float myAttackDelay;
    [SerializeField] float attackSpeed;
    [SerializeField] GameObject attackSpot;
    private Vector3 firstTouchPosition;
    private Vector3 lastTouchPosition;
    private float border;
    int laneNumber;
    bool hasMoved = false;
    Rigidbody myBody;
    [SerializeField] GameObject myAttackHolder;
    GameObject attack;
    bool canAttack;

    void Start()
    {
        border = Screen.height * borderValue / 100;
        laneNumber = 2;
        myBody = gameObject.GetComponent<Rigidbody>();
        canAttack = true;
    }

    void Update()
    {
        MoveLeftRight();
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Right();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Left();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.LeftControl))
        {
            Attack();
        }
    }

    public  void Right() /// moves the player to the right
    {
        if (laneNumber == 3)
        {
            hasMoved = true;// cant go right
        }
        else
        {
            laneNumber += 1;
            gameObject.transform.Translate(0, 0, speed);
            hasMoved = true;
        }
    }

    public void Left() ///moves the play to the left
    {
        if (laneNumber == 1)
        {
            hasMoved = true; // cant go left
        }
        else
        {
            laneNumber -= 1; 
            gameObject.transform.Translate(0, 0, -speed);
            hasMoved = true;
        }
    }

    public void Attack()
    {
        if(canAttack == true)
        {
            canAttack = false;
            StartCoroutine(AttackTimer());
            attack = Instantiate(myAttack, attackSpot.transform.position, Quaternion.Euler(myAttack.transform.rotation.x, myAttack.transform.rotation.y, myAttack.transform.rotation.z)) as GameObject;
            attack.GetComponent<Rigidbody>().AddForce(attackSpeed, 0, 0);
            attack.transform.parent = myAttackHolder.transform;
        }
    }

    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(myAttackDelay);
        canAttack = true;
    }

    public void Jump() ///moves the player up
    {
        if(gameObject.transform.position.y <= jumpLimiter)
        {
            gameObject.transform.Translate(0, jumpHeight, 0);
            StartCoroutine(JumpPushDown());
        }
    }

    IEnumerator JumpPushDown()
    {
        yield return new WaitForSeconds(jumpPushDelay);
        myBody.AddForce(0, -fallSpeed, 0);
    }

    void MoveLeftRight()
    {
        if (Input.touchCount == 1)
        {
            Touch touch0 = Input.GetTouch(0);
            if (touch0.phase == TouchPhase.Began)
            {
                firstTouchPosition = touch0.position;
                lastTouchPosition = touch0.position;
            }
            else if (touch0.phase == TouchPhase.Moved && hasMoved == false)
            {
                lastTouchPosition = touch0.position;
                if (Mathf.Abs(lastTouchPosition.y - firstTouchPosition.y) > border || Mathf.Abs(lastTouchPosition.x - firstTouchPosition.x) > border)
                {
                    if (Mathf.Abs(lastTouchPosition.y - firstTouchPosition.y) > Mathf.Abs(lastTouchPosition.x - firstTouchPosition.x))
                    {
                        if (lastTouchPosition.y > firstTouchPosition.y) //did you swipe further up
                        {
                            Jump();
                        }
                        else
                        {
                            Attack();
                        }
                    }
                    else
                    {
                        if (Mathf.Abs(lastTouchPosition.x - firstTouchPosition.x) > border)
                        {
                            if (lastTouchPosition.x > firstTouchPosition.x) // did you swipe further right
                            {
                                Right();
                            }
                            else
                            {
                                Left();
                            }
                        }
                    }
                }
            }
            else if (touch0.phase == TouchPhase.Ended)
            {
                lastTouchPosition = touch0.position;

                if (Mathf.Abs(lastTouchPosition.y - firstTouchPosition.y) > border || Mathf.Abs(lastTouchPosition.x - firstTouchPosition.x) > border)
                {
                    hasMoved = false;
                }
                else
                {
                    Debug.Log("Tap");
                    Attack();
                    //if (gameObject.transform.position == new Vector3(0, 0, 0))
                    //{
                    //    laneNumber = 2;
                    //    hasMoved = false;
                    //    gameObject.transform.position = new Vector3(Random.Range(-3, 3), Random.Range(-6, 6), 0);
                    //}
                    //else
                    //{
                    //    gameObject.transform.position = new Vector3(0, 0, 0);
                    //    hasMoved = false;

                    //}
                }
            }

        }
    }


}
