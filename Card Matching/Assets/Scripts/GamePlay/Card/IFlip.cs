
using System;
using UnityEngine;

namespace CardMatching.GamePlay
{
    public interface ITransitionEffect
    {
        /// <summary>
        /// initalize the transition with front and back images
        /// </summary>
        /// <param name="frontImage">front face sprite</param>
        /// <param name="backImage">card back sprite</param>
        void Initialize(Sprite frontImage, Sprite backImage);

        /// <summary>
        /// Play transition 
        /// </summary>
        /// <param name="duration">transition duration</param>
        /// <param name="OnComplete">  complete event</param>
        void ShowTransition(float duration, Action OnComplete);
    }
}