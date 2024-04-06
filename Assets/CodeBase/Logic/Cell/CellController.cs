using CodeBase.Infrastructures.Controller;
using CodeBase.Logic.Field;
using UnityEngine;

namespace CodeBase.Logic.Cell
{
    public sealed class CellController : Controller<CellModel, CellView>
    {
        private readonly GameFieldConfig _fieldConfig;

        public CellController(GameFieldConfig fieldConfig)
        {
            _fieldConfig = fieldConfig;
        }
        
        protected override void InitializeInternal()
        {
            _model.ChangeModel += OnUpdateCell;
        }

        protected override void DisposeInternal()
        {
            _model.ChangeModel -= OnUpdateCell;
        }

        protected override void OnSetModel()
        {
            OnUpdateCell();
        }

        private void OnUpdateCell()
        {
            string value = _model.IsEmpty ? string.Empty : _model.Point.ToString();
            Color colorText = _model.Value <= 2 ? _fieldConfig.CellDarkColor :
                _fieldConfig.CellLightColor;
            Color colorCell = _fieldConfig.ColorsCells[_model.Value];
            _view.SetValue(value, colorText);
            _view.SetColor(colorCell);
        }
    }
}