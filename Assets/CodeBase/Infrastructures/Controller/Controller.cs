namespace CodeBase.Infrastructures.Controller
{
    public abstract class Controller<TModel, TView> : IController
        where TView : View.View
    {
        protected TModel _model;
        protected TView _view;
        
        public void SetModel(TModel model)
        {
            _model = model;
            OnSetModel();
        }
        
        public void SetView(TView view)
        {
            _view = view;
            OnSetView();
        }

        public void Initialize()
        {
            _view.Initialize();
            InitializeInternal();
        }
        
        public void Dispose()
        {
            _view.Dispose();
            DisposeInternal();
        }
        
        protected virtual void InitializeInternal() { }
        protected virtual void DisposeInternal() { }
        protected virtual void OnSetModel() { }
        protected virtual void OnSetView() { }
    }
}