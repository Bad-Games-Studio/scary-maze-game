using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Maze
{
    public class NavMeshBakery : MonoBehaviour
    {
        public event Action OnNavMeshGenerated;
        
        
        private Generator _mazeGenerator;
        
        private List<NavMeshSurface> _surfaces;
        
        
        private void Awake()
        {
            _surfaces = new List<NavMeshSurface>();
            _mazeGenerator = GetComponent<Generator>();
        }

        private void OnEnable()
        {
            _mazeGenerator.OnSurfaceCreated += AddNewSurface;
            _mazeGenerator.OnGenerationFinished += BakeNavMesh;
        }

        private void OnDisable()
        {
            _mazeGenerator.OnSurfaceCreated -= AddNewSurface;
            _mazeGenerator.OnGenerationFinished -= BakeNavMesh;
        }

        
        private void AddNewSurface(NavMeshSurface surface)
        {
            _surfaces.Add(surface);
        }
        
        private void BakeNavMesh()
        {
            foreach (var surface in _surfaces)
            {
                surface.BuildNavMesh();
            }
            
            OnNavMeshGenerated?.Invoke();
        }
    }
}
