using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _startText;
    private const float TEXT_FADE_TIME = 1.0f;
    private void Awake()
    {
        _startText
            .DOFade(0, TEXT_FADE_TIME)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("Main");
        }
    }
}
