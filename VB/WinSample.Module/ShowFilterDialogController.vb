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

		Private showNavigationItemController As ShowNavigationItemController
		Private oldListView As ListView

		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()
			showNavigationItemController = Frame.GetController(Of ShowNavigationItemController)()
			If showNavigationItemController IsNot Nothing Then
				AddHandler showNavigationItemController.ShowNavigationItemAction.Execute, AddressOf ShowNavigationItemAction_Execute
				AddHandler showNavigationItemController.CustomUpdateSelectedItem, AddressOf showNavigationItemController_CustomUpdateSelectedItem
			End If
		End Sub

		Private Sub ShowNavigationItemAction_Execute(ByVal sender As Object, ByVal e As SingleChoiceActionExecuteEventArgs)
			If TypeOf e.ShowViewParameters.CreatedView Is ListView Then
				oldListView = CType(e.ShowViewParameters.CreatedView, ListView)
				Dim objectSpace As IObjectSpace = Application.CreateObjectSpace()
				Dim newViewFilterContainer As ViewFilterContainer = objectSpace.CreateObject(Of ViewFilterContainer)()
				newViewFilterContainer.ObjectType = oldListView.ObjectTypeInfo.Type
				newViewFilterContainer.Filter = GetFilterObject(objectSpace, (CType(oldListView.Model, IModelListViewExt)).AdditionalCriteria, newViewFilterContainer.ObjectType)
				Dim filterDetailView As DetailView = Application.CreateDetailView(objectSpace, newViewFilterContainer)
				filterDetailView.Caption = String.Format("Filter for the {0} ListView", oldListView.Caption)
				filterDetailView.ViewEditMode = ViewEditMode.Edit
				e.ShowViewParameters.CreatedView = filterDetailView
				e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow
				Dim dialogCotnroller As DialogController = Application.CreateController(Of DialogController)()
				AddHandler dialogCotnroller.Accepting, AddressOf dialogCotnroller_Accepting
				AddHandler dialogCotnroller.Cancelling, AddressOf dialogCotnroller_Cancelling
				e.ShowViewParameters.Controllers.Add(dialogCotnroller)
			End If
		End Sub

		Private Sub dialogCotnroller_Accepting(ByVal sender As Object, ByVal e As DialogControllerAcceptingEventArgs)
			Dim currentViewFilterContainer As ViewFilterContainer = CType(e.AcceptActionArgs.CurrentObject, ViewFilterContainer)
			CType(oldListView.Model, IModelListViewExt).AdditionalCriteria = currentViewFilterContainer.Criteria
			oldListView.CollectionSource.Criteria("ByViewFilterObject") = CriteriaEditorHelper.GetCriteriaOperator(currentViewFilterContainer.Criteria, currentViewFilterContainer.ObjectType, oldListView.ObjectSpace)
			Application.MainWindow.SetView(oldListView)
			oldListView = Nothing
		End Sub

		Private Sub dialogCotnroller_Cancelling(ByVal sender As Object, ByVal e As EventArgs)
			oldListView = Nothing
		End Sub

		Private Function GetFilterObject(ByVal objectSpace As IObjectSpace, ByVal listViewCriteria As String, ByVal objectType As Type) As ViewFilterObject
			Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Criteria = ? and ObjectType = ?", listViewCriteria, objectType)
			Dim filterObject As ViewFilterObject = objectSpace.FindObject(Of ViewFilterObject)(criteria)
			If filterObject Is Nothing Then
				filterObject = objectSpace.CreateObject(Of ViewFilterObject)()
				filterObject.ObjectType = objectType
				filterObject.Criteria = listViewCriteria
				filterObject.FilterName = "Default"
			End If
			Return filterObject
		End Function

		Private Sub showNavigationItemController_CustomUpdateSelectedItem(ByVal sender As Object, ByVal e As CustomUpdateSelectedItemEventArgs)
			If oldListView IsNot Nothing Then
				e.ProposedSelectedItem = showNavigationItemController.FindNavigationItemByViewShortcut(oldListView.CreateShortcut())
				e.Handled = True
			End If
		End Sub

		Protected Overrides Sub OnDeactivated()
			MyBase.OnDeactivated()
			If showNavigationItemController IsNot Nothing Then
				RemoveHandler showNavigationItemController.ShowNavigationItemAction.Execute, AddressOf ShowNavigationItemAction_Execute
				RemoveHandler showNavigationItemController.CustomUpdateSelectedItem, AddressOf showNavigationItemController_CustomUpdateSelectedItem
				showNavigationItemController = Nothing
			End If
			oldListView = Nothing
		End Sub
	End Class
End Namespace
