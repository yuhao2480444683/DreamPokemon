using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using UnityEngine;

public class SkillService : PokemonElement {

    public string GetSkill(int num)
    {
        return app.model.GetTypeSkill(num);
    }

    public void DirectLearn(int num)
    {
        app.model.DirectLearn(num);
    }

    public bool JudgeRepeatSkill(int num)
    {
        string learnSkillName = app.model.GetTypeSkill(num);
        foreach (var skill in app.model.thisPokemon.Skills)
        {
            if (skill.SkillName.Equals(learnSkillName))
            {
                return true;
            }
        }
        return false;
    }

    public void SetLearnSkill(int num)
    {
        app.model.SetSkillNum(num);
    }

    public string GetLearnSkill()
    {
       return app.model.GetLearnSkill();
    }

    public void ReplaceSkill(int num)
    {
        app.model.ReplaceSkill(num);
    }

    public void SaveThisPokemon()
    {
        for (int i = 0; i < app.model.myPokemons.Count; i++)
        {
            if (app.model.thisPokemon.PokemonType.Equals(app.model.myPokemons[i].PokemonType))
            {
                app.model.myPokemons[i].Assign(app.model.thisPokemon);
            }
        }
    }

    public bool JudgeCanLearn(int num)
    {
        switch (num)
        {
            case 0:
                if (app.detailMessageService.GetMyLevel() < 10)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case 1:
                if (app.detailMessageService.GetMyLevel() < 15)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case 2:
                if (app.detailMessageService.GetMyLevel() < 20)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case 3:
                if (app.detailMessageService.GetMyLevel() < 25)
                {
                    return false;
                }
                else
                {
                    return true;
                }
        }

        return false;
    }
}
