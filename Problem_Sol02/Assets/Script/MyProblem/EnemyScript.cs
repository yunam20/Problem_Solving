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
