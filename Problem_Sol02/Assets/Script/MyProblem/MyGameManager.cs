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
        // 몬스터를 생성하고 위치를 설정합니다. 
        float randomAngle = Random.Range(0f, 360f);

        // 각도를 라디안으로 변환합니다.
        float radians = randomAngle * Mathf.Deg2Rad;

        // 원둘레 상의 무작위 위치를 계산합니다.
        Vector3 spawnPosition = player.transform.position + new Vector3(Mathf.Cos(radians), Mathf.Sin(radians), 0f) * 12f;

        // 오브젝트 풀링을 통한 생성
        GameObject enemy = pool.Get(index);
        enemy.transform.position = spawnPosition;
    }

    public void spawnEnemyTransform(int index)
    {
        float randomX = Random.Range(-5f, 5f);
        float randomY = Random.Range(-5f, 5f);

        // 원둘레 상의 무작위 위치를 계산합니다.
        Vector3 spawnPosition = new Vector3(randomX, randomY, 0);

        // 오브젝트 풀링을 통한 생성
        GameObject enemy = pool.Get(index);
        enemy.transform.position = spawnPosition;
    }
}
