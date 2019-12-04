using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Country : MonoBehaviour //All countries have this script
{
    public enum CountryType
    {
        CLEAN,
        DIRTY
    }

    [SerializeField] private string _countryCode;
    [SerializeField] private float _area; // Area in km2

    private GameObject particleObj;

    public string getCountryCode()
    {
        return _countryCode;
    }

    public int NumParticles { get; set; } // NumParticles should be Area * Instensity

    private Color COUNTRY_COLOR_SMOG_LOW = Color.green;
    private Color COUNTRY_COLOR_SMOG_MID = Color.yellow;
    private Color COUNTRY_COLOR_SMOG_HI = Color.red;
    private Color COUNTRY_COLOR_INACTIVE = Color.grey;

    // What level will the smog intensity stabilize around? Depending on if this is a clean or a dirty country
    private double _stableSmogLevelLow = 0f;
    private double _stableSmogLevelHigh = 0f;
    // How many particles will be added/removed on each FixedUpdate doe to the country's own production?
    private const int _particleProductionRate = 1000;

    // The thresholds on smog concentration that control what color will be shown
    private const float SMOG_C_UPPER_THRESH = 800.0f;
    private const float SMOG_C_LOWER_THRESH = 0.0f;

    private Material _material;
    private CountryType _countryType;
    private float _co2PreviousFrame;
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
        _material.SetFloat("_RimEffect", 1f);
        SetMaterialColor(COUNTRY_COLOR_SMOG_MID);   // Default color
        GetComponent<MeshRenderer>().material = _material;

    }

    void Start()
    {
      //  FactoryVis = Instantiate(FactoryVisualPrefab, transform);
     //   FactoryVis.transform.localPosition = new Vector3(0, 0, 0);
        //FactoryVis.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        
        // Set NumParticles
        double carbonIntensity = MainScript.GetCarbonIntensityByCountry(_countryCode);

        // If carbon intensity is 0, this country is not in the data set and should be disabled
        if (carbonIntensity == 0f)
        {
            SetMaterialColor(COUNTRY_COLOR_INACTIVE);
            Destroy(this);
        }
        else
        {
            NumParticles = (int)(carbonIntensity * _area);

            // Instantiate particle system
            particleObj = Instantiate(GetComponentInParent<ContinentScript>().particleObject,transform,true);
            particleObj.transform.localPosition = new Vector3(0, 0, 0);
            particleObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

            // Set stable levels
            if (carbonIntensity < 500f)
            {
                // Clean country
                _stableSmogLevelLow = carbonIntensity;
                _stableSmogLevelHigh = carbonIntensity + 500f;
                _countryType = CountryType.CLEAN;
            }
            else
            {
                // Dirty country
                _stableSmogLevelLow = carbonIntensity - 500f;
                _stableSmogLevelHigh = carbonIntensity + 500f;
                _countryType = CountryType.DIRTY;
            }

            // Register country with game manager
            MainScript.RegisterCountry(this);
        }

    }

    private void Update()
    {
        float co2 = GetCO2();

        // Set CountryType depending on smog level
        if (co2 > 500f)
        {
            _countryType = CountryType.DIRTY;
        }
        else
        {
            _countryType = CountryType.CLEAN;
        }

        // Set material color depending on CO2 conc
        SetMaterialColor(GetSmogColorByConcentration());

        // Warning if the country is close to converting from Dirty to Clean
        if (co2 < _co2PreviousFrame && _countryType == CountryType.DIRTY && co2 > 500f && co2 < 550f)
        {
            // Set the material color in a blinking pattern
            if (Time.time % 0.25f < 0.125f)
            {
                SetMaterialColor(Color.red);
            }
        }

        _co2PreviousFrame = co2;
    }

    private void FixedUpdate()
    {
        // Particle production
        float smogLevel = GetCO2();
        if (_countryType == CountryType.CLEAN && smogLevel < _stableSmogLevelLow
            || _countryType == CountryType.DIRTY && smogLevel < _stableSmogLevelHigh)
        {
            // Particle level should go up
            NumParticles += _particleProductionRate;
        }
        else if (_countryType == CountryType.CLEAN && smogLevel > _stableSmogLevelLow
            || _countryType == CountryType.DIRTY && smogLevel > _stableSmogLevelHigh)
        {
            // Particle level should go down
            NumParticles -= _particleProductionRate;
        }
    }

    public CountryType GetCountryType()
    {
        return _countryType;
    }

    public float GetArea()
    {
        return _area;
    }

    private Color GetSmogColorByConcentration()
    {
        float c = GetCO2() / SMOG_C_UPPER_THRESH;
        float smogCMid = (SMOG_C_UPPER_THRESH - SMOG_C_LOWER_THRESH) / (SMOG_C_UPPER_THRESH * 2f);

        if (c < smogCMid)
        {
            return Color.Lerp(COUNTRY_COLOR_SMOG_LOW, COUNTRY_COLOR_SMOG_MID, c*2f);
        }
        else
        {
            return Color.Lerp(COUNTRY_COLOR_SMOG_MID, COUNTRY_COLOR_SMOG_HI, (c-smogCMid)*2f);
        }

    }

    private void SetMaterialColor(Color color)
    {
        _material.color = color;
    }


}
