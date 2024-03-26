using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject greenCube;
    public GameObject redCube;
    public GameObject bulletPrefab;
    public Mc.Queue<GameObject> bulletList;
    public int bulletCount;
    public Mc.Stack<int> test;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    void Start()
    {
        // stack 테스트용 코드
        test = new Mc.Stack<int>();
        test.Push(1);
        test.Push(2);
        test.Push(3);
        Debug.Log(test.Pop());
        Debug.Log(test.Pop());

        // 큐를 통한 메모리 풀링
        bulletList = new Mc.Queue<GameObject>();
        bulletCount = 0;

        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletList.Enqueue(bullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireBullet();
        }
    }

    void FireBullet()
    {
        if (bulletCount < 10)
        {
            GameObject bullet = bulletList.Dequeue();
            bullet.SetActive(true);
            bulletCount++;
        }
    }
}
