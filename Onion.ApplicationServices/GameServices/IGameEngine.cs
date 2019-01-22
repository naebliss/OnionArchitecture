using Onion.ApplicationServices.DungeonServices;
using Onion.Core;
using System;
using System.Text;

namespace Onion.ApplicationServices.GameServices
{
    public interface IGameEngine
    {
        Dungeon Dungeon {get;}
        Guid NewGame(Hero hero, DungeonCreationOptions options);
        void LoadGame(Guid dungeonId);
        void DeleteSaveGame(Guid dungeonId);
        void SaveProgress();
        StringBuilder Play();
    }
}
