using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class ScoreBoard : MonoBehaviour
    {
        [SerializeField] private Button m_scoreBoardButton;
        [SerializeField] private GameObject m_scoreBoard;
        [SerializeField] private TextMeshProUGUI[] m_recentScores;
        [SerializeField] private string m_format;

        private List<string> m_stringScores = new List<string>();
        private int m_storedScores;

        private void Awake()
        {
            for (int i = 0; i < m_recentScores.Length; i++)
            {
                m_stringScores.Add("0");
            }
        }

        private void OnEnable()
        {
            m_scoreBoard.SetActive(false);
            UpdateRecentScores();
            m_scoreBoardButton.onClick.AddListener(OnClicked);
        }

        private void OnDisable()
        {
            m_scoreBoardButton.onClick.RemoveListener(OnClicked);
        }

        private void OnClicked()
        {
            m_scoreBoard.SetActive(!m_scoreBoard.activeSelf);
        }

        public void AddLastScore(int score)
        {
            if (m_storedScores >= m_stringScores.Count)
            {
                m_stringScores.RemoveAt(m_stringScores.Count - 1);
            }

            m_stringScores.Insert(0, score.ToString());
            m_storedScores++;

            UpdateRecentScores();
        }

        private void UpdateRecentScores()
        {
            for (int i = 0; i < m_recentScores.Length; i++)
            {
                m_format ??= string.Empty;
                m_recentScores[i].text = string.Format(m_format, m_stringScores[i]);
            }
        }
    }
}
