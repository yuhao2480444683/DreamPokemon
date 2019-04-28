using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisSkillView : PokemonElement
{

    void OnSelect()
    {
        app.controller.NotLearnSkill();
    }
}
