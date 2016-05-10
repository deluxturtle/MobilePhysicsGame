using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Saves level info completion rate.
/// </summary>
public class SaveManager : MonoBehaviour {


    static bool created = false;

	void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //Do Initialization stuff here.


    }

    /// <summary>
    /// Retreives stars from save file for level.
    /// </summary>
    /// <param name="pLevelName">Level you want to read the stars from.</param>
    /// <returns></returns>
    public int GetLevelStars(string pLevelName)
    {

        if (PlayerPrefs.HasKey(pLevelName))
        {
            return PlayerPrefs.GetInt(pLevelName);
        }
        else
        {
            return 0;
        }

    }

    /// <summary>
    /// Based on the percentage of winning, this will calculate the amount of 
    /// stars earned. Check to see if the score is better then save.
    /// </summary>
    /// <param name="pStars">Percentage of how many stars you saved.</param>
    public void SetLevelStars(float pPercent)
    {
        string currentLvlName = SceneManager.GetActiveScene().name;

        int stars;

        if (pPercent >= 0.80f && pPercent < 0.90f)
        {
            stars = 1;
        }
        else if (pPercent >= 0.90f && pPercent < 1)
        {
            stars = 2;
        }
        else if(pPercent >= 1)
        {
            stars = 3;
        }
        else
        {
            stars = 0;
        }


        //update stars if new stars are better.
        if (GetLevelStars(currentLvlName) < stars)
        {
            PlayerPrefs.SetInt(currentLvlName, stars);
        }
    }
}
