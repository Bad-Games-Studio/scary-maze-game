using System;
using UnityEngine;
using UnityEngine.AI;

namespace Agent
{
    public class Spawn : MonoBehaviour
    {
        public Action<NavMeshAgent> OnAgentSpawned;
        
        public GameObject agent;

        private Maze.Generator _mazeGenerator;
        private Maze.NavMeshBakery _navMeshBakery;

        private Vector3 _spawnPosition;
        private float _agentHeight;
        
        private void Awake()
        {
            _mazeGenerator = GetComponentInChildren<Maze.Generator>();
            _navMeshBakery = GetComponentInChildren<Maze.NavMeshBakery>();
            
            _spawnPosition = Vector3.zero;
            _agentHeight = agent.transform.localScale.y;
        }

        private void OnEnable()
        {
            _mazeGenerator.OnEntrancePlatformCreated += SetNewSpawnPosition;
            
            _navMeshBakery.OnNavMeshGenerated += SpawnAgent;
        }

        private void OnDisable()
        {
            _mazeGenerator.OnEntrancePlatformCreated -= SetNewSpawnPosition;
            
            _navMeshBakery.OnNavMeshGenerated -= SpawnAgent;
        }

        private void SetNewSpawnPosition(GameObject entrancePlatform)
        {
            var platform = entrancePlatform.transform;

            var entrancePos = platform.position;
            var platformHeight = platform.localScale.y;

            entrancePos.y += (platformHeight / 2.0f) + _agentHeight;
                
            _spawnPosition = entrancePos;
        }

        private void SpawnAgent()
        {
            var clone = Instantiate(agent, _spawnPosition, Quaternion.identity);
            clone.transform.parent = transform;

            var agentComponent = clone.GetComponent<NavMeshAgent>();
            OnAgentSpawned?.Invoke(agentComponent);
        }
    }
}
