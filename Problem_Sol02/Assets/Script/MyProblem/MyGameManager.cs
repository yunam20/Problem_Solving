using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MyGameManager : MonoBehaviour
{
    public static MyGameManager instance;
    public PoolManager pool;
    public GameObject player;
    float spawnTime = 0f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    private void Update()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime > 1f)
        {
            // spawnEnemy(0);
            spawnEnemyTransform(0);
            spawnTime = 0f;
        }
    }

    public void spawnEnemy(int index)
    {
        // ���͸� �����ϰ� ��ġ�� �����մϴ�. 
        float randomAngle = Random.Range(0f, 360f);

        // ������ �������� ��ȯ�մϴ�.
        float radians = randomAngle * Mathf.Deg2Rad;

        // ���ѷ� ���� ������ ��ġ�� ����մϴ�.
        Vector3 spawnPosition = player.transform.position + new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f) * 12f;

        // ������Ʈ Ǯ���� ���� ����
        GameObject enemy = pool.Get(index);
        enemy.transform.position = spawnPosition;
    }

    public void spawnEnemyTransform(int index)
    {
        float randomX = Random.Range(-5f, 5f);
        float randomY = Random.Range(-5f, 5f);

        // ���ѷ� ���� ������ ��ġ�� ����մϴ�.
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        // ������Ʈ Ǯ���� ���� ����
        GameObject enemy = pool.Get(index);
        enemy.transform.position = spawnPosition;
    }
}
