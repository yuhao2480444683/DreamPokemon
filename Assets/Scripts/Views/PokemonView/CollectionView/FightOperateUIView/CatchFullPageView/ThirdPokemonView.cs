using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPokemonView : PokemonElement {

    void OnSelect()
    {
        app.fightController.OnThirdReplace();
    }
}
