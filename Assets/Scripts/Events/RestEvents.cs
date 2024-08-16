using System;

public class RestEvents
{
    public event Action<int, float> OnStartRest;
    public void StartRest(int restMins, float energyRestoreEffect)
    {
        if (OnStartRest != null)
        {
            OnStartRest(restMins, energyRestoreEffect);
        }
    }

    public event Action<float> OnStartCamp;
    public void StartCamp(float energyRestoreEffect)
    {
        if (OnStartCamp != null)
        {
            OnStartCamp(energyRestoreEffect);
        }
    }

    public event Action OnStartSleep;
    public void StartSleep()
    {
        if (OnStartSleep != null)
        {
            OnStartSleep();
        }
    }


}
