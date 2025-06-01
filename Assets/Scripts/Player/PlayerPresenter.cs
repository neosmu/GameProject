using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPresenter : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] PlayerModel model;

    [Header("View")]
    [SerializeField] Image[] heartImages;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    [SerializeField] TextMeshProUGUI scoreText;
    private void OnEnable()
    {
        model.OnHpChanged += UpdateHPUI;
        model.OnMaxHPChanged += UpdateHPUI;
        model.OnScoreChanged += UpdateScoreUI;

        UpdateHPUI(model.HP);
        UpdateScoreUI(model.Score);
    }

    private void OnDisable()
    {
        model.OnHpChanged -= UpdateHPUI;
        model.OnMaxHPChanged -= UpdateHPUI;
        model.OnScoreChanged -= UpdateScoreUI;
    }

    private void UpdateHPUI(int hp)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].sprite = i < hp ? fullHeart : emptyHeart;
            heartImages[i].enabled = i < model.MaxHP;
        }
    }
    private void UpdateScoreUI(int score)
    {
        if (scoreText != null)
            scoreText.text = $"{score}";
    }
}
