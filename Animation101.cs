using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation101 : MonoBehaviour
{
    public GameObject GO_SUN;
    public GameObject GO_EARTH;
    public GameObject GO_MOON;

    Vector3[] sun_orig;
    Vector3[] earth_orig;
    Vector3[] moon_orig;

    float rotY; //Declaration
    public float deltaRotY;

    // Start is called before the first frame update
    void Start()
    {
        rotY = 0.0f; //Initialization
        Mesh sun_mesh = GO_SUN.GetComponent<MeshFilter>().mesh;
        Mesh earth_mesh = GO_EARTH.GetComponent<MeshFilter>().mesh;
        Mesh moon_mesh = GO_MOON.GetComponent<MeshFilter>().mesh;
        sun_orig = new Vector3[GO_SUN.GetComponent<MeshFilter>().mesh.vertices.Length];
        earth_orig = new Vector3[GO_EARTH.GetComponent<MeshFilter>().mesh.vertices.Length];
        moon_orig = new Vector3[GO_MOON.GetComponent<MeshFilter>().mesh.vertices.Length];

        for(int i = 0 ; i < sun_orig.Length; i++)
        {
            sun_orig[i] = sun_mesh.vertices[i];
        }

        for(int i = 0 ; i < sun_orig.Length; i++)
        {
            earth_orig[i] = earth_mesh.vertices[i];
        }

        for(int i = 0 ; i < moon_orig.Length; i++)
        {
            moon_orig[i] = moon_mesh.vertices[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        //SUN
        rotY += deltaRotY; // in every frame you make a rotation.
        Matrix4x4 rotMat1 = Transformations.RotateM(rotY, Transformations.AXIS.AX_Y);

        Mesh mesh = GO_SUN.GetComponent<MeshFilter>().mesh;
        int size = mesh.vertices.Length;
        Vector3[] transformed = new Vector3[size];

        for(int i = 0; i < size; i++)
        {
            Vector3 orig = sun_orig[i];
            Vector4 temp = new Vector4(orig.x, orig.y, orig.z, 1);
            transformed[i]= rotMat1 * temp;
        }

        mesh.vertices = transformed;
        mesh.RecalculateNormals();

        //EARTH
        Matrix4x4 rotMat2 = Transformations.RotateM(rotY*4, Transformations.AXIS.AX_Y);
        Matrix4x4 trMat1 = Transformations.TranslateM(6, 0, 0);

        Mesh mesh2 = GO_EARTH.GetComponent<MeshFilter>().mesh;
        int size2 = mesh2.vertices.Length;
        Vector3[] transformed2 = new Vector3[size2];

        for(int i = 0; i < size; i++)
        {
            Vector3 orig = earth_orig[i];
            Vector4 temp = new Vector4(orig.x, orig.y, orig.z, 1);
            transformed2[i]= rotMat2 * trMat1 * temp;
        }

        mesh2.vertices = transformed2;
        mesh2.RecalculateNormals();

        //MOON
        Matrix4x4 rotMat3 = Transformations.RotateM(rotY*15, Transformations.AXIS.AX_Y);
        Matrix4x4 trMat2 = Transformations.TranslateM(1.5f, 0, 0);
        Matrix4x4 scMat1 = Transformations.ScaleM(0.3f, 0.3f, 0.3f);

        Mesh mesh3 = GO_MOON.GetComponent<MeshFilter>().mesh;
        int size3 = mesh3.vertices.Length;
        Vector3[] transformed3 = new Vector3[size3];

        for(int i = 0; i < size; i++)
        {
            Vector3 orig = moon_orig[i];
            Vector4 temp = new Vector4(orig.x, orig.y, orig.z, 1);
            Matrix4x4 earths_transforms = rotMat2 * trMat1;
            Matrix4x4 moons_transforms = rotMat3 * trMat2 * scMat1; //Always scale last
            transformed3[i]= earths_transforms * moons_transforms * temp;
        }

        mesh3.vertices = transformed3;
        mesh3.RecalculateNormals();
    }
}
