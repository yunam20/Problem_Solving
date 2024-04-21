using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    int currentHp = 100;
    float speed = 1f;
    // Start is called before the first frame update

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.color = Color.blue; // 오브젝트의 색상을 빨간색으로 변경
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        if (MyGameManager.instance.player.transform != null && currentHp > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                MyGameManager.instance.player.transform.position,speed * Time.deltaTime);
        }
    }
}
