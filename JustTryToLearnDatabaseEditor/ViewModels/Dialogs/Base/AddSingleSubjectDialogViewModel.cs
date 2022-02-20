using System.Collections.Generic;
using System.Linq;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Models.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base
{
    public class AddSingleSubjectDialogViewModel<T> : ParameterizedDialogViewModelBase<ItemResult<T>, IEnumerable<T>>
    where T : NotifiedModel, INamedModel, new()
    {
        private IEnumerable<T> _items;
        
        public void AddNewItem(object parameter)
        {
            string name = parameter as string;
            
            Close(new ItemResult<T>(new T {ItemName = name}));
        }
        
        public bool CanAddNewClass(object parameter)
        {
            return _items.FirstOrDefault(c => c.ItemName == parameter as string) == null;
        }
        
        public override void Activate(IEnumerable<T> parameter)
        {
            _items = parameter;
        }
    }
}