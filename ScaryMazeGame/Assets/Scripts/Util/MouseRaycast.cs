using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Util
{
    public class MouseRaycast : MonoBehaviour
    {
        public float raycastMaxLength;
        
        private Camera _camera;
        private Agent.Spawn _agentSpawner;
        
        private List<NavMeshAgent> _agents;
        
        private void Awake()
        {
            _camera = GetComponent<Camera>();

            _agents = new List<NavMeshAgent>();

            _agentSpawner = FindObjectOfType<Agent.Spawn>();
        }

        private void OnEnable()
        {
            _agentSpawner.OnAgentSpawned += AddNewAgent;
        }

        private void OnDisable()
        {
            _agentSpawner.OnAgentSpawned -= AddNewAgent;
        }

        private void AddNewAgent(NavMeshAgent agent)
        {
            _agents.Add(agent);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                DoRaycast();
            }
        }

        private void DoRaycast()
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, raycastMaxLength))
            {
                return;
            }

            foreach (var agent in _agents)
            {
                agent.destination = hit.point;
            }
        }
    }
}