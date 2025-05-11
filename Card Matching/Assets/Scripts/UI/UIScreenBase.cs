using UnityEngine;

namespace CardMatching.UI
{

    /// <summary>
    /// base class for all UI screen's
    /// </summary>
    public class UIScreenBase : MonoBehaviour
    {

        /// <summary>
        /// To Activate the screen
        /// </summary>
        public virtual void Activate()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// For deactivating the screen
        /// </summary>
        public virtual void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}