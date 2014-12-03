using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeformMesh : MonoBehaviour
{
    //Variables to hold information about our mesh, the mesh and the material.
    Mesh myMesh;
    Material myMat;
    //Lists holds information about the points of our mesh, need to allow for manipluation
    List<Vector3> myPoints;
    List<Vector3> myVertices;
    List<int> myTriangles;
    List<Vector2> myUVs;

    //Size for ease of use, can alter to change the size of the mesh
    float size = 0.5f;

    // Use this for initialization
    void Start()
    {
        //Initalize the lists
        myPoints = new List<Vector3>();
        myVertices = new List<Vector3>();
        myTriangles = new List<int>();
        myUVs = new List<Vector2>();

        //initalize myPoints lists
        myPoints.Add(new Vector3(-size, size, -size));
        myPoints.Add(new Vector3(size, size, -size));
        myPoints.Add(new Vector3(size, -size, -size));
        myPoints.Add(new Vector3(-size, -size, -size));

        myPoints.Add(new Vector3(size, size, size));
        myPoints.Add(new Vector3(-size, size, size));
        myPoints.Add(new Vector3(-size, -size, size));
        myPoints.Add(new Vector3(size, -size, size));

        createMesh();

    }

    // Update is called once per frame
    void Update()
    {
        //Right Click
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                this.ChangeYpos(FindNearestPoint(hit.point));
            }
        }

        //Left Click
        if (Input.GetMouseButton(1))
        {
               RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                this.ChangeYneg(FindNearestPoint(hit.point));
            }
        }

        //Up Arrow
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                this.ChangeXpos(FindNearestPoint(hit.point));
            }
        }
        //Down Arrow
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                this.ChangeXneg(FindNearestPoint(hit.point));
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
            this.RotateFrontFace(180f);
    }

    //Creates the mesh that will be manipluated
    private void createMesh()
    {
        //Adding important components to our Mesh
        gameObject.AddComponent("MeshFilter");
        gameObject.AddComponent("MeshRenderer");
        gameObject.AddComponent("MeshCollider");

        //Error checking to ensure things were done correctly
        myMat = Resources.Load("Materials/Default") as Material;
        if (myMat == null)
        {
            Debug.LogError("Material not found");
            return;
        }
        MeshFilter myMeshFilter = GetComponent<MeshFilter>();
        if (myMeshFilter == null)
        {
            Debug.LogError("Mesh Filter not found");
            return;
        }
        myMesh = myMeshFilter.sharedMesh;
        if (myMesh == null)
        {
            myMeshFilter.mesh = new Mesh();
            myMesh = myMeshFilter.sharedMesh;
        }
        MeshCollider meshCollider = GetComponent < MeshCollider>();
        if (meshCollider == null)
        {
            Debug.LogError("Mesh Collider not found");
            return;
        }
        myMesh.Clear();
        updateMesh();
    }

    //Update the mesh for changes that have occured
    private void updateMesh()
    {
        //Front Plane
        myVertices.Add(myPoints[0]); myVertices.Add(myPoints[1]); myVertices.Add(myPoints[2]); myVertices.Add(myPoints[3]);
        //Back Plane
        myVertices.Add(myPoints[4]); myVertices.Add(myPoints[5]); myVertices.Add(myPoints[6]); myVertices.Add(myPoints[7]);
        //Left Plane
        myVertices.Add(myPoints[5]); myVertices.Add(myPoints[0]); myVertices.Add(myPoints[3]); myVertices.Add(myPoints[6]);
        //Right Plane
        myVertices.Add(myPoints[1]); myVertices.Add(myPoints[4]); myVertices.Add(myPoints[7]); myVertices.Add(myPoints[2]);
        //Top Plane
        myVertices.Add(myPoints[5]); myVertices.Add(myPoints[4]); myVertices.Add(myPoints[1]); myVertices.Add(myPoints[0]);
        //Bottom Plane
        myVertices.Add(myPoints[3]); myVertices.Add(myPoints[2]); myVertices.Add(myPoints[7]); myVertices.Add(myPoints[6]);

        //Front Plane
        myTriangles.Add(0); myTriangles.Add(1); myTriangles.Add(2);
        myTriangles.Add(2); myTriangles.Add(3); myTriangles.Add(0);
        //Back Plane
        myTriangles.Add(4); myTriangles.Add(5); myTriangles.Add(6);
        myTriangles.Add(6); myTriangles.Add(7); myTriangles.Add(4);    
        //Left Plane
        myTriangles.Add(8); myTriangles.Add(9); myTriangles.Add(10);
        myTriangles.Add(10); myTriangles.Add(11); myTriangles.Add(8);
        //Right Plane
        myTriangles.Add(12); myTriangles.Add(13); myTriangles.Add(14);
        myTriangles.Add(14); myTriangles.Add(15); myTriangles.Add(12);
        //Top Plane
        myTriangles.Add(16); myTriangles.Add(17); myTriangles.Add(18);
        myTriangles.Add(18); myTriangles.Add(19); myTriangles.Add(16);
        //Bottom Plane
        myTriangles.Add(20); myTriangles.Add(21); myTriangles.Add(22);
        myTriangles.Add(22); myTriangles.Add(23); myTriangles.Add(20);

        //Front Plane
        myUVs.Add(new Vector2(0, 1));
        myUVs.Add(new Vector2(1, 1));
        myUVs.Add(new Vector2(1, 0));
        myUVs.Add(new Vector2(0, 0));
        //Back Plane
        myUVs.Add(new Vector2(0, 1));
        myUVs.Add(new Vector2(1, 1));
        myUVs.Add(new Vector2(1, 0));
        myUVs.Add(new Vector2(0, 0));
        //Left Plane
        myUVs.Add(new Vector2(0, 1));
        myUVs.Add(new Vector2(1, 1));
        myUVs.Add(new Vector2(1, 0));
        myUVs.Add(new Vector2(0, 0));
        //Right Plane
        myUVs.Add(new Vector2(0, 1));
        myUVs.Add(new Vector2(1, 1));
        myUVs.Add(new Vector2(1, 0));
        myUVs.Add(new Vector2(0, 0));
        //Top Plane
        myUVs.Add(new Vector2(0, 1));
        myUVs.Add(new Vector2(1, 1));
        myUVs.Add(new Vector2(1, 0));
        myUVs.Add(new Vector2(0, 0));
        //Bottom Plane
        myUVs.Add(new Vector2(0, 1));
        myUVs.Add(new Vector2(1, 1));
        myUVs.Add(new Vector2(1, 0));
        myUVs.Add(new Vector2(0, 0));

        //Putting arrays inside of arrays and such
        myMesh.vertices = myVertices.ToArray();
        myMesh.triangles = myTriangles.ToArray();
        myMesh.uv = myUVs.ToArray();
        myVertices.Clear();
        myTriangles.Clear();
        myUVs.Clear();
        MeshCollider myMeshCollider = GetComponent<MeshCollider>();
        myMesh.RecalculateNormals();
        myMesh.RecalculateBounds();
        recalculateTangents(myMesh);
        myMeshCollider.sharedMesh = null;
        myMeshCollider.sharedMesh = myMesh;
        renderer.material = myMat;
        myMesh.Optimize();
    }

    //Some mathematical calculations from the change in stuff
    private static void recalculateTangents(Mesh TMesh)
    {
        int[] TTriangles = TMesh.triangles;
        Vector3[] TVertices = TMesh.vertices;
        Vector2[] TUV = TMesh.uv;
        Vector3[] TNormals = TMesh.normals;

        int TTrianglesCount = TTriangles.Length;
        int TVerticesCount = TVertices.Length;

        Vector3[] tan1 = new Vector3[TVerticesCount];
        Vector3[] tan2 = new Vector3[TVerticesCount];

        Vector4[] tangents = new Vector4[TVerticesCount];

        for (long i = 0; i < TTrianglesCount; i += 3)
        {
            long t1 = TTriangles[i + 0];
            long t2 = TTriangles[i+1];
            long t3 = TTriangles[i + 2];

            Vector3 v1 = TVertices[t1];
            Vector3 v2 = TVertices[t2];
            Vector3 v3 = TVertices[t3];

            Vector2 u1 = TUV[t1];
            Vector2 u2 = TUV[t2];
            Vector2 u3 = TUV[t3];

            float x1 = v2.x - v1.x;
            float x2 = v3.x - v1.x;
            float y1 = v2.y - v1.y;
            float y2 = v3.y - v1.y;
            float z1 = v2.z - v1.z;
            float z2 = v3.z - v1.z;

            float s1 = u2.x - u1.x;
            float s2 = u3.x - u1.x;
            float i1 = u2.y - u1.y;
            float i2 = u3.y - u1.y;

            float div = (s1 * i2) - (s2 * i1);
            float r = div == 0.0f ? 0.0f : 1.0f/div;

            Vector3 sdir = new Vector3((i2*x1 - i1*x2) * r, (i2 * y1 - i1 *y2)*r, (i2*z1-i1*z2)*r);
            Vector3 tdir = new Vector3((s2 * x2 - s1 * x1) * r, (s2 * y2 - s1 * y1) * r, (s2 * z2 - s1 * z1) * r);

            tan1[t1] += sdir;
            tan1[t2] += sdir;
            tan1[t3] += sdir;

            tan2[t1] += tdir; 
            tan2[t1] += tdir; 
            tan2[t1] += tdir;
        }

        for (long a = 0; a < TVerticesCount; ++a)
        {
            Vector3 n = TNormals[a];
            Vector3 t = tan1[a];

            Vector3.OrthoNormalize(ref n, ref t);
            tangents[a].x = t.x;
            tangents[a].y = t.y;
            tangents[a].z = t.z;
            tangents[a].w = (Vector3.Dot(Vector3.Cross(n,t),tan2[a])<0.0f)? -1.0f : 1.0f;
        }

        TMesh.tangents = tangents;
    }

    //Find the closest point to the pointers click
    private Vector3 FindNearestPoint(Vector3 point)
    {
        Vector3 NearestPoint = new Vector3();
        float lastDistance = 99999999f;

        for (int i = 0; i < myPoints.Count; i++)
        {
            float distance = GetDistance(point, myPoints[i]);
            if (distance < lastDistance)
            {
                lastDistance = distance;
                NearestPoint = myPoints[i];
            }
        }
        return NearestPoint;
    }

    //Calculate the distance between two vector3
    private float GetDistance(Vector3 start, Vector3 end)
    {
        return Mathf.Sqrt((Mathf.Pow((start.x - end.x), 2) + Mathf.Pow((start.y - end.y), 2) + Mathf.Pow((start.z - end.z), 2)));
    }

    //Change the Y position of a point in the positive direction
    private void ChangeYpos(Vector3 point)
    {
        int index = -1;
        for(int i = 0; i < myPoints.Count; i++)
        {
            if (myPoints[i] == point)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            Debug.LogError("Could not match points");
        else
        {
            Vector3 newPoint = myPoints[index];
            newPoint.y += 0.01f;
            myPoints[index] = newPoint;
            updateMesh();
        }
    }

    //Change the Y position of a point in the negative direction
    private void ChangeYneg(Vector3 point)
    {
        int index = -1;
        for (int i = 0; i < myPoints.Count; i++)
        {
            if (myPoints[i] == point)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            Debug.LogError("Could not match points");
        else
        {
            Vector3 newPoint = myPoints[index];
            newPoint.y -= 0.01f;
            myPoints[index] = newPoint;
            updateMesh();
        }
    }

    //Change the X position of a point in the positive direction
    private void ChangeXpos(Vector3 point)
    {
        int index = -1;
        for (int i = 0; i < myPoints.Count; i++)
        {
            if (myPoints[i] == point)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            Debug.LogError("Could not match points");
        else
        {
            Vector3 newPoint = myPoints[index];
            newPoint.x -= 0.1f;
            myPoints[index] = newPoint;
            updateMesh();
        }
    }

    //Change the X position of a point in the negative direction
    private void ChangeXneg(Vector3 point)
    {
        int index = -1;
        for (int i = 0; i < myPoints.Count; i++)
        {
            if (myPoints[i] == point)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            Debug.LogError("Could not match points");
        else
        {
            Vector3 newPoint = myPoints[index];
            newPoint.x += 0.1f;
            myPoints[index] = newPoint;
            updateMesh();
        }
    }

    //Change the Z position of a point in the positive direction
    private void ChangeZpos(Vector3 point)
    {
        int index = -1;
        for (int i = 0; i < myPoints.Count; i++)
        {
            if (myPoints[i] == point)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            Debug.LogError("Could not match points");
        else
        {
            Vector3 newPoint = myPoints[index];
            newPoint.x += 0.1f;
            myPoints[index] = newPoint;
            updateMesh();
        }
    }

    //Change the Z position of a point in the negative direction
    private void ChangeZneg(Vector3 point)
    {
        int index = -1;
        for (int i = 0; i < myPoints.Count; i++)
        {
            if (myPoints[i] == point)
            {
                index = i;
                break;
            }
        }

        if (index == -1)
            Debug.LogError("Could not match points");
        else
        {
            Vector3 newPoint = myPoints[index];
            newPoint.x += 0.1f;
            myPoints[index] = newPoint;
            updateMesh();
        }
    }

    //Rotating the Left face, parameter of Clockwise or Anticlockwise
    private void RotateLeftFace(string dir)
    {
        if (dir == "Clockwise")
        {
            Vector3 t = myPoints[0];
            myPoints[0] = myPoints[5];
            myPoints[5] = myPoints[6];
            myPoints[6] = myPoints[3];
            myPoints[3] = t;
            updateMesh();
        }
        else if (dir == "Anticlockwise")
        {
            Vector3 t = myPoints[0];
            myPoints[0] = myPoints[3];
            myPoints[3] = myPoints[6];
            myPoints[6] = myPoints[5];
            myPoints[5] = t;
            updateMesh();
        }
        else
            Debug.LogError("Invalid turn for face");
    }

    //Rotating the Right face, parameter of Clockwise or Anticlockwise
    private void RotateRightFace(string dir)
    {
        if (dir == "Clockwise")
        {
            Vector3 t = myPoints[1];
            myPoints[1] = myPoints[4];
            myPoints[4] = myPoints[7];
            myPoints[7] = myPoints[2];
            myPoints[2] = t;
            updateMesh();
        }
        else if (dir == "Anticlockwise")
        {
            Vector3 t = myPoints[1];
            myPoints[1] = myPoints[2];
            myPoints[2] = myPoints[7];
            myPoints[7] = myPoints[4];
            myPoints[4] = t;
            updateMesh();
        }
        else
            Debug.LogError("Invalid turn for face");
    }

    //Rotating the Top face, parameter of Clockwise or Anticlockwise
    private void RotateTopFace(string dir)
    {
        if (dir == "Clockwise")
        {
            Vector3 t = myPoints[0];
            myPoints[0] = myPoints[5];
            myPoints[5] = myPoints[4];
            myPoints[4] = myPoints[1];
            myPoints[1] = t;
            updateMesh();
        }
        else if (dir == "Anticlockwise")
        {
            Vector3 t = myPoints[0];
            myPoints[0] = myPoints[1];
            myPoints[1] = myPoints[4];
            myPoints[4] = myPoints[5];
            myPoints[5] = t;
            updateMesh();
        }
        else
            Debug.LogError("Invalid turn for face");
    }

    //Rotating the Bottom face, parameter of Clockwise or Anticlockwise
    private void RotateBottomFace(string dir)
    {
        if (dir == "Clockwise")
        {
            Vector3 t = myPoints[2];
            myPoints[2] = myPoints[3];
            myPoints[3] = myPoints[6];
            myPoints[6] = myPoints[7];
            myPoints[7] = t;
            updateMesh();
        }
        else if (dir == "Anticlockwise")
        {
            Vector3 t = myPoints[2];
            myPoints[2] = myPoints[7];
            myPoints[7] = myPoints[6];
            myPoints[6] = myPoints[3];
            myPoints[3] = t;
            updateMesh();
        }
        else
            Debug.LogError("Invalid turn for face");
    }

    //Rotating the Front face, parameter of Clockwise or Anticlockwise
    private void RotateFrontFace(string dir)
    {
        if (dir == "Clockwise")
        {
            Vector3 t = myPoints[0];
            myPoints[0] = myPoints[1];
            myPoints[1] = myPoints[2];
            myPoints[2] = myPoints[3];
            myPoints[3] = t;
            updateMesh();
        }
        else if (dir == "Anticlockwise")
        {
            Vector3 t = myPoints[0];
            myPoints[0] = myPoints[1];
            myPoints[1] = myPoints[2];
            myPoints[2] = myPoints[3];
            myPoints[3] = t;
            updateMesh();
        }
        else
            Debug.LogError("Invalid turn for face");
    }

    //Rotating the Back face, parameter of Clockwise or Anticlockwise
    private void RotateBackFace(string dir)
    {
        if (dir == "Clockwise")
        {
            Vector3 t = myPoints[5];
            myPoints[5] = myPoints[6];
            myPoints[6] = myPoints[7];
            myPoints[7] = myPoints[4];
            myPoints[4] = t;
            updateMesh();
        }
        else if (dir == "Anticlockwise")
        {
            Vector3 t = myPoints[5];
            myPoints[5] = myPoints[4];
            myPoints[4] = myPoints[7];
            myPoints[7] = myPoints[6];
            myPoints[6] = t;
            updateMesh();
        }
        else
            Debug.LogError("Invalid turn for face");
    }


    //Rotate the front face by theta degrees.
    private void RotateFrontFace(float theta)
    {
        //Temp variable to hold the point to change
        Vector3 t = myPoints[0];
        //Some Math
        t.x = Mathf.Cos(theta) * myPoints[0].x + Mathf.Sin(theta) * myPoints[0].y;
        t.y =  (- Mathf.Sin(theta)) * myPoints[0].x + Mathf.Cos(theta) * myPoints[0].y;
        //Make the point equal to the new position
        myPoints[0] = t;

        //Temp variable to hold the point to change
        t = myPoints[1];
        //Some Math
        t.x = Mathf.Cos(theta) * myPoints[1].x + Mathf.Sin(theta) * myPoints[1].y;
        t.y = (-Mathf.Sin(theta)) * myPoints[1].x + Mathf.Cos(theta) * myPoints[1].y;
        //Make the point equal to the new position
        myPoints[1] = t;

        //Temp variable to hold the point to change
        t = myPoints[2];
        //Some Math
        t.x = Mathf.Cos(theta) * myPoints[2].x + Mathf.Sin(theta) * myPoints[2].y;
        t.y = (-Mathf.Sin(theta)) * myPoints[2].x + Mathf.Cos(theta) * myPoints[2].y;
        //Make the point equal to the new position
        myPoints[2] = t;

        //Temp variable to hold the point to change
        t = myPoints[3];
        //Some Math
        t.x = Mathf.Cos(theta) * myPoints[3].x + Mathf.Sin(theta) * myPoints[3].y;
        t.y = (-Mathf.Sin(theta)) * myPoints[3].x + Mathf.Cos(theta) * myPoints[3].y;
        //Make the point equal to the new position
        myPoints[3] = t;

        //Update the mesh to reflect the changes
        updateMesh();
    }

}
