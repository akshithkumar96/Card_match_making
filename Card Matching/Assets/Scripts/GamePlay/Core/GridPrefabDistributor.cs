using System.Collections.Generic;
using UnityEngine;

namespace CardMatching.GamePlay
{
    public class GridPrefabDistributor : IGridDistributor
    {
        private RectTransform _parentTransform;
        private Vector2 _spacing;
        private Vector2 _padding;
        private float _maxElementSize; // Maximum size for square elements
        private bool _centerGrid; // Option to center the grid in the parent

        private int _rows;
        private int _columns;

        private List<Card> _cards;


        public GridPrefabDistributor(RectTransform parentTransform, int rows, int columns, List<Card> cards)
        {
            _parentTransform = parentTransform;
            _rows = rows;
            _columns = columns;
            _cards = cards;

            //For now these values are hardcoded can be changed in futue if required
            _spacing = new Vector2(40f, 20f);
            _padding = new Vector2(40f, 20f);
            _maxElementSize = 200f;
            _centerGrid = true;
        }

        public void DistributePrefabs()
        {
            int totalPrefabs = _rows * _columns;

            // Get parent rect dimensions
            float parentWidth = _parentTransform.rect.width - (_padding.x * 2);
            float parentHeight = _parentTransform.rect.height - (_padding.y * 2);

            // Calculate item size to fit parent transform
            float widthBasedSize = (parentWidth - (_spacing.x * (_columns - 1))) / _columns;
            float heightBasedSize = (parentHeight - (_spacing.y * (_rows - 1))) / _rows;

            // For square elements, use the minimum dimension
            float itemSize = Mathf.Min(widthBasedSize, heightBasedSize);

            // Apply maximum size constraint
            itemSize = Mathf.Min(itemSize, _maxElementSize);

            // Calculate total grid width and height for centering
            float totalGridWidth = (_columns * itemSize) + ((_columns - 1) * _spacing.x);
            float totalGridHeight = (_rows * itemSize) + ((_rows - 1) * _spacing.y);

            // Calculate offset for centering
            float offsetX = _centerGrid ? (parentWidth - totalGridWidth) * 0.5f : 0f;
            float offsetY = _centerGrid ? (parentHeight - totalGridHeight) * 0.5f : 0f;

            // Apply padding
            offsetX += _padding.x;
            offsetY += _padding.y;

            for (int i = 0; i < totalPrefabs; i++)
            {
                int row = i / _columns;
                int column = i % _columns;

                // Set size (equal width and height for square elements)
                _cards[i].SetSize(new Vector2(itemSize, itemSize));

                // Calculate position
                float xPos = offsetX + (column * itemSize) + (column * _spacing.x);
                float yPos = offsetY + (row * itemSize) + (row * _spacing.y);

                // Set anchoring for proper positioning
                if (_centerGrid)
                {
                    _cards[i].SetAnchor(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));

                    // Adjust position for center-based anchoring
                    xPos = xPos - (parentWidth * 0.5f) + (itemSize * 0.5f);
                    yPos = (parentHeight * 0.5f) - yPos - (itemSize * 0.5f);
                }
                else
                {
                    // Top-left anchoring
                    _cards[i].SetAnchor(new Vector2(0, 1), new Vector2(0, 1), new Vector2(0, 1));

                    // Y is negative because we're measuring from top
                    yPos = -yPos;
                }

                // Set position
                _cards[i].SetAnchoredPosition(new Vector2(xPos, yPos));
            }
        }
    }
}