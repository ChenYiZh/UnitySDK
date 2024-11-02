public interface IUpdate
{
    //bool enabled { get; }

    void OnUpdate();

    void OnLateUpdate();

    void OnFixedUpdate();

}