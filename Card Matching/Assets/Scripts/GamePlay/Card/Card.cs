using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CardMatching.GamePlay
{
    public class Card : MonoBehaviour, IPointerClickHandler
    {

        #region Serialize fields
        [SerializeField] RectTransform _rectTransform;
        [SerializeField] Image image;
        #endregion

        //Transition controller
        private ITransitionEffect _flipController;

        //card id
        private int _id;

        private Sprite _faceSprite;
        private Sprite _backSprite;

        /// <summary>
        /// Action event when card gets clicked
        /// </summary>
        public Action<Card> OnCardClick;


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


        public void Reset()
        {
            IsMatched = false;
            IsFlipped = false;
            image.sprite = _backSprite;
            _flipController.ShowTransition(0.2f);
        }

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
            Invoke(nameof(Reset), 1);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("clicked ");
            OnCardClick?.Invoke(this);
        }

        public void Flip()
        {
            _flipController.ShowTransition(0.5f);
            IsFlipped = true;
        }

        public void SetAnchor(Vector2 anchorMax, Vector2 achormin, Vector2 pivot)
        {
            _rectTransform.anchorMin = anchorMax;
            _rectTransform.anchorMax = anchorMax;
            _rectTransform.pivot = pivot;
        }

        public void SetAnchoredPosition(Vector2 anchoredPosition)
        {
            _rectTransform.anchoredPosition = anchoredPosition;
        }

        public void SetSize(Vector2 itemSize)
        {
            _rectTransform.sizeDelta = itemSize;
        }
    }
}