using FakeTrello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeTrello.DAL
{
    interface Irepository
    {
        //List of methods to help deliver features
        void AddBoard(string name, TrelloUser owner);
        void AddList(string name, Board board);
        void AddList(string name, int boardId);
        void AddCard(string name, List list, TrelloUser owner);
        void AddCard(string name, int ListId, int ownerId);

        //Read
        List<Card> GetCardsFromList(int listId);
        List<Card> GetCardsFromBoard(int boardId);
        Card GetCard(int cardId);
        List GetList(int listId);
        List<List> GetListsFromBoard(int boardId); // List of Trello Lists
        List<Board> GetBoardsFromUser(int userId);
        Board GetBoard(int boardId);
        List<TrelloUser> GetCardAttendees(int cardId);

        // Update
        bool AttachUser(int userId, int CardId); // true successful, false: not successful
            //Move
        bool MoveCard(int cardId, int oldListId, int newListId);
            //Copy
        bool CopyCard(int CardId, int newListId, int newOwnerId);

        //Delete
        bool RemoveBoard(int BoardId);
        bool RemoveList(int ListId);
        bool RemoveCard(int cardId);
    }
}
