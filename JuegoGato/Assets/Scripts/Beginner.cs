using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beginner : MonoBehaviour
{
    // Start is called before the first frame update
    static int beginner;

    public int getBeginner()
    {
        return beginner;
    }

    public void setBeginner(int _beginner)
    {
        beginner = _beginner;
    }
}
