using UnityEngine;
using System.Collections;

public class WaterDynamics : MonoBehaviour
{
    public Vector2 range = new Vector2(0.1f, 1);
    public float speed = 1;
    private float[] randomTimes;
    private Mesh mesh;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        int i = 0;
        randomTimes = new float[mesh.vertices.Length];

        while (i < mesh.vertices.Length)
        {
            randomTimes[i] = Random.Range(range.x, range.y);

            i++;
        }

    }

    private void Update()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;
        int i = 0;
        while (i < vertices.Length)
        {
            vertices[i].y = 1 * Mathf.PingPong(Time.time * speed, randomTimes[i]);
            i++;
        }
        mesh.vertices = vertices;
    }
}
