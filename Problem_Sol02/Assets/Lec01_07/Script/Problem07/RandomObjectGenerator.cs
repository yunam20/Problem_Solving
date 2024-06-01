using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class RandomObjectGenerator : MonoBehaviour
{
    public GameObject TargetObject;
    public int ObjectNumber = 0;

#if UNITY_EDITOR
    [CustomEditor(typeof(RandomObjectGenerator))]
    public class RandomObjectGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            RandomObjectGenerator generator = (RandomObjectGenerator)target;
            if (GUILayout.Button("Generate Objects"))
            {
                generator.GenerateObjects();
            }
        }
    }
#endif

    public GameObject RootObject; // 루트 오브젝트를 에디터에서 지정할 수 있도록 합니다.

    public void GenerateObjects()
    {
        GameObject parentObject = RootObject; // 첫 번째 생성된 오브젝트를 루트로 사용합니다.

        for (int i = 0; i < ObjectNumber; i++)
        {
            // 랜덤한 위치를 생성합니다.
            Vector3 randomPosition = new Vector3(
                Random.Range(transform.position.x - transform.localScale.x * 0.5f, transform.position.x + transform.localScale.x * 0.5f),
                Random.Range(transform.position.y - transform.localScale.y * 0.5f, transform.position.y + transform.localScale.y * 0.5f),
                Random.Range(transform.position.z - transform.localScale.z * 0.5f, transform.position.z + transform.localScale.z * 0.5f)
            );

            // TargetObject를 생성하고 랜덤한 위치에 배치합니다.
            GameObject newObject = Instantiate(TargetObject, randomPosition, Quaternion.identity);

            // 부모 오브젝트를 설정합니다.
            newObject.transform.parent = parentObject.transform;

            // 다음 오브젝트를 위해 현재 생성된 오브젝트를 부모로 설정합니다 (선택적).
            // 이렇게 하면 첫번째 오브젝트가 루트가 되고, 그 다음 생성된 오브젝트는 루트의 자식, 다음은 자식의 자식이 됩니다.
            parentObject = newObject; // 이 줄을 주석 처리하면 모든 오브젝트가 같은 부모(루트)를 가집니다.
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // Box 형태의 가이드라인을 그립니다.
        Handles.color = Color.yellow;

        Matrix4x4 cubeTransform = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        
        using (new Handles.DrawingScope(cubeTransform))
        {
            Handles.DrawWireCube(Vector3.zero, Vector3.one);
        }
    }
#endif
}
