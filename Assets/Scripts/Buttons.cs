using UnityEngine;

public class Buttons : MonoBehaviour
{
    public GameObject ui_panel, tutorial_panel;
    public void QuitApplication()
    {
        Debug.Log("Quitting application.");
        ui_panel.gameObject.SetActive(false);
        tutorial_panel.gameObject.SetActive(true);

        try
        {
            // For Android, move the task to the background instead of quitting
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            activity.Call<bool>("moveTaskToBack", true);
        }
        catch (System.Exception e)
        {
            Debug.Log("Not running on Android: " + e.Message);
            // For other platforms, you can use Application.Quit()
            Application.Quit();
        } 
    }
}
