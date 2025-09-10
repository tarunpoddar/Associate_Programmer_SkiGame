using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    
    /* 
     Note: This script example evolves as the course continues. Within this commented
    section is the start of the script as viewed in Course 3: Session 1:

    public delegate void raceAction();

    public static event raceAction OnRaceStart;
    public static event raceAction OnRaceStop; 

     */
    
    public delegate void raceStartAction();
    public static event  raceStartAction OnRaceStart;

    public delegate void raceFinishAction();
    public static event  raceFinishAction OnRaceStop;


    public delegate void retryRaceAction();
    public static event retryRaceAction OnRetryRace;

    public delegate void nextLevelAction();
    public static event nextLevelAction OnNextLevel;

    public delegate IEnumerator quitGameAction();
    public static event quitGameAction OnQuitGame;


    public static void StartRace()
    {
        if (OnRaceStart != null)
            OnRaceStart();
    }

    public static void StopRace()
    {
        if (OnRaceStop != null)
            OnRaceStop();
    }

    public void RetryRace()
    {
        if (OnRetryRace != null)
            OnRetryRace();
    }

    public void NextRace()
    {
        if (OnRetryRace != null)
            OnNextLevel();
    }


    public void QuitGame()
    {
        if (OnQuitGame != null)
            StartCoroutine(OnQuitGame());
    }


}
