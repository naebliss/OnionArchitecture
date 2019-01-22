using Onion.Core;
using System;
using System.Linq;

namespace Onion.ApplicationServices.DungeonServices
{
    public class DungeonFactory : IDungeonFactory
    {
        public Dungeon Create(Hero hero, DungeonCreationOptions options)
        {
            if (hero == null)
                throw new ArgumentNullException("hero");

            var dungeon = new Dungeon
            {
                Hero = hero,
                Monsters = Enumerable.Range(0, options.NrOfMonsters).Select(i => CreateMonster(options.MonsterDifficulty)).ToList(),
                EndBoss = CreateMonster(options.EndBossDifficulty)
            };

            return dungeon;
        }

        private Monster CreateMonster(Difficulty monsterDifficulty)
        {
            switch (monsterDifficulty)
            {
                case Difficulty.Simple: return new Ork();
                case Difficulty.Medium: return new Skeleton();
                case Difficulty.Hard: return new Rabbit();
                default: return new Ork();
            }
        }
    }
}