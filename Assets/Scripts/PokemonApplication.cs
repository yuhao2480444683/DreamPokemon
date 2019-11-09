using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonElement : MonoBehaviour
{
    public PokemonApplication app { get { return GameObject.FindObjectOfType<PokemonApplication>(); } }
}



public class PokemonApplication : MonoBehaviour
{

    public PokemonModel model;
    public PokemonView view;
    public PokemonController controller;
    public FightController fightController;
    public DetailMessageService detailMessageService;
    public EnemyService enemyService;
    public FightService fightService;
    public SkillService skillService;

    void Start()
    {
        
    }

}
