using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        transform.Rotate(Time.deltaTime * 360f, 0f, 0f);

        // 오버랩박스를 이용하여 충돌체 검출
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("RedCube"))
            {
                gameObject.SetActive(false);
                GameManager.instance.bulletCount--;
                GameManager.instance.bulletList.Enqueue(gameObject);
                break;
            }
        }
    }
}
