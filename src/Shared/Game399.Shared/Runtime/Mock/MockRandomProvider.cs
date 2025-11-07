using System.Collections.Generic;

public class MockRandomProvider : IRandomProvider
{
    private readonly Queue<float> _values = new();
    private readonly Queue<int> _ranges = new();

    public void QueueValue(float value) => _values.Enqueue(value);
    public void QueueRange(int value) => _ranges.Enqueue(value);

    public float Value()
        => _values.Count > 0 ? _values.Dequeue() : 0f;

    public int Range(int min, int max)
        => _ranges.Count > 0 ? _ranges.Dequeue() : min;
}