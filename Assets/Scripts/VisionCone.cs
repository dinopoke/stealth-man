using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VisionCone : MonoBehaviour {


    public float viewDistance = 3.0f;
    public int viewAngle = 30;

    public GameObject visionCone;
    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;
    Color viewColor = Color.magenta;
	// Use this for initialization
	void Start () {
	    SetupViewMesh();
	}

    void SetupViewMesh() {
        mesh = new Mesh();
        vertices = new List<Vector3>();
        triangles = new List<int>();

    }
	// Update is called once per frame
	void FixedUpdate () {
	        CheckLineOfSight();
	}

    void CheckLineOfSight() {

        bool playerFound = false;

        ResetViewMesh();

        for (int i = -viewAngle /2; i <= viewAngle /2 ; i++) {     
            RaycastHit2D sightTest = Physics2D.Raycast(transform.position, RotateVector(transform.up, i), viewDistance);
            if (sightTest.collider != null) {
                if (sightTest.collider.gameObject.CompareTag("Player")) {
                    playerFound = true;
                }
                vertices.Add((Quaternion.Inverse(transform.rotation) * ((Vector3)sightTest.point - transform.position)) +  new Vector3(0,0,-1f));


                Debug.DrawLine(transform.position, sightTest.point, viewColor );
            }
            else {
                vertices.Add((Vector3)(RotateVector(Vector2.up, i) * viewDistance) + new Vector3(0,0,-1f));
                Debug.DrawRay(transform.position, RotateVector(transform.up, i) * viewDistance, viewColor);
            }
        }

        CreateMesh();

        if (playerFound) {
            SetViewMaterialColour(visionCone.GetComponent<MeshRenderer>().material.color + new Color(0.1f, 0, 0));
        }
        else
        {
            SetViewMaterialColour(Color.yellow);
        }
    }

    void SetViewMaterialColour(Color newColor) {
        visionCone.GetComponent<MeshRenderer>().material.color = newColor;
    }

    void ResetViewMesh() {
        vertices.Clear();
        triangles.Clear();
        vertices.Add(new Vector3(0,0,-1f));

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
