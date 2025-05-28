using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] int hp;
    public int HP { set { hp = value; OnHpChanged?.Invoke(hp); } get { return hp; } }
    public event Action<int> OnHpChanged;

    [SerializeField] int maxHP;
    public int MaxHP { set { maxHP = value; OnMaxHPChanged?.Invoke(maxHP); } get { return maxHP; } }
    public event Action<int> OnMaxHPChanged;
}
