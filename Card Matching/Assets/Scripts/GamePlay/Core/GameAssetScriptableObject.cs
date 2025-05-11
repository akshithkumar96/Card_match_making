using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CardMatching.GamePlay
{
    [CreateAssetMenu(fileName = "GameAsset", menuName = "ScriptableObjects/GameAsset", order = 2)]
    public class GameAssetScriptableObject : ScriptableObject
    {
        /// <summary>
        /// card board (e.g 2*3,2*4) (muct be even)
        /// </summary>
        public List<string> gridLevel;
    }
}