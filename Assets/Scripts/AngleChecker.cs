using System.Collections.Generic;
using UnityEngine;

public class AngleChecker : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects_to_check;
    [SerializeField] private GameObject origin, point1, point2;
    [SerializeField] private float angle_threshold = 180f; // degrees
    [SerializeField] private Axis axis_to_check = Axis.X;
    private bool correct_angle = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"Angle threshold set to: {angle_threshold} degrees.");
    }

    // Update is called once per frame
    void Update()
    {
        if (!correct_angle)
        {
            CheckAngle();
        }
    }

    private void CheckAngle()
    {
        Vector3 dir1 = point1.transform.position - origin.transform.position;
        Vector3 dir2 = point2.transform.position - origin.transform.position;
        float angle = Vector3.Angle(dir1, dir2);
        float diff_mag = FindMagnitude(angle_threshold - angle);
        if (diff_mag != 0)
        {
            foreach (var obj in objects_to_check)
            {
                RotateObjects(obj, diff_mag);
            }
            Debug.Log($"Calculated angle: {angle} degrees. " +
                      $"Angle difference: {angle_threshold - angle}. " +
                      $"Magnitude: {diff_mag}. Incorrect Angle.");
        }
        else
        {
            correct_angle = true;
            Debug.Log($"Calculated angle: {angle} degrees. Correct Angle.");
            switch (axis_to_check)
            {
                case Axis.X:
                    {
                        angle = objects_to_check[0].transform.rotation.eulerAngles.x;
                        break;
                    }
                case Axis.Y:
                    {
                        angle = objects_to_check[0].transform.rotation.eulerAngles.y;
                        break;
                    }
                case Axis.Z:
                    {
                        angle = objects_to_check[0].transform.rotation.eulerAngles.z;
                        break;
                    }
            }
            Debug.Log($"Final angle: {angle} degrees.");
        }
    }

    private void RotateObjects(GameObject obj, float magnitude)
    {
        switch (axis_to_check)
        {
            case Axis.X:
                {
                    obj.transform.Rotate(Vector3.right, magnitude);
                    break;
                }
            case Axis.Y:
                {
                    obj.transform.Rotate(Vector3.up, magnitude);
                    break;
                }
            case Axis.Z:
                {
                    obj.transform.Rotate(Vector3.forward, magnitude);
                    break;
                }
        }
    }

    private static float FindMagnitude(float f)
    {
        float magnitude = Mathf.Pow(10, Mathf.Floor(Mathf.Log10(Mathf.Abs(f))));
        if ((f == 0) || (Mathf.Abs(magnitude) < Mathf.Pow(10, -5)))
        {
            return 0;
        }
        else if (f < 0)
        {
            magnitude = -magnitude;
        }
        return magnitude;
    }

    public enum Axis
    {
        X,
        Y,
        Z
    }

}
