using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MapGenerator : MonoBehaviour
{
    public GameObject wall0;
    public GameObject wall1;
    public GameObject wall2;

    public bool MapGeneratorTrue = true;

#if UNITY_EDITOR
    [CustomEditor(typeof(MapGenerator))]
    public class MapGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            MapGenerator generator = (MapGenerator)target;
            if (GUILayout.Button("Generate Objects"))
            {
                generator.GenerateObjects();
            }
        }
    }
#endif

    public void GenerateObjects()
    {
        if (!MapGeneratorTrue) return;

        // CSV 파일 경로
        string filePath = "Assets/Midterm/data.csv";

        // 파일의 모든 라인을 읽기
        string[] lines = File.ReadAllLines(filePath);

        // 2차원 배열 생성 (행의 수는 라인 수, 열의 수는 첫 번째 라인에서 쉼표로 분리된 요소의 수)
        string[,] data = new string[lines.Length, lines[0].Split(',').Length];

        // 각 라인을 처리
        for (int i = 0; i < lines.Length; i++)
        {
            string[] columns = lines[i].Split(',');

            // 각 열을 처리
            for (int j = 0; j < columns.Length; j++)
            {
                data[i, j] = columns[j];
            }
        }

        float x = 17.6f/ 2f - 5.7f;
        float z = 18.6f/ 2f - 5.2f;

        // 데이터 출력 (옵션)
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
                        wallObejct.transform.parent = gameObject.transform;
                        break;
                    case "1":
                        wallObejct = Instantiate(wall1);
                        wallObejct.transform.position = new Vector3(x, 0.5f, z);
                        wallObejct.transform.parent = gameObject.transform;
                        break;
                    case "2":
                        wallObejct = Instantiate(wall2);
                        wallObejct.transform.position = new Vector3(x, 1f, z);
                        wallObejct.transform.parent = gameObject.transform;
                        break;
                }
                x += 1f;
            }
            x = 17.6f / 2f - 5.7f;
            z += 1f;
        }
    }
}
