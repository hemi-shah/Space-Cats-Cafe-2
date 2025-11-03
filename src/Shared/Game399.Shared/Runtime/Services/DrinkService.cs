using Game399.Shared.Diagnostics;

namespace Game399.Shared.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IGameLog _log;

        public DrinkService(IGameLog log)
        {
            _log = log;
        }
        
        public bool CanMakeDrink(params DrinkComponent[] components)
        {
            if (components.Length == 0)
            {
                _log.Warn("Can't make drink without any components");
                return false;
            }

            return true;
        }
    }
}