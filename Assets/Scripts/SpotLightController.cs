using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightController : MonoBehaviour
{
    // ���C�g����I�u�W�F�N�g�܂ł̋���
    private float castDist;
    // �I�u�W�F�N�g����ǂ܂ł̋���
    private float planeDist;
    // ���C�g���x
    private Light _light;
    [SerializeField] float _Insentity=5.0f;

    // �X�|�b�g���C�g����܂������Ǝ˂����Ray
    private Ray ray;
    // Ray�̒���
    private float _rayLength;

    // �e�I�u�W�F�N�g
    private GameObject shadow;
    // �ǃI�u�W�F�N�g���W
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
        // �e�I�u�W�F�N�g������
        shadow = null;

        // Ray���Ǝ�
        if (Input.GetKeyDown(KeyCode.F))
        {
            _light.intensity = _light.intensity > 0 ? 0 : _Insentity;
        }
        // ����OFF�̎��͌v�Z���Ȃ�
        if (_light.intensity <= 0)
        {
            return;
        }

        ray = new Ray(transform.position, transform.forward);
        // foreach
        foreach (RaycastHit hit in Physics.RaycastAll(ray,_rayLength))
        {
            // �I�u�W�F�N�g�ȈՃ`�F�b�N
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
                // Ray��\��
                Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.blue, 10, false);

                shadow = hit.transform.GetChild(0).gameObject;
                Debug.Log($"ShadowObj: {shadow.transform.position}");

                // Casting���̋���
                castDist = (hit.point - ray.origin).magnitude;
                Debug.Log($"�I�u�W�F�N�g�܂ł̋���: {castDist}");
            }

            if (hit.collider.CompareTag("Screen"))
            {
                // Ray��\��
                //Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red, 10, false);

                plane_pos = hit.point;

                // �ǂ܂ł̋���
                planeDist = (hit.point - ray.origin).magnitude;
                Debug.Log($"�ǂ܂ł̋���: {planeDist}");
            }
        }

        // ���e�I�u�W�F�N�g���擾�ł���ꍇ
        if (shadow != null && plane_pos != null)
        {
            // �䗦�v�Z
            var ratio = planeDist / castDist;
            Debug.Log($"ratio: {ratio}");
            shadow.transform.localScale = new Vector3(ratio, ratio, shadow.transform.localScale.z);


            Debug.Log("Shadow is found");
            shadow.transform.position = new Vector3(plane_pos.x, plane_pos.y, plane_pos.z);
        }


    }
}
