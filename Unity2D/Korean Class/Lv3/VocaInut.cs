using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class VocaInput : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;
    public Text initialText;

    public Text memoText;

    int randomN;

    List<string> usedNamesList = new List<string>();
    public List<string> vocaList = new List<string>();

    public bool isWrong;

    public bool typeEnd;
    public int Wcheck;
    public bool correct;
    public bool foundName;

    public Text checkText;
    public Text check;

    GameManager gm;

    void Awake()
    {
        inputField.onValueChanged.AddListener(OnValueChangedEvent);
        inputField.onEndEdit.AddListener(OnEndExitEvent);
        inputField.onSelect.AddListener(OnSelectEvent);
        inputField.onDeselect.AddListener(OnDeselectEvent);        
    }

    void Start()
    {
        gm = GameManager.instance;

        isWrong = false;
        correct = false;
        typeEnd = false;
        foundName = false;
        typeEnd = false;

        randomN = 0;
        Wcheck = 0;        
    }

    public void OnValueChangedEvent(string str)
    {
        memoText.text = " ".ToString();
    }

    void comparedWords()
    {
        if (typeEnd == true)
        {
            for (int i = 0; i < vocaList.Count; i++)
            {
                // 작성된 단어가 단어 리스트에 있는지 확인
                if (vocaList[i] == inputField.text)
                {
                    // 첫 번째 답
                    if (Wcheck == 0)
                    {
                        sfxManager.instance.correctSoundPlay();
                        memoText.text = "정답".ToString();
                        isWrong = false;
                        // 정답으로 맞춘 단어들을 저장하는 리스트에 해당 텍스트 저장
                        usedNamesList.Add(inputField.text);
                        checkText.text = usedNamesList[Wcheck].ToString();
                        Wcheck++;
                        gm.spawnNest.wrongAnswerCount = 0;
                        break;
                    }

                    else if (Wcheck > 0)
                    {
                        for (int j = 0; j < usedNamesList.Count; j++)
                        {
                            if (usedNamesList[j] == inputField.text)
                            {
                                sfxManager.instance.wrongSoundPlay();
                                memoText.text = "중복".ToString();
                                gm.spawnNest.removeDone = false;
                                isWrong = true;
                                foundName = true;
                                gm.spawnNest.wrongAnswerCount++;
                                break;
                            }
                        }

                        if (foundName == false)
                        {
                            sfxManager.instance.correctSoundPlay();
                            memoText.text = "정답".ToString();
                            isWrong = false;
                            usedNamesList.Add(inputField.text);
                            checkText.text = usedNamesList[Wcheck].ToString();
                            Wcheck++;
                            gm.spawnNest.wrongAnswerCount = 0;
                            break;
                        }
                    }
                }

                else if (vocaList[i] != inputField.text && i + 1 == vocaList.Count && foundName == false)
                {
                    sfxManager.instance.wrongSoundPlay();
                    memoText.text = "오답".ToString();
                    gm.spawnNest.removeDone = false;
                    isWrong = true;
                    gm.spawnNest.wrongAnswerCount++;
                    break;
                }
            }
        }

        if (foundName == true)
        {
            foundName = false;
        }
    }

    public void OnEndExitEvent(string str)
    {
        typeEnd = true;
        comparedWords();
    }

    public void OnSelectEvent(string str)
    {
        typeEnd = false;
        memoText.text = " ".ToString();
    }
    public void OnDeselectEvent(string str)
    {
        typeEnd = false;
        memoText.text = " ".ToString();
    }

    private void Update()
    {
        setVoca();
    }

    public void setVoca()
    {
        // 현재는 "ㅅㄱ" 하나만 있지만 다른 초성 추가하게 될 경우 randomN가 다른 숫자일 때 지정
        if (randomN == 0)
        {
            initialText.text = "ㅅㄱ".ToString();

            for (int i = 0; i < gm.vocaDataManager.sgWord.Count; i++)
            {
                vocaList.Add(gm.vocaDataManager.sgWord[i]);
            }
        }
        randomN = -1;
    }
}
