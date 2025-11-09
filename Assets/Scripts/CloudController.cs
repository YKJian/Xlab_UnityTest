using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField] private Transform[] m_people;
    [SerializeField] private float m_speed = 2f;

    private int m_index = -1;
    private Vector3 m_position;
    private bool m_isMoving;

    // It moves to the 1st position
    private void Start()
    {
        MoveNext();
    }

    public void MoveNext()
    {
        m_index++;

        // Cycle movement
        if (m_index >= m_people.Length)
        {
            m_index = 0;
        }

        m_position = m_people[m_index].position;
        // y is the same all the time
        m_position.y = transform.position.y;

        m_isMoving = true;
    }

    private void Update()
    {
        // process moving only when moving
        if (!m_isMoving)
        {
            return;
        }

        // first multiply numbers, then vector
        // (just an example)
        // Vector3 velocity = Vector3.zero * (m_speed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, m_position, m_speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, m_position) < 0.01f)
        {
            m_isMoving = false;
        }
    }
}
