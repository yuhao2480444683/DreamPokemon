using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPokemonView : PokemonElement
{
    void OnSelect()
    {
        app.fightController.OnFirstReplace();
    }

}
