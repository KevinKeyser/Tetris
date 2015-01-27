using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TetrisConnection
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TetrisConnection : ITetrisConnection
    {
        SqlConnection connection = new SqlConnection("Server=KEVIN-PC\\SQLEXPRESS; Database=Tetris; user=TetrisUser; password=tetris");


        public int Login(string username, string password)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_Login";
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", Encrypter.PasswordEncrypt(password));

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            connection.Close();
            if(dataTable.Rows.Count > 0)
            {
                return int.Parse(dataTable.Rows[0]["AccountID"].ToString());
            }
            return 0;
            
        }

        public void Logout(int accountID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_Logout";
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.ExecuteNonQuery();

            connection.Close();
        }

        public void SetUserStatus(int accountID, UserStatus userStatus)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_setUserStatus";
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.Parameters.AddWithValue("@UserStatusID", (int)userStatus);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public int CreateAccount(string username, string password, string firstname, string lastname, string email, DateTime birthday)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_CreateAccount";
            command.Parameters.AddWithValue("@Username", username);
            command.Parameters.AddWithValue("@Password", password);
            command.Parameters.AddWithValue("@FirstName", firstname);
            command.Parameters.AddWithValue("@LastName", lastname);
            command.Parameters.AddWithValue("@Email", email);
            command.Parameters.AddWithValue("@Birthday", birthday.ToShortDateString());

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            connection.Close();
            if(dataTable.Rows.Count > 0)
            {
                //SEND EMAIL TO _______ WITH ExternalAccountID
                return int.Parse(dataTable.Rows[0]["AccountID"].ToString());
            }
            return 0;
        }

        public void ChangeEmail(int accountID, string email)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_ChangeEmail";
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.Parameters.AddWithValue("@Email", email);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void ChangePassword(int accountID, string password)
        {
            connection.Open(); 
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_ChangePassword";
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.Parameters.AddWithValue("@Password", Encrypter.PasswordEncrypt(password));
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void ChangeUsername(int accountID, string username)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_ChangeUsername";
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.Parameters.AddWithValue("@Username", username);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateSecurityQuestion(int accountID, int questionNumber, int questionID, string answer)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_UpdateSecurityQuestion";
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.Parameters.AddWithValue("@QuestionNumber", questionNumber);
            command.Parameters.AddWithValue("@QuestionID", questionID);
            command.Parameters.AddWithValue("@Answer", answer);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public string[] GetSecurityQuestions()
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_GetSecurityQuestions";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            connection.Close();
            string[] questions = new string[dataTable.Rows.Count];
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                questions[i] = dataTable.Rows[i]["Question"].ToString();
            }
            return questions;
        }

        public void AddFriend(int accountID, int friendID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_AddFriend";
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.Parameters.AddWithValue("@FriendID", friendID);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteFriend(int accountID, int friendID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_DeleteFriend";
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.Parameters.AddWithValue("@FriendID", friendID);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public int CreateChatGroup(int userID, int[] accountID)
        {
            string users = userID.ToString();
            for (int i = 0; i < accountID.Length; i++)
            {
                users += "," + accountID[i];
            }
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_CreateChatGroup";
            command.Parameters.AddWithValue("@AccountIDs", users);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            connection.Close();
            return int.Parse(dataTable.Rows[0]["ChatGroupID"].ToString());
        }

        public void AddGroupAccess(Guid externalChatGroupID, int accountID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_CreateChatGroup";
            command.Parameters.AddWithValue("@ExternalChatGroupID", externalChatGroupID);
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public int CreateMatch(int accountID, GameMode gameMode, string roomName, string password, bool isChatEnabled, bool isSpectatingEnabled, bool isPowerUpEnabled, bool isCustomPieceEnabled, int customPiece, bool isPausingEnabled, int startingLevel, int maxPlayers, int boardWidth, int boardHeight)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_CreateMatch";
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.Parameters.AddWithValue("@GameMode", (int)gameMode);
            command.Parameters.AddWithValue("@RoomName", roomName);
            command.Parameters.AddWithValue("@Password", Encrypter.PasswordEncrypt(password));
            command.Parameters.AddWithValue("@IsChatEnabled", isChatEnabled);
            command.Parameters.AddWithValue("@IsSpectatingEnabled", isSpectatingEnabled);
            command.Parameters.AddWithValue("@IsPowerUpEnabled", isPowerUpEnabled);
            command.Parameters.AddWithValue("@IsCustomPieceEnabled", isCustomPieceEnabled);
            command.Parameters.AddWithValue("@CustomPiece", customPiece);
            command.Parameters.AddWithValue("@IsPausingEnabled", isPausingEnabled);
            command.Parameters.AddWithValue("@StartingLevel", startingLevel);
            command.Parameters.AddWithValue("@MaxPlayers", maxPlayers);
            command.Parameters.AddWithValue("@BoardWidth", boardWidth);
            command.Parameters.AddWithValue("@BoardHeight", boardHeight);

            SqlDataAdapter dataAdapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            dataAdapter.Fill(dataTable);
            connection.Close();

            return int.Parse(dataTable.Rows[0]["MatchID"].ToString());
        }

        public void StartMatch(int matchID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_StartMatch";
            command.Parameters.AddWithValue("@MatchID", matchID);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public int UpdateMatchRanking(int matchID, int accountID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_UpdateMatchRanking";
            command.Parameters.AddWithValue("@MatchID", matchID);
            command.Parameters.AddWithValue("@AccountID", accountID);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            connection.Close();

            return int.Parse(dataTable.Rows[0]["PlayerRanking"].ToString());
        }

        public void DeleteMatch(int matchID)
        {
            connection.Open();


            connection.Close();
        }

        public int AddAccountToMatch(int matchID, int accountID, int customPieceID)
        {
            connection.Open();


            connection.Close();
            return 1;
        }

        public void UpdateMatchAccount(int matchID, int accountID, int customPieceID)
        {
            connection.Open();


            connection.Close();
        }

        public void UpdateQueuedPieces(int boardID, int[] pieceID)
        {
            connection.Open();


            connection.Close();
        }

        public void DeleteMatchAccount(int matchID, int AccountID)
        {
            connection.Open();


            connection.Close();
        }

        public void UpdateBoardData(int boardID, int[][] PackedColors)
        {
            connection.Open();


            connection.Close();
        }

        public BoardInfo GetBoardInfo(int boardID)
        {
            connection.Open();


            connection.Close();
            return new BoardInfo();
        }

        public void SpectateMatch(int matchID, int accountID)
        {
            connection.Open();


            connection.Close();
        }

        public void StopSpectating(int matchID, int accountID)
        {
            connection.Open();


            connection.Close();
        }

        public Spectator[] GetSpectators(int matchID)
        {
            connection.Open();


            connection.Close();
            return new Spectator[1];
        }

        public void CreatePiece(int accountID, int[][] packedColors)
        {
            connection.Open();


            connection.Close();
        }

        public void UpdatePiece(int pieceID, int[][] packedColors)
        {
            connection.Open();


            connection.Close();
        }

        public PieceInfo GetPiece(int pieceID)
        {
            connection.Open();


            connection.Close();
            return new PieceInfo();
        }

        public void DeletePiece(int pieceID)
        {
            connection.Open();


            connection.Close();
        }

        public void SendMessage(int accountID, int chatGroupID, string message)
        {
            connection.Open();


            connection.Close();
        }

        public Message[] GetMessages(int accountID, int chatGroupID, DateTime lastTimeRecieved)
        {
            connection.Open();


            connection.Close();
            return new Message[1];
        }

        public void DeleteMessage(int messageID)
        {
            connection.Open();


            connection.Close();
        }
    }
}
