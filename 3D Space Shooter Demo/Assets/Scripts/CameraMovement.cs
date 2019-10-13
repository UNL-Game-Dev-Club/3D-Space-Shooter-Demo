using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] GameObject player;
    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 1, -2.75f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // TODO: add camera movement code
        }
    }
}
