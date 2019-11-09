using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTwoButtonView : PokemonElement
{

    void OnSelect()
    {
        app.fightController.OnSkillTwoClick();
    }
    void OnUseThisSkill()
    {
        OnSelect();
    }
}
