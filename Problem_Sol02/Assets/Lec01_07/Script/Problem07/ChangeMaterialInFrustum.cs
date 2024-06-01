using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeMaterialInFrustum : MonoBehaviour
{
    public Material material1; // 프러스텀 내부에 있는 오브젝트에 적용될 머티리얼
    public Material material2; // 프러스텀 밖에 있는 오브젝트에 적용될 머티리얼
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
            Debug.LogError("카메라 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        // 카메라의 프러스텀을 가져오기
        frustum = new FrustumPlanes(thisCamera);

        // 모든 씬에 있는 모든 Renderer 가져오기
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Bounds bounds = renderer.bounds;

            // Renderer의 중심점이 프러스텀 내에 있는지 확인
            if (frustum.IsInsideFrustum(bounds))
            {
                // 프러스텀 내에 있는 경우 Material1 적용
                renderer.material = material1;
                MyGameManager.instance.isGo = true;
            }
            else
            {
                // 프러스텀 밖에 있는 경우 Material2 적용
                renderer.material = material2;
                MyGameManager.instance.isGo = false;
            }
        }
    }
}