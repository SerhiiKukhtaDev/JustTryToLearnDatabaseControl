using System.Collections.ObjectModel;
using DynamicData.Binding;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Classes
{
    public class AddClassDialogViewModel : 
        ParameterizedDialogViewModelBase<ItemResult<Class>, ObservableCollection<Class>>
    {
        private ObservableCollection<Class> _classes;

        public void AddNewClass(object parameter)
        {
            string name = parameter as string;
            
            Close(new ItemResult<Class>(new Class {Name = name}));
        }

        public bool CanAddNewSubject(object parameter)
        {
            return true;
        }
        
        public AddClassDialogViewModel()
        {
            
        }

        public override void Activate(ObservableCollection<Class> parameter)
        {
            _classes = parameter;
        }
    }
}
