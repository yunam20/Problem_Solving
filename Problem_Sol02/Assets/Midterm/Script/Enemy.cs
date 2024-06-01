using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Camera enemyCamera;
    public FrustumPlanes frustum;
    public Material material1; // �������� ���ο� �ִ� ������Ʈ�� ����� ��Ƽ����
    public Material material2; // �������� �ۿ� �ִ� ������Ʈ�� ����� ��Ƽ����
    public GameObject player;

    public Vector3 patrolAreaCenter; // ���� ������ �߽�
    public float patrolRadius = 1f; // ���� ������ �ݰ�
    public float speed = 3.0f; // �̵� �ӵ�

    private Vector3 nextPosition; // ���� ��ǥ ��ġ
    public bool isGo = true;

    private NavMeshAgent agent;

    public void StartRotation()
    {
        StartCoroutine(RotateOverTime(-90f, 3f)); // 90�� ���� ȸ��
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

        // ù ��° ȸ�� �� �߰� ȸ��
        if (angle < 0)
        {
            // �ݽð� ���� ȸ���̸�, �ð� �������� �߰� ȸ��
            yield return StartCoroutine(RotateOverTime(180f, 3f)); // 180�� ���� ȸ��
        }

        // ȸ���� ������ ����� �Ŀ� isGo�� true�� ����
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

        // ī�޶��� ���������� ��������
        frustum = new FrustumPlanes(enemyCamera);

        // ��� ���� �ִ� ��� Renderer ��������
        Renderer[] renderers = FindObjectsOfType<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            if (renderer.gameObject.CompareTag("Player"))
            {
                Bounds bounds = renderer.bounds;

                // Renderer�� �߽����� �������� ���� �ִ��� Ȯ��
                if (frustum.IsInsideFrustum(bounds))
                {
                    isGo = false;
                    agent.SetDestination(player.transform.position);
                    // �������� ���� �ִ� ��� Material1 ����
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
        // Invoke�� ���� �����ϱ� ���� ������ Invoke�� ���
        CancelInvoke("SetNextPosition");
        Invoke("SetNextPosition", Random.Range(3, 7));
    }

    void CalculateNextPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
        randomDirection.y = 0;
        nextPosition = patrolAreaCenter + randomDirection;

        // NavMesh ���� ��ġ�� ����
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