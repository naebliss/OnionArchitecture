using Onion.Core;

namespace Onion.ApplicationServices.DungeonServices
{
    public interface IDungeonFactory
    {
        Dungeon Create(Hero hero, DungeonCreationOptions options);
    }
}