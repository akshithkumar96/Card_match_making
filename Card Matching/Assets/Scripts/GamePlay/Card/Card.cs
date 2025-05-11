using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardMatching.GamePlay
{
    /// <summary>
    /// Individual card script
    /// </summary>
    public class Card : MonoBehaviour, IPointerClickHandler
    {

        #region Serialize fields
        [SerializeField] RectTransform rectTransform;
        [SerializeField] Image image;
        #endregion

        //Transition controller
        private ITransitionEffect _flipController;

        //card id
        private int _id;

        //Sprite of card back and front face
        private Sprite _faceSprite;
        private Sprite _backSprite;

        /// <summary>
        /// Action event when card gets clicked
        /// </summary>
        public event Action<Card> OnCardClick;


        #region Properties

        /// <summary>
        /// Card Id
        /// </summary>
        public int ID => _id;

        /// <summary>
        /// is card matched
        /// </summary>
        public bool IsMatched { get; set; }

        /// <summary>
        /// State of card
        /// </summary>
        public bool IsFlipped { get; set; }
        #endregion

        /// <summary>
        /// Reset the card details 
        /// </summary>
        public void Reset()
        {
            IsMatched = false;
            IsFlipped = false;
            image.sprite = _backSprite;
            _flipController.ShowTransition(0.2f, () => { });
        }

        /// <summary>
        /// Release the card once after match
        /// </summary>
        public void Rellease()
        {
            IsMatched = true;
        }

        /// <summary>
        /// On card clicked
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            OnCardClick?.Invoke(this);
        }

        /// <summary>
        /// Flip the card
        /// </summary>
        /// <param name="OnComplete"></param>
        public void Flip(Action OnComplete)
        {
            _flipController.ShowTransition(0.5f,OnComplete);
            IsFlipped = true;
        }

        #region Card Initialization methods
        /// <summary>
        /// Set the card details
        /// </summary>
        /// <param name="id">card id</param>
        /// <param name="faceSprite">front face sprite</param>
        /// <param name="backSprite"> card back sprite</param>
        public void SetDetail(int id, Sprite faceSprite, Sprite backSprite)
        {
            _id = id;
            _faceSprite = faceSprite;
            _backSprite = backSprite;

            image.sprite = faceSprite;
            if (_flipController == null)
            {
                _flipController = GetComponent<FlipController>();
            }
            _flipController.Initialize(_faceSprite, _backSprite);
            //Reset after 1 second
            Invoke(nameof(Reset), 1);
        }

        /// <summary>
        /// Sets anchors
        /// </summary>
        /// <param name="anchorMax"> anchor max vector 2</param>
        /// <param name="achormin"> anchor min Vector 2</param>
        /// <param name="pivot"> Pivot vector 2</param>
        public void SetAnchor(Vector2 anchorMax, Vector2 achormin, Vector2 pivot)
        {
            rectTransform.anchorMin = anchorMax;
            rectTransform.anchorMax = anchorMax;
            rectTransform.pivot = pivot;
        }

        /// <summary>
        /// Sets anchored position value
        /// </summary>
        /// <param name="anchoredPosition"> anchored position vector 2</param>
        public void SetAnchoredPosition(Vector2 anchoredPosition)
        {
            rectTransform.anchoredPosition = anchoredPosition;
        }

        /// <summary>
        /// Sets rect size of card 
        /// </summary>
        /// <param name="itemSize">vector 2 size to set</param>
        public void SetSize(Vector2 itemSize)
        {
            rectTransform.sizeDelta = itemSize;
        }
        #endregion
    }
}