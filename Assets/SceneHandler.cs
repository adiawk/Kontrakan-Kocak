using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] DemoLoadScene loadScene;

    public void GoToScene(string sceneName)
    {
        loadScene.LoadScene(sceneName);
    }
}
