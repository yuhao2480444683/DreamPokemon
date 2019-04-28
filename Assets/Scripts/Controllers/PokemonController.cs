using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class PokemonController : PokemonElement
{

    //初始化显示队长精灵
    public void InitMyPokemon()
    {
        app.detailMessageService.InitialData();
        app.detailMessageService.SetMyPokemonsFromFile();
        app.detailMessageService.CaptainToThisPokemon();
        app.controller.DisplayThisPokemon();
        app.view.collection.operateUI.gameObject.SetActive(true);
        app.view.collection.operateUI.startButton.gameObject.SetActive(false);
        app.view.collection.operateUI.startButtonText.gameObject.SetActive(false);
        app.view.collection.operateUI.refreshButton.gameObject.SetActive(true);
        app.view.collection.operateUI.refreshButtonText.gameObject.SetActive(true);
    }

    public void ResetWorld()
    {
        app.detailMessageService.CaptainToThisPokemon();
        app.controller.DisplayThisPokemon();
        app.view.collection.operateUI.gameObject.SetActive(true);

        app.view.collection.detailMessageUI.gameObject.SetActive(false);
        app.view.collection.enemyPokemon.gameObject.SetActive(false);
        app.view.collection.enemyMessageUI.gameObject.SetActive(false);
        app.view.collection.fightOperateUI.gameObject.SetActive(false);

        app.view.collection.operateUI.refreshButton.gameObject.SetActive(true);
        app.view.collection.operateUI.refreshButtonText.gameObject.SetActive(true);
        app.view.collection.operateUI.fightButton.gameObject.SetActive(false);
        app.view.collection.operateUI.fightButtonText.gameObject.SetActive(false);

        app.view.collection.fightMessageUI.gameObject.SetActive(false);
    }

    //点击查看精灵详情
    public void CheckMyPokemon()  
    {
        if (app.view.collection.detailMessageUI.gameObject.activeSelf)
        {
            if (app.fightService.JudgeFightState())
            {
                //战斗状态，仅关闭详细界面
                app.view.collection.detailMessageUI.gameObject.SetActive(false);
            }
            else
            {
                //非战斗状态，关闭详细界面，打开操作界面
                app.view.collection.detailMessageUI.gameObject.SetActive(false);
                app.view.collection.operateUI.gameObject.SetActive(true);
            }
            
        }
        else
        {
            UpdateMyDetailMessageUI();
            app.view.collection.detailMessageUI.gameObject.SetActive(true);

            if (app.fightService.JudgeFightState())
            {
                //处于战斗状态，不显示下一精灵、设置队长、学习技能按键
                app.view.collection.detailMessageUI.nextPokemonButton.gameObject.SetActive(false);
                app.view.collection.detailMessageUI.nextPokemonButtonText.gameObject.SetActive(false);
                app.view.collection.detailMessageUI.setCaptainButton.gameObject.SetActive(false);
                app.view.collection.detailMessageUI.setCaptainButtonText.gameObject.SetActive(false);
                app.view.collection.detailMessageUI.learnSkillButton.gameObject.SetActive(false);
                app.view.collection.detailMessageUI.learnSkillButtonText.gameObject.SetActive(false);

            }
            else
            {
                //非战斗状态
                //是否显示设置队长键
                if (app.detailMessageService.IsThisPokemonCaptain())
                {
                    app.view.collection.detailMessageUI.setCaptainButton.gameObject.SetActive(false);
                    app.view.collection.detailMessageUI.setCaptainButtonText.gameObject.SetActive(false);
                }
                else
                {
                    app.view.collection.detailMessageUI.setCaptainButton.gameObject.SetActive(true);
                    app.view.collection.detailMessageUI.setCaptainButtonText.gameObject.SetActive(true);
                }
                app.view.collection.detailMessageUI.learnSkillButton.gameObject.SetActive(true);
                app.view.collection.detailMessageUI.learnSkillButtonText.gameObject.SetActive(true);
                app.view.collection.detailMessageUI.nextPokemonButton.gameObject.SetActive(true);
                app.view.collection.detailMessageUI.nextPokemonButtonText.gameObject.SetActive(true);

                //关闭操作菜单
                app.view.collection.operateUI.gameObject.SetActive(false);
            }

           
        }

    }


    public void CheckEnemy()
    {
        if (app.view.collection.enemyMessageUI.gameObject.activeSelf)
        {
            app.view.collection.enemyMessageUI.gameObject.SetActive(false);
           
        }
        else
        {
            
            app.view.collection.enemyMessageUI.gameObject.SetActive(true);
            UpdateEnemyMessageUI();
        }
    }


    //将当前精灵设为队长精灵
    public void SetCaptain()
    {
        app.detailMessageService.SetCaptain();
        if (app.detailMessageService.IsThisPokemonCaptain())
        {
            app.view.collection.detailMessageUI.setCaptainButton.gameObject.SetActive(false);
            app.view.collection.detailMessageUI.setCaptainButtonText.gameObject.SetActive(false);
        }
        else
        {
            app.view.collection.detailMessageUI.setCaptainButton.gameObject.SetActive(true);
            app.view.collection.detailMessageUI.setCaptainButtonText.gameObject.SetActive(true);
        }
    }

    //显示下一个精灵
    public void DisplayNextPokemon()
    {
        app.detailMessageService.SetNextPokemon();

        if (app.detailMessageService.IsThisPokemonCaptain())
        {
            app.view.collection.detailMessageUI.setCaptainButton.gameObject.SetActive(false);
            app.view.collection.detailMessageUI.setCaptainButtonText.gameObject.SetActive(false);
        }
        else
        {
            app.view.collection.detailMessageUI.setCaptainButton.gameObject.SetActive(true);
            app.view.collection.detailMessageUI.setCaptainButtonText.gameObject.SetActive(true);
        }
        app.controller.DisplayThisPokemon();
        UpdateMyDetailMessageUI();

    }

    //显示当前精灵
    public void DisplayThisPokemon()
    {
        if (app.view.collection.myPokenmon != null)
        {
            Destroy(app.view.collection.myPokenmon.gameObject);
        }
        GameObject pokemonModel = Resources.Load(app.model.thisPokemon.PokemonType) as GameObject;
        var newPokemon = Instantiate(pokemonModel);
        newPokemon.transform.parent = app.view.collection.transform;
        newPokemon.AddComponent<MyPokemonView>();
        app.view.collection.myPokenmon = newPokemon.GetComponent<MyPokemonView>();
        app.view.collection.myPokenmon.gameObject.transform.localPosition = new Vector3(0, 0.5f, 0); 
    }

    public void DisplayThisEnemy()
    {
        if (app.view.collection.enemyPokemon != null)
        {
            Destroy(app.view.collection.enemyPokemon.gameObject);
        }
        GameObject pokemonModel = Resources.Load(app.model.thisEnemy.PokemonType) as GameObject;
        var newPokemon = Instantiate(pokemonModel);
        newPokemon.transform.parent = app.view.collection.transform;
        newPokemon.AddComponent<EnemyPokemonView>();
        app.view.collection.enemyPokemon = newPokemon.GetComponent<EnemyPokemonView>();
        app.view.collection.enemyPokemon.gameObject.transform.localPosition = new Vector3(0, 0.5f, 2.25f);
        app.view.collection.enemyPokemon.gameObject.transform.Rotate(new Vector3(0,180,0));
    }



    public void SetEnemy()
    {
        app.enemyService.SetRandomEnemy();
        app.controller.DisplayThisEnemy();
        if (app.view.collection.enemyPokemon.gameObject.activeSelf)
        {
            app.view.collection.operateUI.fightButton.gameObject.SetActive(true);
            app.view.collection.operateUI.fightButtonText.gameObject.SetActive(true);
        }

       this.UpdateEnemyMessageUI();


    }

    public void UpdateMyDetailMessageUI()
    {
        app.view.collection.detailMessageUI.displayTextUI.levelDisplayTextUI.gameObject.GetComponent<TextMesh>()
                    .text = app.detailMessageService.GetMyLevel().ToString();
        app.view.collection.detailMessageUI.displayTextUI.healthDisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = app.detailMessageService.GetMyMaxHP().ToString();
        app.view.collection.detailMessageUI.displayTextUI.nameDisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = app.detailMessageService.GetMyName();
        app.view.collection.detailMessageUI.displayTextUI.aggressivityDisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = app.detailMessageService.GetMyAggerssivity().ToString();
        app.view.collection.detailMessageUI.displayTextUI.magicDisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = app.detailMessageService.GetMyMagic().ToString();
        app.view.collection.detailMessageUI.displayTextUI.armorDisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = app.detailMessageService.GetMyArmor().ToString();
        app.view.collection.detailMessageUI.displayTextUI.resistanceDisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = app.detailMessageService.GetMyRessistance().ToString();
        app.view.collection.detailMessageUI.displayTextUI.speedDisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = app.detailMessageService.GetMySpeed().ToString();
        int count = app.fightService.GetMyPokemonSkillNum();
        if (count > 0)
        {
            app.view.collection.detailMessageUI.displayTextUI.skill1DisplayTextUI.gameObject.GetComponent<TextMesh>()
                    .text = app.fightService.GetMyPokemonSkillOneName();
            count--;

        }
        else
        {
            app.view.collection.detailMessageUI.displayTextUI.skill1DisplayTextUI.gameObject.GetComponent<TextMesh>()
                    .text = null;
        }

        if (count > 0)
        {
            app.view.collection.detailMessageUI.displayTextUI.skill2DisplayTextUI.gameObject.GetComponent<TextMesh>()
                    .text = app.fightService.GetMyPokemonSkillTwoName();
                count--;
        }
        else
        {
            app.view.collection.detailMessageUI.displayTextUI.skill2DisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = null;
        }

        if (count > 0)
        {
            app.view.collection.detailMessageUI.displayTextUI.skill3DisplayTextUI.gameObject.GetComponent<TextMesh>()
                    .text = app.fightService.GetMyPokemonSkillThreeName();
            count--;
        }
        else
        {
            app.view.collection.detailMessageUI.displayTextUI.skill3DisplayTextUI.gameObject.GetComponent<TextMesh>()
                    .text = null;
        }

        if (count > 0)
        {
            app.view.collection.detailMessageUI.displayTextUI.skill4DisplayTextUI.gameObject.GetComponent<TextMesh>()
                    .text = app.fightService.GetMyPokemonSkillFourName();
        }
        else
        {
            app.view.collection.detailMessageUI.displayTextUI.skill4DisplayTextUI.gameObject.GetComponent<TextMesh>()
                    .text = null;

        }


        
    }

    public void UpdateEnemyMessageUI()
    {
        if (app.view.collection.enemyMessageUI.gameObject.activeSelf)
        {

            app.view.collection.enemyMessageUI.enemyTextUI.levelDisplayTextUI.gameObject.GetComponent<TextMesh>()
                    .text = app.detailMessageService.GetEnemyLevel().ToString();
            app.view.collection.enemyMessageUI.enemyTextUI.healthDisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = app.detailMessageService.GetEnemyMaxHP().ToString();
            app.view.collection.enemyMessageUI.enemyTextUI.nameDisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = app.detailMessageService.GetEnemyName();
            app.view.collection.enemyMessageUI.enemyTextUI.aggressivityDisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = app.detailMessageService.GetEnemyAggerssivity().ToString();
            app.view.collection.enemyMessageUI.enemyTextUI.magicDisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = app.detailMessageService.GetEnemyMagic().ToString();
            app.view.collection.enemyMessageUI.enemyTextUI.armorDisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = app.detailMessageService.GetEnemyArmor().ToString();
            app.view.collection.enemyMessageUI.enemyTextUI.resistanceDisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = app.detailMessageService.GetEnemyRessistance().ToString();
            app.view.collection.enemyMessageUI.enemyTextUI.speedDisplayTextUI.gameObject.GetComponent<TextMesh>()
                .text = app.detailMessageService.GetEnemySpeed().ToString();
            int count = app.fightService.GetEnemySkillNum();
            if (count > 0)
            {
                app.view.collection.enemyMessageUI.enemyTextUI.skill1DisplayTextUI.gameObject.GetComponent<TextMesh>()
                    .text = app.fightService.GetEnemySkillOneName();
                count--;

            }

            if (count > 0)
            {
                app.view.collection.enemyMessageUI.enemyTextUI.skill2DisplayTextUI.gameObject.GetComponent<TextMesh>()
                    .text = app.fightService.GetEnemySkillTwoName();
                count--;
            }

            if (count > 0)
            {
                app.view.collection.enemyMessageUI.enemyTextUI.skill3DisplayTextUI.gameObject.GetComponent<TextMesh>()
                    .text = app.fightService.GetEnemySkillThreeName();
                count--;
            }

            if (count > 0)
            {
                app.view.collection.enemyMessageUI.enemyTextUI.skill4DisplayTextUI.gameObject.GetComponent<TextMesh>()
                    .text = app.fightService.GetEnemySkillFourName();
            }

        }
    }

    public void OnLearnSkillClick()
    {
        app.view.collection.detailMessageUI.gameObject.SetActive(false);
        
        SetLearnSkills();
        
        app.view.collection.learnSkillPage.gameObject.SetActive(true);
        

    }

    public void OnBackToBeginButtonClick()
    {
        app.view.collection.learnSkillPage.gameObject.SetActive(false);
        app.view.collection.operateUI.gameObject.SetActive(true);
    }

    public void SetLearnSkills()
    {
        //设置学习技能界面初始值
        app.view.collection.learnSkillPage.skillOneText.gameObject.GetComponent<TextMesh>().text =
            app.skillService.GetSkill(0);

        if (app.skillService.JudgeCanLearn(0))
        {          
            app.view.collection.learnSkillPage.skillOne.gameObject.GetComponent<MeshRenderer>().material.color =
                Color.white;
        }
        else
        {
            app.view.collection.learnSkillPage.skillOne.gameObject.GetComponent<MeshRenderer>().material.color =
                Color.red;
        }

        app.view.collection.learnSkillPage.skillTwoText.gameObject.GetComponent<TextMesh>().text =
            app.skillService.GetSkill(1);

        if (app.skillService.JudgeCanLearn(1))
        {
            app.view.collection.learnSkillPage.skillTwo.gameObject.GetComponent<MeshRenderer>().material.color =
                Color.white;
        }
        else
        {
            app.view.collection.learnSkillPage.skillTwo.gameObject.GetComponent<MeshRenderer>().material.color =
                Color.red;
        }

        app.view.collection.learnSkillPage.skillThreeText.gameObject.GetComponent<TextMesh>().text =
            app.skillService.GetSkill(2);

        if (app.skillService.JudgeCanLearn(2))
        {
            app.view.collection.learnSkillPage.skillThree.gameObject.GetComponent<MeshRenderer>().material.color =
                Color.white;
        }
        else
        {
            app.view.collection.learnSkillPage.skillThree.gameObject.GetComponent<MeshRenderer>().material.color =
                Color.red;
        }

        app.view.collection.learnSkillPage.skillFourText.gameObject.GetComponent<TextMesh>().text =
            app.skillService.GetSkill(3);
        if (app.skillService.JudgeCanLearn(3))
        {
            app.view.collection.learnSkillPage.skillFour.gameObject.GetComponent<MeshRenderer>().material.color =
                Color.white;
        }
        else
        {
            app.view.collection.learnSkillPage.skillFour.gameObject.GetComponent<MeshRenderer>().material.color =
                Color.red;
        }
  
    }

    public void LearnSkill(int num)
    {
        if (app.fightService.GetMyPokemonSkillNum() < 4)
        {
            if (!app.skillService.JudgeRepeatSkill(num))
            {
                //如果技能不重复(小于4个技能)，直接学习
                if (app.skillService.JudgeCanLearn(num))
                {
                    app.skillService.DirectLearn(num);
                    app.skillService.SaveThisPokemon();
                    app.detailMessageService.UpdatePokemonsToFile();
                    app.view.collection.learnSkillPage.gameObject.SetActive(false);
                    app.controller.UpdateMyDetailMessageUI();
                    app.view.collection.detailMessageUI.gameObject.SetActive(true);
                }
            } 
        }
        else
        {
            //已经学习了4个技能，选择替换
            if (!app.skillService.JudgeRepeatSkill(num))
            {
                //若点击的技能未学过
                if (app.skillService.JudgeCanLearn(num))
                {
                    app.skillService.SetLearnSkill(num);
                    app.view.collection.learnSkillPage.gameObject.SetActive(false);
                    //todo 显示skillFullPage，初始化
                    app.view.collection.skillFullPage.mySkillOneText.gameObject.GetComponent<TextMesh>().text =
                        app.fightService.GetMyPokemonSkillOneName();
                    app.view.collection.skillFullPage.mySkillTwoText.gameObject.GetComponent<TextMesh>().text =
                        app.fightService.GetMyPokemonSkillTwoName();
                    app.view.collection.skillFullPage.mySkillThreeText.gameObject.GetComponent<TextMesh>().text =
                        app.fightService.GetMyPokemonSkillThreeName();
                    app.view.collection.skillFullPage.mySkillFourText.gameObject.GetComponent<TextMesh>().text =
                        app.fightService.GetMyPokemonSkillFourName();
                    app.view.collection.skillFullPage.thisSkillText.gameObject.GetComponent<TextMesh>().text =
                        app.skillService.GetLearnSkill();
                    app.view.collection.skillFullPage.gameObject.SetActive(true);
                }

            }
           
        }
    }
    public void OnLearnSkillOneClick()
    {
        LearnSkill(0);
    }

    public void OnLearnSkillTwoClick()
    {
        LearnSkill(1);
    }

    public void OnLearnSkillThreeClick()
    {
        LearnSkill(2);
    }

    public void OnLearnSkillFourClick()
    {
        LearnSkill(3);
    }

    public void ReplaceSkillOne()
    {
        ReplaceSkill(0);
    }

    public void ReplaceSkillTwo()
    {
        ReplaceSkill(1);
    }

    public void ReplaceSkillThree()
    {
        ReplaceSkill(2);
    }

    public void ReplaceSkillFour()
    {
        ReplaceSkill(3);
    }

    public void ReplaceSkill(int num)
    {
        app.skillService.ReplaceSkill(num);
        app.skillService.SaveThisPokemon();
        app.detailMessageService.UpdatePokemonsToFile();
        app.view.collection.skillFullPage.gameObject.SetActive(false);
        this.UpdateMyDetailMessageUI();
    }

    public void NotLearnSkill()
    {
        app.view.collection.skillFullPage.gameObject.SetActive(false);
        SetLearnSkills();
        app.view.collection.learnSkillPage.gameObject.SetActive(true);
    }

}
