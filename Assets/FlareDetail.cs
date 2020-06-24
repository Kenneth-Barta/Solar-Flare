using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using UnityEngine.UI;

public class FlareDetail : MonoBehaviour
{
    public static Dictionary<string, List<Flare>> flares;

    public class Flare
    {
        public int year, month, day, startTime, endTime, longitude, latitude, area, intensity;
        public string brightness, xrayClass;

        public Flare(string[] data) //Constructor for now parses all data into its flare data values
        {
            Int32.TryParse(data[0], out year);
            Int32.TryParse(data[1], out month);
            Int32.TryParse(data[2], out day);
            Int32.TryParse(data[3], out startTime);
            Int32.TryParse(data[4], out endTime);
            
            if (string.Equals(data[5],"N"))
            {
                Int32.TryParse(data[6], out latitude);
            }
            if (string.Equals(data[5], "S"))
            {
                Int32.TryParse(data[6], out latitude);
                latitude = -latitude;
            }

            if (string.Equals(data[7], "E"))
            {
                Int32.TryParse(data[8], out longitude);
            }
            if (string.Equals(data[7], "W"))
            {
                Int32.TryParse(data[8], out longitude);
                longitude = -longitude;
            }
            
            brightness = data[9];
            Int32.TryParse(data[10], out area);
            xrayClass = data[11];
            Int32.TryParse(data[12], out intensity);
            if(intensity < 1)
            {
                intensity = 1;
            }

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        flares = GetFlareData();
    }

    public Dictionary<string, List<Flare>> GetFlareData()
    {
        Dropdown year = GameObject.Find("Year Selector").GetComponent<Dropdown>();
        Dictionary<string, List<Flare>> flareDict = new Dictionary<string, List<Flare>>();
        foreach (Dropdown.OptionData data in year.options)
        {
            for (int i = 0; i < 12; i++)
            {
                try
                {
                    List<Flare> flaredata = new List<Flare>();
                    string path = "Assets/Resources/" + data.text + "-data.txt";
                    string Line;
                    string[] flare;
                    StreamReader reader = new StreamReader(path, Encoding.Default);

                    using (reader)
                    {
                        do
                        {
                            Line = reader.ReadLine();

                            if (Line != null)
                            {
                                flare = Line.Split(' ');
                                if (int.Parse(flare[1]) - 1 > i)
                                {
                                    // Done w/ month
                                    break;
                                }
                                else if (int.Parse(flare[1]) - 1 == i) { 
                                    // Correct month
                                    flaredata.Add(new Flare(flare));
                                }
                            }
                        }
                        while (Line != null);

                        reader.Close();

                    }
                    flareDict.Add(data.text + i.ToString(), flaredata);
                }
                catch (Exception e)
                {
                    Debug.Log("error in loading file");
                    Debug.Log(e);
                }
            }
        }
        return flareDict;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
