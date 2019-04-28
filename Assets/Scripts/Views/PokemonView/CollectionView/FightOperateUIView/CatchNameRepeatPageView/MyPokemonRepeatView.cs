using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPokemonRepeatView : PokemonElement
{

    void OnSelect()
    {
        app.fightController.ReplaceRepeatPokemon();
    }
}
