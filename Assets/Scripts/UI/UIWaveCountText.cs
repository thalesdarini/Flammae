using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIWaveCountText : MonoBehaviour
{
    LevelManager levelManager;
    Text waveCountText;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = transform.parent.GetComponent<LevelManager>();
        waveCountText = GetComponent<Text>();
    }

    public void UpdateText()
    {
        waveCountText.text = "Onda de inimigos! " + levelManager.CurrentWave().ToString() + "/" + levelManager.NumOfWaves().ToString();
    }
}
