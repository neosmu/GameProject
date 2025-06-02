using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] ObservableProperty<int> hp = new(3);
    public int HP { get => hp.Value; set => hp.Value = value; }
    public event UnityAction<int> OnHpChanged
    {
        add => hp.Subscribe(value);
        remove => hp.Unsubscribe(value);
    }

    [SerializeField] private ObservableProperty<int> maxHP = new(3);
    public int MaxHP { get => maxHP.Value; set => maxHP.Value = value; }
    public event UnityAction<int> OnMaxHPChanged
    {
        add => maxHP.Subscribe(value);
        remove => maxHP.Unsubscribe(value);
    }

    [SerializeField] private ObservableProperty<int> score = new();
    public int Score { get => score.Value; set => score.Value = value; }
    public event UnityAction<int> OnScoreChanged
    {
        add => score.Subscribe(value);
        remove => score.Unsubscribe(value);
    }
}
