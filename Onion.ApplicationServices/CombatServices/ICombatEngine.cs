using Onion.Core;

namespace Onion.ApplicationServices.CombatServices
{
    public interface ICombatEngine
    {
        ICombattant Fight(ICombattant fighter1, ICombattant fighter2);
    }
}
