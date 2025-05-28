using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPresenter : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] PlayerModel model;

    [Header("View")]
    [SerializeField] Image[] heartImages;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    private void OnEnable()
    {
        model.OnHpChanged += UpdateHPUI;
        model.OnMaxHPChanged += UpdateHPUI;
        UpdateHPUI(model.HP);
    }

    private void OnDisable()
    {
        model.OnHpChanged -= UpdateHPUI;
        model.OnMaxHPChanged -= UpdateHPUI;
    }

    private void UpdateHPUI(int hp)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].sprite = i < hp ? fullHeart : emptyHeart;
            heartImages[i].enabled = i < model.MaxHP;
        }
    }
}
