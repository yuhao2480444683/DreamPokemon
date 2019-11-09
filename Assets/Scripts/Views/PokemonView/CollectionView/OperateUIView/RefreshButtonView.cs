using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshButtonView : PokemonElement
{
  

    void OnSelect()
    {
      app.controller.SetEnemy();
    }
    void OnRefreshEnemy()
    {
        OnSelect();
    }

}
