using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrab : MonoBehaviour {

    public bool grabbed = false;

    Vector2 grabPos;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(grabbed)
        {
            transform.SetPositionAndRotation(grabPos, transform.rotation);
        }
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision");
        if(col.gameObject.tag == "Wall")
        {
            grabPos = transform.position;
            grabbed = true;
        }
    }
}