Imports Microsoft.VisualBasic
Imports DevExpress.ExpressApp.DC
Imports System
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Editors
Imports DevExpress.Data.Filtering

Namespace WinSample.Module
	Public Class ShowFilterDialogController
		Inherits WindowController
		Public Sub New()
			TargetWindowType = WindowType.Main
		End Sub
		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()
			AddHandler Frame.GetController(Of ShowNavigationItemController)().ShowNavigationItemAction.Execute, AddressOf ShowNavigationItemAction_Execute
		End Sub
		Private oldListView As View
		Private Sub ShowNavigationItemAction_Execute(ByVal sender As Object, ByVal e As SingleChoiceActionExecuteEventArgs)
			Dim showViewParameters As ShowViewParameters = e.ShowViewParameters
			If TypeOf showViewParameters.CreatedView Is ListView Then
				oldListView = showViewParameters.CreatedView
				Dim objectSpace As IObjectSpace = Application.CreateObjectSpace()
				Dim newViewFilterContainer As ViewFilterContainer = objectSpace.CreateObject(Of ViewFilterContainer)()
				newViewFilterContainer.ObjectType = oldListView.ObjectTypeInfo.Type
				Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Criteria = ? and ObjectTypeName = ?", (CType(oldListView.Model, IModelListViewExt)).AdditionalCriteria, newViewFilterContainer.ObjectType.FullName)
				newViewFilterContainer.Filter = objectSpace.FindObject(Of ViewFilterObject)(criteria)
				showViewParameters.CreatedView = Application.CreateDetailView(objectSpace, newViewFilterContainer)
				showViewParameters.CreatedView.Caption = String.Format("Filter for the {0} ListView", oldListView.Caption)
				showViewParameters.TargetWindow = TargetWindow.NewModalWindow
				Dim dialogCotnroller As DialogController = Application.CreateController(Of DialogController)()
				AddHandler dialogCotnroller.AcceptAction.Execute, AddressOf AcceptAction_Execute
				showViewParameters.Controllers.Add(dialogCotnroller)
			End If
		End Sub
		Private Sub AcceptAction_Execute(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs)
			Dim currentViewFilterContainer As ViewFilterContainer = CType(e.CurrentObject, ViewFilterContainer)
			CType(oldListView, ListView).CollectionSource.Criteria("ByViewFilterObject") = CriteriaEditorHelper.GetCriteriaOperator(currentViewFilterContainer.Criteria, currentViewFilterContainer.ObjectType, oldListView.ObjectSpace)
			CType(oldListView.Model, IModelListViewExt).AdditionalCriteria = currentViewFilterContainer.Criteria
			e.ShowViewParameters.CreatedView = oldListView
		End Sub
	End Class
End Namespace
