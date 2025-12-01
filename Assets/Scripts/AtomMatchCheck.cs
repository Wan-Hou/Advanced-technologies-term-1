using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AtomMatchCheck : MonoBehaviour
{
    public List<GameObject> connected_h = new List<GameObject>();
    public List<GameObject> connected_o = new List<GameObject>();
    public bool isConnectedToAnotherAtom = false;

    [ContextMenu("When atoms interact")]
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collided with: {other.gameObject.name}");
        if (other.gameObject.CompareTag("Hydrogen"))
        {
            switch (gameObject.tag)
            {
                case "Hydrogen":
                {
                    if (!MoleculeManager.instance.h2.initialized && !isConnectedToAnotherAtom)
                    {
                        ActivateMolecule(MoleculeManager.instance.h2, transform, transform.parent, "H2 Molecule Activated");
                        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                    break;
                }
                case "Oxygen":
                {
                    connected_h.Add(other.gameObject);
                    other.gameObject.GetComponent<AtomMatchCheck>().isConnectedToAnotherAtom = true;
                    if (connected_h.Count == 2 && !MoleculeManager.instance.h2o.initialized)
                    {
                        ActivateMolecule(MoleculeManager.instance.h2o, transform, transform.parent, "H2O Molecule Activated");
                        foreach (GameObject h in connected_h)
                        {
                            h.GetComponent<MeshRenderer>().enabled = false;
                        }
                    }
                    break;
                }
                default:
                    break;
            }
        }
        if (other.gameObject.CompareTag("Carbon"))
        {
            switch (gameObject.tag)
            {
                case "Oxygen":
                {
                    if (!MoleculeManager.instance.co.initialized)
                    {
                        ActivateMolecule(MoleculeManager.instance.co, transform, transform.parent, "CO Molecule Activated");
                        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                    break;
                }
                default:
                    break;
            }
        }
        if (other.gameObject.CompareTag("Oxygen"))
        {
            switch(gameObject.tag)
            {
                case "Carbon":
                {
                    bool alreadyConnected = false;
                    foreach (GameObject o in connected_o)
                    {
                        if (o == other.gameObject) 
                        {
                            alreadyConnected = true;
                        }
                    }
                    if (alreadyConnected) break;

                    connected_o.Add(other.gameObject);
                    other.gameObject.GetComponent<AtomMatchCheck>().isConnectedToAnotherAtom = true;
                    if (connected_o.Count == 2 && !MoleculeManager.instance.co2.initialized)
                    {
                        ActivateMolecule(MoleculeManager.instance.co2, transform, transform.parent, "CO2 Molecule Activated");
                        foreach (GameObject o in connected_o)
                        {
                            o.GetComponent<MeshRenderer>().enabled = false;
                        }

                        if (MoleculeManager.instance.co.initialized)
                        {
                            DisableMolecule(MoleculeManager.instance.co, "CO Molecule Deactivated");
                        }
                    }
                    if (connected_o.Count == 1 && !MoleculeManager.instance.co.initialized)
                    {
                        ActivateMolecule(MoleculeManager.instance.co, transform, transform.parent, "CO Molecule Activated");
                        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
                    }
                    break;
                }
                default:
                    break;
            }
            
        }
    }

    [ContextMenu("When atoms stop interacting")]
    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Lost contact with: {other.gameObject.name}");
        if (other.gameObject.CompareTag("Hydrogen"))
        {
            switch (gameObject.tag)
            {
                case "Hydrogen":
                {
                    if (MoleculeManager.instance.h2.initialized && !isConnectedToAnotherAtom)
                    {
                        DisableMolecule(MoleculeManager.instance.h2, "H2 Molecule Deactivated");
                        other.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    break;
                }
                case "Oxygen":
                {
                    other.gameObject.GetComponent<AtomMatchCheck>().isConnectedToAnotherAtom = false;
                    if (MoleculeManager.instance.h2o.initialized)
                    {
                        DisableMolecule(MoleculeManager.instance.h2o, "H2O Molecule Deactivated");
                        foreach (GameObject h in connected_h)
                        {
                            h.GetComponent<MeshRenderer>().enabled = true;
                        }
                    }
                    connected_h.Remove(other.gameObject);
                    break;
                }
                default:
                    break;
            }
        }
        if (other.gameObject.CompareTag("Carbon"))
        {
            switch (gameObject.tag)
            {
                case "Oxygen":
                    {
                        if (MoleculeManager.instance.co.initialized)
                        {
                            DisableMolecule(MoleculeManager.instance.co, "CO Molecule Deactivated");
                            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        if (other.gameObject.CompareTag("Oxygen"))
        {
            switch (gameObject.tag)
            {
                case "Carbon":
                    {
                        other.gameObject.GetComponent<AtomMatchCheck>().isConnectedToAnotherAtom = false;
                        if (connected_o.Count == 1 && MoleculeManager.instance.co.initialized)
                        {
                            DisableMolecule(MoleculeManager.instance.co, "CO Molecule Deactivated");
                            foreach (GameObject o in connected_o)
                            {
                                o.GetComponent<MeshRenderer>().enabled = true;
                            }
                            connected_o.Remove(other.gameObject);
                        }
                        if (connected_o.Count == 2 && MoleculeManager.instance.co2.initialized)
                        {
                            DisableMolecule(MoleculeManager.instance.co2, "CO2 Molecule Deactivated");
                            foreach (GameObject o in connected_o)
                            {
                                o.GetComponent<MeshRenderer>().enabled = true;
                            }
                            connected_o.Remove(other.gameObject);
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }

    public void ActivateMolecule(Molecule molecule, Transform transform, Transform parent, string log)
    {
        molecule.initialized = true;
        molecule.instance =
            Instantiate
            (molecule.prefab,
            transform.position,
            transform.rotation,
            parent);
        GetComponent<MeshRenderer>().enabled = false;
        Debug.Log(log);
        UIManager.instance.InfoLoad(molecule.text_to_load, molecule.image_to_load);
    }

    public void DisableMolecule(Molecule molecule, string log)
    {
        molecule.initialized = false;
        Destroy(molecule.instance);
        Debug.Log(log);
        GetComponent<MeshRenderer>().enabled = true;
        UIManager.instance.DisableInfo(molecule.text_to_load);
        GetComponentInParent<Change_UI>().LoadTextIntoUI();
    }

}
