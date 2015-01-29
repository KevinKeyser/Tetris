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
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
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
            command.Parameters.AddWithValue("@Password", Encrypter.PasswordEncrypt(password));
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

        public void SetCurrentCustomPiece(int accountID, int customPieceID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_UpdateMatchAccount";
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.Parameters.AddWithValue("@CustomPieceID", customPieceID);
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

        public int CreateMatch(int accountID, GameMode gameMode, string roomName, string password, bool isChatEnabled, bool isSpectatingEnabled, bool isPowerUpEnabled, bool isCustomPieceEnabled, bool isPausingEnabled, int startingLevel, int maxPlayers, int boardWidth, int boardHeight)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_CreateMatch";
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.Parameters.AddWithValue("@GameMode", (int)gameMode);
            if(roomName != "")
            {
                command.Parameters.AddWithValue("@RoomName", roomName);
            }
            else
            {
                command.Parameters.AddWithValue("@RoomName", DBNull.Value);
            }
            if (password != "")
            {
                command.Parameters.AddWithValue("@Password", Encrypter.PasswordEncrypt(password));
            }
            else
            {
                command.Parameters.AddWithValue("@Password", DBNull.Value);
            }
            command.Parameters.AddWithValue("@IsChatEnabled", isChatEnabled);
            command.Parameters.AddWithValue("@IsSpectatingEnabled", isSpectatingEnabled);
            command.Parameters.AddWithValue("@IsPowerUpEnabled", isPowerUpEnabled);
            command.Parameters.AddWithValue("@IsCustomPieceEnabled", isCustomPieceEnabled);
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
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_DeleteMatch";
            command.Parameters.AddWithValue("@MatchID", matchID);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public int AddAccountToMatch(int matchID, int accountID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_AddAccountToMatch";
            command.Parameters.AddWithValue("@MatchID", matchID);
            command.Parameters.AddWithValue("@AccountID", accountID);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            connection.Close();
            if(dataTable.Rows.Count > 0)
            {
                return int.Parse(dataTable.Rows[0]["BoardID"].ToString());
            }
            return 0;
        }

        public void UpdateQueuedPieces(int boardID, int[] pieceID)
        {
            connection.Open();
            for (int i = 0; i < pieceID.Length; i++)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "usp_UpdateQueuedPiece";
                command.Parameters.AddWithValue("@BoardID", boardID);
                command.Parameters.AddWithValue("@PieceID", pieceID[i]);
                command.Parameters.AddWithValue("@QueuedPlace", i+1);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public void DeleteMatchAccount(int matchID, int accountID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_DeleteMatchAccount";
            command.Parameters.AddWithValue("@MatchID", matchID);
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateBoardData(int boardID, int?[][] packedColors)
        {
            connection.Open();
            for (int y = 0; y < packedColors.Length; y++)
            {
                for(int x = 0; x < packedColors[y].Length; x++)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_UpdateBoardData";
                    command.Parameters.AddWithValue("@BoardID", boardID);
                    command.Parameters.AddWithValue("@RowID", y);
                    command.Parameters.AddWithValue("@ColumnID", x);
                    if(packedColors[y][x].HasValue)
                    {
                        command.Parameters.AddWithValue("@PackedColor", packedColors[y][x]);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@PackedColor", DBNull.Value);
                    }
                    command.ExecuteNonQuery(); 
                }
            }
           connection.Close();
        }

        public BoardInfo[] GetBoardInfo(int matchID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_GetBoardInfo";
            command.Parameters.AddWithValue("@MatchID", matchID);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            BoardInfo[] boardInfo = new BoardInfo[dataTable.Rows.Count];
            for (int i = 0; i < boardInfo.Length; i++)
            {
                BoardInfo board = new BoardInfo();
                board.AccountID = int.Parse(dataTable.Rows[i]["AccountID"].ToString());
                board.BoardID = int.Parse(dataTable.Rows[i]["BoardID"].ToString());
                if (dataTable.Rows[i]["Ranking"] == DBNull.Value)
                {
                    board.Ranking = null;
                    board.TimeFinished = null;
                }
                else
                {
                    board.Ranking = int.Parse(dataTable.Rows[i]["Ranking"].ToString());
                    board.TimeFinished = DateTime.Parse(dataTable.Rows[i]["TimeFinished"].ToString());
                }
                board.CurrentPieceID = int.Parse(dataTable.Rows[i]["CurrentPieceID"].ToString());
                board.PieceX = int.Parse(dataTable.Rows[i]["PieceX"].ToString());
                board.PieceY = int.Parse(dataTable.Rows[i]["PieceY"].ToString());
                board.Rotation = int.Parse(dataTable.Rows[0]["Rotation"].ToString());
                if (dataTable.Rows[i]["HeldPieceID"] == DBNull.Value)
                {
                    board.HeldPieceID = null;
                }
                else
                {
                    board.HeldPieceID = int.Parse(dataTable.Rows[i]["HeldPieceID"].ToString());
                }
                board.Combo = int.Parse(dataTable.Rows[i]["Combo"].ToString());
                board.Multiplier = float.Parse(dataTable.Rows[i]["Multiplier"].ToString());
                board.Level = int.Parse(dataTable.Rows[i]["Level"].ToString());
                board.Score = int.Parse(dataTable.Rows[i]["Score"].ToString());
                board.PowerUp = (PowerUp)int.Parse(dataTable.Rows[i]["PowerUp"].ToString());
                board.BoardHeight = int.Parse(dataTable.Rows[i]["BoardHeight"].ToString());
                board.BoardWidth = int.Parse(dataTable.Rows[i]["BoardWidth"].ToString());
                command.CommandText = "usp_GetQueuedPieces";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@BoardID", board.BoardID);

                dataAdapter = new SqlDataAdapter(command);
                DataTable queuedPieces = new DataTable();
                dataAdapter.Fill(queuedPieces);
                board.QueuedPieceID = new int[queuedPieces.Rows.Count];
                for (int ii = 0; ii < board.QueuedPieceID.Length; ii++)
                {
                    board.QueuedPieceID[ii] = int.Parse(queuedPieces.Rows[ii]["PieceID"].ToString());
                }

                command.CommandText = "usp_GetBoardData";

                dataAdapter = new SqlDataAdapter(command);
                DataTable boardData = new DataTable();
                dataAdapter.Fill(boardData);
                board.PackedColors = new int?[board.BoardHeight][];
                for (int ii = 0; ii < board.PackedColors.Length; ii++)
                {
                    board.PackedColors[ii] = new int?[board.BoardWidth];
                }

                for (int ii = 0; ii < boardData.Rows.Count; ii++)
                {
                    if (boardData.Rows[ii]["PackedColor"] == DBNull.Value)
                    {
                        board.PackedColors[int.Parse(boardData.Rows[ii]["RowID"].ToString())][int.Parse(boardData.Rows[ii]["ColumnID"].ToString())] = null;
                    }
                    else
                    {
                        board.PackedColors[int.Parse(boardData.Rows[ii]["RowID"].ToString())][int.Parse(boardData.Rows[ii]["ColumnID"].ToString())] = int.Parse(boardData.Rows[ii]["PackedColor"].ToString());
                    }
                }

                boardInfo[i] = board;
            }
            connection.Close();
            return boardInfo;
        }

        public void SpectateMatch(int matchID, int accountID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_SpectateMatch";
            command.Parameters.AddWithValue("@MatchID", matchID);
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void StopSpectating(int matchID, int accountID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_StopSpectating";
            command.Parameters.AddWithValue("@MatchID", matchID);
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public SpectatorInfo[] GetSpectators(int matchID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_GetSpectators";
            command.Parameters.AddWithValue("@MatchID", matchID);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            connection.Close();

            SpectatorInfo[] spectators = new SpectatorInfo[dataTable.Rows.Count];
            for (int i = 0; i < spectators.Length; i++)
            {
                SpectatorInfo temp = new SpectatorInfo();
                temp.AccountID = int.Parse(dataTable.Rows[i]["AccountID"].ToString());
                temp.MatchID = int.Parse(dataTable.Rows[i]["MatchID"].ToString());
                temp.SpectatorID = int.Parse(dataTable.Rows[i]["SpectatorID"].ToString());

                if (dataTable.Rows[i]["TimeEnded"] == DBNull.Value)
                {
                    temp.TimeEnded = null;
                }
                else
                {
                    temp.TimeEnded = DateTime.Parse(dataTable.Rows[i]["TimeEnded"].ToString());
                }
                temp.TimeStarted = DateTime.Parse(dataTable.Rows[i]["TimeStarted"].ToString());
                spectators[i] = temp;
            }
            return spectators;
        }

        public void CreatePiece(int accountID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_CreatePiece";
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdatePiece(int pieceID, int?[][] packedColors)
        {
            connection.Open();
            for(int y = 0; y < packedColors.Length; y++)
            {
                for(int x = 0; x < packedColors[y].Length; x++)
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_UpdatePiece";
                    command.Parameters.AddWithValue("@PieceID", pieceID);
                    command.Parameters.AddWithValue("@RowID", y);
                    command.Parameters.AddWithValue("@ColumnID", x);
                    if(packedColors[y][x].HasValue)
                    {
                        command.Parameters.AddWithValue("@PackedColor", packedColors[y][x]);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@PackedColor", DBNull.Value);
                    }
                    command.ExecuteNonQuery();
                }
            }
            connection.Close();
        }

        public PieceInfo GetPiece(int pieceID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_GetPiece";
            command.Parameters.AddWithValue("@PieceID", pieceID);

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            connection.Close();
            if(dataTable.Rows.Count > 0)
            {
                PieceInfo pieceInfo = new PieceInfo();
                pieceInfo.OwnerID = int.Parse(dataTable.Rows[0]["AccountID"].ToString());
                pieceInfo.PieceID = int.Parse(dataTable.Rows[0]["PieceID"].ToString());
                pieceInfo.PackedColors = new int?[5][];
                for(int i = 0; i < pieceInfo.PackedColors.Length; i++)
                {
                    pieceInfo.PackedColors[i] = new int?[5];
                }

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (dataTable.Rows[i]["PackedColor"] == DBNull.Value)
                    {
                        pieceInfo.PackedColors[int.Parse(dataTable.Rows[i]["RowID"].ToString())][int.Parse(dataTable.Rows[i]["ColumnID"].ToString())] = null;
                    }
                    else
                    {
                        pieceInfo.PackedColors[int.Parse(dataTable.Rows[i]["RowID"].ToString())][int.Parse(dataTable.Rows[i]["ColumnID"].ToString())] = int.Parse(dataTable.Rows[i]["PackedColor"].ToString());
                    }
                }
                return pieceInfo;
            }
            return null;
        }

        public void DeletePiece(int pieceID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_DeletePiece";
            command.Parameters.AddWithValue("@PieceID", pieceID);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void SendMessage(int accountID, int chatGroupID, string message)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_SendMessage";
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.Parameters.AddWithValue("@ChatGroupID", chatGroupID);
            command.Parameters.AddWithValue("@Message", message);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Message[] GetMessages(int accountID, int chatGroupID, DateTime lastTimeRecieved)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_GetMessages";
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.Parameters.AddWithValue("@ChatGroupID", chatGroupID);
            command.Parameters.AddWithValue("@LastTimeRecieved", lastTimeRecieved.ToShortDateString());

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            Message[] messages = new Message[dataTable.Rows.Count];
            for (int i = 0; i < messages.Length; i++)
            {
                Message temp = new Message();
                if(dataTable.Rows[i]["DeletionDate"] == DBNull.Value)
                {
                    temp.DeletionDate = null;
                }
                else
                {
                    temp.DeletionDate = DateTime.Parse(dataTable.Rows[i]["DeletionDate"].ToString());
                }
                temp.GroupID = int.Parse(dataTable.Rows[i]["GroupID"].ToString());
                temp.MessageID = int.Parse(dataTable.Rows[i]["MessageID"].ToString());
                temp.MessageString = dataTable.Rows[i]["Message"].ToString();
                temp.SenderID = int.Parse(dataTable.Rows[i]["AccountID"].ToString());
                temp.TimeSent = DateTime.Parse(dataTable.Rows[i]["TimeSent"].ToString());
            }
            connection.Close();
            return messages;
        }

        public void DeleteMessage(int messageID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_DeleteMessage";
            command.Parameters.AddWithValue("@MessageID", messageID);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public AccountInfo[] GetFriends(int accountID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_GetFriends";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            connection.Close();

            AccountInfo[] accountInfo = new AccountInfo[dataTable.Rows.Count];
            for (int i = 0; i < accountInfo.Length;i++)
            {
                AccountInfo temp = new AccountInfo();
                temp.AccountID = int.Parse(dataTable.Rows[i]["AccountID"].ToString());
                if(dataTable.Rows[i]["CurrentMatchID"] == DBNull.Value)
                {
                    temp.CurrentMatchID = null;
                }
                else
                {
                    temp.CurrentMatchID = int.Parse(dataTable.Rows[i]["CurrentMatchID"].ToString());
                }
                if(dataTable.Rows[i]["CustomPieceID"] == DBNull.Value)
                {
                    temp.CustomPieceID = null;
                }
                else
                {
                    temp.CustomPieceID = int.Parse(dataTable.Rows[i]["CustomPieceID"].ToString());
                }
                temp.ExternalAccountID = Guid.Parse(dataTable.Rows[i]["ExternalAccountID"].ToString());
                temp.FirstName = dataTable.Rows[i]["FirstName"].ToString();
                temp.LastName = dataTable.Rows[i]["LastName"].ToString();
                temp.Username = dataTable.Rows[i]["Username"].ToString();
                temp.UserStatus = (UserStatus)int.Parse(dataTable.Rows[i]["UserStatusID"].ToString());
                accountInfo[i] = temp;
            }
            return accountInfo;
        }

        public void GiveChatGroupOwnership(int chatGroupID, int accountID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_GiveChatGroupOwnership";
            command.Parameters.AddWithValue("@ChatGroupID", chatGroupID);
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void GiveMatchOwnership(int matchID, int accountID)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_GiveMatchOwnership";
            command.Parameters.AddWithValue("@MatchID", matchID);
            command.Parameters.AddWithValue("@AccountID", accountID);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateBoardInfo(int boardID, int? currentPieceID, int pieceX, int pieceY, int rotation, int? heldPieceID, int combo, float multiplier, int level, int score, PowerUp powerup)
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_UpdateBoardInfo";
            command.Parameters.AddWithValue("@BoardID", boardID);
            if(currentPieceID.HasValue)
            {
                command.Parameters.AddWithValue("@CurrentPieceID", currentPieceID.Value);
                command.Parameters.AddWithValue("@PieceX", pieceX);
                command.Parameters.AddWithValue("@PieceY", pieceY);
            }
            else
            {
                command.Parameters.AddWithValue("@CurrentPieceID", DBNull.Value);
                command.Parameters.AddWithValue("@PieceX", DBNull.Value);
                command.Parameters.AddWithValue("@PieceY", DBNull.Value);
            }
            command.Parameters.AddWithValue("@Rotation", rotation);
            if(heldPieceID.HasValue)
            {
                command.Parameters.AddWithValue("@HeldPieceID", heldPieceID.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@HeldPieceID", DBNull.Value);
            }
            command.Parameters.AddWithValue("@Combo", combo);
            command.Parameters.AddWithValue("@Multiplier", multiplier);
            command.Parameters.AddWithValue("@Level", level);
            command.Parameters.AddWithValue("@Score", score);
            if(powerup != PowerUp.None)
            {
                command.Parameters.AddWithValue("@PowerUp", powerup);
            }
            else
            {
                command.Parameters.AddWithValue("@PowerUp", DBNull.Value);
            }
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
