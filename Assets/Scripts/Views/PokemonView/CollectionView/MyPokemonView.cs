using UnityEngine;

public class MyPokemonView : PokemonElement
{

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
       
        app.controller.CheckMyPokemon();
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