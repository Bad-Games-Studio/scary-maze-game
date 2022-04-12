using UnityEngine;

namespace Maze
{
    public class Generator : MonoBehaviour
    {
        public Texture2D sourceMaze;

        public Vector2 cellSize;
        public float wallHeight;


        private void Awake()
        {
            CreateMaze();
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
        }

        private void HandleCell(int x, int y)
        {
            var pixel = sourceMaze.GetPixel(x, y);
            
            var isWhite = Mathf.Approximately(pixel.grayscale, 0.0f);
            if (isWhite)
            {
                return;
            }
            
            var isBlack = Mathf.Approximately(pixel.grayscale, 1.0f);
            if (isBlack)
            {
                InstantiateWall(x, y);
                return;
            }

            InstantiateEntrance(x, y);
        }

        
        private void InstantiateWall(int x, int y)
        {
            
        }

        private void InstantiateEntrance(int x, int y)
        {
            
        }
    }
}
