using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class MeshTerrainGenerator : MonoBehaviour
{
    public int xSize = 250;
    public int zSize = 250;
    public float noiseScale = 0.05f;
    public float heightMultiplier = 20f;
    public AnimationCurve heightCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public Material material;
    
    public Gradient terrainColor;

    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;
    private Texture2D texture;

    private void Start()
    {
        Generate();
        GetComponent<MeshRenderer>().material = material;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Generate();
        }
    }

    public void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.indexFormat = IndexFormat.UInt32;
        mesh.name = "Procedural Terrain";

        CreateShape();
        UpdateMesh();
        CreateTexture();
    }

    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        uvs = new Vector2[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * noiseScale, z * noiseScale);
                y = heightCurve.Evaluate(y) * heightMultiplier;
                
                vertices[i] = new Vector3(x, y, z);
                uvs[i] = new Vector2((float)x / xSize, (float)z / zSize);
                i++;
            }
        }

        triangles = new int[xSize * zSize * 6];
        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    void CreateTexture()
    {
        if (texture == null) 
            texture = new Texture2D(xSize, zSize);
            
        Color[] colorMap = new Color[xSize * zSize];
        
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                int vertIndex = z * (xSize + 1) + x;
                float slope = Vector3.Dot(mesh.normals[vertIndex], Vector3.up);
                colorMap[z * xSize + x] = terrainColor.Evaluate(1 - slope);
            }
        }

        texture.SetPixels(colorMap);
        texture.Apply();

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if(renderer.sharedMaterial == null) 
             renderer.sharedMaterial = new Material(Shader.Find("Standard"));

        renderer.sharedMaterial.mainTexture = texture;
    }
}