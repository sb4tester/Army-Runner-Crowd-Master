using System.Collections.Generic;
using UnityEngine;

namespace ArmyRunner
{
    public sealed class GameBootstrap : MonoBehaviour
    {
        [SerializeField, Min(1)] private int startingLevel = 1;
        public GameState State { get; private set; } = GameState.Boot;
        public LevelDefinition CurrentLevel { get; private set; }
        public LevelCatalogData Catalog { get; private set; }

        private void Awake()
        {
            if (!LevelCatalog.TryLoad(out Catalog, out List<string> errors))
            {
                Debug.LogError("Army Runner could not load its level catalog:\n" + string.Join("\n", errors), this);
                return;
            }

            SaveData save = SaveService.Load();
            int levelId = Mathf.Max(startingLevel, save.currentLevel);
            CurrentLevel = LevelCatalog.GetById(Catalog, levelId);
            if (CurrentLevel == null)
            {
                CurrentLevel = Catalog.levels[0];
                Debug.LogWarning($"Saved level {levelId} does not exist. Falling back to level 1.", this);
            }

            RunnerMotor runner = FindFirstObjectByType<RunnerMotor>();
            if (runner != null)
            {
                runner.ForwardSpeed = CurrentLevel.speed;
                runner.HorizontalLimit = Mathf.Max(0.5f, CurrentLevel.trackWidth * 0.5f - 0.6f);
            }

            State = GameState.MainMenu;
            Debug.Log($"Army Runner ready. Loaded {Catalog.levels.Length} levels. Current: {CurrentLevel.name}.", this);
        }

        public void SetState(GameState nextState)
        {
            State = nextState;
            Time.timeScale = nextState == GameState.Paused ? 0f : 1f;
        }

        private void OnDestroy() => Time.timeScale = 1f;
    }
}
