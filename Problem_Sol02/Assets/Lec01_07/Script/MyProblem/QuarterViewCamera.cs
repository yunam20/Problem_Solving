using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuarterViewCamera : MonoBehaviour
{
    public Transform target; // ī�޶� �ٶ� Ÿ�� ������Ʈ
    public Vector3 offset = new Vector3(0, 10, -10); // Ÿ�� ��� ī�޶��� ��ġ ������
    public float rotationAngleX = 30.0f; // X�� ���� ȸ�� ����
    public float rotationAngleY = 45.0f; // Y�� ���� ȸ�� ����

    void Update()
    {
        // ī�޶� ��ġ �ʱ�ȭ
        transform.position = target.position + offset;

        // ī�޶� ȸ�� ����
        transform.rotation = Quaternion.Euler(rotationAngleX, rotationAngleY, 0);

        // Ÿ���� �ٶ󺸰� ����
        transform.LookAt(target);
    }
}