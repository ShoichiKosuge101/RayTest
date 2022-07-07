using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightController : MonoBehaviour
{
    // ライトからオブジェクトまでの距離
    private float castDist;
    // オブジェクトから壁までの距離
    private float planeDist;
    // ライト強度
    private Light _light;
    [SerializeField] float _Insentity=5.0f;

    // スポットライトからまっすぐ照射されるRay
    private Ray ray;
    // Rayの長さ
    private float _rayLength;

    // 影オブジェクト
    private GameObject shadow;
    // 壁オブジェクト座標
    private Vector3 plane_pos;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        _rayLength = _light.range;
    }

    // Update is called once per frame
    void Update()
    {
        // 影オブジェクト初期化
        shadow = null;

        // Rayを照射
        if (Input.GetKeyDown(KeyCode.F))
        {
            _light.intensity = _light.intensity > 0 ? 0 : _Insentity;
        }
        // 光がOFFの時は計算しない
        if (_light.intensity <= 0)
        {
            return;
        }

        ray = new Ray(transform.position, transform.forward);
        // foreach
        foreach (RaycastHit hit in Physics.RaycastAll(ray,_rayLength))
        {
            // オブジェクト簡易チェック
            //hit.collider.GetComponent<MeshRenderer>().material.color =
            //    hit.collider.tag switch
            //    {
            //        "Casting"=>Color.white,
            //        "ShadowObj"=>Color.red,
            //        _ => Color.green
            //    };

            //Debug.Log($"hit: {hit.collider.gameObject.transform.position}");
            //Debug.Log(hit.transform.name);
            if (hit.collider.CompareTag("Casting"))
            {
                // Rayを表示
                Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.blue, 10, false);

                shadow = hit.transform.GetChild(0).gameObject;
                Debug.Log($"ShadowObj: {shadow.transform.position}");

                // Casting迄の距離
                castDist = (hit.point - ray.origin).magnitude;
                Debug.Log($"オブジェクトまでの距離: {castDist}");
            }

            if (hit.collider.CompareTag("Screen"))
            {
                // Rayを表示
                //Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red, 10, false);

                plane_pos = hit.point;

                // 壁までの距離
                planeDist = (hit.point - ray.origin).magnitude;
                Debug.Log($"壁までの距離: {planeDist}");
            }
        }

        // 投影オブジェクトが取得できる場合
        if (shadow != null && plane_pos != null)
        {
            // 比率計算
            var ratio = planeDist / castDist;
            Debug.Log($"ratio: {ratio}");
            shadow.transform.localScale = new Vector3(ratio, ratio, shadow.transform.localScale.z);


            Debug.Log("Shadow is found");
            shadow.transform.position = new Vector3(plane_pos.x, plane_pos.y, plane_pos.z);
        }


    }
}
