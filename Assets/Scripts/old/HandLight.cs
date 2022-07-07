using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandLight : MonoBehaviour
{
    private Vector3 mouse;
    //private Vector3 target;
    private new Light light;
    private float current;

    // Start is called before the first frame update
    void Start()
    {
        light = this.GetComponent<Light>();
        current = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        // light ŠÈˆÕON:OFF
        if (Input.GetMouseButtonDown(0))
        {
            light.intensity = light.intensity > 0 ? 0 : current;
        }

        mouse = Input.mousePosition;
        //Debug.DrawLine(this.transform.position, mouse,Color.red);
        //Debug.Log(mouse);

        //target = Camera.main.ScreenToViewportPoint(new Vector3(mouse.x, mouse.y, 0));
        //this.transform.position = target;
    }
}
