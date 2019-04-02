using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Prize
{
    public string prize;
    [Range(1, 100)]
    public int weight;
}



public class PrizeWheelv2 : MonoBehaviour
{
    [SerializeField]
    private List<AnimationCurve> animationCurves;
    [SerializeField]
    private List<Prize> prize;

    private bool spinning;
    private float anglePerItem;
    private int randomTime;
    private int itemNumber;
    private int randomPlacement;
    private int plusOrMinus;
    private float maxAngle;

    void Start()
    {
        spinning = false;
        anglePerItem = 360 / prize.Count;
        CheckWeights(prize);
    }

    void Update()
    {

    }

    public void SpinWheel()
    {
        randomTime = Random.Range(3, 4);
        //itemNumber = Random.Range(0, prize.Count); //Will Edit for probability
        itemNumber = CalculateProbability(prize);
        randomPlacement = Random.Range(0, (int)anglePerItem);
        plusOrMinus = Random.Range(1, 2);
        if (plusOrMinus == 1)
        {
            maxAngle = 360 * randomTime + (itemNumber * anglePerItem + (randomPlacement / 2));
        }
        else
        {
            maxAngle = 360 * randomTime + (itemNumber * anglePerItem - (randomPlacement / 2));
        }
        StartCoroutine(SpinTheWheel(5 * randomTime, maxAngle));
    }

    IEnumerator SpinTheWheel(float time, float maxAngle)
    {
        spinning = true;

        float timer = 0.0f;
        float startAngle = transform.eulerAngles.z;
        maxAngle = maxAngle - startAngle;

        int animationCurveNumber = Random.Range(0, animationCurves.Count);
        Debug.Log("Animation Curve No. : " + animationCurveNumber);

        while (timer < time)
        {
            //to calculate rotation
            float angle = maxAngle * animationCurves[animationCurveNumber].Evaluate(timer / time);
            transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + startAngle);
            timer += Time.deltaTime;
            yield return 0;
        }

        transform.eulerAngles = new Vector3(0.0f, 0.0f, maxAngle + startAngle);
        spinning = false;

        Debug.Log("Prize: " + prize[itemNumber].prize);//use prize[itemNumnber] as per requirement
    }

    private void CheckWeights(List<Prize> prizes)
    {
        int weightSum = 0;
        foreach (Prize prize in prizes)
        {
            weightSum += prize.weight;
        }
        if(weightSum != 100)
        {
            Debug.LogError("Weights Must Sum to 100");
        }
    }

    private int CalculateProbability(List<Prize> prizes)
    {
        List<int> weights = new List<int>();
        int index = 0;
        int rand = Random.Range(0, 100);
        foreach (Prize prize in prizes)
        {
            for (int i = 0; i < prize.weight; i++)
            {
                weights.Add(index);
            }
            index++;
        }
        return weights[rand];
    }
}
