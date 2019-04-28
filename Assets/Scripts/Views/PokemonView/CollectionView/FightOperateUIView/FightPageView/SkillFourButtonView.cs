using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFourButtonView : PokemonElement
{
    void OnSelect()
    {
        app.fightController.OnSkillFourClick();
    }

}
