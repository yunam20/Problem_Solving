using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Camera enemyCamera;
    public FrustumPlanes frustum;
    public Material material1; // 프러스텀 내부에 있는 오브젝트에 적용될 머티리얼
    public Material material2; // 프러스텀 밖에 있는 오브젝트에 적용될 머티리얼
    public GameObject player;

    public Vector3 patrolAreaCenter; // 순찰 구역의 중심
    public float patrolRadius = 1f; // 순찰 구역의 반경
    public float speed = 3.0f; // 이동 속도

    private Vector3 nextPosition; // 다음 목표 위치
    public bool isGo = true;

    private NavMeshAgent agent;

    public void StartRotation()
    {
        StartCoroutine(RotateOverTime(-90f, 3f)); // 90도 좌측 회전
    }

    private IEnumerator RotateOverTime(float angle, float duration)
    {
        Quaternion initialRotation = transform.rotation;
        Quaternion finalRotation = transform.rotation * Quaternion.Euler(0f, angle, 0f);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, finalRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = finalRotation;

        // 첫 번째 회전 후 추가 회전
        if (angle < 0)
        {
            // 반시계 방향 회전이면, 시계 방향으로 추가 회전
            yield return StartCoroutine(RotateOverTime(180f, 3f)); // 180도 우측 회전
        }

        // 회전이 완전히 종료된 후에 isGo를 true로 설정
        isGo = true;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        SetNextPosition();
    }

    void Update()
    {
        if (GameSceneManager.instance.endGame) return;

        // 카메라의 프러스텀을 가져오기
        frustum = new FrustumPlanes(enemyCamera);

        // 모든 씬에 있는 모든 Renderer 가져오기
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            if (renderer.gameObject.CompareTag("Player"))
            {
                Bounds bounds = renderer.bounds;

                // Renderer의 중심점이 프러스텀 내에 있는지 확인
                if (frustum.IsInsideFrustum(bounds))
                {
                    isGo = false;
                    agent.SetDestination(player.transform.position);
                    // 프러스텀 내에 있는 경우 Material1 적용
                    renderer.material = material1;

                    patrolRadius = 500f;
                }
                else
                {
                    patrolRadius = 2.5f;
                    if (isGo)
                    {
                        MoveToNextPosition();
                    }
                    else
                    {
                        StartRotation();
                        renderer.material = material2;
                    }
                }
            }
        }
    }

    void SetNextPosition()
    {
        CalculateNextPosition();
        // Invoke를 새로 설정하기 전에 기존의 Invoke를 취소
        CancelInvoke("SetNextPosition");
        Invoke("SetNextPosition", Random.Range(3, 7));
    }

    void CalculateNextPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection.y = 0;
        nextPosition = patrolAreaCenter + randomDirection;

        // NavMesh 상의 위치로 조정
        NavMeshHit hit;
        if (NavMesh.SamplePosition(nextPosition, out hit, patrolRadius, NavMesh.AllAreas))
        {
            nextPosition = hit.position;
        }
    }

    void MoveToNextPosition()
    {
        if (Vector3.Distance(transform.position, nextPosition) < 0.5f)
        {
            SetNextPosition();
        }
        else
        {
            agent.SetDestination(nextPosition);
        }
    }

    public void UpdatePatrolArea(Vector3 newCenter, float newRadius)
    {
        patrolAreaCenter = newCenter;
        patrolRadius = newRadius;
        CalculateNextPosition();
        SetNextPosition();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(patrolAreaCenter, patrolRadius);
    }
}