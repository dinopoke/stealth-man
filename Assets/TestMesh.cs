using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TestMesh : MonoBehaviour {

    public GameObject visionCone;
    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;

	// Use this for initialization
	void Start () {
	
        mesh = new Mesh();

	}
	
	// Update is called once per frame
	void Update () {

        vertices = new List<Vector3>();
        triangles = new List<int>();
        vertices.Add(new Vector3(0,0,-1f));

        vertices.Add((Vector3) RotateVector(Vector2.up, 30 /2)  *  5);
        vertices.Add( (Vector3) RotateVector(Vector2.up , -30 /2) * 5);

        triangles.Add(0);
        triangles.Add(1);
        triangles.Add(2);
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();


        mesh.RecalculateNormals();

        visionCone.GetComponent<MeshFilter>().mesh = mesh; 
	}

    Vector2 RotateVector(Vector2 v, float degrees)
    {
         return Quaternion.Euler(0, 0, degrees) * v;
    }
}
