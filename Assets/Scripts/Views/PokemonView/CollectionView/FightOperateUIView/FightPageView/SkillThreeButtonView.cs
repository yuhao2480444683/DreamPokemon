using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillThreeButtonView : PokemonElement
{

    void OnSelect()
    {
        app.fightController.OnSkillThreeClick();
    }
}
