using UnityEngine;
using System.Collections;
using System;

public class ForceCtrl : MonoBehaviour {

    public Transform cible;

    Rigidbody _rbody;
    float _maxSpeed = 2.0f;
    float _maxForce = 10.0f;
    Vector3 _desiredVelocity;

    public Vector3 DesiredVelocity
    {
        get{return _desiredVelocity;}
    }

    // Use this for initialization
    void Start () {
        _rbody = GetComponent<Rigidbody>();
        _rbody.interpolation = RigidbodyInterpolation.Interpolate;
        // rotation is not controlled by Physics
        _rbody.freezeRotation = true;
	}
	
	// Update is called once per frame
    /**
     * Agent orients toward its velocity or, if zero, towards its desired velocity
     */
	void Update () {
        if( _rbody.velocity.magnitude > 0.05 ) {
            Quaternion newRotation = Quaternion.LookRotation( _rbody.velocity );
            _rbody.MoveRotation( newRotation );
        }
        else if( DesiredVelocity.magnitude > 0.05 ) {
            Quaternion newRotation = Quaternion.LookRotation( DesiredVelocity );
            _rbody.MoveRotation( newRotation );
        }
    }

    // Physics Update
    void FixedUpdate()
    {
        float h_move = Input.GetAxis( "Horizontal" );
        float v_move = Input.GetAxis( "Vertical" );

        Vector3 move = new Vector3( h_move, 0, v_move );
        Vector3 desiredForce = move + forceSeek();
        float intensity = Mathf.Max( _maxForce, desiredForce.magnitude );
        desiredForce = desiredForce.normalized * intensity;

        _rbody.AddForce( desiredForce, ForceMode.Force );
        //if( move.magnitude > 0 ) {
        //    Debug.Log( Time.realtimeSinceStartup+"=>Apply force "+move );
        //}

        
    }

    /**
     * Steering force to a target
     */
    Vector3 forceSeek()
    {
        // Max velocity to target
        _desiredVelocity = ( cible.position - _rbody.transform.position ).normalized * _maxSpeed;
        Vector3 force = _desiredVelocity - _rbody.velocity;

        return force;
    }

    /**
     * When collide with something.
     */
    // void OnCollisionEnter( Collision collision )
    //{
    //    Debug.Log( "Collision with "+collision.collider.name );
    //    // with a wall
    //    if( collision.collider.CompareTag( "WallTag" )) {
    //        string logMsg = "Hit house relvel="+collision.relativeVelocity+Environment.NewLine;
    //        logMsg +=       "          impuls="+collision.impulse+Environment.NewLine;
    //        logMsg +=       "          desVel="+_desiredVelocity;
    //        Debug.Log( logMsg );
    //        // Take normal of first contact
    //        var normal = collision.contacts[0].normal;
    //        var tangentForce = _desiredVelocity - Vector3.Dot( -_desiredVelocity, normal ) * normal;
    //        _rbody.AddForce( tangentForce );
    //    }
    //}
    void OnCollisionStay( Collision collision )
    {
        //foreach( ContactPoint contact in collision.contacts ) {
        //    print( contact.thisCollider.name + " hit " + contact.otherCollider.name );
        //    Debug.DrawRay( contact.point, contact.normal, Color.white );
        //}
        Debug.Log( "Collision with "+collision.collider.name );
        // with a wall
        if( collision.collider.CompareTag( "WallTag" ) ) {
            string logMsg = "Hit house relvel="+collision.relativeVelocity+Environment.NewLine;
            logMsg +=       "          impuls="+collision.impulse+Environment.NewLine;
            logMsg +=       "          desVel="+_desiredVelocity;
            Debug.Log( logMsg );
            // Take normal of first contact
            var normal = collision.contacts[0].normal;
            var tangentForce = _desiredVelocity + Vector3.Dot( -_desiredVelocity, normal ) * normal;
            _rbody.AddForce( tangentForce );
            Debug.DrawLine( _rbody.transform.position, _rbody.transform.position+DesiredVelocity, Color.red );
            Debug.DrawLine( _rbody.transform.position, _rbody.transform.position+tangentForce, Color.white );
            Debug.DrawLine( _rbody.transform.position, _rbody.transform.position+normal, Color.yellow );
        }
    }
}
