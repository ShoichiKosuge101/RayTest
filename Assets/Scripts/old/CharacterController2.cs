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

    // �I�u�W�F�N�g�܂ł̋���
    private float distA = 0f;
    // �ǃI�u�W�F�N�g�܂ł̋���
    private float distB = 0f;

    void Start()
    {
        handLight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        // shadow ������
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
            //�}�E�X�̒��S�ɂ���I�u�W�F�N�g���ׂĂ̍��W�ʒu�����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            foreach (RaycastHit hit in Physics.RaycastAll(ray))
            {
                //Debug.Log($"hit: {hit.collider.gameObject.transform.position}");
                Debug.Log(hit.transform.name);
                if (hit.collider.CompareTag("Casting"))
                {
                    // �e�I�u�W�F�N�g�̎擾
                    shadow = hit.transform.GetChild(0).gameObject;
                    Debug.Log($"ShadowObj: {shadow.transform.position}");

                    // ���C�g����e�I�u�W�F�N�g�܂ł̋���
                    distA = (hit.point - this.handLight.transform.position).magnitude;
                    Debug.Log($"�I�u�W�F�N�g�܂ł̋���: {distA}");
                }

                if(hit.collider.CompareTag("Screen")) 
                {
                    plane_pos = hit.point;

                    // ���C�g����ǃI�u�W�F�N�g�܂ł̋���
                    distB = (hit.point - this.handLight.transform.position).magnitude;
                    Debug.Log($"�ǂ܂ł̋��� {distB}");
                }
            }

            // ���e�I�u�W�F�N�g���擾�ł���ꍇ
            if (shadow != null && plane_pos!= null)
            {
                Debug.Log("Shadow is found");
                shadow.transform.position = new Vector3(plane_pos.x, plane_pos.y, plane_pos.z);

                // �䗦�v�Z
                var ratio = distB / distA;
                Debug.Log($"�䗦: {ratio}");

                // �e�I�u�W�F�N�g�̊g��
                shadow.transform.localScale = new Vector3(ratio, ratio, shadow.transform.localScale.z);
            }



        }

    }
}
