using System;

public class SaveEvents
{
    public event Action OnNewGame;
    public void NewGame()
    {
        if (OnNewGame != null)
        {
            OnNewGame();
        }
    }

    public event Action OnSaveGame;
    public void SaveGame()
    {
        if (OnSaveGame != null)
        {
            OnSaveGame();
        }
    }

    public event Action OnLoadGame;
    public void LoadGame()
    {
        if (OnLoadGame != null)
        {
            OnLoadGame();
        }
    }
}
