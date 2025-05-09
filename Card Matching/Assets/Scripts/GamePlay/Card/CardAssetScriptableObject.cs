using System.Collections.Generic;
using UnityEngine;

namespace CardMatching
{
    [CreateAssetMenu(fileName = "CardDatabase", menuName = "ScriptableObjects/CardSpriteDataBase", order = 1)]
    public class CardAssetScriptableObject : ScriptableObject
    {
        [SerializeField] List<Sprite> CardSpriites;
        [SerializeField] Sprite BackSprite;

        public int Count => CardSpriites.Count;

        public Sprite GetBackSprite => BackSprite;

        public Sprite GetSprite(int index)
        {
            if (index > CardSpriites.Count)
            {
                Debug.Log("Item you are trying to access  isn't available" + index);
                return null;
            }
            return CardSpriites[index];
        }
    }
}