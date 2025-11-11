using UnityEngine;

namespace Old
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private FreeCamera m_camera;
        [SerializeField] private GameObject m_uiPanel;
        [SerializeField] private CloudController m_cloudController;
        [SerializeField] private VillagerToolController m_villagerToolController;

        private void Update()
        {
            // Camera don't have to know about uiPanel
            // activeSelf - newer active
            // [Obsolete] modifier makes method obsolete
            if (m_uiPanel.activeSelf)
            {
                return;
            }
            m_camera.Move();

            if (Input.GetKeyDown(KeyCode.Z))
            {
                m_cloudController.MoveNext();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_villagerToolController.ChangeTools();
            }
        }
    }
}
