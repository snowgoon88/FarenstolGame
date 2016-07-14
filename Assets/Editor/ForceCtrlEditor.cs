using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Assertions;

[CustomEditor(typeof(ForceCtrl))]
public class ForceCtrlEditor : Editor {

    private Rigidbody _rbody;

    void OnEnable()
    {
        Debug.Log( "ForceCtrlEditor" );
        ForceCtrl myCtrl = target as ForceCtrl;
        _rbody = myCtrl.GetComponent<Rigidbody>();
        Assert.IsNotNull( _rbody );
    }

	void OnSceneGUI()
    {
        UnityEditor.Handles.color = Color.white;
        UnityEditor.Handles.ArrowCap( 0, _rbody.position,
            Quaternion.LookRotation( _rbody.velocity ),
            _rbody.velocity.magnitude*10 );
        //Debug.Log( Time.realtimeSinceStartup+"=>velocity "+_rbody.velocity );
    }
}
