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

        private Sprite frontFace; // Sprite for the front of the card
        private Sprite backFace; // Sprite for the back of the card

        // Reference to the Image component
        [SerializeField] private Image cardImage;

        // Track if the card is currently flipping
        private bool isFlipping = false;

        // Track which face is currently showing
        private bool isFrontShowing = true;

        public void Initialize(Sprite frontFace, Sprite backFace)
        {
            this.frontFace = frontFace;
            this.backFace = backFace;
        }

        public void ShowTransition(float duration)
        {
            flipDuration = duration;
            if (isFlipping)
                return;

            StartCoroutine(FlipAnimation());
        }


        private IEnumerator FlipAnimation()
        {
            isFlipping = true;

            // Initial rotation and final rotation
            float startRotation = 0;
            float endRotation = 180;

            float elapsedTime = 0;

            // First half of the flip (show the edge of the card)
            while (elapsedTime < flipDuration / 2)
            {
                elapsedTime += Time.deltaTime;
                float percentComplete = elapsedTime / (flipDuration / 2);
                float curveValue = flipCurve.Evaluate(percentComplete);

                // Apply rotation around Y axis
                float currentRotation = Mathf.Lerp(startRotation, endRotation / 2, curveValue);
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
                if (isFrontShowing)
                {
                    cardImage.sprite = backFace;
                }
                else
                {
                    cardImage.sprite = frontFace;
                }

                // Toggle the face state
                isFrontShowing = !isFrontShowing;
            }

            elapsedTime = 0;

            // Second half of the flip (continue the rotation)
            while (elapsedTime < flipDuration / 2)
            {
                elapsedTime += Time.deltaTime;
                float percentComplete = elapsedTime / (flipDuration / 2);
                float curveValue = flipCurve.Evaluate(percentComplete);

                // Apply rotation around Y axis
                float currentRotation = Mathf.Lerp(endRotation / 2, endRotation, curveValue);
                transform.rotation = Quaternion.Euler(0, currentRotation, 0);

                // Make the card wider as it completes the rotation
                Vector3 scale = transform.localScale;
                scale.x = Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * currentRotation)) * 0.5f + 0.5f;
                transform.localScale = scale;

                yield return null;
            }

            // Reset rotation to 0 (or 360, which is the same) to avoid accumulating rotations
            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = Vector3.one; // Reset scale

            isFlipping = false;
        }
    }
}
