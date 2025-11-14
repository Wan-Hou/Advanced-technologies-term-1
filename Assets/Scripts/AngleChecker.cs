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
        Debug.Log("AngleChecker started.");
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
        float diff_mag = FindMagnitude(angle - angle_threshold);
        if (angle != angle_threshold)
        {
            foreach (var obj in objects_to_check)
            {
                RotateObjects(obj, -diff_mag);
            }
            Debug.Log($"Calculated angle: {angle} degrees. " +
                      $"Magnitude: {diff_mag}. Incorrect Angle.");
        }
        else
        {
            correct_angle = true;
            Debug.Log($"Calculated angle: {angle} degrees. Correct Angle.");
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
        if (f == 0)
        {
            return 0;
        }
        float scale = Mathf.Pow(10, Mathf.Floor(Mathf.Log10(Mathf.Abs(f))) + 1);
        return (1 / scale);
    }

    public enum Axis
    {
        X,
        Y,
        Z
    }

}
