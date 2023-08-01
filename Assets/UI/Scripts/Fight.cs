using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fight : MonoBehaviour
{
    //�������
    public Image[] playerGuesture = new Image[3];
    private int[] guestureNum = new int[3];

    //���Ԫ��
    public Image[] randomElement = new Image[4];

    //���Ԫ��
    public Image[] playerElement = new Image[3];
    private int[] elementNum = new int[3];

    //����ѡ������
    public Dropdown[] dropGuesture = new Dropdown[3];
    
    //���ƺ�Ԫ����ͼ
    public Sprite[] RPS = new Sprite[3];
    public Sprite[] Element = new Sprite[3];

    //�غ϶�ս
    public Image runPlayer;
    private int runNum;
    public Image runPlayerElement;
    private int runEleNum;

    public Image runEnemy;
    private int enemyNum;
    public Image runEnemyElement;
    private int enemyEleNum;

    //�غϲ�����״̬
    public Text Round;
    public Text RoundRound;

    private int roundNum;
    private int roundroundNum;
    private bool isPrepare = false;
    private bool isRoundStart = false;
    private bool isRoundOver = false;

    //Ԫ��״̬
    private bool isFire;
    private bool isWater;
    private bool isIce;

    //��Һ͵��˵�ǰ��Ԫ��״̬
    public Image elementState;
    public Image enemyElementState;

    private int playerState;
    private int enemyState;

    //Ѫ����ʾ���˺���ʾ
    public const float playerHealth = 100f;
    private float playerCurrentHealth;

    public const float enemyHealth = 100f;
    private float enemyCurrentHealth;
        //Ѫ��
    public Image playerBlood;
    public Image enemyBlood;
        //Ԫ���˺�������ʾ
    public Text playerElementHurt;
    public Text enemyElementHurt;
        //Ѫ����ʾ
    public Text playerBloodVisable;
    public Text enemyBloodVisable;
        //�˺�������ʾ
    public Text playerHurtNum;
    public Text enemyHurtNum;

    //�˳�����
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
    

    //����ѡ����
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

    //���Ԫ��
    private void RandomElement()
    {
        foreach (Image i in randomElement)
        {
            i.sprite = Element[Random.Range(0, 3)];
        }
    }

    //����
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
        if (runEnemyElement.sprite.name == "��")
        {
            enemyEleNum = 1;
        }

        if (runEnemyElement.sprite.name == "ˮ2")
        {
            enemyEleNum = 2;
        }

        if (runEnemyElement.sprite.name == "��")
        {
            enemyEleNum = 3;
        }
    }

    //�ж�����Ƿ�Ӯ��
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

    //�����˺��ж������,Ѫ���۳�
    private void AttackJudge()
    {
        int isWin = isPlayerWin(runNum, enemyNum);
        if (isWin == 1)
        {
            //���Ӯ�ˣ����з�ʩ��Ԫ�ء��ݶ�ͷ��������һ��С����ʾԪ��
            //����Ԫ���жϣ�����˺�������Ҫ����һ����ô��Text����ʾ)
            //���ĳһ��Ѫ��Ϊ0��Ҫ�˳���ϷExitGame����
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
                    enemyElementHurt.text = "����������Ӧ";
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
                    enemyElementHurt.text = "�����ڻ���Ӧ";
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
                    enemyElementHurt.text = "�������ᷴӦ";
                    StartCoroutine(TextGradient(enemyElementHurt));
                    StartCoroutine(TextGradient(enemyHurtNum));
                }
            }
        }
        //�������
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
                    playerElementHurt.text = "����������Ӧ";
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
                    playerElementHurt.text = "�����ڻ���Ӧ";
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
                    playerElementHurt.text = "�������ᷴӦ";
                    StartCoroutine(TextGradient(playerElementHurt));
                    StartCoroutine(TextGradient(playerHurtNum));
                }
            }
        }
        //ƽ��
        else
        {
            Debug.Log("equal");
        }
        //��д�����Ѫ�������˺���Ч������������ʽ���˺����ֳ�����Ѫ���Ա߼����֡����Կ��Ǽ���ģ�Ͷ��������⻹��HPֵ����ʾ
        Debug.Log("�ܵ��˺�xx��");

       
    }

    //Text������ʧ
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

    //�غϽ������ʼ��
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

    //�˳���Ϸ
    private void exitGame()
    {
        if (ExitGame.needExit)
        {
            ChangeScene.isFighting = false;
            SceneManager.UnloadSceneAsync("FightUI");
            ExitGame.needExit = false;
        }
    }

    //ESC���
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

    //�غ��ڹ���
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

            judgeBlood();//�ж�Ѫ���Ƿ�Ϊ0

            roundroundNum++;
          
            yield return new WaitForSeconds(5f);
        }

        isRoundStart = false;
        isRoundOver = true;
    }


    //�غϿ�ʼ(����startButton��������һ����ôʵ�ֻغϿ�ʼ�����ٸĶ�����,�����������DragImage�ű�)
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
                if (playerElement[i].sprite.name == "��")
                {
                    elementNum[i] = 1;
                }

                if (playerElement[i].sprite.name == "ˮ2")
                {
                    elementNum[i] = 2;
                }

                if (playerElement[i].sprite.name == "��")
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
