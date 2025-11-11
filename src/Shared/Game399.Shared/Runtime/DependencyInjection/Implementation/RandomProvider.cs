public class RandomProvider : IRandomProvider
{
    public float Value() => UnityEngine.Random.value;
    public int Range(int min, int max) => UnityEngine.Random.Range(min, max);
}