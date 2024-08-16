using System;

public class UIEvents
{
    public event Action OnMenuOpen;
    public void OpenMenu()
    {
        if (OnMenuOpen != null)
        {
            OnMenuOpen();
        }
    }
}
