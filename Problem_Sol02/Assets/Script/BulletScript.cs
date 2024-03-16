using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        gameObject.transform.position =
            GameManager.instance.greenCube.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
        GameManager.instance.bulletCount--;
        GameManager.instance.bulletList.Enqueue(gameObject);
    }
}
