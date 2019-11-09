using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnSkillButtonView : PokemonElement
{
    void OnSelect()
    {
        app.controller.OnLearnSkillClick();
    }

    void OnLearnSkill()
    {
        OnSelect();
    }
}
