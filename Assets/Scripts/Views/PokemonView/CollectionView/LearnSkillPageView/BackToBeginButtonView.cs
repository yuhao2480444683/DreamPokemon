using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToBeginButtonView : PokemonElement {

    void OnSelect()
    {
        app.controller.OnBackToBeginButtonClick();
    }

}
