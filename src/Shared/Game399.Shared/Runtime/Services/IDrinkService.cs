namespace Game399.Shared.Services
{
    public interface IDrinkService
    {
        bool CanMakeDrink(params DrinkComponent[] components);
    }
}