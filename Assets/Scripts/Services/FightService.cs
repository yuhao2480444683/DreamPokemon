using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using UnityEngine;
using Random = System.Random;

public class FightService : PokemonElement {


    public bool CompareSpeed()
    {
        if (app.model.thisPokemon.Speed >= app.model.thisEnemy.Speed)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public int GetEnemySkillNum()
    {
        return app.model.thisEnemy.Skills.Count;
    }

    public int GetMyPokemonSkillNum()
    {
        return app.model.thisPokemon.Skills.Count;
    }

    public int GetMyPokemonNum()
    {
        return app.model.fightPokemons.Count;
    }

    public string GetFirstPokemonName()
    {
        for (int i = 0; i < app.model.fightPokemons.Count; i++)
        {

            if (app.model.thisPokemon.PokemonType.Equals(app.model.fightPokemons[i].PokemonType))
            {
                int num = (i + 1) % app.model.fightPokemons.Count;
                return app.model.fightPokemons[num].PokemonType;  
            }
        }
        return null;
    }

    public string GetSecondPokemonName()
    {
        if (app.model.fightPokemons.Count < 3)
        {
            return null;
        }
        for (int i = 0; i < app.model.fightPokemons.Count; i++)
        {
            if (app.model.thisPokemon.PokemonType.Equals(app.model.fightPokemons[i].PokemonType))
            {
                int num = (i + 2) % app.model.fightPokemons.Count;
                return app.model.fightPokemons[num].PokemonType;
            }
        }
        return null;
    }

    public string GetMyPokemonSkillOneName()
    {
        return app.model.thisPokemon.Skills[0].SkillName;
    }

    public string GetMyPokemonSkillTwoName()
    {
        return app.model.thisPokemon.Skills[1].SkillName;
    }

    public string GetMyPokemonSkillThreeName()
    {
        return app.model.thisPokemon.Skills[2].SkillName;
    }

    public string GetMyPokemonSkillFourName()
    {
        return app.model.thisPokemon.Skills[3].SkillName;
    }

    public string GetEnemySkillOneName()
    {
        return app.model.thisEnemy.Skills[0].SkillName;
    }

    public string GetEnemySkillTwoName()
    {
        return app.model.thisEnemy.Skills[1].SkillName;
    }

    public string GetEnemySkillThreeName()
    {
        return app.model.thisEnemy.Skills[2].SkillName;
    }

    public string GetEnemySkillFourName()
    {
        return app.model.thisEnemy.Skills[3].SkillName;
    }

    public void NextRoundFlag()
    {
        app.model.enemyFlag = 1;
        app.model.userFlag = 1;
    }

    public void InitMyPokemon()
    {
        //初始化战时数据
        app.model.fightPokemons = new List<Pokemon>();
        for (int i = 0; i < app.model.myPokemons.Count; i++)
        {
            app.model.fightPokemons.Add(app.model.myPokemons[i]);
            //回满血量
            app.model.fightPokemons[i].CurrentHP = app.model.fightPokemons[i].Health;
        }

        app.model.userFlag = 1;
        app.model.enemyFlag = 1;
        app.model.ResetPar();
        app.model.ResetUpgrade();
        
    }

    public void SetParCaptain()
    {
        for (int i = 0; i < app.model.fightPokemons.Count; i++)
        {
            if (app.model.fightPokemons[i].IsCaptain == true)
            {
                app.model.SetPar(i);
            }
        }
    }

    public void SetPar(int num)
    {
        app.model.SetPar(num);
    }

    public int PokemonSurvivalNum()
    {
        int count = 0;
        foreach (var pokemon in app.model.fightPokemons)
        {
            if (pokemon.CurrentHP > 0)
            {
                count++;
            }
        }
        return count;
    }

    public void UpdateThisPokemonHP()
    {
        foreach (Pokemon pokemon in app.model.fightPokemons)
        {
            if (pokemon.PokemonType.Equals(app.model.thisPokemon.PokemonType))
            {
                pokemon.CurrentHP = app.model.thisPokemon.CurrentHP;
            }
        }
        
    }

    public void SetFlag(string current)
    {
        if (current.Equals("user"))
        {
            app.model.userFlag = 0;
        }
        else
        {
            app.model.enemyFlag = 0;
        }
    }

    public int CalculateEnemyDamage(int skillNum)
    {
        if (app.model.thisEnemy.Skills[skillNum].Skilltype == 1)
        {
            int totalDamage = Mathf.FloorToInt( app.model.thisEnemy.Skills[skillNum].Coefficient * app.model.thisEnemy.Aggerssivity);
            int actualDamage = totalDamage - app.model.thisPokemon.Armor + app.model.thisEnemy.Level - app.model.thisPokemon.Level;
            if (actualDamage > 0)
            {
                return actualDamage;
            }
            return 0;

        }
        else
        {
            int totalDamage = Mathf.FloorToInt(app.model.thisEnemy.Skills[skillNum].Coefficient * app.model.thisEnemy.Magic);
            int actualDamage = totalDamage - app.model.thisPokemon.Ressistance + app.model.thisEnemy.Level - app.model.thisPokemon.Level;
            if (actualDamage > 0)
            {
                return actualDamage;
            }
            return 0;
        }
    }

    public int CalculateMyDamage(int skillNum)
    {
        if (app.model.thisPokemon.Skills[skillNum].Skilltype == 1)
        {
            int totalDamage = Mathf.FloorToInt(app.model.thisPokemon.Skills[skillNum].Coefficient * app.model.thisPokemon.Aggerssivity);
            int actualDamage = totalDamage - app.model.thisEnemy.Armor + app.model.thisPokemon.Level - app.model.thisEnemy.Level;
            if (actualDamage > 0)
            {
                return actualDamage;
            }
            return 0;

        }
        else
        {
            int totalDamage = Mathf.FloorToInt(app.model.thisPokemon.Skills[skillNum].Coefficient * app.model.thisPokemon.Magic);
            int actualDamage = totalDamage - app.model.thisEnemy.Ressistance + app.model.thisPokemon.Level - app.model.thisEnemy.Level;
            if (actualDamage > 0)
            {
                return actualDamage;
            }
            return 0;
        }
    }

    public bool OnDamageEnd(int damage)
    {
        //伤害结算
        if (app.model.userFlag == 0 && app.model.enemyFlag == 1)
        {
            //用户操作
            if (app.model.thisEnemy.CurrentHP > damage)
            {
                app.model.thisEnemy.CurrentHP -= damage;

                return false;
            }
            else
            {
                app.model.thisEnemy.CurrentHP = 0;
                return true;
            }


        }
        else if(app.model.userFlag == 1 && app.model.enemyFlag == 0)
        {
            //敌方操作
            if (app.model.thisPokemon.CurrentHP > damage)
            {
                app.model.thisPokemon.CurrentHP -= damage;
                return false;
            }
            else
            {
                app.model.thisPokemon.CurrentHP = 0;
                return true;
            }

        }
        else
        {
            //用户操作
            if (app.model.fightFlag == 0)
            {
                if (app.model.thisEnemy.CurrentHP > damage)
                {
                    app.model.thisEnemy.CurrentHP -= damage;
                    return false;
                }
                else
                {
                    app.model.thisEnemy.CurrentHP = 0;
                    return true;
                }


            }
            else
            {
                //敌方操作
                if (app.model.thisPokemon.CurrentHP > damage)
                {
                    app.model.thisPokemon.CurrentHP -= damage;
                    return false;
                }
                else
                {
                    app.model.thisPokemon.CurrentHP = 0;
                    return true;
                }
            }

        }



    }

    public int GetMyPokemonCurrentHP()
    {
        return app.model.thisPokemon.CurrentHP;
    }

    public int GetMyPokemonMaxHP()
    {
        return app.model.thisPokemon.Health;
    }

    public int GetEnemyMaxHP()
    {
        return app.model.thisEnemy.Health;
    }

    public int GetEnemyMaxEX()
    {
        return app.model.thisEnemy.EX;
    }

    public int GetMyPokemonCurrentEX()
    {
        return app.model.thisPokemon.CurrentEX;
    }

    public int GetMyPokemonMaxEX()
    {
        return app.model.thisPokemon.EX;
    }

    public int GetEnemyCurrentHP()
    {
        return app.model.thisEnemy.CurrentHP;
    }

    public bool JudgeRoundEnd()
    {
        if (app.model.userFlag == 0 && app.model.enemyFlag == 0)
        {
            return true;
        }
        return false;
    }

    public string JudgeMyHPState()
    {
        if (app.model.thisPokemon.CurrentHP >= (app.model.thisPokemon.Health / 2))
        {
            return "full";
        }
        else if(app.model.thisPokemon.CurrentHP >= (app.model.thisPokemon.Health / 4))
        {
            return "half";
        }
        else
        {
            return "weak";
        }
    }

    public string JudgeEnemyHPState()
    {
        if (app.model.thisEnemy.CurrentHP >= (app.model.thisEnemy.Health / 2))
        {
            return "full";
        }
        else if (app.model.thisEnemy.CurrentHP >= (app.model.thisEnemy.Health / 4))
        {
            return "half";
        }
        else
        {
            return "weak";
        }
    }

    public bool JudgeSurvival(string pokeName)
    {
        for (int i = 0; i < app.model.fightPokemons.Count; i++)
        {
            if (pokeName.Equals(app.model.fightPokemons[i].PokemonType))
            {
                if (app.model.fightPokemons[i].CurrentHP == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void SaveThisPokemon()
    {
        for (int i = 0; i < app.model.fightPokemons.Count; i++)
        {
            if (app.model.thisPokemon.PokemonType.Equals(app.model.fightPokemons[i].PokemonType))
            {
                app.model.fightPokemons[i].Assign(app.model.thisPokemon);
            }
        }
    }

    public void SetThisPokemon(string pokemonName)
    {
        for (int i = 0; i < app.model.fightPokemons.Count; i++)
        {
            if (pokemonName.Equals(app.model.fightPokemons[i].PokemonType))
            {
                app.model.thisPokemon.Assign(app.model.fightPokemons[i]);
            }
        }
    }

    public void SetRoundFlag(string current)
    {
        if (current.Equals("user"))
        {
            app.model.fightFlag = 0;
        }
        else if(current.Equals("enemy"))
        {
            app.model.fightFlag = 1;
        }
        else if(current.Equals("peace"))
        {
            app.model.fightFlag = -1;
        }
        else
        {
            app.model.fightFlag = 2;
        }
    }

    public bool JudgeFightState()
    {
        if (app.model.fightFlag == -1)
        {
            return false;
        }

        return true;
    }

    public bool JudgeCatchState()
    {
        Random rd = new Random();
        if (app.model.thisEnemy.CurrentHP >= (app.model.thisEnemy.Health / 2))
        {
            int catchProbability = rd.Next(1, 101);
            if (catchProbability < 10)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (app.model.thisEnemy.CurrentHP >= (app.model.thisEnemy.Health / 4))
        {
            int catchProbability = rd.Next(1, 101);
            if (catchProbability < 30)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            int catchProbability = rd.Next(1, 101);
            if (catchProbability < 60)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

    }

    public bool JudgeNameRepeat()
    {
        foreach (var pokemon in app.model.fightPokemons)
        {
            if (pokemon.PokemonType.Equals(app.model.thisEnemy.PokemonType))
            {
                return true;
            }
        }
        return false;
    }

    public bool JudgePokemonFull()
    {
        if (app.model.fightPokemons.Count > 3)
        {
            return true;
        }
        return false;
    }

    public void DirectJoin()
    {
        app.model.fightPokemons.Add(app.model.thisEnemy);
    }

    public void RecoveryHP()
    {
        foreach (var pokemon in app.model.fightPokemons)
        {
            pokemon.CurrentHP = pokemon.Health;
        }
    }

    public void SavePokemons()
    {
        //战时精灵保存入文件，并重读数据
        DataContractJsonSerializer serializer =
            new DataContractJsonSerializer(typeof(List<Pokemon>));
        MemoryStream inStream = new MemoryStream();
        serializer.WriteObject(inStream, app.model.fightPokemons);
        inStream.Position = 0;
        StreamReader sr = new StreamReader(inStream, Encoding.UTF8);
        string injson = sr.ReadToEnd();
        PlayerPrefs.SetString("myPokemons", injson);

        //重新读取数据
        app.detailMessageService.SetMyPokemonsFromFile();
    }


    public void ReplaceRepeatPokemon()
    {
  
        for (int i = 0; i < app.model.fightPokemons.Count; i++)
        {
            if (app.model.thisEnemy.PokemonType.Equals(app.model.fightPokemons[i].PokemonType))
            {
                if (app.model.fightPokemons[i].IsCaptain == true)
                {
                    //若替换队长精灵，则设置为队长
                    app.model.thisEnemy.IsCaptain = true;

                }
                app.model.fightPokemons[i].Assign(app.model.thisEnemy);
            }
        }

    }

    public string GetPokemonName(int num)
    {
        return app.model.fightPokemons[num].PokemonType;
    }

   

    public void ReplaceFullPokemon(int replaceNum)
    {
        if (app.model.fightPokemons[replaceNum].IsCaptain == true)
        {
            app.model.thisEnemy.IsCaptain = true;
        }
        app.model.fightPokemons[replaceNum].Assign(app.model.thisEnemy);
    }

    public bool JudgeSurvival(int num)
    {
        if (app.model.fightPokemons[num].CurrentHP == 0)
        {
            return false;
        }
        return true;
    }

    public string JudgeRound()
    {
        if (app.model.userFlag == 0 && app.model.enemyFlag == 1)
        {
            return "user";
        }
        else if(app.model.userFlag == 1 && app.model.enemyFlag == 0)
        {
            return "enemy";
        }
        else
        {
            if (app.model.fightFlag == 0)
            {
                return "user";
            }

            return "enemy";
        }
    }


    public bool JudgeClickThisPokemon(int num)
    {
        if (app.model.fightPokemons[num].PokemonType.Equals(app.model.thisPokemon.PokemonType))
        {
            return true;
        }

        return false;
    }

    public void CalculateAward()
    {

        int parNum =  app.model.GetParNum();
        int exAverage = app.model.thisEnemy.EX / parNum;
        Random rd = new Random();
        for (int i = 0; i < app.model.fightPokemons.Count; i++)
        {
            if (app.model.JudgePar(i))
            {
                //第i个精灵参与过战斗
                int currentEX = exAverage;
                while (currentEX > 0)
                {
                    //经验结算
                    if (currentEX >= (app.model.fightPokemons[i].EX - app.model.fightPokemons[i].CurrentEX))
                    {
                        //经验溢出,升级
                        app.model.fightPokemons[i].Level += 1;
                        app.model.SetUpgrade(i);
                        app.model.fightPokemons[i].Health += rd.Next(10, 21);
                        app.model.fightPokemons[i].EX += rd.Next(10,21);
                        app.model.fightPokemons[i].Speed += rd.Next(2, 6);
                        app.model.fightPokemons[i].Aggerssivity += rd.Next(2, 10);
                        app.model.fightPokemons[i].Magic += rd.Next(2, 10);
                        app.model.fightPokemons[i].Armor += rd.Next(2, 10);
                        app.model.fightPokemons[i].Ressistance += rd.Next(2, 10);

                        currentEX -= app.model.fightPokemons[i].EX - app.model.fightPokemons[i].CurrentEX;
                        app.model.fightPokemons[i].CurrentEX = 0;

                    }
                    else
                    {
                        app.model.fightPokemons[i].CurrentEX = currentEX;
                        currentEX = 0;
                    }
                }

               
            }
        }
        
    }

    public string LearnSkill(int num)
    {
        string learnSkill =  null;
        if (app.model.JudgeUpgrade(num) == true)
        {
            for (int i = app.model.myPokemons[num].Level; i <= app.model.fightPokemons[num].Level; i++)
            {
                switch (i)
                {
                    case 11:
                        if (app.model.fightPokemons[num].PokemonType.Equals("TestWater"))
                        {
                            learnSkill = "Blister";
                        }
                        if (app.model.fightPokemons[num].PokemonType.Equals("TestFire"))
                        {
                            learnSkill = "Flame";
                        }
                        if (app.model.fightPokemons[num].PokemonType.Equals("Butterfree"))
                        {
                            learnSkill = "RattanWhip";
                        }
                        break;
                    case 20:
                        if (app.model.fightPokemons[num].PokemonType.Equals("TestWater"))
                        {
                            learnSkill = "WaterGun";
                        }
                        if (app.model.fightPokemons[num].PokemonType.Equals("TestFire"))
                        {
                            learnSkill = "FireVortex";
                        }
                        if (app.model.fightPokemons[num].PokemonType.Equals("Butterfree"))
                        {
                            learnSkill = "BulletSeed";
                        }
                        break;
                    case 25:
                        if (app.model.fightPokemons[num].PokemonType.Equals("TestWater"))
                        {
                            learnSkill = "WaterCannon";
                        }
                        if (app.model.fightPokemons[num].PokemonType.Equals("TestFire"))
                        {
                            learnSkill = "SprayFire";
                        }
                        if (app.model.fightPokemons[num].PokemonType.Equals("Butterfree"))
                        {
                            learnSkill = "SpraySolarBeam";
                        }
                        break;
                }
            }
        }
        return learnSkill;
    }



}
