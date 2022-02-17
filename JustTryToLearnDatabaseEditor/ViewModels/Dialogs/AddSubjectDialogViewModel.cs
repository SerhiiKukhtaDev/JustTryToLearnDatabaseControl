using DynamicData.Binding;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Base.DialogResults.Base;

namespace JustTryToLearnDatabaseEditor.ViewModels.Dialogs
{
    public class AddSubjectDialogViewModel : 
        ParameterizedDialogViewModelBase<ItemResult<Subject>, IObservableCollection<Subject>>
    {
        private IObservableCollection<Subject> _subjects;

        public void AddNewSubject(object parameter)
        {
            string name = parameter as string;
            
            Close(new ItemResult<Subject>(false, new Subject {Name = name}));
        }

        public bool CanAddNewSubject(object parameter)
        {
            return true;
        }
        
        public AddSubjectDialogViewModel()
        {
            
        }


        public override void Activate(IObservableCollection<Subject> parameter)
        {
            _subjects = parameter;
        }
    }
}
