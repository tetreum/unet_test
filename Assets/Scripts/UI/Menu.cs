using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();

    public GameObject[] panelList;
    public static Menu Instance;

    public void Start ()
    {
        Instance = this;
        SceneManager.sceneLoaded += OnLevelFinishedLoading;

        // make panel list more easier to access
        foreach (GameObject panel in panelList) {
            panels.Add(panel.name, panel);
        }
        panelList = null;

    }

    public void showPanel(string name, bool hideAll = true)
    {    
        if (hideAll) {
            foreach (GameObject panel in panels.Values) {
                panel.SetActive(false);
            }
        }

        panels[name + "Panel"].SetActive(true);
    }

    public void hidePanel(string name) {
        panels[name + "Panel"].SetActive(false);
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        switch(scene.name)
        {
            case "Main":
                showPanel("Main");
                break;
            case "Level1":
                hidePanel("Main");
                break;
        }
    }
 }
