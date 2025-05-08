using System.Collections.Generic;
using UnityEngine;

namespace CardMatching.Pool
{
    public class Pool : IPool
    {
        /// <summary>
        /// Prefab item of the pool
        /// </summary>
        private GameObject _prefab;

        /// <summary>
        /// Parent transform for spawning 
        /// </summary>
        private Transform _parentTransform;

        /// <summary>
        /// total item in the pool
        /// </summary>
        private int _maxItem;

        /// <summary>
        /// currently used pool items 
        /// </summary>
        private HashSet<GameObject> _activeItems;

        /// <summary>
        /// To store all the free item 
        /// </summary>
        private Queue<GameObject> _freeItems;


        public Pool(int maxItems, GameObject prefab, Transform parentTransform)
        {
            _prefab = prefab;
            _parentTransform = parentTransform;
            _maxItem = maxItems;
            _activeItems = new HashSet<GameObject>();
            _freeItems = new Queue<GameObject>(_maxItem);
            Initialise(_maxItem, _prefab);
        }

        public void Initialise(int maxItems, GameObject prefab)
        {
            for (int i = 0; i < maxItems; i++)
            {
                var gameObject = GameObject.Instantiate(prefab, _parentTransform);
                gameObject.SetActive(false);
                _freeItems.Enqueue(gameObject);
            }
        }

        public GameObject Fetch()
        {
            GameObject obj;
            if (_freeItems.Count > 0)
            {
                obj = _freeItems.Dequeue();
                obj.SetActive(true);
            }
            else
            {
                obj = GameObject.Instantiate(_prefab, _parentTransform);
                _maxItem++;
            }
            _activeItems.Add(obj);
            obj.SetActive(true);
            return obj;
        }

        public void Release(GameObject gameObject)
        {
            gameObject.SetActive(false);
            if (_activeItems.Contains(gameObject))
            {
                _activeItems.Remove(gameObject);
                _freeItems.Enqueue(gameObject);
            }
            else
            {
                UnityEngine.Debug.Log("item that you are trying to release isn't present in the active list so destroying it");
            }
        }

        public void ReleaseAll()
        {

            var ObjectToRelease = new List<GameObject>(_activeItems);

            foreach (var poolItem in ObjectToRelease)
            {
                Release(poolItem);
            }
        }

        public void Destroy()
        {
            foreach (var activeItem in _activeItems)
            {
                GameObject.Destroy(activeItem);
            }
            _activeItems.Clear();
            while (_freeItems.Count > 0)
            {
                var poolItem = _freeItems.Dequeue();
                GameObject.Destroy(poolItem);
            }
            _maxItem = 0;
        }
    }
}
