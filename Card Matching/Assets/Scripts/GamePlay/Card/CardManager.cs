using CardMatching.Pool;
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

        private void OnEnable()
        {
            GameEvent.OnGameStart += OnGameStart;
            GameEvent.GoToHomeScreen += Reset;
        }

        private void OnDisable()
        {
            GameEvent.OnGameStart -= OnGameStart;
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
            if (card.IsFlipped || card.IsMatched)
            {
                return;
            }

            card.Flip();
            if (firstSelectedCard == null)
            {
                firstSelectedCard = card;
            }
            else
            {
                if (firstSelectedCard.ID == card.ID)
                {
                    //its match
                }
                else
                {
                    //reset both card
                }
            }
        }

        private void Reset()
        {
            cardPool.ReleaseAll();
            activeCard.Clear();
        }
    }
}
