using INFT6009.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INFT6009.Services
{
    internal static class DatabaseService
    {
        private static string _databaseFile;
        private static string DatabaseFile
        {
            get
            {
                //If the file path has not yet been set, set it
                if (_databaseFile == null)
                {
                    //Access the App-Specific Folder using AppDataDirectory and create the subfolder "data"
                    //CreateDirectory simply does nothing if the folder already exists
                    string databaseDir = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, "data");
                    System.IO.Directory.CreateDirectory(databaseDir);

                    //Add a filename for our database to the end of the folder path
                    _databaseFile = Path.Combine(databaseDir, "quest_data.sqlite");
                }

                return _databaseFile;
            }
        }

        private static SQLiteConnection _connection;
        public static SQLiteConnection Connection
        {
            get
            {
                //If the connection has not yet been created, create it
                if (_connection == null)
                {
                    //Create the connection (and file) by pointing it at the filepath created in DatabaseFile
                    _connection = new SQLiteConnection(DatabaseFile);
                    //Create a table in the database for our quest model (does nothing if already exists)
                    _connection.CreateTable<QuestModel>();
                }

                return _connection;
            }
        }

        //Provide a static method to add/update quests to the database
        public static void SaveQuest(QuestModel model)
        {
            //If the Id is > 0, then we can tell it has been saved as SQLite has 
            //automatically assigned it an id. So we just update the existing quest
            if (model.Id > 0)
                DatabaseService.Connection.Update(model);
            //If there is no id, then save it for the first time
            else
                DatabaseService.Connection.Insert(model);
        }

        public static void DeleteQuest(QuestModel model)
        {
            //If it has an Id, then we can delete it 
            if (model.Id > 0)
            {
                DatabaseService.Connection.Delete(model);
            }
        }
    }
}
