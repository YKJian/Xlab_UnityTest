using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Golf
{
    [RequireComponent(typeof(Rigidbody))]
    public class Stone : MonoBehaviour
    {
        public event Action<Stone> Hit;
        public event Action<Stone> Missed;

        [SerializeField] private StoneData[] m_data;

        private Rigidbody m_rigidbody;

        public int score { get; private set; } 

        private void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            score = m_data[Random.Range(0, m_data.Length)].score;
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            ChangeDrag(0f);

            if (collision.gameObject.GetComponent<Club>())
            {
                Hit?.Invoke(this);
            }
            else
            {
                Missed?.Invoke(this);
            }
        }

        public void AddForce(Vector3 power) =>
            m_rigidbody.AddForce(power, ForceMode.Force);

        public void ChangeDrag(float drag) =>
            m_rigidbody.linearDamping = drag;
    }
}
