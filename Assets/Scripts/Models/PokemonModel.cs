using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using UnityEngine;
using Random = System.Random;

[DataContract]
public class Skill
{
    [DataMember]
    public string SkillName { get; set; }
    [DataMember]
    public int Skilltype { get; set; }     //伤害类型(1:物理   2:魔法)
    [DataMember]
    public int Frequency { get; set; }     //本局使用次数
    [DataMember]
    public float Coefficient { get; set; }   //伤害系数

    public void Assign(Skill skill)
    {
        this.SkillName = skill.SkillName;
        this.Skilltype = skill.Skilltype;    
        this.Frequency = skill.Frequency;
        this.Coefficient = skill.Coefficient;
    }
}

[DataContract]
public class Pokemon
{
    [DataMember]
    public string PokemonType { get; set; }       //模型类型(string)
    [DataMember]
    public string Name { get; set; }              //名字
    [DataMember]
    public int Level { get; set; }
    [DataMember]
    public int Speed { get; set; }
    [DataMember]
    public int Aggerssivity { get; set; }         //物攻
    [DataMember]
    public int Magic { get; set; }                //法强
    [DataMember]
    public int Armor { get; set; }                //护甲
    [DataMember]
    public int Ressistance { get; set; }     //魔抗
    [DataMember]
    public List<Skill> Skills { get; set; }           //技能
    [DataMember]
    public bool IsCaptain { get; set; }        //队长
    [DataMember]
    public int CurrentHP;
    [DataMember]
    public int Health { get; set; }
    [DataMember]
    public int CurrentEX;
    [DataMember]
    public int EX;


    public void Assign(Pokemon pokemon)
    {
        this.PokemonType = pokemon.PokemonType;
        this.Name = pokemon.Name;
        this.Level = pokemon.Level;
        this.Health = pokemon.Health;
        this.CurrentHP = pokemon.CurrentHP;
        this.EX = pokemon.EX;
        this.CurrentEX = pokemon.CurrentEX;
        this.Speed = pokemon.Speed;
        this.Aggerssivity = pokemon.Aggerssivity;
        this.Magic = pokemon.Magic;
        this.Armor = pokemon.Armor;
        this.Ressistance = pokemon.Ressistance;
        this.IsCaptain = pokemon.IsCaptain;

        this.Skills = new List<Skill>();
        for (int i = 0; i < pokemon.Skills.Count ; i++)
        {
            this.Skills.Add(pokemon.Skills[i]);
        }


    }

}



public class PokemonModel : PokemonElement {

	//data
    
  
    public Pokemon thisPokemon;
    public Pokemon thisEnemy;
    public List<Pokemon> fightPokemons;
    public List<Pokemon> myPokemons;

    private List<Skill> SkillCharmander; //小火龙
    private List<Skill> SkillCharizard;  //喷火龙

    private List<Skill> SkillBulbasaur;  //妙蛙种子
    private List<Skill> SkillVenusaur;   //妙蛙花

    private List<Skill> SkillSquirtle;   //杰尼龟
    private List<Skill> SkillBlastoise;  //水箭龟

    private List<Skill> SkillPikachu;    //皮卡丘

    private List<Skill> SkillButterfree; //巴大蝴

    public int userFlag;
    public int enemyFlag;  //0代表正在或已经结束回合

    public int fightFlag;  //0代表用户操作

    private int [] _participate;
    private bool[] _upgradeFlag;

    private int learnSkillNum;

    public void SetSkillNum(int num)
    {
        this.learnSkillNum = num;
    }

    public string GetLearnSkill()
    {
       return this.GetTypeSkill(learnSkillNum);
    }

    public int GetSkillNum()
    {
        return this.learnSkillNum;
    }

    public void SetPar(int num)
    {
        this._participate[num] = 1;
    }

    public void SetUpgrade(int num)
    {
        this._upgradeFlag[num] = true;
    }

    public void ResetPar()
    {
        _participate = new int[3];
        for (int i = 0; i < 3; i++)
        {
            this._participate[i] = 0;
        }
    }

    public void ResetUpgrade()
    {
        _upgradeFlag = new bool[3];
        for (int i = 0; i < 3; i++)
        {
            this._upgradeFlag[i] = false;
        }
    }

    public int GetParNum()
    {
        return this._participate.Length;
    }

    public int GetUpgradeNum()
    {
        return this._upgradeFlag.Length;
    }

    public bool JudgePar(int num)
    {
        if (this._participate[num] == 1)
        {
            return true;
        }

        return false;
    }

    public bool JudgeUpgrade(int num)
    {
        return this._upgradeFlag[num];
    }

    //火系技能
    private Skill skillFlame = new Skill { SkillName = "Flame",Coefficient = 0.5f,Frequency = 10,Skilltype = 2};
    private Skill skillFireVortex = new Skill { SkillName = "FireVortex", Coefficient = 0.8f, Frequency = 8, Skilltype = 2 };
    private Skill skillSprayFire = new Skill { SkillName = "SprayFire", Coefficient = 1, Frequency = 5, Skilltype = 2 };
    private Skill skillBigFire = new Skill { SkillName = "BigFire", Coefficient = 1.5f, Frequency = 5, Skilltype = 2 };

    //水系技能
    private Skill skillBlister = new Skill { SkillName = "Blister", Coefficient = 0.5f, Frequency = 10, Skilltype = 2 };
    private Skill skillWaterGun = new Skill { SkillName = "WaterGun", Coefficient = 0.8f, Frequency = 8, Skilltype = 2 };
    private Skill skillWaterCannon = new Skill { SkillName = "WaterCannon", Coefficient = 1, Frequency = 5, Skilltype = 2 };
    private Skill skillSurffing = new Skill { SkillName = "Surffing", Coefficient = 1.5f, Frequency = 5, Skilltype = 2 };

    //草系技能
    private Skill skillRattanWhip= new Skill { SkillName = "RattanWhip", Coefficient = 0.5f, Frequency = 10, Skilltype = 2 };
    private Skill skillBulletSeed= new Skill { SkillName = "BulletSeed", Coefficient = 0.8f, Frequency = 8, Skilltype = 2 };
    private Skill skillSolarBeam = new Skill { SkillName = "SpraySolarBeam", Coefficient = 1, Frequency = 5, Skilltype = 2 };
    private Skill skillLeafStorm = new Skill { SkillName = "LeafStorm", Coefficient = 1.5f, Frequency = 5, Skilltype = 2 };

    //电系技能
    private Skill skillElectricShock = new Skill { SkillName = "ElectricShock", Coefficient = 0.5f, Frequency = 10, Skilltype = 2 };
    private Skill skillThunderbolt = new Skill { SkillName = "Thunderbolt", Coefficient = 0.8f, Frequency = 8, Skilltype = 2 };
    private Skill skillThunder = new Skill { SkillName = "Thunder", Coefficient = 1, Frequency = 5, Skilltype = 2 };
    private Skill skillElectromagneticWave = new Skill { SkillName = "ElectromagneticWave", Coefficient = 1.5f, Frequency = 5, Skilltype = 2 };


    public void InitSkills()
    {

        SkillCharmander = new List<Skill>(); 
        SkillCharizard  = new List<Skill>();

        SkillBulbasaur  = new List<Skill>();
        SkillVenusaur   = new List<Skill>();

        SkillSquirtle   = new List<Skill>();
        SkillBlastoise  = new List<Skill>();

        SkillPikachu    = new List<Skill>();

        SkillButterfree = new List<Skill>();


        SkillCharmander.Add(skillFlame);
        SkillCharmander.Add(skillFireVortex);
        SkillCharmander.Add(skillSprayFire);
        SkillCharmander.Add(skillBigFire);

        SkillSquirtle.Add(skillBlister);
        SkillSquirtle.Add(skillWaterGun);
        SkillSquirtle.Add(skillWaterCannon);
        SkillSquirtle.Add(skillSurffing);

        SkillBulbasaur.Add(skillRattanWhip);
        SkillBulbasaur.Add(skillBulletSeed);
        SkillBulbasaur.Add(skillSolarBeam);
        SkillBulbasaur.Add(skillLeafStorm);

        SkillPikachu.Add(skillElectricShock);
        SkillPikachu.Add(skillThunderbolt);
        SkillPikachu.Add(skillThunder);
        SkillPikachu.Add(skillElectromagneticWave);

    }


    public string GetTypeSkill(int num)
    {
        switch (thisPokemon.PokemonType)
        {
            case "Charmander":  return SkillCharmander[num].SkillName;
            case "Squirtle": return SkillSquirtle[num].SkillName;
            case "Bulbasaur": return SkillBulbasaur[num].SkillName;
            case "Pikachu": return SkillPikachu[num].SkillName;
        }
        return null;
    }

    public void DirectLearn(int num)
    {
        switch (this.thisPokemon.PokemonType)
        {
            case "Charmander":
                thisPokemon.Skills.Add(SkillCharmander[num]);
                break;
            case "Squirtle":
                thisPokemon.Skills.Add(SkillSquirtle[num]);
                break;
            case "Bulbasaur":
                thisPokemon.Skills.Add(SkillBulbasaur[num]);
                break;
            case "Pikachu":
                thisPokemon.Skills.Add(SkillPikachu[num]);
                break;
        }
    }

    public void ReplaceSkill(int num)
    {
        switch (this.thisPokemon.PokemonType)
        {
            case "Charmander":
                thisPokemon.Skills.ElementAt(num).Assign(SkillCharmander[num]);
                break;
            case "Squirtle":
                thisPokemon.Skills.ElementAt(num).Assign(SkillSquirtle[num]);
                break;
            case "Bulbasaur":
                thisPokemon.Skills.ElementAt(num).Assign(SkillBulbasaur[num]);
                break;
            case "Pikachu":
                thisPokemon.Skills.ElementAt(num).Assign(SkillPikachu[num]);
                break;
        }
    }

    public Skill skillHit = new Skill
    {
        SkillName = "Hit",
        Frequency = 20,
        Coefficient = 0.3f,
        Skilltype = 1
    };

    public Skill skillBite = new Skill
    {
        SkillName = "Bite",
        Frequency = 20,
        Coefficient = 0.4f,
        Skilltype = 1

    };

    public Skill GetRandomSkill(int pokemonType , int pokemonLevel)   //1 Fire  2 Water  3 Grass  4 Electric
    {
        Random rd = new Random();
        int skillNum = -1;
        if (pokemonType == 1)   //火系精灵
        {
            if (pokemonLevel < 15)
            {
                return skillFlame;
            }
            else if(pokemonLevel < 30)
            {
                skillNum = rd.Next(1, 3);
                switch (skillNum)
                {
                   case 1: return skillFlame;
                   case 2: return skillFireVortex;
                }
            }
            else
            {
                skillNum = rd.Next(1, 4);
                switch (skillNum)
                {
                    case 1: return skillFlame;
                    case 2: return skillFireVortex;
                    case 3: return skillSprayFire;

                }
            }
   
        }
        else if (pokemonType == 2)
        {
            if (pokemonLevel < 15)
            {
                return skillBlister;
            }
            else if (pokemonLevel < 30)
            {
                skillNum = rd.Next(1, 3);
                switch (skillNum)
                {
                    case 1: return skillBlister;
                    case 2: return skillWaterGun;
                }
            }
            else
            {
                skillNum = rd.Next(1, 4);
                switch (skillNum)
                {
                    case 1: return skillBlister;
                    case 2: return skillWaterGun;
                    case 3: return skillWaterCannon;

                }
            }

        }//pt = 2
        else if(pokemonType == 3)
        {
            if (pokemonLevel < 15)
            {
                return skillRattanWhip;
            }
            else if (pokemonLevel < 30)
            {
                skillNum = rd.Next(1, 3);
                switch (skillNum)
                {
                    case 1: return skillRattanWhip;
                    case 2: return skillBulletSeed;
                }
            }
            else
            {
                skillNum = rd.Next(1, 4);
                switch (skillNum)
                {
                    case 1: return skillRattanWhip;
                    case 2: return skillBulletSeed;
                    case 3: return skillSolarBeam;

                }
            }


        }// pt = 3
        else
        {
            if (pokemonLevel < 15)
            {
                return skillElectricShock;
            }
            else if (pokemonLevel < 30)
            {
                skillNum = rd.Next(1, 3);
                switch (skillNum)
                {
                    case 1: return skillElectricShock;
                    case 2: return skillThunderbolt;
                }
            }
            else
            {
                skillNum = rd.Next(1, 4);
                switch (skillNum)
                {
                    case 1: return skillElectricShock;
                    case 2: return skillThunderbolt;
                    case 3: return skillThunder;

                }
            }
        }

        return null;
    }

}
