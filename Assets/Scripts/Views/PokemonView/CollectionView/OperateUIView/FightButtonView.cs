using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightButtonView : PokemonElement
{

    void OnSelect()
    {
        app.fightController.OnFightDisplay();
        app.fightController.OnFightStart();
    }
   

}
