using System;
using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;


        public bool IsGameOver;
        public bool IsGamePaused;



        public UnityEvent onGameStart = new UnityEvent();

        public UnityEvent<bool> onGameOver = new UnityEvent<bool>();

        public UnityEvent<bool> onGamePause = new UnityEvent<bool>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Reset();
            StartGame();
        }

        public void StartGame()
        {
            onGameStart.Invoke();
        }

        public void GameOver(bool isWin)
        {
            IsGameOver = true;
            onGameOver.Invoke(isWin);
        }

        public void GamePause(bool isPaused)
        {
            IsGamePaused = isPaused;
            onGamePause.Invoke(isPaused);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            GamePause(pauseStatus);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            GamePause(!hasFocus);
        }

        private void Reset()
        {
            IsGameOver = false;
            IsGamePaused = false;
        }
    }
}