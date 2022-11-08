using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Scene_Change : MonoBehaviour
{
    public void Scene_change(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
}
