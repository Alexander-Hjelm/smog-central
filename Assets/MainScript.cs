using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Globalization;

public class MainScript : MonoBehaviour
{
    // server address
    public string co2_server_host;
    public int co2_server_port;
    public string offline_co2_table = "offline_co2_table";

    // Singleton ref
    private static MainScript _instance;

    // Ref to in-game Country objects
    private List<Country> _registeredCountries = new List<Country>();

    // co2_map holds a mapping from country codes to co2 figures
    private Dictionary<string, Co2Data> co2_map = 
            new Dictionary<string, Co2Data>();

    // type for data received from co2server
    public struct Co2Data {
        public double carbonIntensity;
        public double fossilFuelPercentage;
        public string time;
    }

    void parse_co2_table(StreamReader sr) {
        string line;
        while ((line = sr.ReadLine()) != null) {
            // split key from json
            string[] kv_pair = line.Split('=');
            
            // find key
            string[] key_split = kv_pair[0].Split('-');
            string key = key_split[0];

            // parse json, manually...
            string trimmed = kv_pair[1].Substring(1, kv_pair[1].Length - 2);
            string[] data_pairs = trimmed.Split(',');
            Co2Data data = new Co2Data();
            foreach (string dp in data_pairs) {
                String[] data_pair = dp.Split(':');
                if (String.Equals(data_pair[1].Trim(), "null")) 
                    continue;
                switch (data_pair[0].Trim()) {
                    case "\"carbonIntensity\"":
                       // Debug.Log("HEJ: " + data_pair[1].Trim());
                        data.carbonIntensity = Double.Parse(data_pair[1].Trim(), CultureInfo.InvariantCulture);
                        break;
                    case "\"fossilFuelPercentage\"":
                        data.fossilFuelPercentage = Double.Parse(data_pair[1].Trim(), CultureInfo.InvariantCulture);
                        break;
                    case "\"time\"":
                        data.time = data_pair[1].Trim();
                        break;
                }
            }

            // insert into co2_map
            co2_map.Add(key, data);
        }
    }

    // download data and store in co2_map
    void download_data() {
        try {
            TcpClient client = new TcpClient();
            client.Connect(hostname: co2_server_host, port: co2_server_port);
            NetworkStream stream = client.GetStream();
            StreamReader sr = new StreamReader(stream);
            parse_co2_table(sr);
        } catch (Exception e) {
            // TODO
            // couldn't get data from server 
            print("couldn't get data from server. Using local offline table.");
            var fileStream = new FileStream(offline_co2_table, FileMode.Open);
            StreamReader sr = new StreamReader(fileStream);
            parse_co2_table(sr);
        }
    }

    // public GameObject FactorySpawn;
    private List<GameObject> lands;

    private void Awake()
    {
        _instance = this;

        lands = new List<GameObject>();

        print(">>>");
        download_data();
        // debug print
        foreach (KeyValuePair<string, Co2Data> kvp in co2_map)
        {
            print("Key = {" + kvp.Key + "}, Value = {" + kvp.Value.carbonIntensity + "}");
        }
        print("<<<");

        foreach(GameObject land in lands)
        {
           // land.GetComponent<Country>().ParticleType = FactorySpawn;
        }

    }

    private void Update()
    {
        // Win check
        bool win = true;
        foreach (Country country in _registeredCountries)
        {
            if (country.GetCountryType() == Country.CountryType.CLEAN)
            {
                win = false;
                break;
            }
        }
        if (win)
        {
            // We have won the game
            Debug.Log("We won the game!");
        }
    }

    public static double GetCarbonIntensityByCountry(string countryCode)
    {
        if (!_instance.co2_map.ContainsKey(countryCode))
            return 1;
        Co2Data data = _instance.co2_map[countryCode];
        return data.carbonIntensity;
    }

    public static void RegisterCountry(Country country)
    {
        _instance._registeredCountries.Add(country);
    }

}
