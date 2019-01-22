using Onion.Core;

namespace Onion.ApplicationServices.CombatServices
{
    public class CombatEngine : ICombatEngine
    {
        public ICombattant Fight(ICombattant fighter1, ICombattant fighter2)
        {
            int round = 0;

            while(fighter1.IsAlive() && fighter2.IsAlive() && round <= 10)
            {
                fighter2.HandleAttacked(fighter1);

                if (!fighter2.IsAlive())
                    return fighter1;

                fighter1.HandleAttacked(fighter2);

                if (!fighter1.IsAlive())
                    return fighter2;
            }

            return null;
        }
    }
}
