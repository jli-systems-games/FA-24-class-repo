using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class customToggleGroup : MonoBehaviour
{
    public Toggle sample1Toggle;
    public Toggle sample2Toggle;
    public Toggle sample3Toggle;
    public Toggle sample4Toggle;
    public Toggle sample5Toggle;
    public Toggle sample6Toggle;
    public Toggle sample7Toggle;
    public Toggle sample8Toggle;
    public Toggle sample9Toggle;
    public Toggle sample10Toggle;

    public bool sample1WasOn;
    public bool sample2WasOn;
    public bool sample3WasOn;
    public bool sample4WasOn;
    public bool sample5WasOn;
    public bool sample6WasOn;
    public bool sample7WasOn;
    public bool sample8WasOn;
    public bool sample9WasOn;
    public bool sample10WasOn;

    public AudioSource sample1;
    public AudioSource sample2;
    public AudioSource sample3;
    public AudioSource sample4;
    public AudioSource sample5;
    public AudioSource sample6;
    public AudioSource sample7;
    public AudioSource sample8;
    public AudioSource sample9;
    public AudioSource sample10;

    public int togglesOn;

    public GameObject nextButton;
    public GameObject sampleSelection;
    public GameObject trackSelection;

    public List<AudioSource> pickedSamples = new List<AudioSource>();

    public void unselect()
    {
        sample1Toggle.isOn = false;
        sample2Toggle.isOn = false;
        sample3Toggle.isOn = false;
        sample4Toggle.isOn = false;
        sample5Toggle.isOn = false;
        sample6Toggle.isOn = false;
        sample7Toggle.isOn = false;
        sample8Toggle.isOn = false;
        sample9Toggle.isOn = false;
        sample10Toggle.isOn = false;
        sample1Toggle.interactable = true;
        sample2Toggle.interactable = true;
        sample3Toggle.interactable = true;
        sample4Toggle.interactable = true;
        sample5Toggle.interactable = true;
        sample6Toggle.interactable = true;
        sample7Toggle.interactable = true;
        sample8Toggle.interactable = true;
        sample9Toggle.interactable = true;
        sample10Toggle.interactable = true;
        nextButton.gameObject.SetActive(false);

    }

    public void nextButtonclick()
    {
        trackSelection.gameObject.SetActive(true);

        sampleSelection.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(sample1WasOn == false && sample1Toggle.isOn)
        {
            togglesOn += 1;
            sample1WasOn = true;
            pickedSamples.Add(sample1);
        }

        if(sample1WasOn == true && !sample1Toggle.isOn)
        {
            togglesOn -= 1;
            sample1WasOn = false;
        }

        if(sample2WasOn == false && sample2Toggle.isOn)
        {
            togglesOn += 1;
            sample2WasOn = true;
        }

        if(sample2WasOn == true && !sample2Toggle.isOn)
        {
            togglesOn -= 1;
            sample2WasOn = false;
        }

        if(sample3WasOn == false && sample3Toggle.isOn)
        {
            togglesOn += 1;
            sample3WasOn = true;
        }

        if(sample3WasOn == true && !sample3Toggle.isOn)
        {
            togglesOn -= 1;
            sample3WasOn = false;
        }

        if(sample4WasOn == false && sample4Toggle.isOn)
        {
            togglesOn += 1;
            sample4WasOn = true;
        }

        if(sample4WasOn == true && !sample4Toggle.isOn)
        {
            togglesOn -= 1;
            sample4WasOn = false;
        }

        if(sample5WasOn == false && sample5Toggle.isOn)
        {
            togglesOn += 1;
            sample5WasOn = true;
        }

        if(sample5WasOn == true && !sample5Toggle.isOn)
        {
            togglesOn -= 1;
            sample5WasOn = false;
        }

        if(sample6WasOn == false && sample6Toggle.isOn)
        {
            togglesOn += 1;
            sample6WasOn = true;
        }

        if(sample6WasOn == true && !sample6Toggle.isOn)
        {
            togglesOn -= 1;
            sample6WasOn = false;
        }

        if(sample7WasOn == false && sample7Toggle.isOn)
        {
            togglesOn += 1;
            sample7WasOn = true;
        }

        if(sample7WasOn == true && !sample7Toggle.isOn)
        {
            togglesOn -= 1;
            sample7WasOn = false;
        }

        if(sample8WasOn == false && sample8Toggle.isOn)
        {
            togglesOn += 1;
            sample8WasOn = true;
        }

        if(sample8WasOn == true && !sample8Toggle.isOn)
        {
            togglesOn -= 1;
            sample8WasOn = false;
        }

        if(sample9WasOn == false && sample9Toggle.isOn)
        {
            togglesOn += 1;
            sample9WasOn = true;
        }

        if(sample9WasOn == true && !sample9Toggle.isOn)
        {
            togglesOn -= 1;
            sample9WasOn = false;
        }

        if(sample10WasOn == false && sample10Toggle.isOn)
        {
            togglesOn += 1;
            sample10WasOn = true;
        }

        if(sample10WasOn == true && !sample10Toggle.isOn)
        {
            togglesOn -= 1;
            sample10WasOn = false;
        }

        if(togglesOn==6)
        {
            sample1Toggle.interactable = false;
            sample2Toggle.interactable = false;
            sample3Toggle.interactable = false;
            sample4Toggle.interactable = false;
            sample5Toggle.interactable = false;
            sample6Toggle.interactable = false;
            sample7Toggle.interactable = false;
            sample8Toggle.interactable = false;
            sample9Toggle.interactable = false;
            sample10Toggle.interactable = false;
            nextButton.gameObject.SetActive(true);
        }

    }
}
