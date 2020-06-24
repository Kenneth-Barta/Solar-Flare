using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlareLauncher : MonoBehaviour
{
    public GameObject simFlare;
    public float flareLifespan;

    private float t;
    private float lastFrameTime;
    private float lastFlareTime;

    // Start is called before the first frame update
    void Start()
    {
        lastFrameTime = 0;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        t = (float)Timer.timer;
        string currentYear = Timer.GetYear(t);
        if (Mathf.Abs(t - lastFrameTime) > 30) 
        {
            // Big jump, time picker used
            // Do nothing
        }
        else
        {
            double flareTime;    // Get time of each flare and convert to float time
            for (int i = 0; i < FlareDetail.flares[currentYear].Count; i++)
            {

                FlareDetail.Flare flare = FlareDetail.flares[currentYear][i];
                flareTime = TimePicker.MDYToFloatTime(flare.month - 1, flare.day, flare.year);
                int flareMinutes = flare.startTime % 100;     // 0000 time format
                int flareHours = (flare.startTime - flareMinutes) / 100;
                flareTime += flareMinutes;
                flareTime += flareHours * 60;
                if (flareTime > lastFrameTime && flareTime <= t)
                {
                    // Flare has occurred between last frame and this one.  Launch a flare.
                    GameObject tempFlare = Instantiate(simFlare, new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(flare.latitude, flare.longitude, 0)));
                     //tempFlare.GetComponent<ParticleSystem>().maxParticles = flare.intensity * 100;
                    ParticleSystem ps = tempFlare.GetComponent<ParticleSystem>();
                    var main = ps.main;
                    var sh = ps.shape;
                    sh.radius = flare.intensity/ 5;

                 if (flare.brightness.Equals("N")) {
                    main.startColor = new Color(230, 150, 0, 255);
                        }
                 if (flare.brightness.Equals("B"))
                    {
                    main.startColor = new Color(255, 200, 0, 255);
                 }
                    Destroy(tempFlare, flareLifespan);
                    lastFlareTime = Time.time;

                    // Also slow down time scale to get to earth in about 8.3 seconds
                    Timer.timeSpeed = 60 * 0.014F;
                    Orbit.speedMultiplier = .014F;
                }
                else if (flareTime > t)
                {
                    // Flare happens in the future.  Since flares sorted by time, can stop searching here.
                    break;
                }
                // Else keep searching
            }
            if (Time.time - flareLifespan > lastFlareTime)
            {
                // If no flare launched in a long time, revert timeSpeed to normal
                Timer.timeSpeed = 60;
                Orbit.speedMultiplier = 1;
            }
        }
        lastFrameTime = t;
    }
}
