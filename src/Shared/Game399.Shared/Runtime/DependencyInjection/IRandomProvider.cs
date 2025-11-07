public interface IRandomProvider
{
    float Value();
    int Range(int min, int max);
}