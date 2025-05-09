using CardMatching.Pool;
using CardMatching.Souds;
using System.Collections.Generic;
using UnityEngine;

namespace CardMatching.GamePlay
{
    public class CardManager : MonoBehaviour
    {
        [SerializeField] CardAssetScriptableObject cardDatabase;
        [SerializeField] private RectTransform parentTransform;

        private IPool cardPool;
        private IGameGenerator gameGenerator;
        [SerializeField] private Card cardPrefab;

        private List<Card> activeCard;

        private bool isPoolInitialized;

        private Card firstSelectedCard;

        private GamePlayManager _gameplayManager;
        private SoundManager _soundManager;

        private bool _blockInteraction;

        private void Awake()
        {
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

        private void InitializePool(int maxItem)
        {
            cardPool = new Pool.Pool(maxItem, cardPrefab.gameObject, parentTransform);
            isPoolInitialized = true;
            activeCard = new List<Card>(maxItem);
            gameGenerator = new CardMatchGameGenerator();
        }

        private void OnGameStart(int row, int column)
        {
            int maxCard = row * column;

            if (!isPoolInitialized) InitializePool(maxCard);
            ResetCards();
            var cardIds = gameGenerator.GenerateCardMatchGame(maxCard, cardDatabase.Count);

            for (int i = 0; i < maxCard; i++)
            {
                var cardObject = cardPool.Fetch();
                var card = cardObject.GetComponent<Card>();

                if (card == null)
                {
                    card = cardObject.AddComponent<Card>();
                }
                activeCard.Add(card);
                card.SetDetail(cardIds[i], cardDatabase.GetSprite(cardIds[i]), cardDatabase.GetBackSprite);
                card.OnCardClick += OnCardClicked;
            }
            IGridDistributor gridDistributor = new GridPrefabDistributor(parentTransform, row, column, activeCard);
            gridDistributor.DistributePrefabs();

        }

        private void OnCardClicked(Card card)
        {
            if (_blockInteraction || card.IsFlipped || card.IsMatched)
            {
                return;
            }
            _blockInteraction = true;
            card.Flip(() =>
            {
                if (firstSelectedCard == null)
                {
                    firstSelectedCard = card;
                }
                else
                {
                    CheckMatch(card);
                }
                _blockInteraction = false;
            });
            _soundManager.Play(Souds.AudioType.Flip);
        }

        private void CheckMatch(Card card)
        {
            if (firstSelectedCard.ID == card.ID)
            {
                //its match
                card.Rellease();
                firstSelectedCard.Rellease();
                cardPool.Release(card.gameObject);
                cardPool.Release(firstSelectedCard.gameObject);
                _gameplayManager.AddMatch();
                _soundManager.Play(Souds.AudioType.Match);
            }
            else
            {
                //reset both card
                firstSelectedCard.Reset();
                card.Reset();
                _soundManager.Play(Souds.AudioType.MisMatch);
            }
            _gameplayManager.AddTurn();
            firstSelectedCard = null;
            if (_gameplayManager.CheckGameOver())
            {
                ResetCards();
            }
        }

        private void ResetCards()
        {
            cardPool.ReleaseAll();
            activeCard.Clear();
            firstSelectedCard = null;
        }
    }
}
