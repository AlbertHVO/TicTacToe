using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAscript : MonoBehaviour
{
    private GameObject LevelManager;
    private int[] Tablero;
    // Start is called before the first frame update
    void Start()
    {
        LevelManager = GameObject.Find("LevelManager");
        Tablero = LevelManager.GetComponent<LevelManager>().getTablero();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
