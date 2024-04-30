using UnityEngine;
using System.Collections;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera; // �÷��̾� ī�޶� ���� public ����
    public float rotationSpeed = 90.0f; // �ʴ� ȸ�� �ӵ�
    private float targetAngle = 0; // ��ǥ ����
    private bool isRotating = false; // ȸ�� ������ Ȯ���ϴ� �÷���
    public TextMeshProUGUI text;

    public float speed = 5.0f; // �÷��̾��� �̵� �ӵ�

    private void Start()
    {
        // ī�޶� �������� �ʾ��� ���, ���� ī�޶� �ڵ����� ã�� �Ҵ�
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (GameSceneManager.instance.endGame) return;

        // �÷��̾��� �̵� �Է��� �޽��ϴ�.
        float horizontal = Input.GetAxis("Horizontal"); // A, D Ű
        float vertical = Input.GetAxis("Vertical"); // W, S Ű

        // ī�޶��� ���⿡ ���� �̵� ������ ����մϴ�.
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;

        // ī�޶� �ٶ󺸴� ������ Y ������ �����Ͽ� ���� �̵��� ����ϰ� �մϴ�.
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // ���� �̵� ���͸� ����մϴ�.
        Vector3 moveDirection = (cameraForward * vertical + cameraRight * horizontal).normalized;

        // �̵� ���Ϳ� ���� �÷��̾ �̵���ŵ�ϴ�.
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        // ī�޶� ȸ�� �Է� ó��
        if (Input.GetKeyDown(KeyCode.O))
        {
            RotateCamera(-90);
            playerCamera.transform.LookAt(transform);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            RotateCamera(90);
            playerCamera.transform.LookAt(transform);
        }

        if (isRotating)
        {
            PerformRotation();
        }

        //playerCamera.transform.LookAt(transform);
    }

    void RotateCamera(float angle)
    {
        if (isRotating)
            return;

        targetAngle += angle;
        isRotating = true;
        StartCoroutine(RotateOverTime(angle));
    }

    IEnumerator RotateOverTime(float angle)
    {
        float startAngle = playerCamera.transform.eulerAngles.y;
        float endAngle = startAngle + angle;
        float timeElapsed = 0;

        while (timeElapsed < 1)
        {
            float angleToRotate = Mathf.Lerp(startAngle, endAngle, timeElapsed / 1);
            playerCamera.transform.RotateAround(transform.position, Vector3.up, angleToRotate - playerCamera.transform.eulerAngles.y);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        playerCamera.transform.RotateAround(transform.position, Vector3.up, endAngle - playerCamera.transform.eulerAngles.y);
        isRotating = false;
    }

    void PerformRotation()
    {
        if (Mathf.Abs(playerCamera.transform.eulerAngles.y - targetAngle) > 0.01f)
        {
            float step = rotationSpeed * Time.deltaTime;
            float angle = Mathf.MoveTowardsAngle(playerCamera.transform.eulerAngles.y, targetAngle, step);
            playerCamera.transform.RotateAround(transform.position, Vector3.up, angle - playerCamera.transform.eulerAngles.y);
        }
        else
        {
            isRotating = false;
            playerCamera.transform.eulerAngles = new Vector3(playerCamera.transform.eulerAngles.x, targetAngle, playerCamera.transform.eulerAngles.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedCube"))
        {
            GameSceneManager.instance.endGame = true;
            text.text = "CLEAR!";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameSceneManager.instance.endGame = true;
            text.text = "Game Over!";
        }
    }
}