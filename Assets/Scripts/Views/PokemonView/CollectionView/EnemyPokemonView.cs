using UnityEngine;

public class EnemyPokemonView : PokemonElement
{
    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        app.controller.CheckEnemy();
    }
    void OnDisplayInformation()
    {
        OnSelect();
    }
    void OnCloseInformation()
    {
        OnSelect();
    }

}