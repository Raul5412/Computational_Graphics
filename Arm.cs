//Víctor Antonio Godínez Rodríguez A01339529
//Raúl González Cardona A01654995

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
        public GameObject FIRST_CUBE;
        public GameObject SECOND_CUBE;
        public GameObject THIRD_CUBE;

            Vector3[] first_orig;
            Vector3[] second_orig;
            Vector3[] third_orig;

            //Declaratio
            float rotZ; 
            float dirZ;
            public float dZ;

        
    // Start is called before the first frame update
    void Start()
    {
        //Initialization
        rotZ = 0.0f; 
        dirZ = 1.0f;
        dZ = 0.5f;

        Mesh first_mesh = FIRST_CUBE.GetComponent<MeshFilter>().mesh;
        Mesh second_mesh = SECOND_CUBE.GetComponent<MeshFilter>().mesh;
        Mesh third_mesh = THIRD_CUBE.GetComponent<MeshFilter>().mesh;
        first_orig = new Vector3[FIRST_CUBE.GetComponent<MeshFilter>().mesh.vertices.Length];
        second_orig = new Vector3[SECOND_CUBE.GetComponent<MeshFilter>().mesh.vertices.Length];
        third_orig = new Vector3[THIRD_CUBE.GetComponent<MeshFilter>().mesh.vertices.Length];

        for(int i = 0 ; i < first_orig.Length; i++)
        {
            first_orig[i] = first_mesh.vertices[i];
        }

        for(int i = 0 ; i < second_orig.Length; i++)
        {
            second_orig[i] = second_mesh.vertices[i];
        }

        for(int i = 0 ; i < third_orig.Length; i++)
        {
            third_orig[i] = third_mesh.vertices[i];
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        rotZ += dirZ * dZ;

        if(rotZ < -45 || rotZ > 45)
        {
            dirZ *= -1;
        }



        //FIRST_CUBE
        Matrix4x4 trMat1 = Transformations.TranslateM(0.5f, 0, 0);
        Matrix4x4 rotMat = Transformations.RotateM(rotZ, Transformations.AXIS.AX_Z);
        Matrix4x4 scMat = Transformations.ScaleM(1, 0.5f, 0.5f);

        Mesh mesh = FIRST_CUBE.GetComponent<MeshFilter>().mesh;
        int size = mesh.vertices.Length;
        Vector3[] transformed = new Vector3[size];

        for(int i = 0; i < size; i++)
        {
            Vector3 orig = first_orig[i];
            Vector4 temp = new Vector4(orig.x, orig.y, orig.z, 1);
            transformed[i]= rotMat * trMat1 * scMat * temp;

        }

        mesh.vertices = transformed;
        mesh.RecalculateNormals();

        //SECOND_CUBE
        Matrix4x4 trMat2 = Transformations.TranslateM(1, 0, 0);

        Mesh mesh2 = SECOND_CUBE.GetComponent<MeshFilter>().mesh;
        int size2 = mesh.vertices.Length;
        Vector3[] transformed2 = new Vector3[size2];

        for(int i = 0; i < size2; i++)
        {
            Vector3 orig = second_orig[i];
            Vector4 temp = new Vector4(orig.x, orig.y, orig.z, 1);
            Matrix4x4 first_transformation = rotMat * trMat2;
            Matrix4x4 second_transformation = rotMat * trMat1 * scMat;
            transformed2[i]= first_transformation * second_transformation * temp;
        }

        mesh2.vertices = transformed2;
        mesh2.RecalculateNormals();

        //THIRD_CUBE
        Mesh mesh3 = THIRD_CUBE.GetComponent<MeshFilter>().mesh;
        int size3 = mesh.vertices.Length;
        Vector3[] transformed3 = new Vector3[size3];

        for(int i = 0; i < size3; i++)
        {
            Vector3 orig = third_orig[i];
            Vector4 temp = new Vector4(orig.x, orig.y, orig.z, 1);
            Matrix4x4 first_transformation = rotMat * trMat2;
            Matrix4x4 second_transformation = rotMat * trMat2;
            Matrix4x4 third_transformation = rotMat * trMat1 * scMat;

            transformed3[i]= first_transformation * second_transformation * third_transformation * temp;
        }

        mesh3.vertices = transformed3;
        mesh3.RecalculateNormals();
        
        
    }
}
