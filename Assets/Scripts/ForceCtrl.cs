using UnityEngine;
using System.Collections;

public class ForceCtrl : MonoBehaviour {

    Rigidbody _rbody;

	// Use this for initialization
	void Start () {
        _rbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Physics Update
    void FixedUpdate()
    {
        float h_move = Input.GetAxis( "Horizontal" );
        float v_move = Input.GetAxis( "Vertical" );

        Vector3 move = new Vector3( h_move, 0, v_move );
        _rbody.AddForce( move, ForceMode.Force );
        //if( move.magnitude > 0 ) {
        //    Debug.Log( Time.realtimeSinceStartup+"=>Apply force "+move );
        //}

        
    }
}
