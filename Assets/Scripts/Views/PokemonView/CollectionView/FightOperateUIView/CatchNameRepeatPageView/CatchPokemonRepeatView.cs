using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPokemonRepeatView : PokemonElement
{

    void OnSelect()
    {
        app.fightController.NotReplaceRepeatPokemon();
    }
}
