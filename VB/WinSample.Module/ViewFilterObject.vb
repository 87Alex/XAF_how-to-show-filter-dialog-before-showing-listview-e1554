Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo
Imports DevExpress.ExpressApp.Editors
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports DevExpress.ExpressApp
Imports DevExpress.Data.Filtering

Namespace WinSample.Module
	<DefaultProperty("FilterName")> _
	Public Class ViewFilterObject
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private _ObjectTypeName As String
		<MemberDesignTimeVisibility(False)> _
		Public Property ObjectTypeName() As String
			Get
				Return _ObjectTypeName
			End Get
			Set(ByVal value As String)
				SetPropertyValue(Of String)("ObjectTypeName", _ObjectTypeName, value)
			End Set
		End Property
		<NonPersistent, MemberDesignTimeVisibility(False)> _
		Public Property ObjectType() As Type
			Get
				If ObjectTypeName IsNot Nothing Then
					Return XafTypesInfo.Instance.FindTypeInfo(ObjectTypeName).Type
				Else
					Return Nothing
				End If
			End Get
			Set(ByVal value As Type)
				Dim stringValue As String
				If value Is Nothing Then
					stringValue = Nothing
				Else
					stringValue = value.FullName
				End If
				Dim savedObjectTypeName As String = ObjectTypeName
				Try
					If stringValue <> ObjectTypeName Then
						ObjectTypeName = stringValue
					End If
				Catch e1 As Exception
					ObjectTypeName = savedObjectTypeName
				End Try
				Criteria = String.Empty
			End Set
		End Property
		Private _Criteria As String
		<CriteriaObjectTypeMember("ObjectType")> _
		Public Property Criteria() As String
			Get
				Return _Criteria
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Criteria", _Criteria, value)
			End Set
		End Property
		Private _FilterName As String
		Public Property FilterName() As String
			Get
				Return _FilterName
			End Get
			Set(ByVal value As String)
				SetPropertyValue("FilterName", _FilterName, value)
			End Set
		End Property
	End Class
End Namespace
