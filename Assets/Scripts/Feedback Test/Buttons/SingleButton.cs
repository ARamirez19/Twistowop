using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleButton : MonoBehaviour
{
	private bool firstTest = true;
	private bool secondTest = false;
	private bool thirdTest = false;
	private bool fourthTest = false;
	private FeedBackTest testFeedBack;

    // Start is called before the first frame update
    void Start()
    {
		testFeedBack = GameObject.Find("FeedBackManager").GetComponent<FeedBackTest>();
    }

    // Update is called once per frame
    public void SinglePress()
	{
		testFeedBack.PlayerSelectSingle();
	}

	public void DoublePress()
	{
		testFeedBack.PlayerSelectDouble();
	}

	public void TriplePress()
	{
		testFeedBack.PlayerSelectTriple();
	}

	public void StartTest()
	{
		if(firstTest == true)
		{
			testFeedBack.FirstTest();
			firstTest = false;
			secondTest = true;
		}
		else if(secondTest == true)
		{
			testFeedBack.SecondTest();
			secondTest = false;
			thirdTest = true;
		}
		else if(thirdTest == true)
		{
			testFeedBack.ThirdTest();
			thirdTest = false;
			fourthTest = true;
		}
		else if(fourthTest == true)
		{
			testFeedBack.FourthTest();
			fourthTest = false;
		}
	}
}
