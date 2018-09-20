using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    [SerializeField] GameObject lArm;
    [SerializeField] GameObject rArm;

    [SerializeField] float armMult;
    [SerializeField] float armReturnDistance;

    bool isLeftArm = true;

    [SerializeField] int caseStateLeft = 0;
    [SerializeField] int caseStateRight = 0;

    Vector3 moveStartPos;
    Vector3 moveEndPos;
    Vector3 moveVector;

    // Use this for initialization
    void Start () {
        MoveArm();
        isLeftArm = false;
        MoveArm();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            moveStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Start Pos " + moveStartPos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            moveEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("End Pos " + moveEndPos);

            moveVector = moveEndPos - moveStartPos;
            Debug.Log("Move Vector = " + moveVector);

            if (isLeftArm)
                caseStateLeft = 1;
            else
                caseStateRight = 1;

            MoveArm();
        }

        if (caseStateLeft == 2)
            GrabOrReturn(lArm);
        if (caseStateRight == 2)
            GrabOrReturn(rArm);

        if (caseStateLeft == 3)
            Grabbed(lArm);
        if (caseStateRight == 3)
            Grabbed(rArm);
    }

    void MoveArm()
    {
        //Debug.Log("MoveArm() called | affa=ect left arm? " + isLeftArm);
        if (isLeftArm)
        {
            switch (caseStateLeft)
            {
                case 1: //Move
                    Move(lArm);
                    break;

                case 2: //Grab or Return
                    GrabOrReturn(lArm);
                    break;

                case 3: //Grabbed
                    Grabbed(lArm);
                    break;

                default: //Idle
                    Idle(lArm);
                    break;
            }
        }

        else
        {
            switch (caseStateRight)
            {
                case 1: //Move
                    Move(rArm);
                    break;

                case 2: //Grab or Return
                    GrabOrReturn(rArm);
                    break;

                case 3: //Grabbed
                    Grabbed(rArm);
                    break;

                default: //Idle
                    Idle(rArm);
                    break;
            }
        }


        //if both arms are out, swap arms
        if(lArm.active && rArm.active)
        {
            isLeftArm = !isLeftArm;
        }
        //if only one arm is out, use the other arm
        if(lArm.active && !rArm.active)
        {
            isLeftArm = false;
        }
        if (!lArm.active && rArm.active)
        {
            isLeftArm = true;
        }
    }

    void Idle(GameObject arm)
    {
        arm.transform.position = transform.position;
        arm.SetActive(false);
    }

    void Move(GameObject arm)
    {
        arm.GetComponent<SpringJoint2D>().enabled = false;
        arm.GetComponent<HandGrab>().grabbed = false;
        arm.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        arm.transform.position = transform.position;
        //Activate the arm
        arm.SetActive(true);
        //Fire the arm in the desired direction
        arm.GetComponent<Rigidbody2D>().AddForce(moveVector * armMult, ForceMode2D.Impulse);

        
        if(arm.name == "HandL")
        {
            Debug.Log("Case1 called for " + arm.name);
            caseStateLeft = 2;
        }
        else
        {
            Debug.Log("Case1 called for " + arm.name);
            caseStateRight = 2;
        }
    }

    void GrabOrReturn(GameObject arm)
    {
        Debug.Log("Case2 called for " + arm.name);
        //Debug.Log("Distance from player to arm is " + Vector3.Distance(transform.position, arm.transform.position));
        if (!arm.GetComponent<HandGrab>().grabbed && Vector3.Distance(transform.position, arm.transform.position) > armReturnDistance)
        {
            Debug.Log("Arm out of range");
            Idle(arm);
            if (arm.name == "HandL")
                caseStateLeft = 0;
            else
                caseStateRight = 0;
        }
        else if (arm.GetComponent<HandGrab>().grabbed)
        {
            if (arm.name == "HandL")
                caseStateLeft = 3;
            else
                caseStateRight = 3;
        }
    }

    void Grabbed(GameObject arm)
    {
        arm.GetComponent<SpringJoint2D>().enabled = true;
    }
}
