using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace TetrisConnection
{
    [ServiceContract]
    public interface ITetrisConnection
    {
        [OperationContract]
        int Login(string username, string password);

        [OperationContract]
        void Logout(int accountID);

        [OperationContract]
        void SetUserStatus(int accountID, UserStatus userStatus);

        [OperationContract]
        int CreateAccount(string username, string password, string firstname, string lastname, string email, DateTime birthday);

        [OperationContract]
        void ChangeEmail(int accountID, string email);

        [OperationContract]
        void ChangePassword(int accountID, string password);

        [OperationContract]
        void ChangeUsername(int accountID, string username);

        void UpdateSecurityQuestion(int accountID, int questionNumber, int questionID, string answer);

        [OperationContract]
        string[] GetSecurityQuestions();
        
        [OperationContract]
        void AddFriend(int accountID, int friendID);

        [OperationContract]
        void DeleteFriend(int accountID, int friendID);

        [OperationContract]
        int CreateChatGroup(int userID, int[] accountID);

        [OperationContract]
        void AddGroupAccess(Guid externalChatGroupID, int accountID);

        [OperationContract]
        int CreateMatch(int accountID, GameMode gameMode, string roomName, string password, bool isChatEnabled, bool isSpectatingEnabled, bool isPowerUpEnabled, bool isCustomPieceEnabled, int customPiece, bool isPausingEnabled, int startingLevel, int maxPlayers, int boardWidth, int boardHeight);

        [OperationContract]
        void StartMatch(int matchID);

        [OperationContract]
        int UpdateMatchRanking(int matchID, int accountID);

        [OperationContract]
        void DeleteMatch(int matchID);

        [OperationContract]
        int AddAccountToMatch(int matchID, int accountID, int customPieceID);

        [OperationContract]
        void UpdateMatchAccount(int matchID, int accountID, int customPieceID);

        [OperationContract]
        void UpdateQueuedPieces(int boardID, int[] pieceID);

        [OperationContract]
        void DeleteMatchAccount(int matchID, int AccountID);

        [OperationContract]
        void UpdateBoardData(int boardID, int[][] PackedColors);

        [OperationContract]
        BoardInfo GetBoardInfo(int boardID);

        [OperationContract]
        void SpectateMatch(int matchID, int accountID);

        [OperationContract]
        void StopSpectating(int matchID, int accountID);

        [OperationContract]
        Spectator[] GetSpectators(int matchID);

        [OperationContract]
        void CreatePiece(int accountID, int[][] packedColors);

        [OperationContract]
        void UpdatePiece(int pieceID, int[][] packedColors);

        [OperationContract]
        PieceInfo GetPiece(int pieceID);

        [OperationContract]
        void DeletePiece(int pieceID);

        [OperationContract]
        void SendMessage(int accountID, int chatGroupID, string message);

        [OperationContract]
        Message[] GetMessages(int accountID, int chatGroupID, DateTime lastTimeRecieved);

        [OperationContract]
        void DeleteMessage(int messageID);


    }
}
