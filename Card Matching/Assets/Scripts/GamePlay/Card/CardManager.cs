using CardMatching.Pool;
using CardMatching.Souds;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatching.GamePlay
{
    /// <summary>
    /// Card Manager for card distribution
    /// </summary>
    public class CardManager : MonoBehaviour
    {
        #region Serializefields
        //card data base scriptable object
        [SerializeField] CardAssetScriptableObject cardDatabase;
        //parent transform of where to spawn the cards
        [SerializeField] private RectTransform parentTransform;
        //card prefab reference
        [SerializeField] private Card cardPrefab;
        #endregion

        //card pool
        private IPool _cardPool;
        //Game grid genrator
        private IGameGenerator _gameGenerator;
        //used cards for the game
        private List<Card> _activeCard;
        //boolean to check if card pool initialized
        private bool _isPoolInitialized;

        //first card selected for match
        private Card _firstSelectedCard;

        //Manager gameplay and sound
        private GamePlayManager _gameplayManager;
        private SoundManager _soundManager;

        //block Interaction until we play animation
        private bool _blockInteraction;

        #region Unity callbacks
        private void Awake()
        {
            //inject the managers
            _gameplayManager = GamePlayManager.GetInstance;
            _soundManager = SoundManager.GetInstance;
        }

        private void OnEnable()
        {
            _gameplayManager.OnGameStart += OnGameStart;
            _gameplayManager.OnGameOut += ResetCards;
        }

        private void OnDisable()
        {
            _gameplayManager.OnGameStart -= OnGameStart;
            _gameplayManager.OnGameOut += ResetCards;
          
        }
        #endregion

        /// <summary>
        /// Card manager initialization 
        /// </summary>
        /// <param name="maxItem"></param>
        private void InitializePool(int maxItem)
        {
            _cardPool = new Pool.Pool(maxItem, cardPrefab.gameObject, parentTransform);
            _isPoolInitialized = true;
            _activeCard = new List<Card>(maxItem);
            _gameGenerator = new CardMatchGameGenerator();
        }

        /// <summary>
        /// On game starts
        /// </summary>
        /// <param name="row">number of rows</param>
        /// <param name="column">number of column</param>
        private void OnGameStart(int row, int column)
        {
            //Initially block the interaction until animation is done
            _blockInteraction = true;
            //total number of card to spawn for the game
            int maxCard = row * column;

            //check if pool is not initlized the create one
            if (!_isPoolInitialized) InitializePool(maxCard);
            //reset all the cards
            ResetCards();
            //Get the card id's to spawn in the game using the generator
            var cardIds = _gameGenerator.GenerateCardMatchGame(maxCard, cardDatabase.Count);

            //
            for (int i = 0; i < maxCard; i++)
            {
                //get the card from poool
                var cardObject = _cardPool.Fetch();
                var card = cardObject.GetComponent<Card>();

                if (card == null)
                {
                    card = cardObject.AddComponent<Card>();
                }
                //add item to card
                _activeCard.Add(card);
                //set card details
                card.SetDetail(cardIds[i], cardDatabase.GetSprite(cardIds[i]), cardDatabase.GetBackSprite);
                //subscribe to on click event
                card.OnCardClick += OnCardClicked;
            }

            ///After getting all card set its position using grid prefab ditributor
            IGridDistributor gridDistributor = new GridPrefabDistributor(parentTransform, row, column, _activeCard);
            gridDistributor.DistributePrefabs();

            //1 seconds is the wait time ,0.2 is waiting time 
            Invoke(nameof(UnBlockInteraction), 1.2f);
        }

        private void UnBlockInteraction()
        {
            _blockInteraction = false;
        }

        /// <summary>
        /// Oncard clicked
        /// </summary>
        /// <param name="card"></param>
        private void OnCardClicked(Card card)
        {
            //check if we can open the card
            if (_blockInteraction || card.IsFlipped || card.IsMatched)
            {
                return;
            }
            //block the interaction until we play animation 
            _blockInteraction = true;
            card.Flip(() =>
            {
                //if no card selected select the first card
                if (_firstSelectedCard == null)
                {
                    _firstSelectedCard = card;
                }
                else
                {
                    //if second card check for match
                    CheckMatch(card);
                }
                //unblock the interaction
                _blockInteraction = false;
            });
            _soundManager.Play(Souds.AudioType.Flip);
        }

        /// <summary>
        /// Vefiry if both card are matched or not
        /// </summary>
        /// <param name="card"></param>
        private void CheckMatch(Card card)
        {
            if (_firstSelectedCard.ID == card.ID)
            {
                //its match
                card.Rellease();
                _firstSelectedCard.Rellease();
                _cardPool.Release(card.gameObject);
                _cardPool.Release(_firstSelectedCard.gameObject);
                _gameplayManager.AddMatch();
                _soundManager.Play(Souds.AudioType.Match);
            }
            else
            {
                //reset both card
                _firstSelectedCard.Reset();
                card.Reset();
                _soundManager.Play(Souds.AudioType.MisMatch);
            }
            _gameplayManager.AddTurn();
            _firstSelectedCard = null;
            if (_gameplayManager.CheckGameOver())
            {
                ResetCards();
            }
        }

        /// <summary>
        /// Resets all card 
        /// </summary>
        private void ResetCards()
        {
            _cardPool.ReleaseAll();
            _activeCard.Clear();
            _firstSelectedCard = null;
        }
    }
}
