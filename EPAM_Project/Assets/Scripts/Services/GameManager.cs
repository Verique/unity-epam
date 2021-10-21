﻿using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class GameManager : MonoBehaviour, IService
    {
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private PauseScreen pauseScreen;

        private EnemySpawner enemySpawner;
        private CameraManager cameraManager;
        private PlayerManager playerManager;
        private Transform playerTransform;
        
        public GameState GameState { get; private set; }
        
        private void Start()
        {
            StartGame();
        }

        private void StartGame()
        {
            playerManager = ServiceLocator.Instance.Get<PlayerManager>();
            playerTransform = playerManager.Transform;
            playerTransform.GetComponent<Health>().IsDead += EndGame;

            ServiceLocator.Instance.Get<InputManager>().PauseKeyUp += Pause;
            
            cameraManager = ServiceLocator.Instance.Get<CameraManager>();
            cameraManager.Target = playerTransform;
            
            enemySpawner = ServiceLocator.Instance.Get<EnemySpawner>();
            enemySpawner.StartSpawning();
            
            foreach (var enemy in enemySpawner.Enemys)
            {
                enemy.GetComponent<Enemy.Enemy>().Target = playerTransform;
            }

            GameState = GameState.Default;
        }

        public void Pause()
        {
            switch (GameState)
            {
                case GameState.Default:
                    GameState = GameState.Pause;
                    pauseScreen.SetActive(true);
                    break;
                case GameState.Pause:
                    GameState = GameState.Default;
                    pauseScreen.SetActive(false);
                    break;
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene("InitialScene");
        }

        private void EndGame()
        {
            GameState = GameState.GameOver;
            enemySpawner.StopSpawning();
            cameraManager.enabled = false;
            
            foreach (var enemy in enemySpawner.Enemys)
            {
                enemy.SetActive(false);
            }
            
            gameOverScreen.SetActive(true);
        }

        public void ToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}