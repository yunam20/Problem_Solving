using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject wall0;
    public GameObject wall1;
    public GameObject wall2;

    void Start()
    {
        // CSV ���� ���
        string filePath = "Assets/Midterm/data.csv";

        // ������ ��� ������ �б�
        string[] lines = File.ReadAllLines(filePath);

        // 2���� �迭 ���� (���� ���� ���� ��, ���� ���� ù ��° ���ο��� ��ǥ�� �и��� ����� ��)
        string[,] data = new string[lines.Length, lines[0].Split(',').Length];

        // �� ������ ó��
        for (int i = 0; i < lines.Length; i++)
        {
            string[] columns = lines[i].Split(',');

            // �� ���� ó��
            for (int j = 0; j < columns.Length; j++)
            {
                data[i, j] = columns[j];
            }
        }

        float x = 17.6f/ 2f - 5.5f;
        float z = 18.6f/ 2f - 5.5f;

        // ������ ��� (�ɼ�)
        for (int i = 0; i < data.GetLength(0); i++)
        {
            for (int j = 0; j < data.GetLength(1); j++)
            {
                GameObject wallObejct;

                switch (data[i, j])
                {
                    case "0":
                        wallObejct = Instantiate(wall0);
                        wallObejct.transform.position = new Vector3(x, 0.5f, z);
                        break;
                    case "1":
                        wallObejct = Instantiate(wall1);
                        wallObejct.transform.position = new Vector3(x, 0.5f, z);
                        break;
                    case "2":
                        wallObejct = Instantiate(wall2);
                        wallObejct.transform.position = new Vector3(x, 1f, z);
                        break;
                }
                x += 1f;
            }
            x = 17.6f / 2f - 5.5f;
            z += 1f;
        }
    }
}
