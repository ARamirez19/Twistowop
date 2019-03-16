using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioFeedbackTest : MonoBehaviour
{
    [SerializeField]
    private AudioSource metronome;

    bool ended = true;
    bool testStarted = false;
    bool testCorrect = false;


    List<int> testSequence;
    List<int> userSequence;

    [SerializeField]
    TextMeshProUGUI finishText;
    [SerializeField]
    GameObject endPanel;
    // Start is called before the first frame update
    void Start()
    {
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
        metronome.Play();
        yield return new WaitForSeconds(1);
        ended = true;
    }

    IEnumerator Double()
    {
        metronome.Play();
        yield return new WaitForSeconds(0.2f);
        metronome.Play();
        yield return new WaitForSeconds(1);
        ended = true;
    }

    IEnumerator Triple()
    {
        metronome.Play();
        yield return new WaitForSeconds(0.2f);
        metronome.Play(); 
        yield return new WaitForSeconds(0.2f);
        metronome.Play();
        yield return new WaitForSeconds(1);
        ended = true;
    }

    public void UserInput(int input)
    {
        if (testStarted)
        {
            userSequence.Add(input);
            if (userSequence.Count == 3)
            {
                testCorrect = CompareTest();
                ReportResults();
            }
        }
    }

    bool CompareTest()
    {
        for (int i = 0; i < 3; i++)
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
