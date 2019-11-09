using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPokemonButtonView : PokemonElement
{
    void OnSelect()
    {
        app.controller.DisplayNextPokemon();
    }

    void OnNextPokemon()
    {
        OnSelect();
    }
}
