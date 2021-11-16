using UnityEngine;
using UnityEngine.Events;

namespace TowerDefense
{
    /// <summary>
    /// provide game events
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// static reference to Game Manager. Singletons could have been handled better but for prototype purposes, this is ok
        /// </summary>
        public static GameManager Instance;

        /// <summary>
        /// game over status
        /// </summary>
        public bool IsGameOver;

        /// <summary>
        /// game pause status
        /// </summary>
        public bool IsGamePaused;

        /// <summary>
        /// fires on game starts 
        /// </summary>
        public UnityEvent onGameStart = new UnityEvent();

        /// <summary>
        /// fires on game over, returns win or lose
        /// </summary>
        public UnityEvent<bool> onGameOver = new UnityEvent<bool>();

        /// <summary>
        /// fires on game pause, returns pause status
        /// </summary>
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