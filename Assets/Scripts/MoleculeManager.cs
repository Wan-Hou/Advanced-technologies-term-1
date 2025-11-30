using TMPro;
using UnityEngine;

public class MoleculeManager : MonoBehaviour
{
    public static MoleculeManager instance = null;
    [Header("Molecule References")]
    public Molecule h2 = new Molecule();
    public Molecule co = new Molecule();
    public Molecule o2 = new Molecule();
    public Molecule h2o = new Molecule();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class Molecule
{
    public bool initialized;
    public GameObject prefab;
    public string text_to_load;
    public Sprite image_to_load;
    public GameObject instance;

    public Molecule()
    {
        initialized = false;
        prefab = null;
        image_to_load = null;
        instance = null;
    }
}
