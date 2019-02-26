using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using UnityEngine.UI;

public class FeedBackTest : MonoBehaviour
{
	private int correctChoices;
	private int incorrectChoices;
	public int feedBackPattern;
	private float alphaLevel = 0.5f;
	private bool firstPattern = false;
	private bool secondPattern = false;
	private bool thirdPattern = false;
	[SerializeField]
	private Text displayText;
	[SerializeField]
	private GameObject firstCircle;
	[SerializeField]
	private GameObject secondCircle;
	[SerializeField]
	private GameObject thirdCircle;
	[SerializeField]
	private AudioSource metronome;


    // Start is called before the first frame update
    void Start()
    {
	
    }

    // Update is called once per frame
    void Update()
    {
       
    }

	private void UpdateText()
	{
		displayText.text = "Correct Choices: " + correctChoices + "/4 Incorrect Choices: " + incorrectChoices;
		firstPattern = false;
		secondPattern = false;
		thirdPattern = false;
	}

	public void FirstTest()
	{
		feedBackPattern = Random.Range(1,4);
		if(feedBackPattern == 1)
		{
			StartCoroutine(FirstTestSingleTimer());
		}
		if(feedBackPattern == 2)
		{
			StartCoroutine(FirstTestDoubleTimer());
		}
		if(feedBackPattern == 3)
		{
			StartCoroutine(FirstTestTripleTimer());
		}
	}

	public void SecondTest()
	{
		feedBackPattern = Random.Range(1,4);
		if(feedBackPattern == 1)
		{
			firstPattern = true;
			metronome.Play();
		}
		if(feedBackPattern == 2)
		{
			StartCoroutine(SecondTestDoubleTimer());
		}
		if(feedBackPattern == 3)
		{
			StartCoroutine(SecondTestTripleTimer());
		}
	}

	public void ThirdTest()
	{
		feedBackPattern = Random.Range(1,4);
		Debug.Log("RANDOM NUMBER " + feedBackPattern);
		if(feedBackPattern == 1)
		{
			firstPattern = true;
			MMVibrationManager.Haptic(HapticTypes.Success);
		}
		if(feedBackPattern == 2)
		{
			StartCoroutine(ThirdTestDoubleTimer());
		}
		if(feedBackPattern == 3)
		{
			StartCoroutine(ThirdTestTripleTimer());
		}
	}

	public void FourthTest()
	{
		feedBackPattern = Random.Range(1,4);
		if(feedBackPattern == 1)
		{
			StartCoroutine(FourthTestSingleTimer());
		}
		if(feedBackPattern == 2)
		{
			StartCoroutine(FourthTestDoubleTimer());
		}
		if(feedBackPattern == 3)
		{
			StartCoroutine(FourthTestTripleTimer());
		}
	}

	public void PlayerSelectSingle()
	{
		if(firstPattern == true)
		{
			correctChoices++;
			UpdateText();
		}
		else
		{
			incorrectChoices++;
			UpdateText();
		}
	}

	public void PlayerSelectDouble()
	{
		if(secondPattern == true)
		{
			correctChoices++;
			UpdateText();
		}
		else
		{
			incorrectChoices++;
			UpdateText();
		}
	}

	public void PlayerSelectTriple()
	{
		if(thirdPattern == true)
		{
			correctChoices++;
			UpdateText();
		}
		else
		{
			incorrectChoices++;
			UpdateText();
		}
	}

	IEnumerator FirstTestSingleTimer()
	{
		firstPattern = true;
		firstCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		yield return new WaitForSeconds(0.2f);
		alphaLevel += 0.5f;
		firstCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
	}

	IEnumerator FirstTestDoubleTimer()
	{
		secondPattern = true;
		firstCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		yield return new WaitForSeconds(0.2f);
		alphaLevel += 0.5f;
		firstCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		alphaLevel -= 0.5f;
		secondCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		yield return new WaitForSeconds(0.2f);
		alphaLevel =+ 1.0f;
		secondCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
	}

	IEnumerator FirstTestTripleTimer()
	{
		thirdPattern = true;
		firstCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		yield return new WaitForSeconds(0.2f);
		alphaLevel += 0.5f;
		firstCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		alphaLevel -= 0.5f;
		thirdCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		yield return new WaitForSeconds(0.2f);
		alphaLevel += 0.5f;
		thirdCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		alphaLevel -= 0.5f;
		secondCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		yield return new WaitForSeconds(0.2f);
		alphaLevel += 0.5f;
		secondCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
	}

	IEnumerator SecondTestDoubleTimer()
	{
		secondPattern = true;
		metronome.Play();
		yield return new WaitForSeconds(0.2f);
		metronome.Play();
	}

	IEnumerator SecondTestTripleTimer()
	{
		thirdPattern = true;
		metronome.Play ();
		yield return new WaitForSeconds(0.2f);
		metronome.Play ();
		yield return new WaitForSeconds(0.2f);
		metronome.Play ();
	}

	IEnumerator ThirdTestDoubleTimer()
	{
		secondPattern = true;
		MMVibrationManager.Haptic(HapticTypes.Success);
		yield return new WaitForSeconds(0.2f);
		MMVibrationManager.Haptic(HapticTypes.Success);
	}

	IEnumerator ThirdTestTripleTimer()
	{
		thirdPattern = true;
		MMVibrationManager.Haptic(HapticTypes.Success);
		yield return new WaitForSeconds(0.2f);
		MMVibrationManager.Haptic(HapticTypes.Success);
		yield return new WaitForSeconds(0.2f);
		MMVibrationManager.Haptic(HapticTypes.Success);
	}

	IEnumerator FourthTestSingleTimer()
	{
		firstPattern = true;
		MMVibrationManager.Haptic(HapticTypes.Success);
		metronome.Play();
		alphaLevel -= 0.5f;
		firstCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		yield return new WaitForSeconds(0.2f);
		alphaLevel += 0.5f;
		firstCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
	}

	IEnumerator FourthTestDoubleTimer()
	{
		secondPattern = true;
		MMVibrationManager.Haptic(HapticTypes.Success);
		metronome.Play();
		alphaLevel -= 0.5f;
		firstCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		yield return new WaitForSeconds(0.2f);
		MMVibrationManager.Haptic(HapticTypes.Success);
		metronome.Play();
		alphaLevel += 0.5f;
		firstCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		alphaLevel -= 0.5f;
		secondCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		yield return new WaitForSeconds(0.2f);
		alphaLevel =+ 1.0f;
		secondCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
	}

	IEnumerator FourthTestTripleTimer()
	{
		thirdPattern = true;
		MMVibrationManager.Haptic(HapticTypes.Success);
		metronome.Play();
		alphaLevel -= 0.5f;
		firstCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		yield return new WaitForSeconds(0.2f);
		MMVibrationManager.Haptic(HapticTypes.Success);
		metronome.Play();
		alphaLevel += 0.5f;
		firstCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		alphaLevel -= 0.5f;
		thirdCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		yield return new WaitForSeconds(0.2f);
		MMVibrationManager.Haptic(HapticTypes.Success);
		metronome.Play();
		alphaLevel += 0.5f;
		thirdCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		alphaLevel -= 0.5f;
		secondCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
		yield return new WaitForSeconds(0.2f);
		alphaLevel += 0.5f;
		secondCircle.GetComponent<SpriteRenderer>().color = new Color(1,1,1,alphaLevel);
	}
}
