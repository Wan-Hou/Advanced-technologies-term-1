using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Change_UI : MonoBehaviour
{
    public string text_to_load;
    public GameObject ui_panel, tutorial_panel;
    public TextMeshProUGUI ui_text_reference;
    public Sprite image_to_load;
    public Image ui_image_reference;

    public void LoadTextIntoUI()
    {
        tutorial_panel.gameObject.SetActive(false);
        Debug.Log("Found");
        ui_text_reference.text = text_to_load;
        ui_image_reference.sprite = image_to_load;
        ui_panel.gameObject.SetActive(true);
    }

    public void DisableUI()
    {
        Debug.Log("Lost");
        if (ui_text_reference.text == text_to_load)
        {
            ui_panel.gameObject.SetActive(false);
        }
    }

}
