using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    public void AddPoint()
    {
        score++;
        Debug.Log("Score: " + score);
    }
}
