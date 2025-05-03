using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {

    private Rigidbody2D thisRigidbody;

    public GameObject navigation;
    Transform[] navpoints;

    public float moveSpeed = 5.0f;
    public float turnSpeed = 5.0f;

    public float pauseTime = 1f;

    int currentNavPointIndex = 1;
    Transform currentNavPoint;


	// Use this for initialization
	void Start () {
        thisRigidbody = GetComponent<Rigidbody2D>();


        navpoints = navigation.GetComponentsInChildren<Transform>();

        if (navpoints.Length > 1) { 
            currentNavPoint = navpoints[1];
        
            StartCoroutine(Patrol());
        }

	}
	
	// Update is called once per frame
	void FixedUpdate () {



	}


    IEnumerator Patrol () {

        while (true) {

            IEnumerator e = RotateTowards(currentNavPoint.position);
            while (e.MoveNext()){
                yield return e.Current;
		    }

            e = (MoveTowards(currentNavPoint.position));
            while (e.MoveNext()){
			    yield return e.Current;
		    }

            if (currentNavPointIndex < navpoints.Length - 1) {
                currentNavPointIndex = currentNavPointIndex + 1;
            }
            else{
                currentNavPointIndex = 1;
            }

            currentNavPoint = navpoints[currentNavPointIndex];
 
        }
    }




    

    IEnumerator RotateTowards(Vector3 location) {
        Quaternion newRotation = Quaternion.LookRotation(transform.position - location, Vector3.forward);
        newRotation.x = 0.0f;
        newRotation.y = 0.0f;
        Quaternion startRotation = transform.rotation;
        float increment = 0;

        while (!((transform.rotation.eulerAngles - newRotation.eulerAngles).magnitude < 0.5f)) {
            FreezeRigidBody();
            ++increment;
            transform.rotation = Quaternion.RotateTowards(startRotation, newRotation, increment* turnSpeed * Time.fixedDeltaTime * 20);

            yield return null;
        }
        yield return new WaitForSeconds(pauseTime);
    }

    IEnumerator MoveTowards(Vector2 location) {

        while(thisRigidbody.position != location) { 
            FreezeRigidBody();
            thisRigidbody.position = Vector2.MoveTowards(thisRigidbody.position, location, moveSpeed * Time.fixedDeltaTime);
            
            StartCoroutine(RotateTowardsSmooth(location));

            yield return null;
        }
        yield return new WaitForSeconds(pauseTime* 2);
    }

    void FreezeRigidBody() {
            thisRigidbody.angularVelocity = 0;
            thisRigidbody.velocity = new Vector2(0, 0);
    }
    

    IEnumerator RotateTowardsSmooth(Vector3 location) {
        Quaternion newRotation = Quaternion.LookRotation(transform.position - location, Vector3.forward);
        newRotation.x = 0.0f;
        newRotation.y = 0.0f;
        Quaternion startRotation = transform.rotation;
        float lerpValue = 0;
        while (true) { 
            thisRigidbody.angularVelocity = 0;
            thisRigidbody.velocity = new Vector2(0, 0);  
            lerpValue = lerpValue + (turnSpeed *Time.fixedDeltaTime / 20);
            if (lerpValue >= 1)
                break;
            transform.rotation = Quaternion.Slerp(startRotation, newRotation, lerpValue);
            yield return null;
        }
        
    }






        
    /// 
    ///  THESE FUNCTIONS ARE NOT CURRENTLY USED
    /// 

    IEnumerator MoveTowardsSmooth(Vector2 location) {

        Vector2 startPosition = thisRigidbody.position;
        float lerpValue = 0;

        while(true) { 
            thisRigidbody.angularVelocity = 0;
            thisRigidbody.velocity = new Vector2(0, 0);
            lerpValue = lerpValue + (moveSpeed*Time.fixedDeltaTime/ 10);
            if(lerpValue>=1)
                break;   
            thisRigidbody.position = Vector2.Lerp(startPosition, location, lerpValue);


            //rigidBody.velocity = (location - rigidBody.position) * Time.fixedDeltaTime *  moveSpeed * 10;
            
            StartCoroutine(RotateTowards(location));

            yield return null;
        }
        yield return new WaitForSeconds(pauseTime);
        
    }

    void RotateAtAngle (float angle) {

        transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, -angle), turnSpeed * Time.deltaTime);

    }

    void MoveForward () {
        thisRigidbody.AddForce(transform.up * moveSpeed * 100 * Time.deltaTime);
    }
}
