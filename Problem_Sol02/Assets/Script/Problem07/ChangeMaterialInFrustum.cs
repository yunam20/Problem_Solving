using Unity.VisualScripting;
using UnityEngine;

public class ChangeMaterialInFrustum : MonoBehaviour
{
    public Material material1; // 프러스텀 내부에 있는 오브젝트에 적용될 머티리얼
    public Material material2; // 프러스텀 밖에 있는 오브젝트에 적용될 머티리얼

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
        FrustumPlanes frustum = new FrustumPlanes(thisCamera);

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
            }
            else
            {
                // 프러스텀 밖에 있는 경우 Material2 적용
                renderer.material = material2;
            }
        }
    }
}

// 프러스텀 플레인 클래스
public class FrustumPlanes
{
    private readonly Plane[] planes;

    public FrustumPlanes(Camera camera)
    {
        planes = GeometryUtility.CalculateFrustumPlanes(camera);
    }

    public bool IsInsideFrustum(Bounds bounds)
    {
        Vector3[] corners = new Vector3[8];

        // Compute all 8 corners of the bounds
        corners[0] = bounds.min;
        corners[1] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        corners[2] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        corners[3] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
        corners[4] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        corners[5] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        corners[6] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        corners[7] = bounds.max;

        // Check each corner to see if any of them is inside the frustum
        foreach (Vector3 corner in corners)
        {
            bool inside = true;
            foreach (Plane plane in planes)
            {
                if (plane.GetDistanceToPoint(corner) < 0)
                {
                    inside = false;
                    break;
                }
            }
            if (inside)
            {
                return true;
            }
        }
        return false;
    }
}