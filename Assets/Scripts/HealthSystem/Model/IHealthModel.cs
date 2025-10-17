using MVP;

public interface IHealthModel : IModel
{
    public ReactiveProperty<int> CurrentHealth { get; set; }
    public ReactiveProperty<int> MaxHealth { get; set; }
}