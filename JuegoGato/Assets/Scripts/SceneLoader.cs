using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void CargarEscena(int _scene)
    {
        SceneManager.LoadScene(_scene);
    }
    public void exit()
    {
        Application.Quit();
    }
}