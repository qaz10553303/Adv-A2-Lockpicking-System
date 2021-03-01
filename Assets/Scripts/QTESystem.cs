using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTESystem : MonoBehaviour
{
    [System.Serializable]
    public struct CheckPin
    {
        public int moveDir;//-1 to move left, 0 to stay, 1 to move right
        public GameObject go;
        public float speed;
    }

    [System.Serializable]
    public struct SuccessArea
    {
        public float width;
        public float xPos;
        public GameObject go;
    }


    private GameManager gm;

    public CheckPin pin;
    public SuccessArea successArea;
    public GameObject bar;

    public List<Image> lightPadList;
    public Text timeText;
    public Text skillLvText;
    public Text difficultyText;
    public Text toolAmountText;

    public class ColorSet
    {
        public Color red = new Color(1, 0, 0, 1);
        public Color green = new Color(0, 1, 0, 1);
    }

    private ColorSet colorSet = new ColorSet();



    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Init_();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        if (gm.canOperate == false) return;
        Space();
        PinMove();
        
    }

    void PinMove()
    {
        Vector2 pos = pin.go.transform.position;
        switch (pin.moveDir)
        {
            case 0:
                break;
            case -1:
                pin.go.transform.position = new Vector2(Input.GetMouseButton(0)?pos.x - pin.speed/(gm.skillLv+1) * Time.deltaTime: pos.x - pin.speed* Time.deltaTime, pos.y);
                break;
            case 1:
                pin.go.transform.position = new Vector2(Input.GetMouseButton(0)?pos.x + pin.speed/(gm.skillLv+1) * Time.deltaTime : pos.x + pin.speed * Time.deltaTime, pos.y);
                break;
        }
        if (pin.go.transform.position.x >= bar.transform.position.x + bar.GetComponent<RectTransform>().rect.width / 2)
        {
            pin.moveDir = -1;
        }
        if (pin.go.transform.position.x <= bar.transform.position.x - bar.GetComponent<RectTransform>().rect.width / 2)
        {
            pin.moveDir = 1;
        }
    }

    void Space()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pin.go.transform.localPosition.x<=successArea.xPos+successArea.width/2&&pin.go.transform.localPosition.x >= successArea.xPos - successArea.width / 2)
            {
                gm.unlockedNumOfPin += 1;
            }
            else
            {
                gm.unlockedNumOfPin -= 1;
            }
            StartCoroutine("WaitAndReset");
        }
    }

    void RamdomizeSuccessArea()
    {
        successArea.go.GetComponent<RectTransform>().sizeDelta = new Vector2(successArea.width,successArea.go.GetComponent<RectTransform>().sizeDelta.y);
        float minX = -bar.GetComponent<RectTransform>().rect.width / 2 * 0.6f + successArea.width / 2;
        float maxX = bar.GetComponent<RectTransform>().rect.width/2-successArea.width/2;
        successArea.xPos = Random.Range(minX, maxX);
        successArea.go.transform.localPosition = new Vector2(successArea.xPos,0);
    }

    IEnumerator WaitAndReset()
    {
        gm.canOperate = false;
        yield return new WaitForSeconds(1f);
        Init_();
        yield return new WaitForSeconds(1.5f);
        gm.canOperate = true;
    }



    void UpdateUI()
    {
        for (int i = 0; i < lightPadList.Count; i++)
        {
            lightPadList[i].color = gm.unlockedNumOfPin > i ? colorSet.green : colorSet.red;
        }
        timeText.text = ((int)gm.timeLeft).ToString();
        toolAmountText.text = "Tool Amount: " + gm.toolAmount;
        skillLvText.text = "Skill Level: " + gm.skillLv;
    }

    public void IncreaseSkillLevel(int amount)
    {
        gm.skillLv += amount;
    }


    void Init_()
    {
        pin.go.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0);
        pin.moveDir = 1;
        pin.speed = gm.pinSpeed;

        successArea.width = 100 / gm.levelDifficulty;
        RamdomizeSuccessArea();

        for (int i = 0; i < lightPadList.Count; i++)
        {
            lightPadList[i].color = colorSet.red;
            lightPadList[i].gameObject.SetActive(i <= gm.levelDifficulty ? true : false);
        }
        switch (gm.levelDifficulty)
        {
            case 1:
                difficultyText.text = "Easy";
                break;
            case 2:
                difficultyText.text = "Normal";
                break;
            case 3:
                difficultyText.text = "Hard";
                break;
        }
        Debug.Log("MinX= "+ (successArea.xPos - successArea.width / 2).ToString());
        Debug.Log("MaxX= " + (successArea.xPos + successArea.width / 2).ToString());
    }
}
