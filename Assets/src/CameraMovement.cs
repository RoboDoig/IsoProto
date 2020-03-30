using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float scrollSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f).normalized * Time.deltaTime * moveSpeed;

        // zoom is set by camera orthographic size
        Camera.main.orthographicSize += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * scrollSpeed;
    }
}
