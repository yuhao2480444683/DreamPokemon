using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPokemonSurvivalView : PokemonElement {

    void OnSelect()
    {
        app.fightController.OnSurvivalChangeThreeClick();
    }
}
