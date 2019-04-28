using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonBallOneView : PokemonElement
{
    void OnSelect()
    {
        app.fightController.PokeBallOneClick();

    }

}
