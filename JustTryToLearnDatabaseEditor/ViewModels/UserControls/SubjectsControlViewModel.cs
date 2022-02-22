using System.Threading.Tasks;
using JustTryToLearnDatabaseEditor.Models;
using JustTryToLearnDatabaseEditor.Services.Database;
using JustTryToLearnDatabaseEditor.Services.Interfaces;
using JustTryToLearnDatabaseEditor.ViewModels.Dialogs.Subjects;
using JustTryToLearnDatabaseEditor.ViewModels.UserControls.Utils;

namespace JustTryToLearnDatabaseEditor.ViewModels.UserControls
{
    public class SubjectsControlViewModel : RelativeItemUserControlViewModel<Subject, School>
    {
        private readonly ISubjectService _subjectService;
        
        private new const string AddItemViewModelName = nameof(AddSubjectDialogViewModel);
        private new const string EditItemViewModelName = nameof(EditSubjectDialogViewModel);

        private const string DeleteMessage =
            "Видалення цього предмета призведе до видалення всіх питань які залежать від нього. " +
            "Дійсно бажаєте видалити?";


        public SubjectsControlViewModel(IDialogService dialogService, ISubjectService subjectService) : 
            base(dialogService, DeleteMessage, AddItemViewModelName, EditItemViewModelName)
        {
            _subjectService = subjectService;
        }

        protected override void OnItemAdded(Subject item, School parent)
        {
            item.SetParent(parent);

            (parent as IContainItems<Subject>).AddItem(item);
            _subjectService.InsertSubjectAsync(item);
        }

        protected override void OnItemEditRequested(Subject item, Subject newItem, School parent)
        {
            item.Name = newItem.Name;
            _subjectService.UpdateSubject(item);
        }

        protected override void OnItemRemoved(Subject item, School parent)
        {
            _subjectService.RemoveSubject(item);
        }
    }
}
