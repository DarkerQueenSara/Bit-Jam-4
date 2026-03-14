using UnityEngine;

[ExecuteAlways]
public class Spline : MonoBehaviour
{
    [SerializeField] Node[] nodes;
    [SerializeField, Range(0, 1)] float f;
    [SerializeField] float speed, prev;
    [SerializeField] int resolution = 20;
    [SerializeField] GameObject nodePrefab;

    [SerializeField] float roadWidth = 1f;
    [SerializeField] Transform cart;
    Mesh mesh;
    Vector3[] mapping;

    void Update()
    {
        if (nodes.Length < 2) return;
        mapping = new Vector3[nodes.Length * resolution];

        int index = 0;
        for (int i = 0; i < nodes.Length; i++)
        {
            Node A = nodes[i];
            Node B = nodes[(i + 1) % nodes.Length];

            for (int r = 0; r < resolution; r++)
            {
                float t = r / (float)(resolution - 1);
                mapping[index++] = JoinNodes(A, B, t);
            }
        }

        GenerateMesh();

        if (Application.isPlaying)  f += speed * Time.deltaTime;
        if (f >= 1) f -= 1;

        cart.position = CalcPosition(f);
        cart.rotation = CalcRotation(f + speed * Time.deltaTime * prev);
    }

    Vector3 CalcPosition(float fac)
    {
        float total = fac * nodes.Length;
        int section = Mathf.FloorToInt(total);
        float factor = total - section;
        return JoinNodes(nodes[section], nodes[(section == nodes.Length - 1) ? 0 : section + 1], factor) + transform.position;
    }

    Quaternion CalcRotation(float fac)
    {
        if (fac >= 1) fac -= 1;
        float total = fac * nodes.Length;
        int section = Mathf.FloorToInt(total);
        float factor = total - section;
        return Quaternion.LookRotation(Tangent(nodes[section], nodes[(section == nodes.Length - 1) ? 0 : section + 1], factor), Vector3.up);
    }

    Vector3 Tangent(Node A, Node B, float factor)
    {
        return CubicTangent(A.transform.position, A.childB.position, B.childA.position, B.transform.position, factor);
    }

    Vector3 CubicTangent(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float t)
    {
        return
            3 * Mathf.Pow(1 - t, 2) * (B - A) +
            6 * (1 - t) * t * (C - B) +
            3 * Mathf.Pow(t, 2) * (D - C);
    }


    void GenerateMesh()
    {
        if (mapping == null || mapping.Length < 2)
            return;

        if (mesh == null)
        {
            mesh = new Mesh();
            mesh.name = "Spline Road Mesh";
            GetComponent<MeshFilter>().sharedMesh = mesh;
        }

        int count = mapping.Length;

        Vector3[] verts = new Vector3[count * 2];
        Vector2[] uvs = new Vector2[count * 2];
        int[] tris = new int[(count - 1) * 6];

        for (int i = 0; i < count; i++)
        {
            // Smooth forward direction
            Vector3 forward;
            if (i == 0)
                forward = (mapping[1] - mapping[0]).normalized;
            else if (i == count - 1)
                forward = (mapping[count - 1] - mapping[count - 2]).normalized;
            else
            {
                Vector3 f1 = (mapping[i] - mapping[i - 1]).normalized;
                Vector3 f2 = (mapping[i + 1] - mapping[i]).normalized;
                forward = ((f1 + f2) * 0.5f).normalized;
            }

            Vector3 left = Vector3.Cross(Vector3.up, forward).normalized;

            verts[i * 2 + 0] = mapping[i] + left * (roadWidth * 0.5f);
            verts[i * 2 + 1] = mapping[i] - left * (roadWidth * 0.5f);

            float u = i / (float)(count - 1);
            uvs[i * 2 + 0] = new Vector2(u, 0);
            uvs[i * 2 + 1] = new Vector2(u, 1);
        }

        int index = 0;
        for (int i = 0; i < count - 1; i++)
        {
            int a = i * 2;
            int b = a + 1;
            int c = a + 2;
            int d = a + 3;

            // Correct winding order (normals up)
            tris[index++] = a;
            tris[index++] = b;
            tris[index++] = c;

            tris[index++] = b;
            tris[index++] = d;
            tris[index++] = c;
        }

        mesh.Clear();
        mesh.vertices = verts;
        mesh.uv = uvs;
        mesh.triangles = tris;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }

    Vector3 JoinNodes(Node A, Node B, float factor)
    {
        return CubicLerp(A.transform.position, A.childB.position, B.childA.position, B.transform.position, factor) - transform.position;
    }

    Vector3 QuadraticLerp(Vector3 A, Vector3 B, Vector3 C, float factor)
    {
        Vector3 AB = Vector3.Lerp(A, B, factor);
        Vector3 BC = Vector3.Lerp(B, C, factor);

        return Vector3.Lerp(AB, BC, factor);
    }

    Vector3 CubicLerp(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float factor)
    {
        Vector3 ABC = QuadraticLerp(A, B, C, factor);
        Vector3 BCD = QuadraticLerp(B, C, D, factor);

        return Vector3.Lerp(ABC, BCD, factor);
    }

    public void InsertNodeBetweenEnds()
    {
        if (nodes == null || nodes.Length < 2)
            return;

        Node first = nodes[0];
        Node last = nodes[nodes.Length - 1];

        GameObject newNodeObj = Instantiate(nodePrefab);
        newNodeObj.transform.parent = transform;

        newNodeObj.transform.position = (first.transform.position + last.transform.position) * 0.5f;

        Node newNode = newNodeObj.GetComponent<Node>();

        var list = new System.Collections.Generic.List<Node>(nodes);
        list.Add(newNode);
        nodes = list.ToArray();
    }

    public void RemoveLastNode()
    {
        if (nodes == null || nodes.Length < 3)
            return;

        Node last = nodes[nodes.Length - 1];

        GameObject.DestroyImmediate(last.gameObject);

        var list = new System.Collections.Generic.List<Node>(nodes);
        list.RemoveAt(nodes.Length - 1);
        nodes = list.ToArray();
    }
}
