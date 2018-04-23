Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Imports DevExpress.ExpressApp
Imports System.Reflection
Imports DevExpress.ExpressApp.Model


Namespace WinSample.Module
	Public Interface IModelListViewExt
	Inherits IModelNode
		Property AdditionalCriteria() As String
	End Interface

	Public NotInheritable Partial Class WinSampleModule
		Inherits ModuleBase
		Public Sub New()
			InitializeComponent()
		End Sub
		Public Overrides Sub ExtendModelInterfaces(ByVal extenders As ModelInterfaceExtenders)
			MyBase.ExtendModelInterfaces(extenders)
			extenders.Add(Of IModelListView, IModelListViewExt)()
		End Sub
	End Class
End Namespace
