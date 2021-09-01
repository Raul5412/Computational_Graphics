//Víctor Antonio Godínez Rodríguez A01339529
//Raúl González Cardona A01654995


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTriangle : MonoBehaviour
{
    public GameObject transformedGO;

    Vector3[] geometry;
    Vector3[] normals;
    int[] topology;

    void TransformCube()
    {
        Matrix4x4 rotM = Transformations.RotateM(-50, Transformations.AXIS.AX_X);
        Matrix4x4 trM = Transformations.TranslateM(7, 8, -4);
        Matrix4x4 trDP = Transformations.TranslateM(3.12F, -3.85F, -5.27F);
        Matrix4x4 trP = Transformations.TranslateM(-3.12F, 3.85F, 5.27F);

        Vector3[] transformedU = new Vector3[geometry.Length];
        Vector3[] transformedD = new Vector3[geometry.Length];
        Vector3[] transformedT = new Vector3[geometry.Length];
        Vector3[] transformed = new Vector3[geometry.Length];

        for(int i = 0; i < geometry.Length; i++)
        {
            //                              x           y               z           w
            Vector4 temp = new Vector4(geometry[i].x, geometry[i].y, geometry[i].z, 1);
            transformedU[i] = trM * temp;

            Vector4 centerC = new Vector4(transformedU[i].x, transformedU[i].y, transformedU[i].z, 1);
            transformedD[i] = trDP * centerC;

            Vector4 pivoteD = new Vector4(transformedD[i].x, transformedD[i].y, transformedD[i].z, 1);
            transformedT[i] = rotM * pivoteD;

            Vector4 rotate = new Vector4(transformedT[i].x, transformedT[i].y, transformedT[i].z, 1);
            transformed[i] = trP * rotate;

            Debug.Log("transformedU["+i+"]="+transformed[i]);
        }

        Mesh mesh = transformedGO.GetComponent<MeshFilter>().mesh;
        mesh.vertices = transformed;
        mesh.triangles = topology;
        mesh.RecalculateNormals();
    }

    // Start is called before the first frame update
    void Start()
    {

        /* TRIANGLE
         Vector3 v0 = new Vector3(0, 0, 0)
         Vector3 v1 = new Vector3(10, 0, 0)
         Vector3 v2 = new Vector3(5, 7, 0)

         geometry = new Vector3[] {v0, v1, v2}
         topology = new int [] {0, 1, 2}
        */

        // CUBE
         float S = 10.0f; //Side (cara del cubo)
         float HS = S / 2.0f; //Half side (la mitad de la cara del cubo)

                                  //x   y    z
         Vector3 v0 = new Vector3(-HS, -HS, HS);
         Vector3 v1 = new Vector3(HS, -HS, HS);
         Vector3 v2 = new Vector3(HS, HS, HS);
         Vector3 v3 = new Vector3(-HS, HS, HS);
         Vector3 v4 = new Vector3(HS, -HS, -HS);
         Vector3 v5 = new Vector3(HS, HS, -HS);
         Vector3 v6 = new Vector3(-HS, -HS, -HS);
         Vector3 v7 = new Vector3(-HS, HS, -HS);

         geometry = new Vector3[] { v0, v1, v2, v3, v4, v5, v6, v7};
         topology = new int[] { 0, 1, 2, 0, 2, 3, 1, 4, 5, 1, 5, 2, 4, 6, 7, 4, 7, 5,
                                 6, 0, 3, 6, 3, 7, 3, 2, 5, 3, 5, 7, 1, 0, 6, 1, 6, 4};

         Vector3 n0 = new Vector3(0, 0, 1);
         Vector3 n1 = new Vector3(1, 0, 0);
         Vector3 n2 = new Vector3(0, 0, -1);
         Vector3 n3 = new Vector3(-1, 0, 0);
         Vector3 n4 = new Vector3(0, 1, 0);
         Vector3 n5 = new Vector3(0, -1, 0);

        normals = new Vector3[] { n0, n1, n2, n3, n4, n5, n5, n5 };

        /*PYRAMIDE
        Vector3 v0 = new Vector3(0, 0, 0);
        Vector3 v1 = new Vector3(10, 0, 0);
        Vector3 v2 = new Vector3(5, 10, -5);
        Vector3 v3 = new Vector3(10, 0, -10);
        Vector3 v4 = new Vector3(0, 0, -10);

        geometry = new Vector3[] {v0, v1, v2, v3, v4};
        topology = new int[] { 0, 1, 2, 1, 3, 2, 0, 4, 2, 3, 4, 2};
        */


        // For Unity
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();

        mesh.vertices = geometry;
        mesh.triangles = topology;
        mesh.normals = normals;

        TransformCube();
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(Vector3.zero, new Vector3(10, 0, 0), Color.red);
        Debug.DrawLine(Vector3.zero, new Vector3(0, 10, 0), Color.green);
        Debug.DrawLine(Vector3.zero, new Vector3(0, 0, 10), Color.blue);
    }
}
