namespace Onion.ApplicationServices.DungeonServices
{
    public class DungeonCreationOptions
    {
        public Difficulty MonsterDifficulty { get; set; }
        public int NrOfMonsters { get; set; }
        public Difficulty EndBossDifficulty { get; set; }
    }
}