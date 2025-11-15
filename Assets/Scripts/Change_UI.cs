using UnityEngine;

public class Change_UI : MonoBehaviour
{
    public string text_to_load;
    public Sprite image_to_load;

    public void LoadTextIntoUI()
    {
        Debug.Log($"{transform.name} Found");
        UIManager.instance.InfoLoad
            (text_to_load, image_to_load);
    }

    public void DisableUI()
    {
        Debug.Log($"{transform.name} Lost");
        UIManager.instance.DisableInfo
            (text_to_load);
    }

}
