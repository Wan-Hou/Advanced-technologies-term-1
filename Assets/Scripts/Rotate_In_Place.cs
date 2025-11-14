using UnityEngine;

public class Rotate_In_Place : MonoBehaviour
{
    public Vector3 rotation;

     void Update()
    {
        this.transform.Rotate(rotation);
    }
}
