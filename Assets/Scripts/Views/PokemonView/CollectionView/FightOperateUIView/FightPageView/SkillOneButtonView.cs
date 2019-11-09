using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOneButtonView : PokemonElement
{

    void OnSelect()
    {
        app.fightController.OnSkillOneClick();
    }
    void OnUseThisSkill()
    {
        OnSelect();
    }
}
