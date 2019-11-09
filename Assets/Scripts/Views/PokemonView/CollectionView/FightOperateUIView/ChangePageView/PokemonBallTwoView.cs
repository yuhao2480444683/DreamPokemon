using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonBallTwoView : PokemonElement
{
    void OnSelect()
    {
        app.fightController.PokeBallTwoClick();
    }
    void OnChangeThisPokemon()
    {
        OnSelect();
    }
}
