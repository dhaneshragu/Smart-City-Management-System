Imports System.Data.SqlClient
Public Class HomePage
    Public Property uid As Integer = -1
    Public Property u_name As String = "Hello"
    Public mainForm As Form
    Public Sub SetMainForm(ByRef parentForm As Form)
        mainForm = parentForm
    End Sub
    Private Sub ed_dept_Click(sender As Object, e As EventArgs) Handles ed_dept.Click
        Dim ed = Ed_GlobalDashboard
        ed.userID = uid
        ed.userName = u_name
        ed.Show()
        Me.ParentForm.Close()
        Me.Close()
    End Sub

    Private Sub electionDept_Click(sender As Object, e As EventArgs) Handles electionDept.Click
        Dim elec = New ElectionDashboard With {
            .uid = uid,
            .u_name = u_name
        }
        elec.Show()
        Me.ParentForm.Close()
        Me.Close()
    End Sub

    Private Sub tranDept_Click(sender As Object, e As EventArgs) Handles tranDept.Click
        Dim transport = New TransportationDashboard With {
            .uid = uid,
            .u_name = u_name
        }
        transport.Show()
        Me.ParentForm.Close()
        Me.Close()
    End Sub

    Private Sub event_dept_Click(sender As Object, e As EventArgs) Handles event_dept.Click
        Dim fest = New EventDashboard() With {
            .uid = uid,
            .u_name = u_name
        }
        fest.Show()
        Me.ParentForm.Close()
        Me.Close()
    End Sub

    Private Sub lib_dept_Click(sender As Object, e As EventArgs) Handles lib_dept.Click

        If uid = 9 Then
            Dim library = New lib_adminDash()
            library.Show()
            Me.ParentForm.Close()
            Me.Close()
        Else
            Dim library = New lib_dash() With {
            .uid = uid,
            .u_name = u_name
            }
            library.Show()
            Me.ParentForm.Close()
            Me.Close()
        End If


    End Sub

    Private Sub bankDept_Click(sender As Object, e As EventArgs) Handles bankDept.Click
        Dim bank = New BankingLogin() With {
                .uid = uid
            }
        bank.Show()
        Me.ParentForm.Close()
        Me.Close()
    End Sub

    Private Sub healthDept_Click(sender As Object, e As EventArgs) Handles healthDept.Click
        Dim health = New Healthcare_homepage() With {
            .uid = uid,
            .u_name = u_name
        }
        health.Show()
        Me.ParentForm.Close()
        Me.Close()
    End Sub

    Private Sub HomePage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'MessageBox.Show(uid.ToString + " " + u_name)
    End Sub

    Private Sub Service_dept_Click(sender As Object, e As EventArgs) Handles Service_dept.Click
        Dim service_portal = New Service_Portal() With {
            .uid = uid,
            .u_name = u_name
        }
        service_portal.Show()
        Me.ParentForm.Close()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim employment_portal = New Employment_portal() With {
            .uid = uid,
            .u_name = u_name
        }
        employment_portal.Show()
        Me.ParentForm.Close()
        Me.Close()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If uid >= 552 AndAlso uid <= 556 Then
            Dim Complaints_admin_dashboard = New Complaints_admin_dashboard() With {
            .uid = uid,
            .u_name = u_name
        }
            Complaints_admin_dashboard.Show()
            Me.ParentForm.Close()
            Me.Close()

        Else
            Dim Complaint_User_Dashboard = New Complaint_User_Dashboard() With {
            .uid = uid,
            .u_name = u_name
        }
            Complaint_User_Dashboard.Show()
            Me.ParentForm.Close()
            Me.Close()
        End If

    End Sub
End Class