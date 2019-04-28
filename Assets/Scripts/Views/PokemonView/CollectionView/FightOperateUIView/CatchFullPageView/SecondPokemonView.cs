using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPokemonView : PokemonElement
{
    void OnSelect()
    {
        app.fightController.OnSecondReplace();
    }
}
