using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager {

    private static ScoreManager instance;

    private int _Score;
    private Text _ScoreText;

    private int _ChainNum;

    public static ScoreManager Instance
    {
        get
        {
            if(instance == null) {
                instance = new ScoreManager();
                instance.Init();
            }
            return instance;
        }
    }

    public void Init() {
        _Score = 0;
    }

    public void SetScore(int score) {
        _Score = score;
    }

    public void AddScore(int value) {
        float result = (float)value;
        if(PlayerManager.Instance.m_Character == 2) {
            result = (float)value * 1.2f;
        }
        _Score += (int)result;
    }

    public int GetScore() {
        return _Score;
    }
}
