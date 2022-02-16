public interface IHealthView : IView
{    
    BaseHealthPresenter Presenter { get; set; }
    void TakeDamage(int value);

    void Heal(int value);
}