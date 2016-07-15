using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Assertions;

[CustomEditor(typeof(ForceCtrl))]
public class ForceCtrlEditor : Editor {

    ForceCtrl _myCtrl;
    private Rigidbody _rbody;
    
    void OnEnable()
    {
        Debug.Log( "ForceCtrlEditor" );
        _myCtrl = target as ForceCtrl;
        _rbody = _myCtrl.GetComponent<Rigidbody>();
        Assert.IsNotNull( _rbody );
    }

	void OnSceneGUI()
    {
        // velocity
        if( _rbody.velocity.magnitude > 0.1 ) {
            Handles.color = Color.white;
            Handles.ArrowCap( 0, _rbody.position,
                Quaternion.LookRotation( _rbody.velocity ),
                _rbody.velocity.magnitude );
        }
        // desired velocity
        Vector3 _desiredVelocity = _myCtrl.DesiredVelocity;
        if( _desiredVelocity.magnitude > 0.1 ) {
            Handles.color = Color.red;
            Handles.ArrowCap( 0, _rbody.position,
                Quaternion.LookRotation( _desiredVelocity ),
                _desiredVelocity.magnitude );
        }

        //Debug.Log( Time.realtimeSinceStartup+"=>velocity "+_rbody.velocity );
    }
}
