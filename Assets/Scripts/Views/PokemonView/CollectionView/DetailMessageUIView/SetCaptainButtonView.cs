using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCaptainButtonView : PokemonElement
{
     void OnSelect()
    {
        app.controller.SetCaptain();
    }
	
}
