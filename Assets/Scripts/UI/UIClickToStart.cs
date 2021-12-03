using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClickToStart : MonoBehaviour
{
    [SerializeField] LevelChanger levelChanger;

    public void PlayerClicked()
    {
        levelChanger.ChangeTo(1);
    }
}
