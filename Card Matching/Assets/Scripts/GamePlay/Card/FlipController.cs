using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CardMatching.GamePlay
{
    /// <summary>
    /// For flip transition effect
    /// </summary>
    public class FlipController : MonoBehaviour, ITransitionEffect
    {
        [SerializeField] private float flipDuration = 0.5f; // Duration of the flip animation
        [SerializeField] private AnimationCurve flipCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
        // Reference to the Image component
        [SerializeField] private Image cardImage;


        private Sprite _frontFace; // Sprite for the front of the card
        private Sprite _backFace; // Sprite for the back of the card

        // Track if the card is currently flipping
        private bool _isFlipping = false;

        // Track which face is currently showing
        private bool _isFrontShowing = true;

        public void Initialize(Sprite frontFace, Sprite backFace)
        {
            this._frontFace = frontFace;
            this._backFace = backFace;
            _isFrontShowing = true;
            _isFlipping = false;
        }

        /// <summary>
        /// Play transition animation 
        /// </summary>
        /// <param name="duration">animation duration</param>
        /// <param name="onComplete"> event on complete</param>
        public void ShowTransition(float duration, Action onComplete)
        {
            flipDuration = duration;
            if (_isFlipping || !gameObject.activeSelf)
                return;

            StartCoroutine(FlipAnimation(onComplete));
        }

        /// <summary>
        /// Play flip animation 
        /// </summary>
        /// <param name="onComplete"> event on complete</param>
        /// <returns></returns>
        private IEnumerator FlipAnimation(Action onComplete)
        {
            _isFlipping = true;

            // Initial rotation and final rotation
            float startRotation = 0;
            float midRotation = 90;

            float elapsedTime = 0;

            // First half of the flip (show the edge of the card)
            while (elapsedTime < flipDuration / 2)
            {
                elapsedTime += Time.deltaTime;
                float percentComplete = elapsedTime / (flipDuration / 2);
                float curveValue = flipCurve.Evaluate(percentComplete);

                // Apply rotation around Y axis
                float currentRotation = Mathf.Lerp(startRotation, midRotation , curveValue);
                transform.rotation = Quaternion.Euler(0, currentRotation, 0);

                // Make the card narrower as it rotates to simulate perspective
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * currentRotation)) * 0.5f + 0.5f;
                transform.localScale = scale;

                yield return null;
            }

            // Swap the sprite at the midpoint of the animation
            if (cardImage != null)
            {
                if (_isFrontShowing)
                {
                    cardImage.sprite = _backFace;
                }
                else
                {
                    cardImage.sprite = _frontFace;
                }

                // Toggle the face state
                _isFrontShowing = !_isFrontShowing;
            }

            elapsedTime = 0;

            // Second half of the flip (continue the rotation)
            while (elapsedTime < flipDuration / 2)
            {
                elapsedTime += Time.deltaTime;
                float percentComplete = elapsedTime / (flipDuration / 2);
                float curveValue = flipCurve.Evaluate(percentComplete);

                // Apply rotation around Y axis
                float currentRotation = Mathf.Lerp(midRotation , startRotation, curveValue);
                transform.rotation = Quaternion.Euler(0, currentRotation, 0);

                // Make the card wider as it completes the rotation
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * currentRotation)) * 0.5f + 0.5f;
                transform.localScale = scale;

                yield return null;
            }

            // Reset rotation to 0 (or 360, which is the same) to avoid accumulating rotations
            transform.localScale = Vector3.one; // Reset scale

            _isFlipping = false;
            onComplete?.Invoke();
        }
    }
}
