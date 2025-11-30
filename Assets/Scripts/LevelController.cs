using System;
using System.Collections.Generic;
using UnityEngine;

namespace Golf
{
    public class LevelController : MonoBehaviour
    {
        public event Action<int> Finished;

        [SerializeField] private int m_missedCount;
        [SerializeField] [Min(0)] private float m_spawnRate = 0.5f;
        [SerializeField] private StoneSpawner m_stoneSpawner;
        [SerializeField] private ScoreManager m_scoreManager;

        private float m_time;
        private List<Stone> m_stones;
        private int m_currentMissedCount;

        private void OnEnable()
        {
            m_currentMissedCount = m_missedCount;
            m_stones = new List<Stone>();
        }

        private void Update()
        {
            m_time += Time.deltaTime;

            if (m_time >= m_spawnRate)
            {
                Stone stone = m_stoneSpawner.Spawn();
                m_stones.Add(stone);

                stone.Hit += OnHitStone;
                stone.Missed += OnMissed;

                m_time = 0;
            }
        }

        private void OnHitStone(Stone stone)
        {
            Unsubscribe(stone);
            
            m_scoreManager.Increase(stone.score); 
            
            if (m_scoreManager.score <= 0)
            {
                Gameover();
            }
        }

        private void OnMissed(Stone stone)
        {
            Unsubscribe(stone);

            m_currentMissedCount--;

            if (m_currentMissedCount <= 0)
            {
                Gameover();
            }
        }

        private void Unsubscribe(Stone stone)
        {
            stone.Hit -= OnHitStone;
            stone.Missed -= OnMissed;
        }

        private void Gameover()
        {
            Debug.Log("Gameover");
            Finished?.Invoke(m_scoreManager.score);

            foreach (var item in m_stones)
            {
                if (item) Destroy(item.gameObject);
            }
            m_stones.Clear();
        }

        public List<Stone> GetStones() => m_stones;
    }
}
