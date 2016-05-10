using UnityEngine;
using System.Collections;

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
    /// Saves how many stars you got in the level.
    /// </summary>
    /// <param name="pStars"></param>
    public void SetLevelStars(int pStars)
    {
        string currentLvlName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        PlayerPrefs.SetInt(currentLvlName, pStars);
    }
}
