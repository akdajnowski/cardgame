using Adic;

public class DialogContextRoot : ContextRoot
{
    public override void Init()
    {

    }

    public override void SetupContainers()
    {
        var container = AddContainer<InjectionContainer>();

        //Register any extensions the container may use.
       // container.RegisterExtension<UnityBindingContainerExtension>()
        //        .Bind<DialogEngine>().ToSingleton();
    }
}
