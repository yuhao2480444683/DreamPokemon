using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTwoView : PokemonElement
{
    void OnSelect()
    {
        app.controller.OnLearnSkillTwoClick();
    }


}
