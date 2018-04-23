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
	<NonPersistent> _
	Public Class ViewFilterContainer
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private _Filter As ViewFilterObject
		<DataSourceProperty("Filters"), ImmediatePostData> _
		Public Property Filter() As ViewFilterObject
			Get
				Return _Filter
			End Get
			Set(ByVal value As ViewFilterObject)
				SetPropertyValue("Filter", _Filter, value)
			End Set
		End Property
		Private _Filters As XPCollection(Of ViewFilterObject)
		<MemberDesignTimeVisibility(False)> _
		Public ReadOnly Property Filters() As XPCollection(Of ViewFilterObject)
			Get
				If _Filters Is Nothing AndAlso ObjectType IsNot Nothing Then
					_Filters = New XPCollection(Of ViewFilterObject)(Session, New BinaryOperator("ObjectTypeName", ObjectType.FullName))
				End If
				Return _Filters
			End Get
		End Property
		<CriteriaObjectTypeMember("ObjectType"), ImmediatePostData> _
		Public Property Criteria() As String
			Get
				If Filter IsNot Nothing Then
					Return Filter.Criteria
				Else
					Return String.Empty
				End If
			End Get
			Set(ByVal value As String)
				If Filter IsNot Nothing Then
					Filter.Criteria = value
				End If
			End Set
		End Property
		Private _ObjectType As Type
		<MemberDesignTimeVisibility(False)> _
		Public Property ObjectType() As Type
			Get
				Return _ObjectType
			End Get
			Set(ByVal value As Type)
				SetPropertyValue("ObjectType", _ObjectType, value)
			End Set
		End Property
	End Class
End Namespace
