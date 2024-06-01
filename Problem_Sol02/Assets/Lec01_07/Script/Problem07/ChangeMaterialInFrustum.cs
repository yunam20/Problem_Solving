using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeMaterialInFrustum : MonoBehaviour
{
    public Material material1; // �������� ���ο� �ִ� ������Ʈ�� ����� ��Ƽ����
    public Material material2; // �������� �ۿ� �ִ� ������Ʈ�� ����� ��Ƽ����
    public FrustumPlanes frustum;
    private Camera thisCamera;
    
    private void Start()
    {
        thisCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (thisCamera == null)
        {
            Debug.LogError("ī�޶� ������Ʈ�� ã�� �� �����ϴ�.");
            return;
        }

        // ī�޶��� ���������� ��������
        frustum = new FrustumPlanes(thisCamera);

        // ��� ���� �ִ� ��� Renderer ��������
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Bounds bounds = renderer.bounds;

            // Renderer�� �߽����� �������� ���� �ִ��� Ȯ��
            if (frustum.IsInsideFrustum(bounds))
            {
                // �������� ���� �ִ� ��� Material1 ����
                renderer.material = material1;
                MyGameManager.instance.isGo = true;
            }
            else
            {
                // �������� �ۿ� �ִ� ��� Material2 ����
                renderer.material = material2;
                MyGameManager.instance.isGo = false;
            }
        }
    }
}