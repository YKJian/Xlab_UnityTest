using UnityEngine;

namespace Old
{
    public class VillagerToolController : MonoBehaviour
    {
        [SerializeField] private GameObject[] m_toolPrefabs;
        [SerializeField] private GameObject[] m_tools;

        public void ChangeTools()
        {
            for (int i = 0; i < m_tools.Length; i++)
            {
                GameObject randomTool = m_toolPrefabs[Random.Range(0, m_toolPrefabs.Length)];
                m_tools[i] = ReplaceTool(m_tools[i], randomTool);
            }
        }

        private GameObject ReplaceTool(GameObject oldTool, GameObject randomTool)
        {
            Vector3 position = oldTool.transform.position;
            Quaternion rotation = oldTool.transform.rotation;
            Transform parent = oldTool.transform.parent;

            GameObject newTool = Instantiate(randomTool, position, rotation, parent);
            Destroy(oldTool);
            return newTool;
        }
    }
}
