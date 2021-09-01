using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTest : MonoBehaviour
{
    public Vector3 A;
    public Vector3 B;
    public Vector3 C;
    public float t;

    // Start is called before the first frame update
    void Start()
    {
        float rad = Mathematics.AngleBetween(A, B);
        float deg = Mathf.Rad2Deg * rad;
        Debug.Log("Angle Between A and B: " + deg + "");
           
    }

    void DrawAxes()
    {
        Debug.DrawLine(Vector3.zero, new Vector3(10, 0, 0), Color.red);
        Debug.DrawLine(Vector3.zero, new Vector3(0, 10, 0), Color.green);
        Debug.DrawLine(Vector3.zero, new Vector3(0, 0, 10), Color.blue);
    }

    void DrawLine()
    {
        // y = mx + b
        // A + t(B-A) where A, B are points in any dimension 
        // This uses the fact that A-B is bew vector
        // 't' is a parameter that controls how much to move between A and B
        // A is the starting point, and B is the finish point of the line
        Debug.DrawLine(A, B, Color.cyan);
        Vector3 pos = A + t * (B-A);
        transform.position = pos;
    }

    void DrawTriangle()
    {
        Debug.DrawLine(A, B, Color.cyan);
        Debug.DrawLine(B, C, Color.cyan);
        Debug.DrawLine(A, C, Color.cyan);
    }

    void DrawNormal()
    {
        Vector3 AB = Mathematics.Substract(A, B);
        Vector3 AC = Mathematics.Substract(A, C);
        Vector3 n = Mathematics.Cross(AB, AC);
        Debug.Log(Mathematics.Magnitude(n));
        Vector3 nu = Mathematics.Normalized(n); // Unitary "n"
        Vector3 centroid = new Vector3((A.x+B.x+C.x)/3, (A.y+B.y+C.y)/3, (A.z+B.z+C.z)/3);
        Debug.DrawLine(centroid, centroid+nu, Color.magenta);

    }

    // Update is called once per frame
    void Update()
    {
        DrawAxes();
        //DrawLine();
        DrawTriangle();
        DrawNormal();
        
    }
}
