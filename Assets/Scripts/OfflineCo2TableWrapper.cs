using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfflineCo2TableWrapper : MonoBehaviour
{
    public const string table = "AT={\"carbonIntensity\": 182.6478230448546, \"fossilFuelPercentage\": 25.110414115532855, \"time\": \"2019-11-21 19:18:35\"}\n" +
                                "BE={\"carbonIntensity\": 313.3982286806077, \"fossilFuelPercentage\": 52.15750883193253, \"time\": \"2019-11-21 19:18:40\"}\n" +
                                "BG={\"carbonIntensity\": 357.4880626774063, \"fossilFuelPercentage\": 45.85547783437956, \"time\": \"2019-11-21 19:18:46\"}\n" +
                                "CY={\"carbonIntensity\": 650, \"fossilFuelPercentage\": 100, \"time\": \"2019-11-21 19:18:51\"}\n" +
                                "CZ={\"carbonIntensity\": 462.43629318356454, \"fossilFuelPercentage\": 58.51883791395994, \"time\": \"2019-11-21 19:18:56\"}\n" +
                                "DK-DK1={\"carbonIntensity\": 242.86578848938615, \"fossilFuelPercentage\": 30.55414998337253, \"time\": \"2019-11-21 19:19:02\"}\n" +
                                "EE={\"carbonIntensity\": 722.8704743804204, \"fossilFuelPercentage\": 57.194529614853664, \"time\": \"2019-11-21 19:19:07\"}\n" +
                                "FI={\"carbonIntensity\": 178.90688816902642, \"fossilFuelPercentage\": 23.07664628399592, \"time\": \"2019-11-21 19:19:12\"}\n" +
                                "FR={\"carbonIntensity\": 111.52896729068001, \"fossilFuelPercentage\": 17.129844039330862, \"time\": \"2019-11-21 19:19:18\"}\n" +
                                "DE={\"carbonIntensity\": 482.31772635620763, \"fossilFuelPercentage\": 60.76880554737225, \"time\": \"2019-11-21 19:19:23\"}\n" +
                                "HR={\"carbonIntensity\": 278.3928861575317, \"fossilFuelPercentage\": 81.48733912929043, \"time\": \"2019-11-21 19:19:28\"}\n" +
                                "GR={\"fossilFuelPercentage\": null, \"time\": \"2019-11-21 19:19:33\"}\n" +
                                "HU={\"carbonIntensity\": 279.8400058907064, \"fossilFuelPercentage\": 43.166104026888554, \"time\": \"2019-11-21 19:19:38\"}\n" +
                                "IE={\"carbonIntensity\": 373.10802429849167, \"fossilFuelPercentage\": 59.413654343513166, \"time\": \"2019-11-21 19:19:44\"}\n" +
                                "IT-CNO={\"carbonIntensity\": 322.04305596211924, \"fossilFuelPercentage\": 55.43186274481434, \"time\": \"2019-11-21 19:19:49\"}\n" +
                                "EL={\"time\": \"2019-11-21 19:19:54\"}\n" +
                                "LV={\"carbonIntensity\": 240.13518800616725, \"fossilFuelPercentage\": 36.13486206334916, \"time\": \"2019-11-21 19:19:59\"}\n" +
                                "LT={\"carbonIntensity\": 276.7672899223448, \"fossilFuelPercentage\": 51.961240720768664, \"time\": \"2019-11-21 19:20:05\"}\n" +
                                "LU={\"fossilFuelPercentage\": null, \"time\": \"2019-11-21 19:20:10\"}\n" +
                                "MT={\"fossilFuelPercentage\": null, \"time\": \"2019-11-21 19:20:15\"}\n" +
                                "NL={\"carbonIntensity\": 550.9955713982065, \"fossilFuelPercentage\": 80.92045810210048, \"time\": \"2019-11-21 19:20:21\"}\n" +
                                "PL={\"carbonIntensity\": 633.1959573330832, \"fossilFuelPercentage\": 78.59303848255378, \"time\": \"2019-11-21 19:20:26\"}\n" +
                                "PT={\"carbonIntensity\": 405.12818057240077, \"fossilFuelPercentage\": 59.659749967779355, \"time\": \"2019-11-21 19:20:31\"}\n" +
                                "RO={\"carbonIntensity\": 232.3250658472344, \"fossilFuelPercentage\": 32.769973661106235, \"time\": \"2019-11-21 19:20:36\"}\n" +
                                "SK={\"carbonIntensity\": 249.85159983046375, \"fossilFuelPercentage\": 35.06837738056369, \"time\": \"2019-11-21 19:20:42\"}\n" +
                                "SI={\"carbonIntensity\": 233.89178839581155, \"fossilFuelPercentage\": 35.024929636367155, \"time\": \"2019-11-21 19:20:47\"}\n" +
                                "ES={\"carbonIntensity\": 296.8252554833781, \"fossilFuelPercentage\": 53.09291554548084, \"time\": \"2019-11-21 19:20:53\"}\n" +
                                "SE={\"carbonIntensity\": 58.50840282661395, \"fossilFuelPercentage\": 9.293316023701491, \"time\": \"2019-11-21 20:20:58\"}\n" +
                                "GB={\"carbonIntensity\": 313.7440100444684, \"fossilFuelPercentage\": 55.61633436652601, \"time\": \"2019-11-21 20:21:03\"}\n" +
                                "IS={\"carbonIntensity\": 28.176785011725197, \"fossilFuelPercentage\": 0, \"time\": \"2019-11-21 20:21:09\"}\n" +
                                "NO-NO1={\"carbonIntensity\": 27.73818064148482, \"fossilFuelPercentage\": 0.8977819256761964, \"time\": \"2019-11-21 20:21:14\"}\n" +
                                "CH={\"fossilFuelPercentage\": null, \"time\": \"2019-11-21 20:21:19\"}\n" +
                                "ME={\"fossilFuelPercentage\": null, \"time\": \"2019-11-21 20:21:24\"}\n" +
                                "MK={\"fossilFuelPercentage\": null, \"time\": \"2019-11-21 20:21:30\"}\n" +
                                "AL={\"fossilFuelPercentage\": null, \"time\": \"2019-11-21 20:21:35\"}\n" +
                                "RS={\"carbonIntensity\": 516.7806731511921, \"fossilFuelPercentage\": 62.310632035907076, \"time\": \"2019-11-21 20:21:40\"}\n" +
                                "TR={\"carbonIntensity\": 482.38555099763386, \"fossilFuelPercentage\": 70.15579949381427, \"time\": \"2019-11-21 20:21:45\"}\n" +
                                "BA={\"carbonIntensity\": 378.6806596701649, \"fossilFuelPercentage\": 44.565217391304344, \"time\": \"2019-11-21 20:21:51\"}\n" +
                                "AZ={\"fossilFuelPercentage\": null, \"time\": \"2019-11-21 20:21:56\"}\n" +
                                "MD={\"carbonIntensity\": 562.8023652278754, \"fossilFuelPercentage\": 86.10297341257184, \"time\": \"2019-11-21 20:22:02\"}\n" +
                                "UA={\"carbonIntensity\": 258.6817287066727, \"fossilFuelPercentage\": 34.56252930185752, \"time\": \"2019-11-21 20:22:07\"}\n" +
                                "BY={\"fossilFuelPercentage\": null, \"time\": \"2019-11-21 20:22:13\"}\n" +
                                "GE={\"carbonIntensity\": 275.0663310170262, \"fossilFuelPercentage\": 53.26246455222234, \"time\": \"2019-11-21 20:22:18\"}\n" +
                                "AM={\"carbonIntensity\": 196.9967776584318, \"fossilFuelPercentage\": 38.23845327604726, \"time\": \"2019-11-21 20:22:23\"}" +
                                "RU-1={\"carbonIntensity\": 366.1464036814555, \"fossilFuelPercentage\": 68.77257107890198, \"time\": \"2019-12-03 15:46:52\"}";
}