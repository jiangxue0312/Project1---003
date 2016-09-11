using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class LandscapeScript : MonoBehaviour
{

    // The array of the landscape map
    public float[,] landscape;
    // The level of the landscape
    public int level;
    // The initial seed value
    public float seed;
    public float roughness;
    // The last index of the map

    int last;
    // The shader
    public Shader shader;
    //public Material material;
    
   
    public SunScript pointLight;
    

    // Use this for initialization
    void Start()
    {
        // The diamond-square algorithm begins with a 2D array of size 2^n + 1.
        int size = (int)Mathf.Pow(2, level) + 1;
        last = size - 1;
        // Create the landscape
        landscape = new float[size, size];
        // Set the initial value to the corner
        landscape[0, 0] = 0; 
        landscape[0, last] = 0; 
        landscape[last, 0] = 0; 
        landscape[last, last] = 0; 
        // Generate the landscape map
        Generate(size, seed * roughness);

        MeshFilter Landmesh = gameObject.AddComponent<MeshFilter>();
        Landmesh.mesh = CreateLandMesh();
        MeshRenderer Landrender = gameObject.AddComponent<MeshRenderer>();
        //Landrender.material = material1;
        Landrender.material.shader = shader;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        // Get renderer component (in order to pass params to shader)
        MeshRenderer renderer = gameObject.GetComponent<MeshRenderer>();

        // Pass updated light positions to shader
        renderer.material.SetColor("_PointLightColor", pointLight.color);
        renderer.material.SetVector("_PointLightPosition", pointLight.GetWorldPosition());
        
    }

   

    // Generate the landscape
    // Reference from stackoverflow.com/questions/2755750/diamond-square-algorithm
    void Generate(int sizeofmap, float seed)
    {
        for (int size = sizeofmap; size > 1; size /= 2, seed /= 2)
        {
            int middle = size / 2;
            // Perform diamond step
            for (int y = middle; y < last; y += size)
            {
                for (int x = middle; x < last; x += size)
                {
                    landscape[x, y] =
                    (landscape[x - middle, y - middle]
                    + landscape[x + middle, y - middle]
                    + landscape[x + middle, y + middle]
                    + landscape[x - middle, y + middle]) / 4.0f + (Random.value * 2 * seed - seed);
                }
            }

            // Perform square step
            for (int y = 0; y <= last; y += middle)
            {
                for (int x = (y + middle) % size; x <= last; x += size)
                {
                    landscape[x, y] =
                    (landscape[(x - middle + last) % (last), y]
                    + landscape[(x + middle) % (last), y]
                    + landscape[x, (y + middle) % (last)]
                    + landscape[x, (y - middle + last) % (last)]) / 4.0f + (Random.value * 2 * seed - seed);
                
                }
            }
        }
    }

    // Reference from answers.unity3d.com/questions/1033085/heightmap-to-mesh.html
     
    Mesh CreateLandMesh()
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Color> colors = new List<Color>();

        // Add vertices
        for (int i = 0; i < last - 1; i++)
        {
            for (int j = 0; j < last - 1; j++)
            {
                vertices.Add(new Vector3(i, landscape[i, j], j));
                vertices.Add(new Vector3(i, landscape[i, j + 1], j + 1));
                vertices.Add(new Vector3(i + 1, landscape[i + 1, j], j));
                vertices.Add(new Vector3(i + 1, landscape[i + 1, j + 1], j + 1));


            }
        }
        // Add colors
        for (int i = 0; i < last - 1; i++)
        {
            for (int j = 0; j < last - 1; j++)
            {
                ColorVertice(colors, landscape[i, j]);
                ColorVertice(colors, landscape[i, j + 1]);
                ColorVertice(colors, landscape[i + 1, j]);
                ColorVertice(colors, landscape[i + 1, j + 1]);
            }
        }

        // Generate triangles
        for (int k = 0; k <= vertices.Count - 4; k += 4)
        {
            triangles.Add(k);
            triangles.Add(k + 1);
            triangles.Add(k + 2);

            triangles.Add(k + 1);
            triangles.Add(k + 3);
            triangles.Add(k + 2);
        }
   
    
        Mesh mesh = new Mesh();
        mesh.name = "Landscape";
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.colors = colors.ToArray();
        mesh.RecalculateNormals();

        return mesh;
    }
    // Color the landscape
    void ColorVertice(List<Color> colors, float height)
    {
        if (height < 0.1f * seed * roughness)
        {
            colors.Add(Color.grey);
        }
        else if (height < 0.2f * seed * roughness)
        {
            colors.Add(Color.yellow);
        }
        else if (height < 0.4f * seed * roughness)
        {
            colors.Add(Color.green);
        }
        else
        {
            colors.Add(Color.white);
        }
        

    }

}
