using System.Collections;
using System.Collections.Generic;

namespace CardMatching.GamePlay
{
    public interface IGameGenerator
    {
        /// <summary>
        /// Generates a random list of sprite IDs for a card matching game
        /// </summary>
        /// <param name="totalCards">Total number of cards in the game</param>
        /// <param name="spriteCount">Total number of different sprites available</param>
        /// <returns>List of sprite IDs where each ID appears at least twice</returns>
        List<int> GenerateCardMatchGame(int totalCards, int spriteCount);
    }
}