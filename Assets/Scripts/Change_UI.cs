using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Change_UI : MonoBehaviour
{
    public string text_to_load;
    public GameObject ui_panel;
    public TextMeshProUGUI ui_text_reference;
    public Sprite image_to_load;
    public Image ui_image_reference;

    public void LoadTextIntoUI()
    {
        ui_text_reference.text = text_to_load;
        ui_image_reference.sprite = image_to_load;
        ui_panel.gameObject.SetActive(true);
    }

    public void DisableUI()
    {
        ui_panel.gameObject.SetActive(false);
    }

}
