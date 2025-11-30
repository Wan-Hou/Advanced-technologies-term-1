using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AtomMatchCheck : MonoBehaviour
{
    public List<GameObject> connected_h = new List<GameObject>();
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
                        ActivateMolecule(MoleculeManager.instance.h2, transform, "H2 Molecule Activated");
                        GetComponent<MeshRenderer>().enabled = false;
                    }
                    break;
                }
                case "Oxygen":
                {
                    connected_h.Add(other.gameObject);
                    other.gameObject.GetComponent<AtomMatchCheck>().isConnectedToAnotherAtom = true;
                    if (connected_h.Count == 2 && !MoleculeManager.instance.h2o.initialized)
                    {
                        ActivateMolecule(MoleculeManager.instance.h2o, transform, "H2O Molecule Activated");
                        foreach (GameObject h in connected_h)
                        {
                            h.GetComponent<MeshRenderer>().enabled = false;
                        }
                        GetComponent<MeshRenderer>().enabled = false;
                    }
                    break;
                }
                default:
                    break;
            }
        }
        if (other.gameObject.CompareTag("Carbon"))
        {
            if (!MoleculeManager.instance.co.initialized)
            {
                MoleculeManager.instance.co.initialized = true;
                MoleculeManager.instance.co.instance =
                    Instantiate
                    (MoleculeManager.instance.co.prefab,
                    transform.position,
                    Quaternion.identity,
                    transform);
                Debug.Log("CO Molecule Activated");
            }
            GetComponent<MeshRenderer>().enabled = false;
        }
        if (other.gameObject.CompareTag("Oxygen"))
        {
            switch(gameObject.tag)
            {
                case "Carbon":
                {
                    if (!MoleculeManager.instance.co.initialized)
                    {
                        MoleculeManager.instance.co.initialized = true;
                        MoleculeManager.instance.co.instance =
                            Instantiate
                            (MoleculeManager.instance.co.prefab,
                            transform.position,
                            Quaternion.identity,
                            transform);
                        Debug.Log("CO Molecule Activated");
                        GetComponent<MeshRenderer>().enabled = false;
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
            if (MoleculeManager.instance.h2.initialized)
            {
                MoleculeManager.instance.h2.initialized = false;
                Destroy(MoleculeManager.instance.h2.instance);
                Debug.Log("H2 Molecule Deactivated");
            }


            switch (gameObject.tag)
            {
                case "Hydrogen":
                {
                    if (MoleculeManager.instance.h2.initialized && !isConnectedToAnotherAtom)
                    {
                        MoleculeManager.instance.h2.initialized = false;
                        Destroy(MoleculeManager.instance.h2.instance);
                        Debug.Log("H2 Molecule Deactivated");
                        GetComponent<MeshRenderer>().enabled = true;
                    }
                    break;
                }
                case "Oxygen":
                {
                    other.gameObject.GetComponent<AtomMatchCheck>().isConnectedToAnotherAtom = false;
                    if (MoleculeManager.instance.h2o.initialized)
                    {
                        MoleculeManager.instance.h2o.initialized = false;
                        Destroy(MoleculeManager.instance.h2o.instance);
                        Debug.Log("H2O Molecule Deactivated");
                        foreach (GameObject h in connected_h)
                        {
                            h.GetComponent<MeshRenderer>().enabled = false;
                        }
                        GetComponent<MeshRenderer>().enabled = false;
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
            if (MoleculeManager.instance.co.instance)
            {
                MoleculeManager.instance.co.initialized = false;
                Destroy(MoleculeManager.instance.co.instance);
                Debug.Log("CO Molecule Deactivated");
            }
            GetComponent<MeshRenderer>().enabled = true;
        }
        if (other.gameObject.CompareTag("Oxygen"))
        {
            if (MoleculeManager.instance.co.instance)
            {
                MoleculeManager.instance.co.initialized = false;
                Destroy(MoleculeManager.instance.co.instance);
                Debug.Log("CO Molecule Deactivated");
                GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    public void ActivateMolecule(Molecule molecule, Transform transform, string log)
    {
        molecule.initialized = true;
        molecule.instance =
            Instantiate
            (molecule.prefab,
            transform.position,

            Quaternion.identity,
            transform);
        Debug.Log(log);
    }

    public void DisableMolecule(Molecule molecule, string log)
    {
        molecule.initialized = false;
        Destroy(molecule.instance);
        Debug.Log(log);
    }

}
