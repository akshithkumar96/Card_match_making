using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CardMatching.GamePlay
{
    [CreateAssetMenu(fileName = "GameAsset", menuName = "ScriptableObjects/GameAsset", order = 2)]
    public class GameAssetScriptableObject : ScriptableObject
    {
        public List<string> gridLevel;
    }
}