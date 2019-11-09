using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleButtonView : PokemonElement
{

    void OnSelect()
    {
        app.fightController.OnBattleButtonClick();
    }
    void OnBattle()
    {
        OnSelect();
    }
}
