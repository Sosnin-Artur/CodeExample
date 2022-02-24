public interface IFactory
{    
}

public interface IFactory <out TResult> : IFactory
{
    TResult Create();
}

public interface IFactory<in TParameter1, out TResult> : IFactory
{
    TResult Create(TParameter1 p1);
}

public interface IFactory<in TParameter1, in TParameter2, out TResult> : IFactory
{
    TResult Create(TParameter1 p1, TParameter2 p2);
}

public interface IFactory<in TParameter1, in TParameter2, in TParameter3, out TResult> : IFactory
{
    TResult Create(TParameter1 p1, TParameter2 p2, TParameter3 p3);
}
