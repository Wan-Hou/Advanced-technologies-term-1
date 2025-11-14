using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    [Header("UI References")]
    public GameObject information_panel;
    public GameObject tutorial_panel;
    public TextMeshProUGUI information_text_reference;
    public Image information_image_reference;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void InfoLoad(string text_to_load, Sprite image_to_load)
    {
        tutorial_panel.gameObject.SetActive(false);
        information_text_reference.text = text_to_load;
        information_image_reference.sprite = image_to_load;
        information_panel.gameObject.SetActive(true);
    }

    public void DisableInfo(string loaded_text)
    {
        if (information_text_reference.text == loaded_text)
        {
            information_panel.gameObject.SetActive(false);
        }
    }

    public void QuitApplication()
    {
        information_panel.gameObject.SetActive(false);

        try
        {
            // For Android, move the task to the background instead of quitting
            AndroidJavaObject activity =
                new AndroidJavaClass("com.unity3d.player.UnityPlayer").
                    GetStatic<AndroidJavaObject>("currentActivity");
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
