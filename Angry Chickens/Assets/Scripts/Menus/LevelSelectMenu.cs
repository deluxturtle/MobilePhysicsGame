using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelSelectMenu : MonoBehaviour 
{
    public GameObject buttonGroup;

    //Private
    List<GameObject> levelButtons = new List<GameObject>();

	void Start()
    {
        SaveManager saveManager = GameObject.FindObjectOfType<SaveManager>();

        if (buttonGroup == null)
        {
            Debug.LogWarning("Button group not assigned in level select panel.");
        }
        else
        {
            for(int i = 0; i < buttonGroup.transform.childCount; i++)
            {
                for(int j = 0; j < buttonGroup.transform.GetChild(i).childCount; j++)
                {
                    string btnName = "";
                    Button btn = buttonGroup.transform.GetChild(i).GetChild(j).GetComponent<Button>();
                    btnName = btn.gameObject.name;


                    //if previous level has a star go ahead and assign button the link to the level.
                    if (j - 1 > 0 && saveManager.GetLevelStars(buttonGroup.transform.GetChild(i).GetChild(j - 1).name) > 0)
                    {
                        btn.onClick.AddListener(() =>
                        {
                            LoadLevel(btnName);
                        });
                    }
                    else
                    {
                        //Lock the button
                        if (j != 0)
                        {
                            btn.interactable = false;
                        }
                    }

                }
            }
        }
    }

    /// <summary>
    /// Loads level with given string parameter.
    /// Can't check if scene exists with scenemanager in current unity build.
    /// </summary>
    /// <param name="pName"></param>
    public void LoadLevel(string pName)
    {
        SceneManager.LoadScene(pName);
    }
}
