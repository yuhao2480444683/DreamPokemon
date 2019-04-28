using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySkillThreeView : PokemonElement
{

    void OnSelect()
    {
        app.controller.ReplaceSkillThree();
    }

}
