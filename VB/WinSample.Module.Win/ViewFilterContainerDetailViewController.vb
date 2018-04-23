Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Win.Editors

Namespace WinSample.Module
	Public Class ViewFilterContainerDetailViewController
		Inherits ViewController
		Public Sub New()
			TargetObjectType = GetType(ViewFilterContainer)
			TargetViewType = ViewType.DetailView
		End Sub
		Protected Overrides Overloads Sub OnViewControlsCreated()
			MyBase.OnViewControlsCreated()
			Dim editor As LookupPropertyEditor = CType((CType(View, DetailView)).FindItem("Filter"), LookupPropertyEditor)
			AddHandler editor.Control.Popup, AddressOf Control_Popup
			AddHandler editor.Control.Closed, AddressOf Control_Closed
		End Sub
		Private Sub Control_Closed(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Controls.ClosedEventArgs)
			If (CType(sender, LookupEdit)).Frame IsNot Nothing Then
				RemoveHandler (CType(sender, LookupEdit)).Frame.GetController(Of NewObjectViewController)().ObjectCreated, AddressOf ViewFilterContainerDetailViewController_ObjectCreated
			End If
		End Sub
		Private Sub Control_Popup(ByVal sender As Object, ByVal e As EventArgs)
			AddHandler (CType(sender, LookupEdit)).Frame.GetController(Of NewObjectViewController)().ObjectCreated, AddressOf ViewFilterContainerDetailViewController_ObjectCreated
		End Sub
		Private Sub ViewFilterContainerDetailViewController_ObjectCreated(ByVal sender As Object, ByVal e As ObjectCreatedEventArgs)
			If TypeOf e.CreatedObject Is ViewFilterObject Then
				Dim newViewFilterObject As ViewFilterObject = CType(e.CreatedObject, ViewFilterObject)
				newViewFilterObject.ObjectType = (CType(View.CurrentObject, ViewFilterContainer)).ObjectType
			End If
		End Sub
	End Class
End Namespace
