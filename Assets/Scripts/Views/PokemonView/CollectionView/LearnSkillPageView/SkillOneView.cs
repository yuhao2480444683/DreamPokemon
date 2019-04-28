using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillOneView : PokemonElement
{
    void OnSelect()
    {
        app.controller.OnLearnSkillOneClick();
    }
	
}
