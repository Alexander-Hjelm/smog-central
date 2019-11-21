using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;


public class MainScript : MonoBehaviour
{
    // server address
    public string co2_server_host;
    public int co2_server_port;

    // co2_map holds a mapping from country codes to co2 figures
    public Dictionary<string, Co2Data> co2_map = 
            new Dictionary<string, Co2Data>();

    // type for data received from co2server
    public struct Co2Data {
        public double carbonIntensity;
        public double fossilFuelPercentage;
        public string time;
    }

    // download data and store in co2_map
    void download_data() {
        try {
            TcpClient client = new TcpClient();
            client.Connect(hostname: co2_server_host, port: co2_server_port);
            NetworkStream stream = client.GetStream();
            StreamReader sr = new StreamReader(stream);

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
                    switch (data_pair[0]) {
                        case "\"carbonIntensity\"":
                            data.carbonIntensity = Double.Parse(data_pair[1].Trim());
                            break;
                        case "\"fossilFuelPercentage\"":
                            data.fossilFuelPercentage = Double.Parse(data_pair[1].Trim());
                            break;
                        case "\"time\"":
                            data.time = data_pair[1].Trim();
                            break;
                    }
                }

                // insert into co2_map
                co2_map.Add(key, data);
            }
        } catch (Exception e) {
            // TODO
            // couldn't get data from server 
            print("couldn't get data from server");
        }
    }

    // public GameObject FactorySpawn;
    private List<GameObject> lands;

    void Start()
    {
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


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

}
