using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VisaulFeedbackTest : MonoBehaviour
{
    [SerializeField]
    private GameObject firstCircle;
    [SerializeField]
    private GameObject secondCircle;
    [SerializeField]
    private GameObject thirdCircle;
    bool ended = true;
    bool testStarted = false;
    bool testCorrect = false;

    SpriteRenderer firstSR;
    SpriteRenderer secondSR;
    SpriteRenderer thirdSR;

    private Color alphaColor = new Color(1, 1, 1, 0.5f);
    private Color normalColor = new Color(1, 1, 1, 1);

    List<int> testSequence;
    List<int> userSequence;

    [SerializeField]
    TextMeshProUGUI finishText;
    [SerializeField]
    GameObject endPanel;
    // Start is called before the first frame update
    void Start()
    {
        firstSR = firstCircle.GetComponent<SpriteRenderer>();
        secondSR = secondCircle.GetComponent<SpriteRenderer>();
        thirdSR = thirdCircle.GetComponent<SpriteRenderer>();
        testSequence = new List<int>();
        userSequence = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTest()
    {
        if (!testStarted)
        {
            StartCoroutine(StartPattern());
        }
    }

    
    IEnumerator StartPattern()
    {
        testStarted = true;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitUntil(() => ended == true);
            ended = false;
            int number = Random.Range(1, 4);
            testSequence.Add(number);
            if (number == 1)
            {
                StartCoroutine(Single());
            }
            if (number == 2)
            {
                StartCoroutine(Double());
            }
            if (number == 3)
            {
                StartCoroutine(Triple());
            }
        }
        
    }

    IEnumerator Single()
    {
        firstSR.color = alphaColor;
        yield return new WaitForSeconds(0.2f);
        firstSR.color = normalColor;
        yield return new WaitForSeconds(1);
        ended = true;
    }

    IEnumerator Double()
    {
        secondSR.color = alphaColor;
        yield return new WaitForSeconds(0.2f);
        secondSR.color = normalColor;
        yield return new WaitForSeconds(0.2f);
        secondSR.color = alphaColor;
        yield return new WaitForSeconds(0.2f);
        secondSR.color = normalColor;
        yield return new WaitForSeconds(1);
        ended = true;
    }

    IEnumerator Triple()
    {
        thirdSR.color = alphaColor;
        yield return new WaitForSeconds(0.2f);
        thirdSR.color = normalColor;
        yield return new WaitForSeconds(0.2f);
        thirdSR.color = alphaColor;
        yield return new WaitForSeconds(0.2f);
        thirdSR.color = normalColor;
        yield return new WaitForSeconds(0.2f);
        thirdSR.color = alphaColor;
        yield return new WaitForSeconds(0.2f);
        thirdSR.color = normalColor;
        yield return new WaitForSeconds(1);
        ended = true;
    }

    public void UserInput(int input)
    {
        if (testStarted)
        { 
            userSequence.Add(input);
            if(userSequence.Count == 3)
            {
                testCorrect = CompareTest();
                ReportResults();
            }
        }
    }

    bool CompareTest()
    {
        for(int i=0; i<3; i++)
        {
            if (userSequence[i] != testSequence[i])
            {
                return false;
            }
        }
        return true;
    }

    void ReportResults()
    {
        endPanel.SetActive(true);
        if (testCorrect)
        {
            finishText.text = "Test Passed";
            Amplitude.Instance.logEvent("Visual Test Passed");
        }
        else
        {
            finishText.text = "Test Failed";
            Amplitude.Instance.logEvent("Visual Test Failed");
        }
    }
}
