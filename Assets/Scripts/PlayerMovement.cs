using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject hand1;
    [SerializeField] GameObject hand2;

    bool hand1Active = false;
    bool hand2Active = false;
    void ThrowHand()
    {
        Vector3 clickStartPos;
        Vector3 clickEndPos;
        bool setStart = false;
        if (Input.GetButtonDown("Fire1"))
            if(setStart == false)
            {
                clickStartPos = Input.mousePosition;
                setStart = true;
            }
        if (Input.GetButtonUp("Fire1"))
        {
            clickEndPos = Input.mousePosition;
            if(hand1Active == false)
            {
                hand1.transform.LookAt(clickEndPos);
            }
        }

    }

}
