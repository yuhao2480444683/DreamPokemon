using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class EnemyService : PokemonElement
{

    public void SetRandomEnemy()
    {

        string randomEnemyType = "";
        int CaptainLevel = -1;
        Random rd = new Random();
        int randomNum = rd.Next(0, 4);

        switch (randomNum)
        {
            case 0:
                randomEnemyType = "Charmander";
                break;
            case 1:
                randomEnemyType = "Squirtle";
                break;
            case 2:
                randomEnemyType = "Bulbasaur";
                break;
            case 3: randomEnemyType = "Pikachu";
                break;
         
        }


        foreach (Pokemon pokemon in app.model.myPokemons)
        {
            if (pokemon.IsCaptain == true)
            {
                CaptainLevel = pokemon.Level;
                break;
            }
        }


        Pokemon randomEnemy = new Pokemon
        {
            PokemonType = randomEnemyType,
            Level = rd.Next(CaptainLevel - 5, CaptainLevel + 1),
            Health = rd.Next(CaptainLevel * 10, (CaptainLevel + 1) * 10 + 1),
            EX = rd.Next(CaptainLevel * 10, (CaptainLevel + 1) * 10 + 1),
            Speed = rd.Next((CaptainLevel * 2) - 10, (CaptainLevel * 2) + 10 + 1),
            Aggerssivity = rd.Next((CaptainLevel * 10) - 5, (CaptainLevel * 10) + 5 + 1),
            Magic = rd.Next((CaptainLevel * 10) - 5, (CaptainLevel * 10) + 5 + 1),
            Armor = rd.Next(CaptainLevel - 5, CaptainLevel + 5 + 1),
            Ressistance = rd.Next(CaptainLevel - 5, CaptainLevel + 5 + 1),
            IsCaptain = false
        };
        randomEnemy.CurrentHP = randomEnemy.Health;
        randomEnemy.CurrentEX = 0;
        randomEnemy.Skills = new List<Skill>();



        switch (randomEnemyType)
        {
            case "Charmander":
                randomEnemy.Skills.Add(app.model.GetRandomSkill(1, randomEnemy.Level));
                break;
            case "Squirtle":
                randomEnemy.Skills.Add(app.model.GetRandomSkill(2, randomEnemy.Level));
                break;
            case "Bulbasaur":
                randomEnemy.Skills.Add(app.model.GetRandomSkill(3, randomEnemy.Level));
                break;
            case "Pikachu":
                randomEnemy.Skills.Add(app.model.GetRandomSkill(4,randomEnemy.Level));
                break;
          
        }


        randomEnemy.Skills.Add(app.model.skillHit);
        randomEnemy.Skills.Add(app.model.skillBite);


        app.model.thisEnemy = new Pokemon();
        app.model.thisEnemy.Skills = new List<Skill>();
        app.model.thisEnemy.Assign(randomEnemy);


    }

    

}
