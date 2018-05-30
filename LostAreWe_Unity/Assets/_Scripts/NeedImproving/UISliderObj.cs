using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UISliderObj : IUIObj{

    [SerializeField] private Slider _mySlider;

    [SerializeField] private string _name;
    public string Name
    {
        get { return _name; }
        set { }
    }

    public float MaxValue
    {
        get { return _mySlider.maxValue; }
        set { _mySlider.maxValue = value; }
    }

    public float CurrentValue
    {
        get { return _mySlider.value; }
        set { _mySlider.value = value; }
    }

    public Color _fillColour;
    public Color _backgroundColour;

    public void Setup()
    {
        _mySlider.transform.GetChild(0).GetComponent<Image>().color = _backgroundColour;
        _mySlider.transform.GetChild(1).GetComponentInChildren<Image>().color = _fillColour;
    }
}
