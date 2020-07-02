using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // Variables Privadas 
    private int[] Tablero;
    bool empate = false;
    bool ganaste = false;
    bool perdiste = false;

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
        Tablero = new int[9];
        for (int i = 0; i < 9; i++)
        {
            Tablero[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int[] getTablero()
    {
        return Tablero;
    }

    public void escogerCasillaPlayer(int _casilla)
    {
        if(Tablero[_casilla - 1] == 0)
        {
            Tablero[_casilla - 1] = 1;
            colocarMarca(_casilla, 1);
            checarCasillas();
            if(!ganaste && !perdiste)
            {
                checarEmpate();
            }
            if (!empate && !ganaste && !perdiste)
            {
                escogerCasillaCPU();
            }
        }
    }

    public void escogerCasillaCPU()
    {
        bool encontrado = false;
        int _casilla = 0;
        while (!encontrado)
        {
            _casilla = Random.Range(0, 8);
            if (Tablero[_casilla] == 0)
            {
                Tablero[_casilla] = 2;
                colocarMarca(_casilla+1, 2);
                checarCasillas();
                encontrado = true;
            }
        }
    }

    public void checarCasillas()
    {
        if(Tablero[0] == 1 && Tablero[1] == 1 && Tablero[2] == 1)
        {
            cargarWin();
        }

        if (Tablero[0] == 2 && Tablero[1] == 2 && Tablero[2] == 2)
        {
            cargarLose();
        }
        if (Tablero[0] == 1 && Tablero[3] == 1 && Tablero[6] == 1)
        {
            cargarWin();
        }
        if (Tablero[0] == 2 && Tablero[3] == 2 && Tablero[6] == 2)
        {
            cargarLose();
        }

        if (Tablero[2] == 1 && Tablero[5] == 1 && Tablero[8] == 1)
        {
            cargarWin();
        }
        if (Tablero[2] == 2 && Tablero[5] == 2 && Tablero[8] == 2)
        {
            cargarLose();
        }

        if (Tablero[6] == 1 && Tablero[7] == 1 && Tablero[8] == 1)
        {
            cargarWin();
        }
        if (Tablero[6] == 2 && Tablero[7] == 2 && Tablero[8] == 2)
        {
            cargarLose();
        }

        if (Tablero[1] == 1 && Tablero[4] == 1 && Tablero[7] == 1)
        {
            cargarWin();
        }
        if (Tablero[1] == 2 && Tablero[4] == 2 && Tablero[7] == 2)
        {
            cargarLose();
        }

        if (Tablero[3] == 1 && Tablero[4] == 1 && Tablero[5] == 1)
        {
            cargarWin();
        }
        if (Tablero[3] == 2 && Tablero[4] == 2 && Tablero[5] == 2)
        {
            cargarLose();
        }

        if (Tablero[2] == 1 && Tablero[4] == 1 && Tablero[6] == 1)
        {
            cargarWin();
        }
        if (Tablero[2] == 2 && Tablero[4] == 2 && Tablero[6] == 2)
        {
            cargarLose();
        }

        if (Tablero[0] == 1 && Tablero[4] == 1 && Tablero[8] == 1)
        {
            cargarWin();
        }
        if (Tablero[0] == 2 && Tablero[4] == 2 && Tablero[8] == 2)
        {
            cargarLose();
        }
    }

    void cargarWin()
    {
        ganaste = true;
        SceneLoader.GetComponent<SceneLoader>().CargarEscena(3);

    }

    void cargarLose()
    {
        perdiste = true;
        SceneLoader.GetComponent<SceneLoader>().CargarEscena(2);
    }

    public void checarEmpate()
    {
        if (Tablero[0] != 0 && Tablero[1] != 0 && Tablero[2] != 0 && Tablero[3] != 0 && Tablero[4] != 0 && Tablero[5] != 0 && Tablero[6] != 0 && Tablero[7] != 0 && Tablero[8] != 0)
        {
            empate = true;
            SceneLoader.GetComponent<SceneLoader>().CargarEscena(4);
        }
    }

    public void colocarMarca(int _casilla, int _marca)
    {
        if(_casilla == 1)
        {
            if(_marca == 1)
            {
                Slot1Btn.image.sprite = Cruz;
            }
            if(_marca == 2)
            {
                Slot1Btn.image.sprite = Circulo;
            }
        }
        if (_casilla == 2)
        {
            if (_marca == 1)
            {
                Slot2Btn.image.sprite = Cruz;
            }
            if (_marca == 2)
            {
                Slot2Btn.image.sprite = Circulo;
            }
        }
        if (_casilla == 3)
        {
            if (_marca == 1)
            {
                Slot3Btn.image.sprite = Cruz;
            }
            if (_marca == 2)
            {
                Slot3Btn.image.sprite = Circulo;
            }
        }
        if (_casilla == 4)
        {
            if (_marca == 1)
            {
                Slot4Btn.image.sprite = Cruz;
            }
            if (_marca == 2)
            {
                Slot4Btn.image.sprite = Circulo;
            }
        }
        if (_casilla == 5)
        {
            if (_marca == 1)
            {
                Slot5Btn.image.sprite = Cruz;
            }
            if (_marca == 2)
            {
                Slot5Btn.image.sprite = Circulo;
            }
        }
        if (_casilla == 6)
        {
            if (_marca == 1)
            {
                Slot6Btn.image.sprite = Cruz;
            }
            if (_marca == 2)
            {
                Slot6Btn.image.sprite = Circulo;
            }
        }
        if (_casilla == 7)
        {
            if (_marca == 1)
            {
                Slot7Btn.image.sprite = Cruz;
            }
            if (_marca == 2)
            {
                Slot7Btn.image.sprite = Circulo;
            }
        }
        if (_casilla == 8)
        {
            if (_marca == 1)
            {
                Slot8Btn.image.sprite = Cruz;
            }
            if (_marca == 2)
            {
                Slot8Btn.image.sprite = Circulo;
            }
        }
        if (_casilla == 9)
        {
            if (_marca == 1)
            {
                Slot9Btn.image.sprite = Cruz;
            }
            if (_marca == 2)
            {
                Slot9Btn.image.sprite = Circulo;
            }
        }
    }
}
