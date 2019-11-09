using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonView : PokemonElement {

    void OnSelect()
    {
        app.fightController.OnBackButtonClick();
    }
    void OnBack()
    {
        OnSelect();
    }
}
