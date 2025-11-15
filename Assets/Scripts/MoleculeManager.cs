using TMPro;
using UnityEngine;

public class MoleculeManager : MonoBehaviour
{
    public static MoleculeManager instance = null;
    [Header("Molecule References")]
    public bool h2_initialized = false;
    public GameObject h2_prefab = null;
    public GameObject h2_molecule = null;
    public bool co_initialized = false;
    public GameObject co_prefab = null;
    public GameObject co_molecule = null;

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
