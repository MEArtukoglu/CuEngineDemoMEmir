using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace MEmir.Game
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance { get; private set; }
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform spawnLocation;
        [SerializeField] private Transform spawnCamera;
        private Transform m_Player;

        

        private void Awake()
        {
            if (Instance)
            {
                Debug.LogError("SpawnManager: Multipile Instances Detected!");
                return;
            }
            Instance = this;
        }

        public void SpawnPlayer()
        {
            if (m_Player)
                DestroyPlayer();

            m_Player = Instantiate(playerPrefab, spawnLocation.position, spawnLocation.rotation).transform;
            spawnCamera.gameObject.SetActive(false);
        }

        public void DestroyPlayer()
        {
            if (m_Player)
            {
                Destroy(m_Player.gameObject);
                spawnCamera.gameObject.SetActive(true);
            }
        }
    }
}