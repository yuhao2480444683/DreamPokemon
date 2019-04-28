using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySkillTwoView : PokemonElement
{
    void OnSelect()
    {
        app.controller.ReplaceSkillTwo();
    }


}
