using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPokemonSurvivalView : PokemonElement {

    void OnSelect()
    {
        app.fightController.OnSurvivalChangeTwoClick();
    }
}
