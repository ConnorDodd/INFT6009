using INFT6009.Services;
using SQLite;
using System.Collections.ObjectModel;

namespace INFT6009.Models.ViewModels
{
    //An example of inheriting ObservableObject
    internal class QuestViewModel : ObservableObject
    {
        SQLiteConnection _connection;

        public QuestViewModel()
        {
            //Get the Connection object to use for access
            _connection = DatabaseService.Connection;
        }

        //Load and return a list of all quests (sorted by date/time)
        public List<QuestModel> Quests
        {
            get
            {
                //Return all objects inside the QuestModel table of the database
                var quests = _connection.Table<QuestModel>()
                    .OrderBy(x => x.Date) //Sort primarily by date
                    .ThenBy(x => x.Time) //Then secondarily by time
                    .ToList();
                return quests;
            }
        }
    }
}
