using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour //All countries have this script
{
    [SerializeField] private string _countryCode;
    [SerializeField] private float _area; // Area in km2

    private GameObject particleObj;

    public string getCountryCode()
    {
        return _countryCode;
    }

    public int NumParticles { get; set; } // NumParticles should be Area * Instensity
    public int Production { get; set; }

    private Color SMOG_COLOR_LOW = Color.green;
    private Color SMOG_COLOR_MID = Color.yellow;
    private Color SMOG_COLOR_HI = Color.red;
    // The thresholds on smog concentration that control what color will be shown
    private const float SMOG_C_UPPER_THRESH = 1000.0f;
    private const float SMOG_C_LOWER_THRESH = 0.0f;

    private Material _material;
    private int randNum;
  //  public GameObject FactoryVisualPrefab;
  //  private GameObject FactoryVis;

    public float GetCO2()
    {
        if(NumParticles == 0)
        {
            return 0;
        }
        return NumParticles/_area;
    }

    private void Awake()
    {
        // Material setup
		_material = new Material(Shader.Find("Custom/Rim"));
        _material.color = SMOG_COLOR_MID;
        particleObj = Instantiate(GetComponentInParent<ContinentScript>().particleObject,transform,true);
        particleObj.transform.localPosition = new Vector3(0, 0, 0);
        particleObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    void Start()
    {
        Production = 1;
      //  FactoryVis = Instantiate(FactoryVisualPrefab, transform);
     //   FactoryVis.transform.localPosition = new Vector3(0, 0, 0);
        //FactoryVis.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        
        // Set NumParticles
        double carbonIntensity = MainScript.GetCarbonIntensityByCountry(_countryCode);
        NumParticles = (int)(carbonIntensity * _area);
    }

    private void Update()
    {
        randNum = Random.Range(1, 100);
        if (randNum <= Production)
        {
            NumParticles++;
        }

        // Set material color depending on CO2 conc
        SetMaterialColor(GetSmogColorByConcentration());
    }

    public float GetArea()
    {
        return _area;
    }

    private Color GetSmogColorByConcentration()
    {
        float c = GetCO2();
        float smogCMid = (SMOG_C_UPPER_THRESH - SMOG_C_LOWER_THRESH) / 2;

        if (c < smogCMid)
        {
            return Color.Lerp(SMOG_COLOR_LOW, SMOG_COLOR_MID, c*2f);
        }
        else
        {
            return Color.Lerp(SMOG_COLOR_MID, SMOG_COLOR_HI, (c-smogCMid)*2f);
        }

    }

    private void SetMaterialColor(Color color)
    {
        _material.color = color;
    }


}
