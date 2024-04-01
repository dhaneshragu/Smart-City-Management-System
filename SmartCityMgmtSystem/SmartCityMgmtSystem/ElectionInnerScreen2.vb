Imports System.Data.SqlClient
Imports System.IO
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Header
Imports MySql.Data.MySqlClient
Public Class ElectionInnerScreen2

    Dim lstviewItem As ListViewItem
    Dim lstviewText As ListViewItem
    Dim lstviewItemImageList As ImageList = New ImageList()

    Private Sub LoadData()
        Try
            ' Set the properties of the ListView
            ListView1.View = View.Details
            ListView1.Columns.Add("LOGO")
            ListView1.Columns.Add("DEPARTMENT")
            ListView1.Columns.Add("MINISTER") ' Add a new column for Minister
            ListView1.Columns(0).Width = 129
            ListView1.Columns(1).Width = 500 ' Adjust the width of Department column
            ListView1.Columns(2).Width = 350 ' Adjust the width of Minister column

            ' Create and set up the ImageList
            Dim lstviewItemImageList As New ImageList()
            lstviewItemImageList.ImageSize = New Size(120, 120)
            ListView1.SmallImageList = lstviewItemImageList

            Dim image_list() As String
            image_list = New String() {"icons8-active-directory-100.png", "icons8-student-center-100.png", "icons8-taj-mahal-100.png", "icons8-commodity-100.png", "icons8-transportation-100.png", "icons8-complain-100.png", "icons8-health-100.png", "icons8-live-video-on-100.png", "icons8-system-information-100.png"}

            Dim text_list() As String
            text_list = New String() {"Ministry of Labor & Employement", "Ministry of Finance", "Ministry of Power", "Ministry of Culture", " Ministry of Transport", "Ministry of Health", "Ministry of Education", "Ministry of Information & Broadcasting", "Ministry of Information Technology"}

            Dim minister_list() As String ' Add minister names
            minister_list = New String() {"John Doe", "Jane Smith", "Mike Johnson", "Chris Brown", "Emily White", "David Lee", "Jessica Taylor", "Matthew Davis", "Sarah Clark", "Kevin Miller"}

            'backend logic
            'Get connection from globals
            Dim Con = Globals.GetDBConnection()
            Dim reader As MySqlDataReader
            Dim cmd As MySqlCommand

            Try
                Con.Open()
            Catch ex As Exception
                MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            cmd = New MySqlCommand("SELECT ministries.ministry_name, users.name AS minister_name FROM ministries INNER JOIN users ON ministries.minister_uid = users.user_id", Con)
            reader = cmd.ExecuteReader
            ' Create a DataTable to store the data
            Dim dataTable As New DataTable()

            'Fill the DataTable with data from the SQL table
            dataTable.Load(reader)
            reader.Close()
            Con.Close()

            Dim text_index As Integer = 0

            For Each imageFileName As String In image_list
                Dim imagePath As String = Path.Combine(GetSolutionDirectory(), "SmartCityMgmtSystem\Resources", imageFileName)
                If File.Exists(imagePath) Then
                    Dim image As Image = Image.FromFile(imagePath)
                    lstviewItemImageList.Images.Add(image)

                    ' Create a ListViewItem with the image index
                    Dim listItem As New ListViewItem()
                    listItem.ImageIndex = lstviewItemImageList.Images.Count - 1
                    listItem.UseItemStyleForSubItems = False


                    ' Retrieve department and minister names from the dataTable
                    Dim departmentName As String = ""
                    Dim ministerName As String = ""
                    departmentName = dataTable.Rows(text_index)("ministry_name").ToString()
                    ministerName = dataTable.Rows(text_index)("minister_name").ToString()

                    ' Add Department and Minister as sub-items
                    Dim departmentSubItem = listItem.SubItems.Add(departmentName)
                    departmentSubItem.Font = New Font("Verdana", 14, FontStyle.Regular)
                    departmentSubItem.ForeColor = Color.White

                    Dim ministerSubItem = listItem.SubItems.Add(ministerName)
                    ministerSubItem.Font = New Font("Verdana", 14, FontStyle.Regular)
                    ministerSubItem.ForeColor = Color.White

                    text_index += 1

                    ' Add ListViewItem to ListView
                    ListView1.Items.Add(listItem)
                End If
            Next
        Catch ex As Exception
            ' Catching error
            MessageBox.Show("Error: " & ex.Message)
        End Try
    End Sub





    Private Function GetSolutionDirectory() As String
        Dim currentDirectory As String = Application.StartupPath

        ' Navigate up the directory tree until finding the .sln file
        Do Until Directory.GetFiles(currentDirectory, "*.sln").Length > 0
            ' If reached the root directory, return an empty string
            If Directory.GetParent(currentDirectory) Is Nothing Then
                Return ""
            End If
            currentDirectory = Directory.GetParent(currentDirectory).FullName
        Loop

        Return currentDirectory
    End Function

    Private Sub ElectionInnerScreen1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadData()

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class