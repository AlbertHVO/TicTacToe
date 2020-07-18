using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // Variables Privadas 
    private GameObject IA;
    private GameObject Beginner;
    private bool gameOver = false;
    private int[,] Tablero = new int[3,3];
    //int ganador = -1; // 0 = empate, 1 = ganaste, 2 = perdiste

    // Varibles Publicas 
    public Button Slot1Btn;
    public Button Slot2Btn;
    public Button Slot3Btn;
    public Button Slot4Btn;
    public Button Slot5Btn;
    public Button Slot6Btn;
    public Button Slot7Btn;
    public Button Slot8Btn;
    public Button Slot9Btn;

    public Sprite Cruz;
    public Sprite Circulo;

    public GameObject SceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                Tablero[i, j] = 0;
            }
        }
        IA = GameObject.Find("IA");
        Beginner = GameObject.Find("BeginnerManager");
        if(Beginner.GetComponent<Beginner>().getBeginner() == 2)
        {
            IA.GetComponent<IAscript>().tomarDesicion();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int[,] getTablero()
    {
        return Tablero;
    }

    public void escogerCasillaPlayer(int _casilla)
    {
        bool elegida = false;
        if (_casilla == 1)
        {
            if(Tablero[0, 0] == 0)
            {
                Tablero[0, 0] = 1;
                Slot1Btn.image.sprite = Cruz;
                elegida = true;
            }
        }
        if (_casilla == 2)
        {
            if (Tablero[0, 1] == 0)
            {
                Tablero[0, 1] = 1;
                Slot2Btn.image.sprite = Cruz;
                elegida = true;
            }
        }
        if (_casilla == 3)
        {
            if (Tablero[0, 2] == 0)
            {
                Tablero[0, 2] = 1;
                Slot3Btn.image.sprite = Cruz;
                elegida = true;
            }
        }
        if (_casilla == 4)
        {
            if (Tablero[1, 0] == 0)
            {
                Tablero[1, 0] = 1;
                Slot4Btn.image.sprite = Cruz;
                elegida = true;
            }
        }
        if (_casilla == 5)
        {
            if (Tablero[1, 1] == 0)
            {
                Tablero[1, 1] = 1;
                Slot5Btn.image.sprite = Cruz;
                elegida = true;
            }
        }
        if (_casilla == 6)
        {
            if (Tablero[1, 2] == 0)
            {
                Tablero[1, 2] = 1;
                Slot6Btn.image.sprite = Cruz;
                elegida = true;
            }
        }
        if (_casilla == 7)
        {
            if (Tablero[2, 0] == 0)
            {
                Tablero[2, 0] = 1;
                Slot7Btn.image.sprite = Cruz;
                elegida = true;
            }
        }
        if (_casilla == 8)
        {
            if (Tablero[2, 1] == 0)
            {
                Tablero[2, 1] = 1;
                Slot8Btn.image.sprite = Cruz;
                elegida = true;
            }
        }
        if (_casilla == 9)
        {
            if (Tablero[2, 2] == 0)
            {
                Tablero[2, 2] = 1;
                Slot9Btn.image.sprite = Cruz;
                elegida = true;
            }
        }
        checarCasillas();
        checarEmpate();
        if (!gameOver && elegida == true)
        {
            IA.GetComponent<IAscript>().tomarDesicion();
        }
    }

    public void escogerCasillaCPU(int _x, int _y)
    {
        Tablero[_x, _y] = 2;
        if(_x == 0 && _y == 0)
        {
            Slot1Btn.image.sprite = Circulo;
        }
        if (_x == 0 && _y == 1)
        {
            Slot2Btn.image.sprite = Circulo;
        }
        if (_x == 0 && _y == 2)
        {
            Slot3Btn.image.sprite = Circulo;
        }
        if (_x == 1 && _y == 0)
        {
            Slot4Btn.image.sprite = Circulo;
        }
        if (_x == 1 && _y == 1)
        {
            Slot5Btn.image.sprite = Circulo;
        }
        if (_x == 1 && _y == 2)
        {
            Slot6Btn.image.sprite = Circulo;
        }
        if (_x == 2 && _y == 0)
        {
            Slot7Btn.image.sprite = Circulo;
        }
        if (_x == 2 && _y == 1)
        {
            Slot8Btn.image.sprite = Circulo;
        }
        if (_x == 2 && _y == 2)
        {
            Slot9Btn.image.sprite = Circulo;
        }
        checarCasillas();
        checarEmpate();
    }

    public void checarCasillas()
    {
        int puntPlayer = 0;
        int puntCPU = 0;

        // Horizontales
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(Tablero[i, j] == 1)
                {
                    puntPlayer++;
                }
                if (Tablero[i, j] == 2)
                {
                    puntCPU++;
                }
                if(puntPlayer == 3)
                {
                    cargarWin();
                }
                if (puntCPU == 3)
                {
                    cargarLose();
                }
            }
            puntPlayer = 0;
            puntCPU = 0;
        }

        puntPlayer = 0;
        puntCPU = 0;

        // Verticales
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (Tablero[j, i] == 1)
                {
                    puntPlayer++;
                }
                if (Tablero[j, i] == 2)
                {
                    puntCPU++;
                }
                if (puntPlayer == 3)
                {
                    cargarWin();
                }
                if (puntCPU == 3)
                {
                    cargarLose();
                }
            }
            puntPlayer = 0;
            puntCPU = 0;
        }

        if (Tablero[0, 0] == 1 && Tablero[1, 1] == 1 && Tablero[2, 2] == 1){
            cargarWin();
        }
        if (Tablero[0, 0] == 2 && Tablero[1, 1] == 2 && Tablero[2, 2] == 2){
            cargarLose();
        }

        if (Tablero[0, 2] == 1 && Tablero[1, 1] == 1 && Tablero[2, 0] == 1)
        {
            cargarWin();
        }
        if (Tablero[0, 2] == 2 && Tablero[1, 1] == 2 && Tablero[2, 0] == 2)
        {
            cargarLose();
        }
    }

    public void checarEmpate()
    {
        int cont = 0;
        for(int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if(Tablero[i, j] != 0)
                {
                    cont++;
                }
            }
        }
        if(cont == 9)
        {
            cargarEmpate();
        }
    }

    void cargarWin()
    {
        gameOver = true;
        SceneLoader.GetComponent<SceneLoader>().CargarEscena(3);
    }

    void cargarLose()
    {
        gameOver = true;
        SceneLoader.GetComponent<SceneLoader>().CargarEscena(2);
    }

    void cargarEmpate()
    {
        gameOver = true;
        SceneLoader.GetComponent<SceneLoader>().CargarEscena(4);
    }
}
