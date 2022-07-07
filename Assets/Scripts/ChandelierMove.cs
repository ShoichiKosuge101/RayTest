using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierMove : MonoBehaviour
{
    private float speed = 0.1f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            this.transform.position += new Vector3(0, 0, 0.1f) * speed;
        }
        if (Input.GetKey(KeyCode.K))
        {
            this.transform.position += new Vector3(0, 0, -0.1f) * speed;
        }

        if (Input.GetKey(KeyCode.L))
        {
            this.transform.position += new Vector3(0.1f, 0, 0) * speed;
        }
        if (Input.GetKey(KeyCode.J))
        {
            this.transform.position += new Vector3(-0.1f, 0, 0) * speed;
        }
    }
}
