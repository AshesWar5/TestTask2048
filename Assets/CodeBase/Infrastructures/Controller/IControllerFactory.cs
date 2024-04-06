namespace CodeBase.Infrastructures.Controller
{
    public interface IControllerFactory
    {
        TController Create<TController, TModel, TView>(TModel model, TView view) 
            where TController : Controller<TModel, TView>
            where TView : View.View;
    }
}