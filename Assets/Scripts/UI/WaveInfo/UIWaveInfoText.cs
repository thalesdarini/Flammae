using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIWaveInfoText : MonoBehaviour
{
    LevelManager levelManager;
    Text waveInfoText;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = transform.parent.parent.parent.parent.Find("WaveAlert").GetComponent<LevelManager>();
        waveInfoText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        waveInfoText.text = "Onda atual: " + levelManager.CurrentWave().ToString() + "/" + levelManager.NumOfWaves().ToString();
    }
}
