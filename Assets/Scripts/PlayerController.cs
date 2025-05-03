using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float playerSpeed = 5f;
    float speed ;

    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
        speed = playerSpeed;
	    rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {
        float verticalSpeed = Input.GetAxis("Vertical") * speed * 100 * Time.deltaTime ;
        float horizontalSpeed = Input.GetAxis("Horizontal") * speed * 100 * Time.deltaTime;



        //rigidBody.velocity = new Vector2(horizontalSpeed, verticalSpeed);

        //Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        //transform.position += move * speed * Time.deltaTime;
    }
}
