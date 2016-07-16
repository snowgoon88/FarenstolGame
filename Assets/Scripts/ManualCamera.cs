using UnityEngine;
using System.Collections;

public class ManualCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// LateUpdate is called once per frame, after every update
	void LateUpdate () {
        float h_move = Input.GetAxis( "Horizontal" );
        float v_move = Input.GetAxis( "Vertical" );

        Vector3 move = new Vector3( h_move, 0, v_move );
        transform.position = transform.position + move * 0.5f;
    }
}
