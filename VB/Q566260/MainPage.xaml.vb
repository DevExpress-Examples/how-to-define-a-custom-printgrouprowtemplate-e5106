Imports Microsoft.VisualBasic
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Printing
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.Linq
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes

Namespace Q566260
	Partial Public Class MainPage
		Inherits UserControl
		Public Sub New()
			InitializeComponent()

			Dim customers As New ObservableCollection(Of Customer)()
			For i As Integer = 1 To 29
				customers.Add(New Customer() With {.ID = i, .Name = "Name" & i, .SomeProperty = "Value" & i})
			Next i
			grid.ItemsSource = customers
		End Sub

		Private Sub Button_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim preview As New DocumentPreviewWindow()
			Dim link As New PrintableControlLink(TryCast(grid.View, DevExpress.Xpf.Printing.IPrintableControl))
			link.ExportServiceUri = "../ExportService1.svc"
			Dim model As New LinkPreviewModel(link)
			preview.Model = model
			link.CreateDocument(True)
			preview.ShowDialog()
		End Sub
	End Class
	Public Class Customer
		Private privateID As Integer
		Public Property ID() As Integer
			Get
				Return privateID
			End Get
			Set(ByVal value As Integer)
				privateID = value
			End Set
		End Property

		Private privateName As String
		Public Property Name() As String
			Get
				Return privateName
			End Get
			Set(ByVal value As String)
				privateName = value
			End Set
		End Property
		Private privateSomeProperty As String
		Public Property SomeProperty() As String
			Get
				Return privateSomeProperty
			End Get
			Set(ByVal value As String)
				privateSomeProperty = value
			End Set
		End Property
	End Class

	Public Class MyConverter
		Implements IValueConverter

		Public Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.Convert
			Dim column As GridColumn = TryCast(value, GridColumn)
			If column.FieldName = "Name" Then
				Return New SolidColorBrush(Colors.Red)
			End If
			Return New SolidColorBrush(Colors.Green)

		End Function

		Public Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
			Return value
		End Function
	End Class
End Namespace
