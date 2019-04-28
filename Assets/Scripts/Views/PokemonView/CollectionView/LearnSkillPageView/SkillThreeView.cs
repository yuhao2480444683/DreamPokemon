using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThreeView : PokemonElement
{

    void OnSelect()
    {
        app.controller.OnLearnSkillThreeClick();
    }

}
