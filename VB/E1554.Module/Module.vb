Imports System
Imports System.Collections.Generic

Imports DevExpress.ExpressApp
Imports System.Reflection
Imports DevExpress.ExpressApp.Model
Imports System.ComponentModel


Namespace E1554.Module
    Public Interface IModelListViewExt
        Inherits IModelNode

        <DefaultValue("")> _
        Property AdditionalCriteria() As String
    End Interface

    Public NotInheritable Partial Class E1554Module
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
