using UnityEngine;
using System.Collections;

public class FollowingCamera : MonoBehaviour {

    public GameObject target;
    private Vector3 _offset;

    // Use this for initialization
    void Start ()
    {
        // Initial Offset
        _offset = transform.position - target.transform.position;
    }
	
	// LastUpdate is called once per frame, after everyone updated
	void LateUpdate ()
    {
        transform.position = target.transform.position + _offset;
	}
}
