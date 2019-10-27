using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    public float tumble;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // TODO: add rotation
    }
}