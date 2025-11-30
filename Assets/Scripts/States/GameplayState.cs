using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Golf
{
    public class GameplayState: StateBase
    {
        [SerializeField] private TextMeshProUGUI m_scoreText;
        [SerializeField] private TextMeshProUGUI m_usageText;
        [SerializeField] private GameObject m_gameplayPanel;
        [SerializeField] private ScoreBoard m_scoreBoard;
        [SerializeField] private Button m_slowDown;
        [SerializeField] private ScoreManager m_scoreManager;
        [SerializeField] private LevelController m_levelController;
        [SerializeField] private PlayerController m_playerController;

        private GameStateMachine m_gameStateMachine;
        private int m_usageCount;

        public override void Initialize(GameStateMachine gameStateMachine)
        {
            m_gameplayPanel.SetActive(false);
            m_gameStateMachine = gameStateMachine;
        }

        public override void Enter()
        {
            m_scoreManager.Reset();
            m_usageCount = 3;

            m_scoreManager.ScoreChanged += OnScoreChanged;

            m_slowDown.onClick.AddListener(OnSlowedDown);
            m_slowDown.interactable = true;

            m_scoreText.gameObject.SetActive(true);
            m_scoreText.text = m_scoreManager.score.ToString();
            m_usageText.gameObject.SetActive(true);
            m_usageText.text = m_usageCount.ToString();

            m_gameplayPanel.SetActive(true);

            m_levelController.enabled = true;
            m_playerController.enabled = true;

            m_levelController.Finished += OnFinished;
        }

        public override void Exit()
        {
            m_gameplayPanel.SetActive(false);

            m_scoreManager.ScoreChanged -= OnScoreChanged;

            m_slowDown.onClick.RemoveListener(OnSlowedDown);

            m_levelController.enabled = false;
            m_playerController.enabled = false;

            m_levelController.Finished -= OnFinished;
        }

        private void OnFinished(int score)
        {
            m_gameStateMachine.Enter<GameOverState>();
            m_scoreBoard.AddLastScore(score);
        }

        private void OnScoreChanged(int score)
        {
            m_scoreText.text = score.ToString();
        }

        private void OnSlowedDown()
        {
            var stones = m_levelController.GetStones();

            if (stones.Count > 0)
            {
                Stone lastStone = stones[stones.Count - 1];
                if (lastStone)
                {
                    lastStone.ChangeDrag(7f);
                }

                m_usageCount--;
                m_usageText.text = m_usageCount.ToString();

                if (m_usageCount <= 0)
                {
                    m_slowDown.interactable = false;
                }
            }
        }
    }
}