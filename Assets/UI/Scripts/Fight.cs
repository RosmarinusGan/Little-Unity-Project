using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fight : MonoBehaviour
{
    //玩家手势
    public Image[] playerGuesture = new Image[3];
    private int[] guestureNum = new int[3];

    //随机元素
    public Image[] randomElement = new Image[4];

    //玩家元素
    public Image[] playerElement = new Image[3];
    private int[] elementNum = new int[3];

    //下拉选择手势
    public Dropdown[] dropGuesture = new Dropdown[3];
    
    //手势和元素贴图
    public Sprite[] RPS = new Sprite[3];
    public Sprite[] Element = new Sprite[3];

    //回合对战
    public Image runPlayer;
    private int runNum;
    public Image runPlayerElement;
    private int runEleNum;

    public Image runEnemy;
    private int enemyNum;
    public Image runEnemyElement;
    private int enemyEleNum;

    //回合参数和状态
    public Text Round;
    public Text RoundRound;

    private int roundNum;
    private int roundroundNum;
    private bool isPrepare = false;
    private bool isRoundStart = false;
    private bool isRoundOver = false;

    //元素状态
    private bool isFire;
    private bool isWater;
    private bool isIce;

    //玩家和敌人当前的元素状态
    public Image elementState;
    public Image enemyElementState;

    private int playerState;
    private int enemyState;

    //血量显示与伤害显示
    public const float playerHealth = 100f;
    private float playerCurrentHealth;

    public const float enemyHealth = 100f;
    private float enemyCurrentHealth;
        //血条
    public Image playerBlood;
    public Image enemyBlood;
        //元素伤害种类显示
    public Text playerElementHurt;
    public Text enemyElementHurt;
        //血量显示
    public Text playerBloodVisable;
    public Text enemyBloodVisable;
        //伤害数字显示
    public Text playerHurtNum;
    public Text enemyHurtNum;

    //退出界面
    public static bool isExit;

    // Start is called before the first frame update
    void Start()
    {
        isRoundOver = true;
        isPrepare = true;
        isExit = false;
        roundNum = 0;
        roundroundNum = 1;
        playerCurrentHealth = playerHealth;
        enemyCurrentHealth = enemyHealth;

        playerBloodVisable.text = playerCurrentHealth + " / " + playerHealth;
        enemyBloodVisable.text = enemyCurrentHealth + " / " + enemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isPrepare)
        {
            DropGuesture();
        }
    
        if (isRoundOver)
        {
            ClearRound();
        }
        Round.text = new string("Round " + roundNum);

        isGameExit();
        exitGame();
    }
    

    //下拉选手势
    private void DropGuesture()
    {
        for(int i = 0; i < 3; i++)
        {
            if(dropGuesture[i].value == 0)
            {
                playerGuesture[i].sprite = RPS[0];
            }

            if (dropGuesture[i].value == 1)
            {
                playerGuesture[i].sprite = RPS[1];
            }

            if (dropGuesture[i].value == 2)
            {
                playerGuesture[i].sprite = RPS[2];
            }
        }
    }

    //随机元素
    private void RandomElement()
    {
        foreach (Image i in randomElement)
        {
            i.sprite = Element[Random.Range(0, 3)];
        }
    }

    //敌人
    private void EnemyAI()
    {
        runEnemy.sprite = RPS[Random.Range(0, 3)];
        if (runEnemy.sprite.name == "Rock")
        {
            enemyNum = 1;
        }

        if (runEnemy.sprite.name == "Scissors")
        {
            enemyNum = 2;
        }

        if (runEnemy.sprite.name == "Paper")
        {
            enemyNum = 3;
        }

        runEnemyElement.sprite = Element[Random.Range(0, 3)];
        if (runEnemyElement.sprite.name == "火")
        {
            enemyEleNum = 1;
        }

        if (runEnemyElement.sprite.name == "水2")
        {
            enemyEleNum = 2;
        }

        if (runEnemyElement.sprite.name == "冰")
        {
            enemyEleNum = 3;
        }
    }

    //判断玩家是否赢了
    private int isPlayerWin(int player, int enemy)
    {
        if(player == enemy)
        {
            return 0;
        }

        if ((player == 1 && enemy == 2) || (player == 2 && enemy == 3) || (player == 3 && enemy == 1))
        {
            return 1;
        }
        else return -1;

    }

    //攻击伤害判断与进行,血量扣除
    private void AttackJudge()
    {
        int isWin = isPlayerWin(runNum, enemyNum);
        if (isWin == 1)
        {
            //玩家赢了，给敌方施加元素。暂定头像下设置一个小框显示元素
            //进行元素判断，造成伤害。（需要考虑一下怎么做Text来显示)
            //如果某一方血量为0需要退出游戏ExitGame（）
            Debug.Log("player win");
            if (enemyState == 0)
            {
                enemyState = runEleNum;
                enemyElementState.sprite = runPlayerElement.sprite;
                enemyCurrentHealth = enemyCurrentHealth > 0 ? enemyCurrentHealth - 5 : 0;
                enemyBlood.fillAmount = enemyCurrentHealth / enemyHealth;
                enemyBloodVisable.text = enemyCurrentHealth + " / " + enemyHealth;

                enemyHurtNum.text = "- 5";
                StartCoroutine(TextGradient(enemyHurtNum));
            }
            else
            {
                if(runEleNum == 0)
                {
                    enemyCurrentHealth = enemyCurrentHealth > 0 ? enemyCurrentHealth - 5 : 0;
                    enemyBlood.fillAmount = enemyCurrentHealth / enemyHealth;
                    enemyBloodVisable.text = enemyCurrentHealth + " / " + enemyHealth;

                    enemyHurtNum.text = "- 5";
                    StartCoroutine(TextGradient(enemyHurtNum));
                }

                if ((enemyState == 1 && runEleNum == 2) || (enemyState == 2 && runEleNum == 1))
                {
                    enemyState = 0;
                    enemyElementState.sprite = null;

                    enemyCurrentHealth = enemyCurrentHealth > 0 ? enemyCurrentHealth - 15 : 0;
                    enemyBlood.fillAmount = enemyCurrentHealth / enemyHealth;
                    enemyBloodVisable.text = enemyCurrentHealth + " / " + enemyHealth; 

                    enemyHurtNum.text = "- 15";
                    enemyElementHurt.text = "触发蒸发反应";
                    StartCoroutine(TextGradient(enemyElementHurt));
                    StartCoroutine(TextGradient(enemyHurtNum));
                }

                if ((enemyState == 1 && runEleNum == 3) || (enemyState == 3 && runEleNum == 1))
                {
                    enemyState = 0;
                    enemyElementState.sprite = null;

                    enemyCurrentHealth = enemyCurrentHealth > 0 ? enemyCurrentHealth - 20 : 0;
                    enemyBlood.fillAmount = enemyCurrentHealth / enemyHealth;
                    enemyBloodVisable.text = enemyCurrentHealth + " / " + enemyHealth;

                    enemyHurtNum.text = "- 20";
                    enemyElementHurt.text = "触发融化反应";
                    StartCoroutine(TextGradient(enemyElementHurt));
                    StartCoroutine(TextGradient(enemyHurtNum));
                }

                if ((enemyState == 2 && runEleNum == 3) || (enemyState == 3 && runEleNum == 2))
                {
                    enemyState = 0;
                    enemyElementState.sprite = null;

                    enemyCurrentHealth = enemyCurrentHealth > 0 ? enemyCurrentHealth - 10 : 0;
                    enemyBlood.fillAmount = enemyCurrentHealth / enemyHealth;
                    enemyBloodVisable.text = enemyCurrentHealth + " / " + enemyHealth;

                    enemyHurtNum.text = "- 10";
                    enemyElementHurt.text = "触发冻结反应";
                    StartCoroutine(TextGradient(enemyElementHurt));
                    StartCoroutine(TextGradient(enemyHurtNum));
                }
            }
        }
        //玩家输了
        else if (isWin == -1)
        {
            Debug.Log("player lose");
            if (playerState == 0)
            {
                playerState = enemyEleNum;
                elementState.sprite = runEnemyElement.sprite;
                playerCurrentHealth = playerCurrentHealth > 0 ? playerCurrentHealth - 5 : 0;
                playerBlood.fillAmount = playerCurrentHealth / playerHealth;
                playerBloodVisable.text = playerCurrentHealth + " / " + playerHealth;

                playerHurtNum.text = "- 5";
                StartCoroutine(TextGradient(playerHurtNum));
            }
            else
            {
                if(enemyEleNum == 0)
                {
                    playerCurrentHealth = playerCurrentHealth > 0 ? playerCurrentHealth - 5 : 0;
                    playerBlood.fillAmount = playerCurrentHealth / playerHealth;
                    playerBloodVisable.text = playerCurrentHealth + " / " + playerHealth;

                    playerHurtNum.text = "- 5";
                    StartCoroutine(TextGradient(playerHurtNum));
                }

                if ((playerState == 1 && enemyEleNum == 2) || (playerState == 2 && enemyEleNum == 1))
                {
                    playerState = 0;
                    elementState.sprite = null;

                    playerCurrentHealth = playerCurrentHealth > 0 ? playerCurrentHealth - 15 : 0;
                    playerBlood.fillAmount = playerCurrentHealth / playerHealth;
                    playerBloodVisable.text = playerCurrentHealth + " / " + playerHealth;

                    playerHurtNum.text = "- 15";
                    playerElementHurt.text = "触发蒸发反应";
                    StartCoroutine(TextGradient(playerElementHurt));
                    StartCoroutine(TextGradient(playerHurtNum));
                }

                if ((playerState == 1 && enemyEleNum == 3) || (playerState == 3 && enemyEleNum == 1))
                {
                    playerState = 0;
                    elementState.sprite = null;

                    playerCurrentHealth = playerCurrentHealth > 0 ? playerCurrentHealth - 20 : 0;
                    playerBlood.fillAmount = playerCurrentHealth / playerHealth;
                    playerBloodVisable.text = playerCurrentHealth + " / " + playerHealth;

                    playerHurtNum.text = "- 20";
                    playerElementHurt.text = "触发融化反应";
                    StartCoroutine(TextGradient(playerElementHurt));
                    StartCoroutine(TextGradient(playerHurtNum));
                }

                if ((playerState == 2 && enemyEleNum == 3) || (playerState == 3 && enemyEleNum == 2))
                {
                    playerState = 0;
                    elementState.sprite = null;

                    playerCurrentHealth = playerCurrentHealth > 0 ? playerCurrentHealth - 10 : 0;
                    playerBlood.fillAmount = playerCurrentHealth / playerHealth;
                    playerBloodVisable.text = playerCurrentHealth + " / " + playerHealth;

                    playerHurtNum.text = "- 10";
                    playerElementHurt.text = "触发冻结反应";
                    StartCoroutine(TextGradient(playerElementHurt));
                    StartCoroutine(TextGradient(playerHurtNum));
                }
            }
        }
        //平局
        else
        {
            Debug.Log("equal");
        }
        //待写。想给血条做个伤害特效，或者其他方式把伤害表现出来，血条旁边减数字。可以考虑加入模型动画，另外还有HP值的显示
        Debug.Log("受到伤害xx点");

       
    }

    //Text渐变消失
    IEnumerator TextGradient(Text text)
    {
        while (text.color.a != 0)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a > 0 ? (text.color.a * 255 - 10f) / 255f : 0);
            yield return new WaitForSeconds(0.1f);
        }

        if (text.color.a == 0)
        {
            text.text = null;
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        }

    }

    //回合结束后初始化
    private void ClearRound()
    {
        roundNum++;
        roundroundNum = 1;
        RandomElement();
        isPrepare = true;
        isRoundOver = false;

        for(int i = 0; i < 3; i++)
        {
            playerElement[i].sprite = null;
            elementNum[i] = 0;
        }

        Debug.Log("Round over");
    }

    private void judgeBlood()
    {
        if(playerCurrentHealth == 0)
        {
            ExitGame.sceneName = "FightLose";
            SceneManager.LoadSceneAsync("FightLose", LoadSceneMode.Additive);
            ExitGame.needExit = true;
        }

        if(enemyCurrentHealth == 0)
        {
            ExitGame.sceneName = "FightWin";
            SceneManager.LoadSceneAsync("FightWin", LoadSceneMode.Additive);
            ExitGame.needExit = true;
        }
    }

    //退出游戏
    private void exitGame()
    {
        if (ExitGame.needExit)
        {
            ChangeScene.isFighting = false;
            SceneManager.UnloadSceneAsync("FightUI");
            ExitGame.needExit = false;
        }
    }

    //ESC面板
    private void isGameExit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isExit)
            {
                isExit = true;
                ExitGame.sceneName = "ContinueFight";
                SceneManager.LoadSceneAsync("ContinueFight", LoadSceneMode.Additive);
            }
        }
    }

    //回合内攻击
    IEnumerator GameFight()
    {
        
        for(int i = 0; i < 3; i++)
        {
            RoundRound.text = "Fighting " + roundroundNum;

            runPlayer.sprite = playerGuesture[i].sprite;
            runNum = guestureNum[i];
            runPlayerElement.sprite = playerElement[i].sprite;
            runEleNum = elementNum[i];
            EnemyAI();

            AttackJudge();

            judgeBlood();//判断血量是否为0

            roundroundNum++;
          
            yield return new WaitForSeconds(5f);
        }

        isRoundStart = false;
        isRoundOver = true;
    }


    //回合开始(按下startButton），（想一下怎么实现回合开始后不能再改动手势,或许可以隐藏DragImage脚本)
    public void RoundFight()
    {
        isPrepare = false;
        isRoundStart = true;
        isRoundOver = false;

        for(int i = 0; i < 3; i++)
        {
            if (playerElement[i].sprite == null)
            {
                elementNum[i] = 0;
            }
            else
            {
                if (playerElement[i].sprite.name == "火")
                {
                    elementNum[i] = 1;
                }

                if (playerElement[i].sprite.name == "水2")
                {
                    elementNum[i] = 2;
                }

                if (playerElement[i].sprite.name == "冰")
                {
                    elementNum[i] = 3;
                }
            }
        }
        
        for(int i = 0; i < 3; i++)
        {
            if(playerGuesture[i].sprite.name == "Rock")
            {
                guestureNum[i] = 1;
            }

            if (playerGuesture[i].sprite.name == "Scissors")
            {
                guestureNum[i] = 2;
            }

            if (playerGuesture[i].sprite.name == "Paper")
            {
                guestureNum[i] = 3;
            }
        }

        StartCoroutine(GameFight());
        
    }
}
