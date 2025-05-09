using System;
using System.Collections.Generic;

namespace CardMatching.GamePlay
{
    /// <summary>
    /// class for generating card matching game arrangements
    /// </summary>
    public class CardMatchGameGenerator : IGameGenerator
    {
        /// <summary>
        /// Generates a random list of sprite IDs for a card matching game
        /// </summary>
        /// <param name="totalCards">Total number of cards in the game (must be even)</param>
        /// <param name="spriteCount">Total number of different sprites available</param>
        /// <returns>List of sprite IDs where each ID appears at least twice</returns>
        public List<int> GenerateCardMatchGame(int totalCards, int spriteCount)
        {
            // Ensure totalCards is even
            if (totalCards % 2 != 0)
            {
                throw new ArgumentException("Total cards must be an even number for proper pairing");
            }

            // Calculate how many pairs we need
            int totalPairs = totalCards / 2;

            // Create a list to hold our selected sprite IDs
            List<int> result = new List<int>(totalCards);

            // Generate the required pairs
            Random random = new Random();
            for (int i = 0; i < totalPairs; i++)
            {
                // Select a random sprite ID from available sprites
                int randomSpriteId = random.Next(spriteCount);

                // Add this sprite ID twice to create a pair
                result.Add(randomSpriteId);
                result.Add(randomSpriteId);
            }

            // Shuffle the list to randomize the positions
            ShuffleList(result);

            return result;
        }

        /// <summary>
        /// Shuffles a list 
        /// </summary>
        /// <param name="list">The list to shuffle</param>
        private void ShuffleList<T>(List<T> list)
        {
            Random random = new Random();
            int n = list.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }
    }
}