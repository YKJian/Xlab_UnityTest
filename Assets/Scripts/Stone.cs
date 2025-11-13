using UnityEngine;

namespace Golf
{
    public class Stone : MonoBehaviour
    {
        [SerializeField] private int m_clubLayerMask;
        private bool m_hasCollided = false;

        private void OnCollisionEnter(Collision collision)
        {
            if (!m_hasCollided)
            {
                if (collision.gameObject.layer == m_clubLayerMask)
                {
                    Debug.Log("Stone has collided with the club");
                }
                else
                {
                    Debug.Log("Stone has collided with ground");
                }
            }
            
            m_hasCollided = true;
        }
    }
}
