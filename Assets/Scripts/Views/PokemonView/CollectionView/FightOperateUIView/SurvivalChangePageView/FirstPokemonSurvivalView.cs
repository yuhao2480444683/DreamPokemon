using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPokemonSurvivalView : PokemonElement {

    void OnSelect()
    {
        app.fightController.OnSurvivalChangeOneClick();
    }
}
