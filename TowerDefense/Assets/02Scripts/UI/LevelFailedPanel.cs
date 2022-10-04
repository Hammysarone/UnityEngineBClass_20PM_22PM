using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;


public class LevelFailedPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _level;
    [SerializeField] private Button _BackToLobbyButton;
    [SerializeField] private Button _ReplayButton;

    public void SetUp(int level, UnityAction buttonAction)
    {
        _level.text = level.ToString();
        _BackToLobbyButton.onClick.AddListener(buttonAction);
        _ReplayButton.onClick.AddListener(buttonAction);
        gameObject.SetActive(true);
    }
}
