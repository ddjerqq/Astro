namespace Domain.ValueObjects;

public record ItemQuality(float Value, bool statTrak)
{
    float Value { get; set; } = Value;
    private bool IsStatTrack { get; set; } = statTrak;
}