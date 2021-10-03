using Qiskit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlochMapping : MonoBehaviour
{
    //1 = |0>
    //2 = |+>
    //3 = |i>
    //4 = |-i>
    //5 = |->
    //6 = |1>

    public static Dictionary<int, ComplexNumber> assignments = new Dictionary<int, ComplexNumber>()
    {
        { 1, new ComplexNumber(){ Real=1, Complex=0 } },
        { 2, new ComplexNumber(){ Real=1/Mathf.Sqrt(2), Complex=1/Mathf.Sqrt(2) } },
        { 3, new ComplexNumber(){ Real=0, Complex=1 } },
        { 4, new ComplexNumber(){ Real=0, Complex=-1 } },
        { 5, new ComplexNumber(){ Real=1/Mathf.Sqrt(2), Complex=-1/Mathf.Sqrt(2) } },
        { 6, new ComplexNumber(){ Real=-1, Complex=0 } }
    };

    /*
    Vector3 ComplexNumberToVector3(ComplexNumber c)
    {

        Theta = 

        double X = ;
        double Y = ;
        double Z = ;
        return new Vector3().normalized;
    }*/

    public static ComplexNumber Vector3ToComplexNumber(Vector3 v)
    {
        double factor = Mathf.PI / 180.0;
        v.Normalize();

        Plane XZ = new Plane(Vector3.right, Vector3.forward, Vector3.zero);
        Plane XY = new Plane(Vector3.right, Vector3.up, Vector3.zero);

        Vector3 toXZ = Vector3.ProjectOnPlane(v, XZ.normal).normalized;
        Vector3 toXY = Vector3.ProjectOnPlane(v, XY.normal).normalized;

        double theta = Vector3.SignedAngle(toXZ, Vector3.right, XZ.normal) * factor;
        double phi = Vector3.SignedAngle(toXY, Vector3.right, XY.normal) * factor;

        ComplexNumber c = new ComplexNumber
        {
            Real = theta,//Angle between Y and Z,
            Complex = phi//Angle between X and Y
        };
        return c;
    }


}
