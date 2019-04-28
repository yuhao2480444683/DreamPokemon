using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButtonView : PokemonElement
{


    void OnSelect()
    {
        app.fightController.OnChangeButtonClick();
    }

}
