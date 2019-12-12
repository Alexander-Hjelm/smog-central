using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui : MonoBehaviour
{

    public enum Type {
        source,
        target
    };

    /* public stuff */
    public Type type;
    public GameObject fan;


    /* private stuff */
    private string prefix;
    private UnityEngine.UI.Text text_script;
    private FanScript fan_script;
    private GameObject country;
    private Country country_script;
    private Dictionary<string, string> country_names = 
            new Dictionary<string, string> {
        ["NO"] = "Norway",
        ["SE"] = "Sweden",
        ["EL"] = "Greece",
        ["IT"] = "Italy",
        ["HR"] = "Croatia",
        ["RS"] = "Serbia",
        ["AL"] = "Albania",
        ["BG"] = "Bulgaria",
        ["RO"] = "Romania",
        ["TR"] = "Turkey",
        ["EE"] = "Estonia",
        ["UA"] = "Ukraine",
        ["BY"] = "Belarus",
        ["MK"] = "N. Macedonia",
        ["BA"] = "Bosnia and Herzegovia",
        ["ES"] = "Spain",
        ["FR"] = "France",
        ["GB"] = "United Kingdom",
        ["HU"] = "Hungary",
        ["CH"] = "Switzerland",
        ["AT"] = "Austria",
        ["SI"] = "Slovenia",
        ["RU"] = "Russia",
        ["PL"] = "Poland",
        ["DE"] = "Germany",
        ["LI"] = "Lichtenstein",
        ["PT"] = "Portugal",
        ["MD"] = "Moldova",
        ["SK"] = "Slovakia",
        ["CZ"] = "Czechia",
        ["LU"] = "Luxembourg",
        ["BE"] = "Belgium",
        ["NL"] = "Netherlands",
        ["IE"] = "Ireland",
        ["IS"] = "Iceland",
        ["DK"] = "Denmark",
        ["LT"] = "Lithuania",
        ["LV"] = "Latvia",
        ["FI"] = "Finland"
    };

    // Start is called before the first frame update
    void Start()
    {
        switch (this.type) {
            case Type.source: prefix = "source: \n"; break;
            case Type.target: prefix = "target: \n"; break;
        }

        this.text_script = GetComponent<UnityEngine.UI.Text>();
        this.text_script.text = "EMTPY";
        this.fan_script = fan.GetComponent<FanScript>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (this.type) {
            case Type.source: country = this.fan_script.standsOnLand; break;
            case Type.target: country = this.fan_script.aimsAtLand; break;
        }
        if (country == null){
            this.text_script.text = prefix + "-";
            return;
        }
        country_script = country.GetComponentInParent<Country>();
        float ci = (country_script.GetCO2() > 2.0f) ? country_script.GetCO2() : 0.0f;
        string uitext = prefix + country_names[country_script._countryCode] 
                + "\n" + ci.ToString("0.00");
        this.text_script.text = uitext;
    }
}
