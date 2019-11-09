using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeButtonView : PokemonElement
{

    void OnSelect()
    {
        app.fightController.OnEscapeButtonClick();
    }
    void OnEscape()
    {
        OnSelect();
    }
}
