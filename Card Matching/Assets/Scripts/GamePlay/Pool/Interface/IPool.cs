using UnityEngine;

namespace CardMatching.Pool
{
    public interface IPool
    {
        /// <summary>
        /// Initise the pool
        /// </summary>
        /// <param name="maxItems"> max number to be initialized</param>
        /// <param name="prefab"> Pool Object </param>
        void Initialise(int maxItems, GameObject prefab);

        /// <summary>
        /// Releases the item back to pool
        /// </summary>
        /// <param name="item"></param>
        void Release(GameObject item);

        /// <summary>
        /// Release all item back to pool
        /// </summary>
        void ReleaseAll();

        /// <summary>
        /// Get the item from the pool
        /// </summary>
        /// <returns> pool item prefab </returns>
        GameObject Fetch();

        /// <summary>
        /// Destroys all the item in pool
        /// </summary>
        void Destroy();
    }
}