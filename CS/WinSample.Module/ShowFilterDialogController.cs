using DevExpress.ExpressApp.DC;
using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.Data.Filtering;

namespace WinSample.Module {
    public class ShowFilterDialogController : WindowController {
        public ShowFilterDialogController() {
            TargetWindowType = WindowType.Main;
        }
        protected override void OnActivated() {
            base.OnActivated();
            Frame.GetController<ShowNavigationItemController>().ShowNavigationItemAction.Execute += new SingleChoiceActionExecuteEventHandler(ShowNavigationItemAction_Execute);
        }
        View oldListView;
        void ShowNavigationItemAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e) {
            ShowViewParameters showViewParameters = e.ShowViewParameters;
            if (showViewParameters.CreatedView is ListView) {
                oldListView = showViewParameters.CreatedView;
                IObjectSpace objectSpace = Application.CreateObjectSpace();
                ViewFilterContainer newViewFilterContainer = objectSpace.CreateObject<ViewFilterContainer>();
                newViewFilterContainer.ObjectType = oldListView.ObjectTypeInfo.Type;
                CriteriaOperator criteria = CriteriaOperator.Parse("Criteria = ? and ObjectTypeName = ?", ((IModelListViewExt)oldListView.Model).AdditionalCriteria, newViewFilterContainer.ObjectType.FullName);
                newViewFilterContainer.Filter = objectSpace.FindObject<ViewFilterObject>(criteria);
                showViewParameters.CreatedView = Application.CreateDetailView(objectSpace, newViewFilterContainer);
                showViewParameters.CreatedView.Caption = String.Format("Filter for the {0} ListView", oldListView.Caption);
                showViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                DialogController dialogCotnroller = Application.CreateController<DialogController>();
                dialogCotnroller.AcceptAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(AcceptAction_Execute);
                showViewParameters.Controllers.Add(dialogCotnroller);
            }
        }
        void AcceptAction_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e) {
            ViewFilterContainer currentViewFilterContainer = (ViewFilterContainer)e.CurrentObject;
            ((ListView)oldListView).CollectionSource.Criteria["ByViewFilterObject"] = CriteriaEditorHelper.GetCriteriaOperator(currentViewFilterContainer.Criteria, currentViewFilterContainer.ObjectType, oldListView.ObjectSpace);
            ((IModelListViewExt)oldListView.Model).AdditionalCriteria = currentViewFilterContainer.Criteria;
            e.ShowViewParameters.CreatedView = oldListView;
        }
    }
}
