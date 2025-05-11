using System;


namespace CardMatching.GamePlay
{
    /// <summary>
    /// Monitors match score
    /// </summary>
    public class ScoreManager
    {
        private static ScoreManager instance;

        /// <summary>
        /// store match count
        /// </summary>
        private int _matchCount;
        /// <summary>
        /// stores turn count
        /// </summary>
        private int _turnCount;

        // events for updating score details 
        #region Events
            public event Action<int> OnMatchUpdate;
            public event Action<int> OnTurnUpdate;
        #endregion

        /// <summary>
        /// private controctor for singleton
        /// </summary>
        private ScoreManager()
        {
        }

        public static ScoreManager GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ScoreManager();
                }
                return instance;
            }
        }

        public int GetMatchCount => _matchCount;
        public int GetTurnCount => _turnCount;

        /// <summary>
        /// Reset
        /// </summary>
        public void Reset()
        {
            _matchCount = 0;
            _turnCount = 0;
        }

        /// <summary>
        /// Update the match count
        /// </summary>
        public void UpdateMatchCount()
        {
            _matchCount++;
            OnMatchUpdate?.Invoke(_matchCount);
        }

        /// <summary>
        /// update the turn count
        /// </summary>
        public void UpdateTurnCount()
        {
            _turnCount++;
            OnTurnUpdate?.Invoke(_turnCount);
        }
    }
}