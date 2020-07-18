using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.XR.WSA.Input;

public class Node
{
    private int x, y;
    private int numHijos;
    private int ganesPosibles;
    private int ganesPosiblesPosicion;
    private int jugador; // 1 = player, 2 = cpu
    private bool nodoHoja;
    private int nivel;

    public Node[] hijos;
    public int[,] TableroNode;
    public int posiblesJugadas;

    public Node(int[,] _tablero, int _jugador, int _nivel)
    {
        jugador = _jugador;
        nivel = _nivel;
        nodoHoja = false;
        TableroNode = new int[3, 3];
        numHijos = 0;
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                TableroNode[i, j] = _tablero[i, j];
            }
        }
        hijos = new Node[9];
    }

    public int getNivel()
    {
        return nivel;
    }

    public void setNivel(int _nivel)
    {
        nivel = _nivel;
    }

    public void setNodoHoja(bool _nodoHoja)
    {
        nodoHoja = _nodoHoja;
    }

    public bool getNodoHoja()
    {
        return nodoHoja;
    }

    public void setJugador(int _jugador)
    {
        jugador = _jugador;
    }

    public int getJugador()
    {
        return jugador;
    }

    public void setNumHijos(int _numHijos)
    {
        numHijos = _numHijos;
    }

    public int getNumHijos()
    {
        return numHijos;
    }
    public void setGanesPosibles(int _ganesPosibles)
    {
        ganesPosibles = _ganesPosibles;
    }

    public int getGanesPosibles()
    {
        return ganesPosibles;
    }

    public void setGanesPosiblesPosicion(int _ganesPosibles)
    {
        ganesPosiblesPosicion = _ganesPosibles;
    }

    public int getGanesPosiblesPosicion()
    {
        return ganesPosiblesPosicion;
    }

    public void setNewPosition(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public int getNewPositionX()
    {
        return x;
    }

    public int getNewPositionY()
    {
        return y;
    }
}

public class IAscript : MonoBehaviour
{
    private GameObject LevelManager;
    private int[,] Tablero;

    private Node root;

    void Start()
    {
        LevelManager = GameObject.Find("LevelManager");
    }

    public void tomarDesicion()
    {
        // Recibiendo el tablero y creando el nodo raiz
        Tablero = LevelManager.GetComponent<LevelManager>().getTablero();
        root = new Node(Tablero, 1, 0);

        // Creando el arbol de posibilidades
        crearHijos(root);

        // minmax
        minmax(root);

        // Escogiendo la jugada
        escogerJugada(root);
    }

    public void minmax(Node _root)
    {
        for(int i = 0; i < _root.getNumHijos(); i++)
        {
            minmax(_root.hijos[i]);
        }
        scoreHijos(_root);
    }

    public void scoreHijos(Node _root)
    {
        int score = 0;
        int index = 0;
        // Maximizar
        if (_root.getJugador() == 1 && _root.getNodoHoja() == false)
        {
            for (int i = 0; i < _root.getNumHijos(); i++)
            {
                if(_root.hijos[i].getGanesPosibles() >= score)
                {
                    score = _root.hijos[i].getGanesPosibles();
                    index = i;
                }
            }
            _root.setGanesPosibles(score);
        }

        // Minimizar
        if (_root.getJugador() == 2 && _root.getNodoHoja() == false)
        {
            for (int i = 0; i < _root.getNumHijos(); i++)
            {
                if (_root.hijos[i].getGanesPosibles() <= score)
                {
                    score = _root.hijos[i].getGanesPosibles();
                    index = i;
                }
            }
            _root.setGanesPosibles(score);
        }
    }

    public void crearHijos(Node _root)
    {
        int _jugador;
        int _nivel = _root.getNivel()+1;
        if (_root.getJugador() == 1) // Si el tablero es movimiento de jugador sigue la pc
        {
            _jugador = 2;
        }
        else // si no entonces sigue movimiento del jugador
        {
            _jugador = 1;
        }
        int numHijo = 0;
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(_root.TableroNode[i,j] == 0)
                {
                    // Checando si es un tablero del cpu o del jugador
                    _root.hijos[numHijo] = new Node(_root.TableroNode, _jugador, _nivel);
                    if(_root.hijos[numHijo].getJugador() == 1)
                    {
                        _root.hijos[numHijo].TableroNode[i, j] = 1;
                    }
                    if (_root.hijos[numHijo].getJugador() == 2)
                    {
                        _root.hijos[numHijo].TableroNode[i, j] = 2;
                    }
                    // Guardando la nueva posicion
                    _root.hijos[numHijo].setNewPosition(i, j);

                    // Calculando los scores del nuevo tablero
                    if(_root.hijos[numHijo].getJugador() == 2)
                    {
                        calcularGanesPosiblesCPU(_root.hijos[numHijo]);
                    }
                    if(_root.hijos[numHijo].getJugador() == 1)
                    {
                        calcularGanesPosiblesPlayer(_root.hijos[numHijo]);
                    }
                    calcularGanesPosiblesPosicion(_root.hijos[numHijo]);
                    checarTableroGanador(_root.hijos[numHijo]);
                    
                    // Checar si es un tablero que aun puede tener hijos
                    if((_root.hijos[numHijo].getGanesPosibles() < 100 && _root.hijos[numHijo].getGanesPosibles() > -100) && checarTableroFull(_root.hijos[numHijo]) == false)
                    {
                        crearHijos(_root.hijos[numHijo]);
                    }
                    else // Soy nodo hoja
                    {
                        _root.hijos[numHijo].setNodoHoja(true);
                    }
                    numHijo++;
                }
            }
        }
        _root.setNumHijos(numHijo);
        //imprimirHijos(_root);
    }

    public void calcularGanesPosiblesCPU(Node _root)
    {
        int ganesPosibles = 0;
        // checando Horizontaeles
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(_root.TableroNode[i, j] == 1)
                {
                    break;
                }
                if(j == 2)
                {
                    ganesPosibles++;
                }
            }
        }

        // Checando Verticales
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (_root.TableroNode[j, i] == 1)
                {
                    break;
                }
                if (j == 2)
                {
                    ganesPosibles++;
                }
            }
        }

        // checando diagonales
        for(int i = 0; i < 3; i++)
        {
            if (_root.TableroNode[i, i] == 1)
            {
                break;
            }
            if(i == 2)
            {
                ganesPosibles++;
            }
        }
        if(_root.TableroNode[0, 2] != 1 && _root.TableroNode[1, 1] != 1 && _root.TableroNode[2, 0] != 1)
        {
            ganesPosibles++;
        }
        _root.setGanesPosibles(ganesPosibles);
    }

    public void calcularGanesPosiblesPlayer(Node _root)
    {
        int ganesPosibles = 0;
        // checando Horizontaeles
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (_root.TableroNode[i, j] == 2)
                {
                    break;
                }
                if (j == 2)
                {
                    ganesPosibles--;
                }
            }
        }

        // Checando Verticales
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (_root.TableroNode[j, i] == 2)
                {
                    break;
                }
                if (j == 2)
                {
                    ganesPosibles--;
                }
            }
        }

        // checando diagonales
        for (int i = 0; i < 3; i++)
        {
            if (_root.TableroNode[i, i] == 2)
            {
                break;
            }
            if (i == 2)
            {
                ganesPosibles--;
            }
        }
        if (_root.TableroNode[0, 2] != 2 && _root.TableroNode[1, 1] != 2 && _root.TableroNode[2, 0] != 2)
        {
            ganesPosibles--;
        }

        _root.setGanesPosibles(ganesPosibles);
    }
    public void calcularGanesPosiblesPosicion(Node _root)
    {
        int ganesPosibles = 0;
        bool diagonal = true;
        // checando Horizontaeles
        for (int j = 0; j < 3; j++)
        {
            if (_root.TableroNode[_root.getNewPositionX(), j] == 1)
            {
                break;
            }
            if (j == 2)
            {
                ganesPosibles++;
            }
        }

        // checando Verticales
        for (int j = 0; j < 3; j++)
        {
            if (_root.TableroNode[j, _root.getNewPositionY()] == 1)
            {
                break;
            }
            if (j == 2)
            {
                ganesPosibles++;
            }
        }

        // Checando diagonales
        if((_root.getNewPositionX() == 0 && _root.getNewPositionY() == 1) ||
            (_root.getNewPositionX() == 1 && _root.getNewPositionY() == 0) ||
            (_root.getNewPositionX() == 1 && _root.getNewPositionY() == 2) ||
            (_root.getNewPositionX() == 2 && _root.getNewPositionY() == 1))
        {
            diagonal = false;
        }

        if (diagonal)
        {
            if((_root.getNewPositionX() == 0 && _root.getNewPositionY() == 0) || (_root.getNewPositionX() == 1 && _root.getNewPositionY() == 1) || (_root.getNewPositionX() == 2 && _root.getNewPositionY() == 2))
            {
                if (_root.TableroNode[0, 0] != 1 && _root.TableroNode[1, 1] != 1 && _root.TableroNode[2, 2] != 1)
                {
                    ganesPosibles++;
                }
            }
            if ((_root.getNewPositionX() == 0 && _root.getNewPositionY() == 2) || (_root.getNewPositionX() == 1 && _root.getNewPositionY() == 1) || (_root.getNewPositionX() == 2 && _root.getNewPositionY() == 0))
            {
                if (_root.TableroNode[0, 2] != 1 && _root.TableroNode[1, 1] != 1 && _root.TableroNode[2, 0] != 1)
                {
                    ganesPosibles++;
                }
            }
        }
        _root.setGanesPosiblesPosicion(ganesPosibles);
    }

    public void escogerJugada(Node _root)
    {
        int numHijos = _root.getNumHijos();
        int mejorHijo = 0;
        int puntuacionMejorHijo = 0;
        for(int i = 0; i < numHijos; i++)
        {
            if(_root.hijos[i].getGanesPosibles() >= puntuacionMejorHijo)
            {
                mejorHijo = i;
                puntuacionMejorHijo = _root.hijos[i].getGanesPosiblesPosicion();
            }
            if (buenInicio(_root.hijos[i]))
            {
                mejorHijo = i;
                break;
            }
        }
        LevelManager.GetComponent<LevelManager>().escogerCasillaCPU(_root.hijos[mejorHijo].getNewPositionX(), _root.hijos[mejorHijo].getNewPositionY());
    }

    public bool buenInicio(Node _root)
    {
        int numUnos = 0;
        if(_root.TableroNode[1,1] == 2)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(_root.TableroNode[i, j] == 1)
                    {
                        numUnos++;
                    }
                }
            }
            if(numUnos <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void imprimirHijos(Node _root)
    {
        int numHijos = _root.getNumHijos();
        for(int hijo = 0; hijo < numHijos; hijo++)
        {
            Debug.Log(_root.hijos[hijo].TableroNode[0, 0] + " " + _root.hijos[hijo].TableroNode[0, 1] + " " + _root.hijos[hijo].TableroNode[0, 2] + "\n" +
            _root.hijos[hijo].TableroNode[1, 0] + " " + _root.hijos[hijo].TableroNode[1, 1] + " " + _root.hijos[hijo].TableroNode[1, 2] + "\n" +
            _root.hijos[hijo].TableroNode[2, 0] + " " + _root.hijos[hijo].TableroNode[2, 1] + " " + _root.hijos[hijo].TableroNode[2, 2] + "\n" +
            "posibles ganes: " + _root.hijos[hijo].getGanesPosibles() + " posicion nueva: " + _root.hijos[hijo].getNewPositionX() + ", " + _root.hijos[hijo].getNewPositionY() + "\n" +
            "posibles ganes posicion: " + _root.hijos[hijo].getGanesPosiblesPosicion() + " Nivel: " + _root.hijos[hijo].getNivel() + "Nodo Hoja: " + _root.hijos[hijo].getNodoHoja());
        }
    }

    public bool checarTableroFull(Node _root)
    {
        int cont = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (_root.TableroNode[i, j] != 0)
                {
                    cont++;
                }
            }
        }
        if (cont == 9)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void checarTableroGanador(Node root)
    {
        int puntPlayer = 0;
        int puntCPU = 0;

        // Horizontales
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (root.TableroNode[i, j] == 1)
                {
                    puntPlayer++;
                }
                if (root.TableroNode[i, j] == 2)
                {
                    puntCPU++;
                }
                if (puntPlayer == 3)
                {
                    root.setGanesPosibles(-100);
                }
                if (puntCPU == 3)
                {
                    root.setGanesPosibles(100);
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
                if (root.TableroNode[j, i] == 1)
                {
                    puntPlayer++;
                }
                if (root.TableroNode[j, i] == 2)
                {
                    puntCPU++;
                }
                if (puntPlayer == 3)
                {
                    root.setGanesPosibles(-100);
                }
                if (puntCPU == 3)
                {
                    root.setGanesPosibles(100);
                }
            }
            puntPlayer = 0;
            puntCPU = 0;
        }

        if (root.TableroNode[0, 0] == 1 && root.TableroNode[1, 1] == 1 && root.TableroNode[2, 2] == 1)
        {
            root.setGanesPosibles(-100);
        }
        if (root.TableroNode[0, 0] == 2 && root.TableroNode[1, 1] == 2 && root.TableroNode[2, 2] == 2)
        {
            root.setGanesPosibles(100);
        }

        if (root.TableroNode[0, 2] == 1 && root.TableroNode[1, 1] == 1 && root.TableroNode[2, 0] == 1)
        {
            root.setGanesPosibles(-100);
        }
        if (root.TableroNode[0, 2] == 2 && root.TableroNode[1, 1] == 2 && root.TableroNode[2, 0] == 2)
        {
            root.setGanesPosibles(100);
        }
    }
}
