using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour
{
    // TODO: add destroy code
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}