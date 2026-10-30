using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace ArmyRunner
{
    public enum GameState { Boot, MainMenu, Running, Paused, Combat, Won, Lost }
    public enum CombatResult { Victory, Defeat, Draw }

    [Serializable]
    public sealed class LevelCatalogData
    {
        public int version = 1;
        public LevelDefinition[] levels = Array.Empty<LevelDefinition>();
    }

    [Serializable]
    public sealed class LevelDefinition
    {
        public int id;
        public string name;
        public float speed = 7f;
        public float trackWidth = 10f;
        public string[] gateValues = Array.Empty<string>();
        public int obstaclesCount;
        public bool movingObstacles;
        public int enemyPower;
        public float distance;

        public List<string> Validate()
        {
            var errors = new List<string>();
            if (id <= 0) errors.Add("Level id must be greater than zero.");
            if (string.IsNullOrWhiteSpace(name)) errors.Add("Level name is required.");
            if (speed <= 0f) errors.Add("Level speed must be greater than zero.");
            if (trackWidth <= 0f) errors.Add("Track width must be greater than zero.");
            if (distance <= 0f) errors.Add("Distance must be greater than zero.");
            if (enemyPower < 0 || obstaclesCount < 0) errors.Add("Enemy power and obstacle count cannot be negative.");
            if (gateValues == null || gateValues.Length == 0) errors.Add("At least one gate value is required.");
            return errors;
        }
    }

    public static class GateMath
    {
        public const int DefaultMaximumCrowd = 99999;

        public static bool TryApply(int currentCount, string gateValue, out int result, int maximumCrowd = DefaultMaximumCrowd)
        {
            result = Math.Max(0, currentCount);
            if (string.IsNullOrWhiteSpace(gateValue) || maximumCrowd < 1) return false;
            string value = gateValue.Trim().Replace("×", "x").Replace("÷", "/");
            char operation = value[0];
            if (!int.TryParse(value.Substring(1), NumberStyles.Integer, CultureInfo.InvariantCulture, out int operand) || operand < 0)
                return false;

            long calculated;
            switch (operation)
            {
                case '+': calculated = (long)currentCount + operand; break;
                case '-': calculated = (long)currentCount - operand; break;
                case 'x':
                case 'X': calculated = (long)currentCount * operand; break;
                case '/':
                    if (operand == 0) return false;
                    calculated = currentCount / operand;
                    break;
                default: return false;
            }
            result = (int)Math.Max(0L, Math.Min(maximumCrowd, calculated));
            return true;
        }
    }

    public static class CombatResolver
    {
        public static CombatResult Resolve(int playerCrowd, int enemyPower)
        {
            if (playerCrowd > enemyPower) return CombatResult.Victory;
            if (playerCrowd < enemyPower) return CombatResult.Defeat;
            return CombatResult.Draw;
        }
    }

    public static class LevelCatalog
    {
        private const string ResourcePath = "ArmyRunner/levels";

        public static bool TryLoad(out LevelCatalogData catalog, out List<string> errors)
        {
            errors = new List<string>();
            catalog = null;
            TextAsset json = Resources.Load<TextAsset>(ResourcePath);
            if (json == null)
            {
                errors.Add("Level data missing at Resources/ArmyRunner/levels.json.");
                return false;
            }

            try { catalog = JsonUtility.FromJson<LevelCatalogData>(json.text); }
            catch (Exception exception)
            {
                errors.Add("Level JSON parse failed: " + exception.Message);
                return false;
            }

            if (catalog == null || catalog.levels == null || catalog.levels.Length == 0)
            {
                errors.Add("Level catalog is empty.");
                return false;
            }

            var ids = new HashSet<int>();
            foreach (LevelDefinition level in catalog.levels)
            {
                if (level == null) { errors.Add("Catalog contains a null level."); continue; }
                foreach (string error in level.Validate()) errors.Add($"Level {level.id}: {error}");
                if (!ids.Add(level.id)) errors.Add($"Duplicate level id: {level.id}.");
            }
            return errors.Count == 0;
        }

        public static LevelDefinition GetById(LevelCatalogData catalog, int id)
        {
            if (catalog == null || catalog.levels == null) return null;
            foreach (LevelDefinition level in catalog.levels)
                if (level != null && level.id == id) return level;
            return null;
        }
    }

    [Serializable]
    public sealed class SaveData
    {
        public int schemaVersion = 1;
        public int currentLevel = 1;
        public int unlockedLevel = 1;
        public bool soundEnabled = true;
    }

    public static class SaveService
    {
        private const string SaveKey = "army-runner-save-v1";
        private const int CurrentSchemaVersion = 1;

        public static SaveData Load()
        {
            if (!PlayerPrefs.HasKey(SaveKey)) return CreateDefault();
            try
            {
                SaveData data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SaveKey));
                if (data == null || data.schemaVersion != CurrentSchemaVersion) return CreateDefault();
                data.currentLevel = Mathf.Max(1, data.currentLevel);
                data.unlockedLevel = Mathf.Max(data.currentLevel, data.unlockedLevel);
                return data;
            }
            catch { return CreateDefault(); }
        }

        public static void Save(SaveData data)
        {
            if (data == null) return;
            data.schemaVersion = CurrentSchemaVersion;
            data.currentLevel = Mathf.Max(1, data.currentLevel);
            data.unlockedLevel = Mathf.Max(data.currentLevel, data.unlockedLevel);
            PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(data));
            PlayerPrefs.Save();
        }

        private static SaveData CreateDefault() => new SaveData();
    }
}
