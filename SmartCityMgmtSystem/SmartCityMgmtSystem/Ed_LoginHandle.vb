Imports MySql.Data.MySqlClient

Public Class Ed_LoginHandle
    Public Sub GetEdProfileByUserID(ByVal userID As Integer)
        ' Create a Profile instance to store the result
        Dim profile As New Ed_GlobalDashboard.Profile()

        ' Get connection from globals
        Dim Con = Globals.GetDBConnection()

        Con.Open()

            ' Check if an entry corresponding to the userID exists in the ed_profile table
            Dim query As String = "SELECT * FROM ed_profile WHERE Ed_User_ID = @userID"
            Dim cmd As New MySqlCommand(query, Con)
            cmd.Parameters.AddWithValue("@userID", userID)

            Dim reader As MySqlDataReader = cmd.ExecuteReader()

            If reader.Read() Then
                ' Populate the Profile fields from the database query result
                If Not reader.IsDBNull(reader.GetOrdinal("Ed_User_ID")) Then
                    profile.Ed_User_ID = reader.GetInt32(reader.GetOrdinal("Ed_User_ID"))
                End If

                If Not reader.IsDBNull(reader.GetOrdinal("Ed_User_Type")) Then
                    profile.Ed_User_Type = CType([Enum].Parse(GetType(Ed_GlobalDashboard.UserType), reader.GetString(reader.GetOrdinal("Ed_User_Type"))), Ed_GlobalDashboard.UserType)
                End If

                If Not reader.IsDBNull(reader.GetOrdinal("Ed_User_Role")) Then
                    profile.Ed_User_Role = CType([Enum].Parse(GetType(Ed_GlobalDashboard.UserRole), reader.GetString(reader.GetOrdinal("Ed_User_Role"))), Ed_GlobalDashboard.UserRole)
                End If

                If Not reader.IsDBNull(reader.GetOrdinal("Ed_Affiliation")) Then
                    profile.Ed_Affiliation = reader.GetInt32(reader.GetOrdinal("Ed_Affiliation"))
                End If

                If Not reader.IsDBNull(reader.GetOrdinal("Ed_Name")) Then
                    profile.Ed_Name = reader.GetString(reader.GetOrdinal("Ed_Name"))
                End If

                If Not reader.IsDBNull(reader.GetOrdinal("Ed_DOB")) Then
                    profile.Ed_DOB = reader.GetDateTime(reader.GetOrdinal("Ed_DOB"))
                End If

                If Not reader.IsDBNull(reader.GetOrdinal("Ed_Class")) Then
                    profile.Ed_Class = reader.GetInt32(reader.GetOrdinal("Ed_Class"))
                End If

                If Not reader.IsDBNull(reader.GetOrdinal("Ed_Sem")) Then
                    profile.Ed_Sem = reader.GetInt32(reader.GetOrdinal("Ed_Sem"))
                End If

                If Not reader.IsDBNull(reader.GetOrdinal("Ed_LastEduQlf")) Then
                    profile.Ed_LastEduQlf = reader.GetString(reader.GetOrdinal("Ed_LastEduQlf"))
                End If

            Else
                ' If the userID is not found in ed_profile, extract data from users table
                reader.Close()

                Dim usersQuery As String = "SELECT user_id, name, dob, occupation FROM users WHERE user_id = @userID"
                cmd = New MySqlCommand(usersQuery, Con)
                cmd.Parameters.AddWithValue("@userID", userID)
                reader = cmd.ExecuteReader()

                If reader.Read() Then
                ' Populate profile with data from users table
                If Not reader.IsDBNull(reader.GetOrdinal("user_id")) Then
                    profile.Ed_User_ID = reader.GetInt32(reader.GetOrdinal("user_id"))
                End If
                If Not reader.IsDBNull(reader.GetOrdinal("name")) Then
                    profile.Ed_Name = reader.GetString(reader.GetOrdinal("name"))
                End If
                If Not reader.IsDBNull(reader.GetOrdinal("dob")) Then
                    profile.Ed_DOB = reader.GetDateTime(reader.GetOrdinal("dob"))
                End If
                profile.Ed_User_Type = Ed_GlobalDashboard.UserType.Student
                    ' You need to determine how to set other fields like Ed_User_Type, Ed_User_Role, etc.
                    ' For now, I'm leaving them blank.
                End If

                ' Insert the new profile into ed_profile table
                reader.Close()
                Dim insertQuery As String = "INSERT INTO ed_profile (Ed_User_ID, Ed_Name, Ed_DOB) VALUES (@userID, @name, @dob)"
                cmd = New MySqlCommand(insertQuery, Con)
                cmd.Parameters.AddWithValue("@userID", profile.Ed_User_ID)
                cmd.Parameters.AddWithValue("@name", profile.Ed_Name)
                cmd.Parameters.AddWithValue("@dob", profile.Ed_DOB)
                cmd.ExecuteNonQuery()
            End If

        reader.Close()
        Con.Close()

        ' Set the profile in Ed_GlobalDashboard
        Ed_GlobalDashboard.Ed_Profile = profile
    End Sub
End Class
