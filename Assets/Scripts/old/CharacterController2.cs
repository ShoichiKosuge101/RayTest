using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterController2 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject obj;
    private GameObject shadow;
    private Vector3 plane_pos;
    private Light handLight;

    // オブジェクトまでの距離
    private float distA = 0f;
    // 壁オブジェクトまでの距離
    private float distB = 0f;

    void Start()
    {
        handLight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        // shadow 初期化
        shadow = null;

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-0.01f, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0.01f, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0.01f, 0);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -0.01f, 0);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Instance Create");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit=new RaycastHit();

            if(Physics.Raycast(ray,out hit))
            {
                Instantiate(obj, hit.point, Quaternion.identity);
            }
        }

        if (Input.GetMouseButtonDown(0) && handLight.intensity>0)
        {
            //マウスの中心にあるオブジェクトすべての座標位置を取る
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            foreach (RaycastHit hit in Physics.RaycastAll(ray))
            {
                //Debug.Log($"hit: {hit.collider.gameObject.transform.position}");
                Debug.Log(hit.transform.name);
                if (hit.collider.CompareTag("Casting"))
                {
                    // 影オブジェクトの取得
                    shadow = hit.transform.GetChild(0).gameObject;
                    Debug.Log($"ShadowObj: {shadow.transform.position}");

                    // ライトから親オブジェクトまでの距離
                    distA = (hit.point - this.handLight.transform.position).magnitude;
                    Debug.Log($"オブジェクトまでの距離: {distA}");
                }

                if(hit.collider.CompareTag("Screen")) 
                {
                    plane_pos = hit.point;

                    // ライトから壁オブジェクトまでの距離
                    distB = (hit.point - this.handLight.transform.position).magnitude;
                    Debug.Log($"壁までの距離 {distB}");
                }
            }

            // 投影オブジェクトが取得できる場合
            if (shadow != null && plane_pos!= null)
            {
                Debug.Log("Shadow is found");
                shadow.transform.position = new Vector3(plane_pos.x, plane_pos.y, plane_pos.z);

                // 比率計算
                var ratio = distB / distA;
                Debug.Log($"比率: {ratio}");

                // 影オブジェクトの拡大
                shadow.transform.localScale = new Vector3(ratio, ratio, shadow.transform.localScale.z);
            }



        }

    }
}
