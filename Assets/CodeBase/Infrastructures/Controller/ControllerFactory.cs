using Zenject;

namespace CodeBase.Infrastructures.Controller
{
    public sealed class ControllerFactory : IControllerFactory
    {
        private readonly DiContainer _container;

        public ControllerFactory(DiContainer container)
        {
            _container = container;
        }

        public TController Create<TController, TModel, TView>(TModel model, TView view)
            where TController : Controller<TModel, TView>
            where TView : View.View
        {
            var controller = _container.Resolve<TController>();
            controller.SetView(view);
            controller.SetModel(model);
            controller.Initialize();
            return controller;
        }
    }
}