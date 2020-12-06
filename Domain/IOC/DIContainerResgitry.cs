using Unity;

namespace Domain
{
    public sealed class DIContainerResgitry : IDIContainerResgitry
    {
        private static readonly IUnityContainer _container = new UnityContainer();

        // Explicit static constructor to tell C# compiler 
        // not to mark type as beforefieldinit - making it as singleton and thread safe
        static DIContainerResgitry() { }
        private DIContainerResgitry() { }

        public static IUnityContainer Container
        {
            get { return _container; }
        }

        public void Configure()
        {
            /* Register all interfaces and classes */
            //_container.RegisterType<>();
        }
    }
}
