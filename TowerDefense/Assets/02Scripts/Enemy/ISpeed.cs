using System;

public interface ISpeed
{
    float speed { get; set; }
    float speedOrigin { get; set; }
    event Action<float> OnSpeedChanged;
}