using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;


public class PlayerVisionCone : MonoBehaviour {


    public float viewDistance = 4.0f;
    public int viewAngle = 30;

    public GameObject visionCone;
    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;
    Color viewColor = Color.magenta;

    public float playerSpeed = 5f;
    float speed;
    
    int layerMask = 1 << 8;

	// Use this for initialization
	void Start () {
        speed = playerSpeed;
	    SetupViewMesh();
	}

    void SetupViewMesh() {
        mesh = new Mesh();
        vertices = new List<Vector3>();
        triangles = new List<int>();

    }
	// Update is called once per frame
	void FixedUpdate () {

        CheckMovement();
        CheckLineOfSight();

	}

    void CheckMovement() {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = playerSpeed * 4f;
        }
        else {
            speed = playerSpeed;

        }
    }

    void CheckLineOfSight() {

        ResetViewMesh();
        

        for (int i = -viewAngle /2; i <= viewAngle /2 ; i++) {     
            RaycastHit2D sightTest = Physics2D.Raycast(transform.position, RotateVector(transform.up, i), viewDistance, layerMask);
            if (sightTest.collider != null) {
                vertices.Add((Quaternion.Inverse(transform.rotation) * ((Vector3)sightTest.point - transform.position)) +  new Vector3(0,0,-2f));

                Debug.DrawLine(transform.position, sightTest.point, viewColor );
            }
            else {
                vertices.Add((Vector3)(RotateVector(Vector2.up, i) * viewDistance) + new Vector3(0,0,-2f));
                Debug.DrawRay(transform.position, RotateVector(transform.up, i) * viewDistance, viewColor);
            }
        }

        CreateMesh();

    }

    void SetViewMaterialColour(Color newColor) {
        visionCone.GetComponent<MeshRenderer>().material.color = newColor;
    }

    void ResetViewMesh() {
        vertices.Clear();
        triangles.Clear();
        vertices.Add(new Vector3(0,0,-2f));

    }

    void CreateMesh() {
        CreateTriangles();

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();


        mesh.RecalculateNormals();

        visionCone.GetComponent<MeshFilter>().mesh = mesh; 
    }

    void CreateTriangles() {
        for (int i = 0; i < vertices.Count - 1; i++) {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i);

        }

    }

    Vector2 RotateVector(Vector2 v, float degrees)
    {
         return Quaternion.Euler(0, 0, degrees) * v;
    }
}
