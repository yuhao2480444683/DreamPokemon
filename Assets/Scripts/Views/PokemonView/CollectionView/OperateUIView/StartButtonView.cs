using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonView : PokemonElement {

    void OnSelect()
    {
        app.controller.InitMyPokemon();
    }
}
