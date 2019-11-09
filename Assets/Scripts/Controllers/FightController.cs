using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class FightController : PokemonElement {


    public void OnFightDisplay()
    {

        //关闭详细界面（敌我双方）
        if (app.view.collection.detailMessageUI.gameObject.activeSelf)
        {
            app.view.collection.detailMessageUI.gameObject.SetActive(false);
        }

        if (app.view.collection.enemyMessageUI.gameObject.activeSelf)
        {
            app.view.collection.enemyMessageUI.gameObject.SetActive(false);
        }

        if (app.view.collection.operateUI.gameObject.activeSelf)
        {
            app.view.collection.operateUI.gameObject.SetActive(false);
        }

        if (!app.view.collection.fightOperateUI.gameObject.activeSelf)
        {
            app.view.collection.fightOperateUI.gameObject.SetActive(true);
            app.view.collection.fightOperateUI.operateButton.gameObject.SetActive(true);
        }

        //初始化我方战时数据
        app.fightService.InitMyPokemon();

        //战斗默认队长先上场
        app.detailMessageService.CaptainToThisPokemon();
        app.controller.DisplayThisPokemon();

        //todo 显示双方血条、经验条
        UpdateMyHPUI();
        UpdateMyEXUI();
        UpdateEnemyHPUI();
        app.view.collection.fightMessageUI.gameObject.SetActive(true);
        app.view.collection.fightMessageUI.myHPDisplayUI.gameObject.SetActive(true);
        app.view.collection.fightMessageUI.myEXDisplayUI.gameObject.SetActive(true);
        app.view.collection.fightMessageUI.enemyHPDisplayUI.gameObject.SetActive(true);
        app.fightService.SetParCaptain();
        app.fightService.SetRoundFlag("fight");
    }


    public void OnBattleButtonClick()
    {
        app.view.collection.fightOperateUI.operateButton.gameObject.SetActive(false);

        //显示当前精灵所有技能
        app.view.collection.fightOperateUI.fightPage.gameObject.SetActive(true);
        int skillNum = app.fightService.GetMyPokemonSkillNum();
        if (skillNum > 0)
        {
            app.view.collection.fightOperateUI.fightPage.skillOneButton.gameObject.SetActive(true);
            app.view.collection.fightOperateUI.fightPage.skillOneButtonText.gameObject.GetComponent<TextMesh>().text =
                app.fightService.GetMyPokemonSkillOneName();
            app.view.collection.fightOperateUI.fightPage.skillOneButtonText.gameObject.SetActive(true);
           skillNum--;
        }
        else
        {
            app.view.collection.fightOperateUI.fightPage.skillOneButton.gameObject.SetActive(false);
            app.view.collection.fightOperateUI.fightPage.skillOneButtonText.gameObject.SetActive(false);
        }
        if (skillNum > 0)
        {
            app.view.collection.fightOperateUI.fightPage.skillTwoButton.gameObject.SetActive(true);
            app.view.collection.fightOperateUI.fightPage.skillTwoButtonText.gameObject.GetComponent<TextMesh>().text =
                app.fightService.GetMyPokemonSkillTwoName();
            app.view.collection.fightOperateUI.fightPage.skillTwoButtonText.gameObject.SetActive(true);
            skillNum--;
        }
        else
        {
            app.view.collection.fightOperateUI.fightPage.skillTwoButton.gameObject.SetActive(false);
            app.view.collection.fightOperateUI.fightPage.skillTwoButtonText.gameObject.SetActive(false);
        }
        if (skillNum > 0)
        {
            app.view.collection.fightOperateUI.fightPage.skillThreeButton.gameObject.SetActive(true);
            app.view.collection.fightOperateUI.fightPage.skillThreeButtonText.gameObject.GetComponent<TextMesh>().text =
                app.fightService.GetMyPokemonSkillThreeName();
            app.view.collection.fightOperateUI.fightPage.skillThreeButtonText.gameObject.SetActive(true);
            skillNum--;
        }
        else
        {
            app.view.collection.fightOperateUI.fightPage.skillThreeButton.gameObject.SetActive(false);
            app.view.collection.fightOperateUI.fightPage.skillThreeButtonText.gameObject.SetActive(false);


        }
        if (skillNum > 0)
        {
            app.view.collection.fightOperateUI.fightPage.skillFourButton.gameObject.SetActive(true);
            app.view.collection.fightOperateUI.fightPage.skillFourButtonText.gameObject.GetComponent<TextMesh>().text =
                app.fightService.GetMyPokemonSkillFourName();
            app.view.collection.fightOperateUI.fightPage.skillFourButtonText.gameObject.SetActive(true);
            skillNum--;
        }
        else
        {
            app.view.collection.fightOperateUI.fightPage.skillFourButton.gameObject.SetActive(false);
            app.view.collection.fightOperateUI.fightPage.skillFourButtonText.gameObject.SetActive(false);

        }

    }

    public void OnCatchButtonClick()
    {
        app.view.collection.fightOperateUI.operateButton.gameObject.SetActive(false);

        //捕捉判断的特效显示
        app.view.collection.enemyPokemon.gameObject.SetActive(false);
        Invoke(null,3);

        if (app.fightService.JudgeCatchState())
        {
            //捕捉成功
           
            //判断是否有重名精灵
            if (app.fightService.JudgeNameRepeat())
            {
                //重名询问
                app.view.collection.fightOperateUI.catchNameRepeatPage.gameObject.SetActive(true);
               
            }
            else
            {
                //不重名
                if (app.fightService.JudgePokemonFull())
                {
                    //显示放生界面
                    app.view.collection.fightOperateUI.catchFullPage.gameObject.SetActive(true);
                    app.view.collection.fightOperateUI.catchFullPage.firstPokemonName.gameObject.GetComponent<TextMesh>().text =
                        app.fightService.GetPokemonName(0);
                    app.view.collection.fightOperateUI.catchFullPage.secondPokemonName.gameObject.GetComponent<TextMesh>().text =
                        app.fightService.GetPokemonName(1);
                    app.view.collection.fightOperateUI.catchFullPage.thirdPokemonName.gameObject.GetComponent<TextMesh>().text =
                        app.fightService.GetPokemonName(2);
                    app.view.collection.fightOperateUI.catchFullPage.catchPokemonName.gameObject.GetComponent<TextMesh>().text =
                        app.detailMessageService.GetEnemyName();
                }
                else
                {
                    //精灵未满
                  
                    app.fightService.DirectJoin();

                    app.fightService.CalculateAward();
                    
                    app.fightService.RecoveryHP();
                    app.fightService.SavePokemons();

                    app.fightService.SetRoundFlag("peace");
                    app.controller.ResetWorld();
                }
            }
        }
        else
        {
            //捕捉失败,敌方重现
            app.view.collection.enemyPokemon.gameObject.SetActive(true);
            if (app.fightService.JudgeRoundEnd())
            {
                app.fightService.NextRoundFlag();
                Invoke("OnFightStart", 2);
            }
            else
            {
                app.fightService.SetRoundFlag("enemy");
                Invoke("ComputerOperate", 2);
            }
        }

    }

    public void OnEscapeButtonClick()
    {
        app.view.collection.fightOperateUI.gameObject.SetActive(false);
        app.controller.ResetWorld();
        app.fightService.SetRoundFlag("peace");
    }

    public void OnChangeButtonClick()
    {
        app.view.collection.fightOperateUI.operateButton.gameObject.SetActive(false);

        this.DisplaySurvivalChangePage();
       
    }


    public void OnFightStart()
    {
        //初始化显示完毕，开始战斗

        if (app.fightService.CompareSpeed())
        {
            //用户行动
            Invoke("UserOperate", 2);
        }
        else
        {
            //电脑行动
            Invoke("ComputerOperate", 2);

        }
    }


    public void UserOperate()
    {
        app.fightService.SetFlag("user");
        //显示用户战斗界面
        app.view.collection.fightOperateUI.gameObject.SetActive(true);
        app.view.collection.fightOperateUI.operateButton.gameObject.SetActive(true);
        app.view.collection.fightOperateUI.changePage.gameObject.SetActive(false);
        app.view.collection.fightOperateUI.fightPage.gameObject.SetActive(false);
        app.view.collection.fightOperateUI.survivalChangePage.gameObject.SetActive(false);
        app.view.collection.fightOperateUI.catchFullPage.gameObject.SetActive(false);
        app.view.collection.fightOperateUI.catchNameRepeatPage.gameObject.SetActive(false);

    }

    public void ComputerOperate()
    {
        //选择一个技能攻击，或者几率逃跑
        app.fightService.SetFlag("enemy");

        //随机使用一个技能
        System.Random rd = new Random();
        int skillNum = rd.Next(0, app.fightService.GetEnemySkillNum());
        int damage = app.fightService.CalculateEnemyDamage(skillNum);
        if (app.fightService.OnDamageEnd(damage))
        {
            app.fightService.SaveThisPokemon();
            app.view.collection.myPokenmon.gameObject.SetActive(false);
            app.view.collection.fightMessageUI.myHPDisplayUI.gameObject.SetActive(false);
            app.view.collection.fightMessageUI.myEXDisplayUI.gameObject.SetActive(false);
            Invoke(null, 1);
            //我方当前精灵阵亡
            if (app.fightService.PokemonSurvivalNum() > 0)
            {
                
                //还有未出战精灵，给用户选择或逃跑选项

                app.fightService.SaveThisPokemon();
                this.DisplaySurvivalChangePage();
               
            }
            else
            {
                //我方精灵全部阵亡,结束战斗
                Invoke(null,2);
                app.controller.ResetWorld();
            }
        }
        else
        {
            //此回合结束,我方未阵亡
            app.fightService.SaveThisPokemon();

            Invoke("UpdateMyHPUI",2);
            
            if (app.fightService.JudgeRoundEnd())
            {
                app.fightService.NextRoundFlag();
                Invoke("OnFightStart",2);
            }
            else
            {
                app.fightService.SetRoundFlag("user");
                Invoke("UserOperate", 2);
            }

        }
       
    }

    public void OnDamageCalculate(int skillNum)
    {
        int damage = app.fightService.CalculateMyDamage(skillNum);
        if (app.fightService.OnDamageEnd(damage))
        {
            Invoke(null,1);


            app.fightService.CalculateAward();

            app.fightService.RecoveryHP();
            app.fightService.SavePokemons();

            app.fightService.SetRoundFlag("peace");
            app.controller.ResetWorld();
        }
        else
        {
            //此回合结束,敌方未阵亡
            Invoke("UpdateEnemyHPUI", 2);

            if (app.fightService.JudgeRoundEnd())
            {
                app.fightService.NextRoundFlag();
                Invoke("OnFightStart", 2);
            }
            else
            {
                app.fightService.SetRoundFlag("enemy");
                Invoke("ComputerOperate", 2);
            }

        }



    }


    public void UpdateMyHPUI()
    {
        //更新我方生命值条
        app.view.collection.fightMessageUI.myHPDisplayUI.currentHP.gameObject.GetComponent<TextMesh>().text =
            app.fightService.GetMyPokemonCurrentHP().ToString();
        app.view.collection.fightMessageUI.myHPDisplayUI.maxHP.gameObject.GetComponent<TextMesh>().text = app.fightService.GetMyPokemonMaxHP().ToString();
        app.view.collection.fightMessageUI.myHPDisplayUI.hpSlider.GetComponent<Slider>().minValue = 0;
        app.view.collection.fightMessageUI.myHPDisplayUI.hpSlider.GetComponent<Slider>().maxValue = app.fightService.GetMyPokemonMaxHP();
        app.view.collection.fightMessageUI.myHPDisplayUI.hpSlider.gameObject.GetComponent<Slider>().value = app.fightService.GetMyPokemonCurrentHP();
        //血量颜色
        if (app.fightService.JudgeMyHPState().Equals("full"))
        {
            app.view.collection.fightMessageUI.myHPDisplayUI.hpSlider.fillColor.gameObject.GetComponent<Image>().color = Color.green;
        }
        else if(app.fightService.JudgeMyHPState().Equals("half"))
        {
            app.view.collection.fightMessageUI.myHPDisplayUI.hpSlider.fillColor.gameObject.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            app.view.collection.fightMessageUI.myHPDisplayUI.hpSlider.fillColor.gameObject.GetComponent<Image>().color = Color.red;
        }

        
    }

    public void UpdateEnemyHPUI()
    {
        //更新敌方生命值条
        app.view.collection.fightMessageUI.enemyHPDisplayUI.currentHP.gameObject.GetComponent<TextMesh>().text =
            app.fightService.GetEnemyCurrentHP().ToString();
        app.view.collection.fightMessageUI.enemyHPDisplayUI.maxHP.gameObject.GetComponent<TextMesh>().text = app.fightService.GetEnemyMaxHP().ToString();
        app.view.collection.fightMessageUI.enemyHPDisplayUI.hpSlider.gameObject.GetComponent<Slider>().minValue = 0;
        app.view.collection.fightMessageUI.enemyHPDisplayUI.hpSlider.gameObject.GetComponent<Slider>().maxValue = app.fightService.GetEnemyMaxHP();
        app.view.collection.fightMessageUI.enemyHPDisplayUI.hpSlider.gameObject.GetComponent<Slider>().value = app.fightService.GetEnemyCurrentHP();
        if (app.fightService.JudgeEnemyHPState().Equals("full"))
        {
            app.view.collection.fightMessageUI.enemyHPDisplayUI.hpSlider.fillColor.gameObject.GetComponent<Image>().color = Color.green;
        }
        else if (app.fightService.JudgeEnemyHPState().Equals("half"))
        {
            app.view.collection.fightMessageUI.enemyHPDisplayUI.hpSlider.fillColor.gameObject.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            app.view.collection.fightMessageUI.enemyHPDisplayUI.hpSlider.fillColor.gameObject.GetComponent<Image>().color = Color.red;
        }
    }

    public void UpdateMyEXUI()
    {
       
        app.view.collection.fightMessageUI.myEXDisplayUI.currentEX.gameObject.GetComponent<TextMesh>().text =
            app.fightService.GetMyPokemonCurrentEX().ToString();
        app.view.collection.fightMessageUI.myEXDisplayUI.maxEX.gameObject.GetComponent<TextMesh>().text =
            app.fightService.GetMyPokemonMaxEX().ToString();

        //我方经验值条显示
        app.view.collection.fightMessageUI.myEXDisplayUI.exSlider.GetComponent<Slider>().minValue = 0;
        app.view.collection.fightMessageUI.myEXDisplayUI.exSlider.GetComponent<Slider>().maxValue = app.fightService.GetMyPokemonMaxEX();
        app.view.collection.fightMessageUI.myEXDisplayUI.exSlider.GetComponent<Slider>().value =
            app.fightService.GetMyPokemonCurrentEX();

    }


    public void OnSkillOneClick()
    {
        app.view.collection.fightOperateUI.fightPage.gameObject.SetActive(false);
        this.OnDamageCalculate(0);
    }

    public void OnSkillTwoClick()
    {
        app.view.collection.fightOperateUI.fightPage.gameObject.SetActive(false);
        this.OnDamageCalculate(1);
    }

    public void OnSkillThreeClick()
    {
        app.view.collection.fightOperateUI.fightPage.gameObject.SetActive(false);
        this.OnDamageCalculate(2);
    }

    public void OnSkillFourClick()
    {
        app.view.collection.fightOperateUI.fightPage.gameObject.SetActive(false);
        this.OnDamageCalculate(3);
    }

    public void PokeBallOneClick()
    {
        app.view.collection.fightOperateUI.changePage.gameObject.SetActive(false);
        if (app.fightService.JudgeSurvival(app.fightService.GetFirstPokemonName()))
        {
            //第一个精灵还存活，保存当前精灵血量，更换精灵
            app.fightService.SaveThisPokemon();
            app.fightService.SetThisPokemon(app.fightService.GetFirstPokemonName());
            app.controller.DisplayThisPokemon();
            app.controller.UpdateMyDetailMessageUI();
            
            //todo 重置我方显示条
            UpdateMyHPUI();
            UpdateMyEXUI();
            app.view.collection.fightMessageUI.myHPDisplayUI.gameObject.SetActive(true);
            app.view.collection.fightMessageUI.myEXDisplayUI.gameObject.SetActive(true);


            if (app.fightService.JudgeRoundEnd())
            {
                app.fightService.NextRoundFlag();
                Invoke("OnFightStart", 2);
            }
            else
            {
                app.fightService.SetRoundFlag("enemy");
                Invoke("ComputerOperate", 2);
            }
        }
        else
        {
            //第一个未存活
            Invoke(null,2);
            app.view.collection.fightOperateUI.changePage.gameObject.SetActive(true);
        }
    }

    public void PokeBallTwoClick()
    {
        app.view.collection.fightOperateUI.changePage.gameObject.SetActive(false);
        if (app.fightService.JudgeSurvival(app.fightService.GetSecondPokemonName()))
        {
            //第二个精灵还存活，保存当前精灵血量，更换精灵
            app.fightService.SaveThisPokemon();
            app.fightService.SetThisPokemon(app.fightService.GetSecondPokemonName());
            app.controller.DisplayThisPokemon();
            app.controller.UpdateMyDetailMessageUI();

            //todo 重置我方显示条
            UpdateMyHPUI();
            UpdateMyEXUI();
            app.view.collection.fightMessageUI.myHPDisplayUI.gameObject.SetActive(true);
            app.view.collection.fightMessageUI.myEXDisplayUI.gameObject.SetActive(true);

            if (app.fightService.JudgeRoundEnd())
            {
                app.fightService.NextRoundFlag();
                Invoke("OnFightStart", 2);
            }
            else
            {
                app.fightService.SetRoundFlag("enemy");
                Invoke("ComputerOperate", 2);
            }
        }
        else
        {
            //第二个未存活
            Invoke(null, 2);
            app.view.collection.fightOperateUI.changePage.gameObject.SetActive(true);
        }
    }



    

    public void ReplaceRepeatPokemon()
    {
        app.view.collection.fightOperateUI.catchNameRepeatPage.gameObject.SetActive(false);
        app.fightService.ReplaceRepeatPokemon();

        app.fightService.CalculateAward();

        app.fightService.RecoveryHP();
        app.fightService.SavePokemons();

        app.fightService.SetRoundFlag("peace");
        app.controller.ResetWorld();
    }

    public void NotReplaceRepeatPokemon()
    {
        app.view.collection.fightOperateUI.catchNameRepeatPage.gameObject.SetActive(false);

        app.fightService.CalculateAward();

        app.fightService.RecoveryHP();
        app.fightService.SavePokemons();

        app.fightService.SetRoundFlag("peace");
        app.controller.ResetWorld();
    }

    public void OnFirstReplace()
    {
        this.OnReplace(0);
    }

    public void OnSecondReplace()
    {
        this.OnReplace(1);
    }

    public void OnThirdReplace()
    {
        this.OnReplace(2);
    }

    public void OnReplace(int num)
    {
        app.view.collection.fightOperateUI.catchFullPage.gameObject.SetActive(false);
        app.fightService.ReplaceFullPokemon(num);
        app.fightService.CalculateAward();

        app.fightService.RecoveryHP();
        app.fightService.SavePokemons();

        app.fightService.SetRoundFlag("peace");
        app.controller.ResetWorld();
    }

    public void DisplaySurvivalChangePage()
    {
        app.view.collection.fightOperateUI.survivalChangePage.gameObject.SetActive(true);
        app.view.collection.fightOperateUI.survivalChangePage.firstPokemonSurvival.gameObject.SetActive(false);
        app.view.collection.fightOperateUI.survivalChangePage.secondPokemonSurvival.gameObject.SetActive(false);
        app.view.collection.fightOperateUI.survivalChangePage.thirdPokemonSurvival.gameObject.SetActive(false);
        app.view.collection.fightOperateUI.survivalChangePage.firstPokemonName.gameObject.SetActive(false);
        app.view.collection.fightOperateUI.survivalChangePage.secondPokemonName.gameObject.SetActive(false);
        app.view.collection.fightOperateUI.survivalChangePage.thirdPokemonName.gameObject.SetActive(false);
        int count = app.fightService.GetMyPokemonNum();
        if (count > 0)
        {
            app.view.collection.fightOperateUI.survivalChangePage.firstPokemonSurvival.gameObject.SetActive(true);
            app.view.collection.fightOperateUI.survivalChangePage.firstPokemonName.gameObject.SetActive(true);
            app.view.collection.fightOperateUI.survivalChangePage.firstPokemonName.gameObject.GetComponent<TextMesh>().text
                = app.fightService.GetPokemonName(0);
            if (app.fightService.JudgeSurvival(0))
            {
                app.view.collection.fightOperateUI.survivalChangePage.firstPokemonSurvival.gameObject
                    .GetComponent<MeshRenderer>().material.color = Color.white;
            }
            else
            {
                app.view.collection.fightOperateUI.survivalChangePage.firstPokemonSurvival.gameObject
                    .GetComponent<MeshRenderer>().material.color = Color.red;
            }

            count--;
        }

        if (count > 0)
        {
            app.view.collection.fightOperateUI.survivalChangePage.secondPokemonSurvival.gameObject.SetActive(true);
            app.view.collection.fightOperateUI.survivalChangePage.secondPokemonName.gameObject.SetActive(true);
            app.view.collection.fightOperateUI.survivalChangePage.secondPokemonName.gameObject.GetComponent<TextMesh>().text
                = app.fightService.GetPokemonName(1);
            if (app.fightService.JudgeSurvival(1))
            {
                app.view.collection.fightOperateUI.survivalChangePage.secondPokemonSurvival.gameObject
                    .GetComponent<MeshRenderer>().material.color = Color.white;
            }
            else
            {
                app.view.collection.fightOperateUI.survivalChangePage.secondPokemonSurvival.gameObject
                    .GetComponent<MeshRenderer>().material.color = Color.red;
            }

            count--;
        }

        if (count > 0)
        {
            app.view.collection.fightOperateUI.survivalChangePage.thirdPokemonSurvival.gameObject.SetActive(true);
            app.view.collection.fightOperateUI.survivalChangePage.thirdPokemonName.gameObject.SetActive(true);
            app.view.collection.fightOperateUI.survivalChangePage.thirdPokemonName.gameObject.GetComponent<TextMesh>().text
                = app.fightService.GetPokemonName(2);


            if (app.fightService.JudgeSurvival(2))
            {
                app.view.collection.fightOperateUI.survivalChangePage.thirdPokemonSurvival.gameObject
                    .GetComponent<MeshRenderer>().material.color = Color.white;
            }
            else
            {
                app.view.collection.fightOperateUI.survivalChangePage.thirdPokemonSurvival.gameObject
                    .GetComponent<MeshRenderer>().material.color = Color.red;
            }

        }
       
    }

    public void OnSurvivalChangeOneClick()
    {
       this.ReplaceSurvivalPokemon(0);
    }
    public void OnSurvivalChangeTwoClick()
    {
        this.ReplaceSurvivalPokemon(1);
    }
    public void OnSurvivalChangeThreeClick()
    {
        this.ReplaceSurvivalPokemon(2);
    }

    public void ReplaceSurvivalPokemon(int num)
    {
        if (app.fightService.JudgeRound().Equals("enemy"))
        {
            //被击杀被迫替换，下一回合应为用户操作
            if (app.fightService.JudgeSurvival(num))
            {
                //存活，可替换
                app.view.collection.fightOperateUI.survivalChangePage.gameObject.SetActive(false);

               
                app.fightService.SetThisPokemon(app.fightService.GetPokemonName(num));
                app.controller.DisplayThisPokemon();
                app.controller.UpdateMyDetailMessageUI();


                //todo 重置我方显示条
                UpdateMyHPUI();
                UpdateMyEXUI();
                app.view.collection.fightMessageUI.myHPDisplayUI.gameObject.SetActive(true);
                app.view.collection.fightMessageUI.myEXDisplayUI.gameObject.SetActive(true);

                app.fightService.SetPar(num);

                if (app.fightService.JudgeRoundEnd())
                {
                    app.fightService.NextRoundFlag();
                    Invoke("OnFightStart", 2);
                }
                else
                {
                    app.fightService.SetRoundFlag("user");
                    Invoke("UserOperate", 2);
                }

            }
        }

        if (app.fightService.JudgeRound().Equals("user"))
        {
            //主动更换精灵，下一回合应为敌方操作
            if (app.fightService.JudgeSurvival(num))
            {
                //存活，可替换
                if (!app.fightService.JudgeClickThisPokemon(num))
                {
                    //替换的不是当前精灵
                    app.view.collection.fightOperateUI.survivalChangePage.gameObject.SetActive(false);

                    app.fightService.SaveThisPokemon();
                    app.fightService.SetThisPokemon(app.fightService.GetPokemonName(num));
                    app.controller.DisplayThisPokemon();
                    app.controller.UpdateMyDetailMessageUI();


                    //todo 重置我方显示条
                    UpdateMyHPUI();
                    UpdateMyEXUI();
                    app.view.collection.fightMessageUI.myHPDisplayUI.gameObject.SetActive(true);
                    app.view.collection.fightMessageUI.myEXDisplayUI.gameObject.SetActive(true);

                    app.fightService.SetPar(num);
                    if (app.fightService.JudgeRoundEnd())
                    {
                        app.fightService.NextRoundFlag();
                        Invoke("OnFightStart", 2);
                    }
                    else
                    {
                        app.fightService.SetRoundFlag("enemy");
                        Invoke("ComputerOperate", 2);
                    }
                }
            }
        }
        
    }

    public void OnBackButtonClick()
    {
        app.view.collection.fightOperateUI.survivalChangePage.gameObject.SetActive(false);
        app.view.collection.fightOperateUI.fightPage.gameObject.SetActive(false);
        app.view.collection.fightOperateUI.operateButton.gameObject.SetActive(true);
    }

}
