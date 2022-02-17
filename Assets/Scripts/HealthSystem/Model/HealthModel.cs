public class HealthModel : IHealthModel
{       
    public ReactiveProperty<int> CurrentHealth { get; set; }    
    public ReactiveProperty<int> MaxHealth { get; set; }
}
