using Onion.ApplicationServices.CombatServices;
using Onion.ApplicationServices.DungeonServices;
using Onion.Core;
using Onion.DomainServices;
using System;
using System.Text;

namespace Onion.ApplicationServices.GameServices
{
    public class GameEngine : IGameEngine
    {
        private readonly ICombatEngine combatEngine;
        private readonly IRepository<Dungeon> dungeonRepository;
        private readonly IDungeonFactory dungeonFactory;
        public Dungeon Dungeon { get; private set; }

        public GameEngine(ICombatEngine combatEngine, IRepository<Dungeon> dungeonRepository, IDungeonFactory dungeonFactory)
        {
            this.combatEngine = combatEngine;
            this.dungeonRepository = dungeonRepository;
            this.dungeonFactory = dungeonFactory;
        }

        public Guid NewGame(Hero hero, DungeonCreationOptions options)
        {
            Dungeon = dungeonFactory.Create(hero, options);
            dungeonRepository.Create(Dungeon);

            return Dungeon.Id;
        }

        public void LoadGame(Guid dungeonId)
        {
            Dungeon = dungeonRepository.Find(dungeonId);
        }

        public void DeleteSaveGame(Guid dungeonId)
        {
            var tmp = dungeonRepository.Find(dungeonId);
            dungeonRepository.Delete(tmp);
            Dungeon = null;
        }

        public void SaveProgress() {
            if (Dungeon == null)
                return;

            dungeonRepository.Update(Dungeon);
        }

        public StringBuilder Play()
        {
            StringBuilder questLog = new StringBuilder();

            foreach (var monster in Dungeon.Monsters)
            {
                var winner = combatEngine.Fight(Dungeon.Hero, monster);
                
                if (winner == null)
                {
                    questLog.AppendLine($"Fight between {Dungeon.Hero.Name} and {monster.Name} was a tie, the hero returns home to get stronger");
                    return questLog;
                }

                if (winner == monster)
                {
                    questLog.AppendLine($"Our hero {Dungeon.Hero.Name} fought bravely against {monster.Name} but was defeated, Runaway!");
                    return questLog;
                }
                questLog.AppendLine($"Our hero {Dungeon.Hero.Name} defeated {monster.Name}, Excelsior!");
            }

            questLog.AppendLine($"After our hero {Dungeon.Hero.Name} defeated all monsters he faces his final challenge, {Dungeon.EndBoss.Name}!");

            var bossfightWinner = combatEngine.Fight(Dungeon.Hero, Dungeon.EndBoss);

            if (bossfightWinner == null)
            {
                questLog.AppendLine($"Fight between {Dungeon.Hero.Name} and {Dungeon.EndBoss.Name} was a tie, the hero has met his match");
                return questLog;
            }

            if (bossfightWinner == Dungeon.EndBoss)
            {
                questLog.AppendLine($"Our hero {Dungeon.Hero.Name} fought bravely against {Dungeon.EndBoss.Name} but was defeated, he died");
                return questLog;
            }
            questLog.AppendLine($"Our hero {Dungeon.Hero.Name} defeated {Dungeon.EndBoss.Name}, Excelsior!");

            return questLog;
        }
    }
}