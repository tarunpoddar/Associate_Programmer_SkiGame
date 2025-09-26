using UnityEngine;

namespace Assets.Scripts
{
    public class RaceDataManager : MonoBehaviour
    {
        public static RaceDataManager Instance { get; private set; }

        private int totalRacesCompleted = 0;

        private void Awake()
        {
            // Singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Load saved data
            totalRacesCompleted = PlayerPrefs.GetInt("TotalRaces", 0);
        }

        public void IncrementRaceCount()
        {
            totalRacesCompleted++;
            print($"Races Completed: {totalRacesCompleted}");
            PlayerPrefs.SetInt("TotalRaces", totalRacesCompleted);
            PlayerPrefs.Save();
        }

        public int GetTotalRacesCompleted()
        {
            return totalRacesCompleted;
        }
    }
}
