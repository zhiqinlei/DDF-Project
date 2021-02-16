using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOnOff : MonoBehaviour
{
	[Range(6f, 18f)]
	public float currentTime;   // 6 equal to sunrise , 18 euals to midnight
	public float dayLengthInSeconds; //seconds a day will last

	void Start()
	{

	}
	void Update()
	{
		float speed = 12f / dayLengthInSeconds;

		currentTime += Time.deltaTime * speed;

		if (currentTime >= 18f)
			currentTime = 4f;
		transform.rotation = Quaternion.Euler((currentTime - 6) * 15f, 0f, 0f);
	}
}
