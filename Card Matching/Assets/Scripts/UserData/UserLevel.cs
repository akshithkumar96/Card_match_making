using System;

/// <summary>
/// User progress class
/// </summary>

namespace CardMatching.UserData
{
    /// <summary>
    /// User progress  class 
    /// </summary>
    [Serializable]
    public class UserLevel
    {
        /// <summary>
        /// Highscore for now I will keep high match count
        /// Later if required can be changed
        /// </summary>
        public int HighScore;
        /// <summary>
        /// Total number of games user Completed.
        /// consider as level 
        /// </summary>
        public int GamePlayed;
    }
}
