using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class AgentManager : MonoBehaviour {

    public Transform[] spawnPts;
    public Transform attractionPt;
    public GameObject agent;
    public float deltaTimeSpawn;

    float _timeLastSpawn;
    int _nbSpawn;

	// Use this for initialization
	void Start () {
        _nbSpawn = 0;
        _timeLastSpawn = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        float curTime = Time.time;
        if( curTime - _timeLastSpawn > deltaTimeSpawn ) {
            // Spawn a new Agent
            int spawnPointIndex = Random.Range( 0, spawnPts.Length );

            GameObject newAgent = Instantiate( agent, spawnPts[spawnPointIndex].position, spawnPts[spawnPointIndex].rotation ) as GameObject;
            Assert.IsNotNull( newAgent );
            _nbSpawn += 1;
            newAgent.name = "Agent"+_nbSpawn;
            // Give him some initial speed
            Rigidbody rbdy_agent = newAgent.GetComponent<Rigidbody>();
            Assert.IsNotNull( rbdy_agent );
            rbdy_agent.velocity = spawnPts[spawnPointIndex].rotation * Vector3.forward * 3.0f;
            // and an attraction point
            var behaviorScript = newAgent.GetComponent<ForceCtrl>();
            Assert.IsNotNull( behaviorScript );
            behaviorScript.cible = attractionPt;

            _timeLastSpawn = curTime;
        }
	}
}
