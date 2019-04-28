using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFourView : PokemonElement {

    void OnSelect()
    {
        app.controller.OnLearnSkillFourClick();
    }

}
