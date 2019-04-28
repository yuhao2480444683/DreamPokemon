using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySkillOneView : PokemonElement
{
    void OnSelect()
    {
        app.controller.ReplaceSkillOne();
    }


}
