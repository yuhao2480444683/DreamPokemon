using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using UnityEngine;
using Random = System.Random;


public class DetailMessageService : PokemonElement {

    public void InitialData()
    {
        var pokemons = new List<Pokemon>();
        var skills = new List<Skill>();

        Skill skill = new Skill
        {
            SkillName = "Hit",
            Frequency = 20,
            Coefficient = 0.3f,
            Skilltype = 1
        };
        skills.Add(skill);
        
        skill = new Skill
        {
            SkillName = "Bite",
            Frequency = 20,
            Coefficient = 0.4f,
            Skilltype = 1

        };
        skills.Add(skill);

        Pokemon pokemon1 = new Pokemon
        {
            PokemonType = "Pikachu",
            Level = 10,
            Health = 120,
            CurrentHP = 120,
            EX = 90,
            CurrentEX = 0,
            Speed = 35,
            Aggerssivity = 106,
            Magic = 125,
            Armor = 10,
            Ressistance = 15,
            IsCaptain = true
        };
        pokemon1.Skills = new List<Skill>();
        pokemon1.Skills.Add(skills[0]);
        pokemon1.Skills.Add(skills[1]);


        Pokemon pokemon2 = new Pokemon
        {
            PokemonType = "Charmander",
            Level = 10,
            Health = 110,
            CurrentHP = 110,
            EX = 100,
            CurrentEX = 0,
            Speed = 30,
            Aggerssivity = 120,
            Magic = 110,
            Armor = 15,
            Ressistance = 20,
            IsCaptain = false
        };
        pokemon2.Skills = new List<Skill>();
        pokemon2.Skills.Add(skills[0]);
        pokemon2.Skills.Add(skills[1]);


        pokemons.Add(pokemon1);
        pokemons.Add(pokemon2);

        app.model.InitSkills();

        app.model.fightFlag = -1;

        //序列化（初始数据） 
        DataContractJsonSerializer serializer =
            new DataContractJsonSerializer(typeof(List<Pokemon>));
        MemoryStream inStream = new MemoryStream();
        serializer.WriteObject(inStream, pokemons);
        inStream.Position = 0;
        StreamReader sr = new StreamReader(inStream, Encoding.UTF8);
        string injson = sr.ReadToEnd();

        PlayerPrefs.SetString("myPokemons", injson);



    }

    public void SetNextPokemon()
    {

        for (int i = 0; i < app.model.myPokemons.Count; i++)
        {

            if (app.model.thisPokemon.PokemonType.Equals(app.model.myPokemons[i].PokemonType))
            {
                int num = (i + 1) % app.model.myPokemons.Count;
                app.model.thisPokemon.Assign(app.model.myPokemons[num]);
                break;
            }
        }
    }

    public void UpdatePokemonsToFile()
    {
        DataContractJsonSerializer serializer =
            new DataContractJsonSerializer(typeof(List<Pokemon>));
        MemoryStream inStream = new MemoryStream();
        serializer.WriteObject(inStream, app.model.myPokemons);
        inStream.Position = 0;
        StreamReader sr = new StreamReader(inStream, Encoding.UTF8);
        string injson = sr.ReadToEnd();
        PlayerPrefs.SetString("myPokemons", injson);

    }

    public string GetCaptainType()
    {
        foreach (Pokemon pokemon in app.model.myPokemons)
        {
            if (pokemon.IsCaptain == true)
            {
                return pokemon.PokemonType;
            }
        }
        return null;
    }


    public void SetCaptain()
    {
        for (int i = 0; i < app.model.myPokemons.Count; i++)
        {
            if (app.model.thisPokemon.PokemonType == app.model.myPokemons[i].PokemonType)
            {
                app.model.myPokemons[i].IsCaptain = true;
                app.model.thisPokemon.Assign(app.model.myPokemons[i]);

            }
            else
            {
                app.model.myPokemons[i].IsCaptain = false;
            }
        }
        app.detailMessageService.UpdatePokemonsToFile();

    }

    public void SetMyPokemonsFromFile()
    {
        String outjson = PlayerPrefs.GetString("myPokemons");
        MemoryStream outStream =
            new MemoryStream(Encoding.UTF8.GetBytes(outjson));
        DataContractJsonSerializer serializer =
            new DataContractJsonSerializer(typeof(List<Pokemon>));
        List<Pokemon> outPokemons = (List<Pokemon>)serializer.ReadObject(outStream);

        app.model.myPokemons = new List<Pokemon>();

        for (int i = 0; i < outPokemons.Count; i++)
        {
            app.model.myPokemons.Add(outPokemons[i]);
        }
    }

    public void CaptainToThisPokemon()
    {

        foreach (Pokemon pokemon in app.model.myPokemons)
        {
            if (pokemon.IsCaptain == true)
            {
                app.model.thisPokemon = new Pokemon();
                app.model.thisPokemon.Skills = new List<Skill>();
                app.model.thisPokemon.Assign(pokemon); 
            }
        }

    }

    public bool IsThisPokemonCaptain()
    {
        if (app.model.thisPokemon.IsCaptain == true)
        {
            return true;
        }
        return false;
    }

    public Pokemon GetCaptainPokemon()
    {
        foreach (Pokemon pokemon in app.model.myPokemons)
        {
            if (pokemon.IsCaptain == true)
            {
                return pokemon;
            }
        }

        return null;
    }

    public int GetMyLevel()
    {
        return app.model.thisPokemon.Level;
    }

    public int GetMyMaxHP()
    {
        return app.model.thisPokemon.Health;
    }

    public string GetMyName()
    {
        return app.model.thisPokemon.PokemonType;
    }

    public int GetMyAggerssivity()
    {
        return app.model.thisPokemon.Aggerssivity;
    }

    public int GetMyMagic()
    {
        return app.model.thisPokemon.Magic;
    }

    public int GetMyArmor()
    {
        return app.model.thisPokemon.Armor;
    }

    public int GetMyRessistance()
    {
        return app.model.thisPokemon.Ressistance;
    }

    public int GetMySpeed()
    {
        return app.model.thisPokemon.Speed;
    }


    public int GetEnemyLevel()
    {
        return app.model.thisEnemy.Level;
    }

    public int GetEnemyMaxHP()
    {
        return app.model.thisEnemy.Health;
    }

    public string GetEnemyName()
    {
        return app.model.thisEnemy.PokemonType;
    }

    public int GetEnemyAggerssivity()
    {
        return app.model.thisEnemy.Aggerssivity;
    }

    public int GetEnemyMagic()
    {
        return app.model.thisEnemy.Magic;
    }

    public int GetEnemyArmor()
    {
        return app.model.thisEnemy.Armor;
    }

    public int GetEnemyRessistance()
    {
        return app.model.thisEnemy.Ressistance;
    }

    public int GetEnemySpeed()
    {
        return app.model.thisEnemy.Speed;
    }










}
