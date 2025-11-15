using UnityEngine;
using UnityEngine.Events;

public class AtomMatchCheck : MonoBehaviour
{

    [ContextMenu("When atoms interact")]
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collided with: {other.gameObject.name}");
        if (other.gameObject.CompareTag("Hydrogen"))
        {
            if (!MoleculeManager.instance.h2_initialized)
            {
                MoleculeManager.instance.h2_initialized = true;
                MoleculeManager.instance.h2_molecule = 
                    Instantiate
                    (MoleculeManager.instance.h2_prefab,
                    transform.position,
                    Quaternion.identity,
                    transform);
                Debug.Log("H2 Molecule Activated");
            }
            GetComponent<MeshRenderer>().enabled = false;
        }
        if (other.gameObject.CompareTag("Carbon"))
        {
            if (!MoleculeManager.instance.co_initialized)
            {
                MoleculeManager.instance.co_initialized = true;
                MoleculeManager.instance.co_molecule =
                    Instantiate
                    (MoleculeManager.instance.co_prefab,
                    transform.position,
                    Quaternion.identity,
                    transform);
                Debug.Log("CO Molecule Activated");
            }
            GetComponent<MeshRenderer>().enabled = false;
        }
        if (other.gameObject.CompareTag("Oxygen"))
        {
            if (!MoleculeManager.instance.co_initialized)
            {
                MoleculeManager.instance.co_initialized = true;
                MoleculeManager.instance.co_molecule =
                    Instantiate
                    (MoleculeManager.instance.co_prefab,
                    transform.position,
                    Quaternion.identity,
                    transform);
                Debug.Log("CO Molecule Activated");
            }
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    [ContextMenu("When atoms stop interacting")]
    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Lost contact with: {other.gameObject.name}");
        if (other.gameObject.CompareTag("Hydrogen"))
        {
            if (MoleculeManager.instance.h2_initialized)
            {
                MoleculeManager.instance.h2_initialized = false;
                Destroy(MoleculeManager.instance.h2_molecule);
                Debug.Log("H2 Molecule Deactivated");
            }
            GetComponent<MeshRenderer>().enabled = true;
        }
        if (other.gameObject.CompareTag("Carbon"))
        {
            if (MoleculeManager.instance.co_initialized)
            {
                MoleculeManager.instance.co_initialized = false;
                Destroy(MoleculeManager.instance.co_molecule);
                Debug.Log("CO Molecule Deactivated");
            }
            GetComponent<MeshRenderer>().enabled = true;
        }
        if (other.gameObject.CompareTag("Oxygen"))
        {
            if (MoleculeManager.instance.co_initialized)
            {
                MoleculeManager.instance.co_initialized = false;
                Destroy(MoleculeManager.instance.co_molecule);
                Debug.Log("CO Molecule Deactivated");
            }
            GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
