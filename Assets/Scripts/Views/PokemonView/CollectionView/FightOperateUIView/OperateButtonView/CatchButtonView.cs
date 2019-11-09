using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchButtonView : PokemonElement
{

    void OnSelect()
    {
        app.fightController.OnCatchButtonClick();
    }
    void OnCatch()
    {
        OnSelect();
    }
}
