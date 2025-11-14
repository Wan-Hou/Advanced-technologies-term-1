using UnityEngine;

public class Buttons : MonoBehaviour
{
    public void QuitApplication()
    {
        Debug.Log("Quitting application.");
        UIManager.instance.QuitApplication();
    }
}
