using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        var tr = this.transform;
        // ��]�N�I�[�^�j�I��
        var _axis = Vector3.up;
        var _center = Vector3.zero;

        //var angleAxis = Quaternion.AngleAxis(360 / 2 * Time.deltaTime, _axis);

        //var pos = tr.position;

        //pos -= _center;
        //pos = angleAxis * pos;
        //pos += _center;

        //tr.position = pos;
        //var _updateRotation = true;

        //if (_updateRotation)
        //{
        //    tr.rotation = tr.rotation * angleAxis;
        //}

        // �}�E�X�̒��S�ɂ���I�u�W�F�N�g�̍��W�ʒu�����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            // Ray�ՓˑΏۂ̃^�O�`�F�b�N
            if (!hit.collider.CompareTag("Obj"))
            {
                return;
            }

            Debug.Log(hit.collider.gameObject.transform.position);

            // �q�C���X�^���X��L����
            var child = GetChildren(hit.collider.gameObject);
            if (!child.activeSelf)
            {
                child.SetActive(true);
            }
            //GameObject child = hit.collider.gameObject.transform.GetChild(0).gameObject;
            //Debug.Log(child);

            //child.SetActive(true);

            //var roundCenter = hit.collider.gameObject.transform.position;
            //if (child != null)
            //{
            //    child.transform.RotateAround(
            //        _center,
            //        _axis,
            //        360 / 2 * Time.deltaTime);
            //}

        }

        // �}�E�X�̒��S�ɂ���I�u�W�F�N�g���ׂĂ̍��W�ʒu�����
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //foreach (RaycastHit hit in Physics.RaycastAll(ray))
        //{
        //    Debug.Log(hit.collider.gameObject.transform.position);
        //}

        //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);
    }

    // �ŏ��̎q�I�u�W�F�N�g��Ԃ�
    private GameObject GetChildren(GameObject gameObject)
    {
        var children = gameObject.transform.GetChild(0).gameObject;
        // �q�I�u�W�F�N�g���Ȃ��ꍇ�͏I��
        if (children == null)
        {
            return null;
        }

        return children;
    }
}
