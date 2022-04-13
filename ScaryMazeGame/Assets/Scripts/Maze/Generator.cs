using System;
using UnityEngine;

namespace Maze
{
    public class Generator : MonoBehaviour
    {
        [Header("Maze settings")]
        public Texture2D sourceMaze;

        public Vector3 cellScale;
        public float floorVerticalScale;

        [Header("Building blocks")]
        public GameObject floorPrefab;
        public GameObject wallPrefab;
        public GameObject entrancePlatformPrefab;


        private Vector3 _mazeCenterPosition;
        private Vector3 _localCenterOffset;

        private Vector3 _wallScale;
        private Vector3 _entrancePlatformScale;
        private Vector3 _floorScale;

        private void Awake()
        {
            CalculateCenterOffset();
            CreateMaze();
        }

        private void CalculateCenterOffset()
        {
            _mazeCenterPosition = transform.position;
            _localCenterOffset = new Vector3
            {
                x = cellScale.x * sourceMaze.width / 2.0f,
                y = 0,
                z = cellScale.z * sourceMaze.height / 2.0f
            };

            _wallScale = cellScale;
            _entrancePlatformScale = new Vector3
            {
                x = cellScale.x,
                y = floorVerticalScale,
                z = cellScale.z
            };
            _floorScale = new Vector3
            {
                x = cellScale.x * (sourceMaze.width - 1),
                y = floorVerticalScale,
                z = cellScale.z * (sourceMaze.height - 1)
            };
        }
        
        private void CreateMaze()
        {
            for (var y = 0; y < sourceMaze.height; ++y)
            {
                for (var x = 0; x < sourceMaze.width; ++x)
                {
                    HandleCell(x, y);
                }
            }

            InstantiateFloor();
        }

        private void HandleCell(int x, int y)
        {
            var pixel = sourceMaze.GetPixel(x, y);
            
            var isWhite = Mathf.Approximately(pixel.grayscale, 1.0f);
            if (isWhite && IsOnEdge(x, y))
            {
                InstantiateEntrance(x, y);
                return;
            }

            if (isWhite)
            {
                return;
            }

            var isBlack = Mathf.Approximately(pixel.grayscale, 0.0f);
            if (isBlack)
            {
                InstantiateWall(x, y);
                return;
            }
            
            Debug.Log($"Maze cell at ({x}, {y}) is ignored due to the wrong format.");
        }


        private bool IsOnEdge(int x, int y)
        {
            var xOnEdge = (x == 0) || (x + 1 == sourceMaze.width);
            var yOnEdge = (y == 0) || (y + 1 == sourceMaze.height);
            return xOnEdge || yOnEdge;
        }
        
        private Vector3 WallPositionAt(int x, int y)
        {
            var position = new Vector3
            {
                x = x * cellScale.x,
                y = cellScale.y / 2.0f,
                z = y * cellScale.z
            };

            return position - _localCenterOffset + _mazeCenterPosition;
        }

        private Vector3 EntrancePositionAt(int x, int y)
        {
            if (x == 0)
            {
                x -= 1;
            }
            else if (x + 1 == sourceMaze.width)
            {
                x += 1;
            }
            else if (y == 0)
            {
                y -= 1;
            }
            else if (y + 1 == sourceMaze.height)
            {
                y += 1;
            }

            var position = new Vector3
            {
                x = x * cellScale.x,
                y = -floorVerticalScale / 2.0f,
                z = y * cellScale.z
            };
            return position - _localCenterOffset + _mazeCenterPosition;
        }

        private void InstantiateWall(int x, int y)
        {
            InstantiateMazePart(wallPrefab, WallPositionAt(x, y), _wallScale);
        }
        
        private void InstantiateEntrance(int x, int y)
        {
            InstantiateMazePart(entrancePlatformPrefab, EntrancePositionAt(x, y), _entrancePlatformScale);
        }

        private void InstantiateFloor()
        {
            var position = new Vector3
            {
                x = _mazeCenterPosition.x,
                y = _mazeCenterPosition.y - (floorVerticalScale / 2.0f),
                z = _mazeCenterPosition.z
            };
            InstantiateMazePart(floorPrefab, position, _floorScale);
        }

        private void InstantiateMazePart(GameObject prefab, Vector3 position, Vector3 scale)
        {
            var clone = Instantiate(prefab, position, Quaternion.identity);
            clone.transform.localScale = scale;
            clone.isStatic = true;
            clone.transform.parent = transform;
        }
    }
}
