using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeTrello.Models;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Diagnostics;
namespace FakeTrello.DAL
{
    public class FakeTrelloRepository : IRepository
    {
        //public FakeTrelloContext Context { get; set; }
        //private FakeTrelloContext context; // Data member
        //implicitly private, if public is not noted
        SqlConnection _trelloConnecion;
        //default constructor
        public FakeTrelloRepository()
        {
            //Context = new FakeTrelloContext();
            _trelloConnecion = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }
        public void AddBoard(string name, ApplicationUser owner)
        {
            //Board board = new Board { Name = name, Owner = owner };
            //Context.Boards.Add(board);
            //Context.SaveChanges();
            _trelloConnecion.Open();
            try
            {
                var addBoardCommand = _trelloConnecion.CreateCommand();
                addBoardCommand.CommandText = @"INSERT INTO Boards (Name, Owner_Id) VALUES(@Name, @Owner_Id);";
                var nameParam = new SqlParameter("name", SqlDbType.VarChar) { Value = name };
                addBoardCommand.Parameters.Add(nameParam);
                var ownerParam = new SqlParameter("name", SqlDbType.Int) { Value = owner.Id };
                addBoardCommand.Parameters.Add(ownerParam);
                addBoardCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnecion.Close();
            }
        }
        public void AddCard(string name, int listId, string ownerId)
        {
            throw new NotImplementedException();
        }
        public void AddCard(string name, List list, ApplicationUser owner)
        {
            throw new NotImplementedException();
        }
        public void AddList(string name, int boardId)
        {
            throw new NotImplementedException();
        }
        public void AddList(string name, Board board)
        {
            throw new NotImplementedException();
        }
        public bool AttachUser(string userId, int cardId)
        {
            throw new NotImplementedException();
        }
        public bool CopyCard(int cardId, int newListId, string newOwnerId)
        {
            throw new NotImplementedException();
        }
        public Board GetBoard(int boardId)
        {
            _trelloConnecion.Open();
            try
            {
                var getBoardCommand = _trelloConnecion.CreateCommand();
                getBoardCommand.CommandText = $@"
                    SELECT distinct BoardId, Name, URL, Owner_Id 
                    FROM Boards 
                    WHERE BoardID = @boardId";
                var boardIdParam = new SqlParameter("boardId", SqlDbType.Int);
                boardIdParam.Value = boardId;
                getBoardCommand.Parameters.Add(boardIdParam);
                var reader = getBoardCommand.ExecuteReader();
                reader.Read();
                if (reader.Read())
                {
                    var board = new Board
                    {
                        BoardId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        URL = reader.GetString(2),
                        Owner = new ApplicationUser { Id = reader.GetString(3) }
                    };
                    return board;
                }
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnecion.Close();
            }
            return null;
        }


        public List<Board> GetBoardsFromUser(string userId) //this should match the @ below
        {
            _trelloConnecion.Open();
            try
            {
                var getBoardCommand = _trelloConnecion.CreateCommand();
                getBoardCommand.CommandText = $@"
                    SELECT distinct BoardId, Name, URL, Owner_Id 
                    FROM Boards 
                    WHERE Owner_Id = @userId";
                var boardIdParam = new SqlParameter("boardId", SqlDbType.VarChar);
                boardIdParam.Value = userId;
                getBoardCommand.Parameters.Add(boardIdParam);
                var reader = getBoardCommand.ExecuteReader();
                reader.Read();

                var boards = new List<Board>();
                while (reader.Read())
                {
                    var board = new Board
                    {
                        BoardId = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        URL = reader.GetString(2),
                        Owner = new ApplicationUser { Id = reader.GetString(3) }
                    };
                    boards.Add(board); //this is referring to the var listed above which is a list of boards
                }
                return boards;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnecion.Close();
            }
            return null;
        }

        public Card GetCard(int cardId)
        {
            throw new NotImplementedException();
        }
        public List<ApplicationUser> GetCardAttendees(int cardId)
        {
            throw new NotImplementedException();
        }
        public List<Card> GetCardsFromBoard(int boardId)
        {
            throw new NotImplementedException();
        }
        public List<Card> GetCardsFromList(int listId)
        {
            throw new NotImplementedException();
        }
        public List GetList(int listId)
        {
            throw new NotImplementedException();
        }
        public List<List> GetListsFromBoard(int boardId)
        {
            throw new NotImplementedException();
        }
        public bool MoveCard(int cardId, int oldListId, int newListId)
        {
            throw new NotImplementedException();
        }
        public bool RemoveBoard(int boardId)
        {
            _trelloConnecion.Open();

            try
            {
                var removeBoardCommand = _trelloConnecion.CreateCommand();
                removeBoardCommand.CommandText = @"
                DELETE
                FROM Boards
                Where boardId = @boardId
                ";
                var boardIdParameter = new SqlParameter("boardId", SqlDbType.Int); //int should match the parameter above
                boardIdParameter.Value = boardId;

                removeBoardCommand.Parameters.Add(boardIdParameter);

                removeBoardCommand.ExecuteNonQuery();

                return true;
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnecion.Close();
            }
            return false;
        }

        public bool RemoveCard(int cardId)
        {
            throw new NotImplementedException();
        }
        public bool RemoveList(int listId)
        {
            throw new NotImplementedException();
        }
        public void EditBoardName(int boardId, string newname)
        {
            //Board found_board = GetBoard(boardId);
            //if (found_board != null)
            //{
            //    found_board.Name = newname; // Akin to 'git add'
            //    Context.SaveChanges(); // Akin to 'git commit'
            //}
            // False Positive: SaveChanges is missing.

            _trelloConnecion.Open();
            try
            {
                var updatedBoardCommand = _trelloConnecion.CreateCommand();
                updatedBoardCommand.CommandText = @"
                UPDATE Boards
                Set Name = @Newname
                Where boardid = @boardId;
                ";
                var nameParameter = new SqlParameter("Newname", SqlDbType.VarChar);
                nameParameter.Value = newname; //this should match the argument above
                updatedBoardCommand.Parameters.Add(nameParameter);
                var boardIdParameter = new SqlParameter("boardId", SqlDbType.Int);
                boardIdParameter.Value = boardId;
                updatedBoardCommand.Parameters.Add(boardIdParameter);

                updatedBoardCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _trelloConnecion.Close();
            }

        }
    }
}