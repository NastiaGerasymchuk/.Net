using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace lab1Lect.DataBaseWork
{
    class LecturerService
    {
        private readonly DBService _dataBase;
        private readonly Model _getData;
        public LecturerService(DBService dataBase, Model getData)
        {
            //_dataBase = new DBService();
            //_dataBase.OpenConnection();
            //_getData = new Models();            
            //_dataBase.OpenConnection();
            _getData = getData;
            _dataBase = dataBase;

        }
        public string LecturerToString(Lecturer lecturer)
        {
            return $"{_getData.getPersonById(lecturer.PersonId,_dataBase.GetSqlConnection())} {_getData.getPlaceWorkById(lecturer.PlaceWorkId, _dataBase.GetSqlConnection())}  {_getData.getPostById(lecturer.PostId, _dataBase.GetSqlConnection())} {_getData.getStavkaById(lecturer.StavkaId, _dataBase.GetSqlConnection())}, Characteristic {lecturer.Characteristic}";
        }
        public string PersonToString(Person person)
        {
            return $"First name: {person.FirstName}, LastName {person.LastName}, Middle Name {person.MiddleName}, Phone Number {person.PhoneNumber}, Address {_getData.getAddressById(person.AddressId,_dataBase.GetSqlConnection())}";
        }
        public List<Lecturer> GetListLecturers()
        {
            string sql = "SELECT * from Lecturers";
            SqlCommand myCommand = new SqlCommand(sql, _dataBase.GetSqlConnection());
            List<Lecturer> lecturers = new List<Lecturer>();
            using (SqlDataReader myDataReader = myCommand.ExecuteReader())
            {
                while (myDataReader.Read())
                {
                    lecturers.Add(new Lecturer((int)myDataReader["Id"], (int)myDataReader["PersonId"], (int)myDataReader["PlaceWorkId"],

                      (int)myDataReader["StavkaId"], (int)myDataReader["PostId"], (String)myDataReader["Characteristic"]));

                }
                myDataReader.Close();
            }
            return lecturers;
        }
        public Dictionary<Lecturer, List<Subject>> GetSubjectsWithLecturer()
        {
            Dictionary<Lecturer, List<Subject>> lecturerSubj = new Dictionary<Lecturer, List<Subject>>();
            string sqlSubj = "";
            List<Lecturer> lecturers = this.GetListLecturers();  //LecturerDao. GetListLecturer(GetAll)
            foreach (Lecturer lecturer in lecturers)
            {

                //Console.WriteLine($"{lecturer}\n********TEACH SUBJECT AS*********");
                sqlSubj = $"Select Subjects.Id AS Id,SubjName,Hours From SubLecturers inner join Subjects on Subjects.Id=SubLecturers.SubjectId where LecturerId={lecturer.Id};";
                SqlCommand myCommand = new SqlCommand(sqlSubj, _dataBase.GetSqlConnection());
                List<Subject> subjects = new List<Subject>();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    while (myDataReader.Read())
                    {
                        subjects.Add(new Subject((int)myDataReader["Id"], (String)myDataReader["SubjName"], (int)myDataReader["Hours"]));
                    }
                    //Console.WriteLine();
                    myDataReader.Close();
                }
                lecturerSubj.Add(lecturer, subjects);
                //Console.WriteLine();

            }
            return lecturerSubj;
        }
        public Dictionary<Subject, List<Lecturer>> GetSubjectWithLecturers()
        {
            //string sqlSubjTeachers = "";
            Dictionary<Subject, List<Lecturer>> subjLecturers = new Dictionary<Subject, List<Lecturer>>();

            string sqlSubj = $"SELECT  SubjectId,SUM(Hours) as GenHours FROM SubLecturers Group by SubjectId";
            SqlCommand myCommand = new SqlCommand(sqlSubj, _dataBase.GetSqlConnection());
            Dictionary<int, int> listIdSubj = new Dictionary<int, int>();
            using (SqlDataReader myDataReader = myCommand.ExecuteReader())
            {
                while (myDataReader.Read())
                {
                    listIdSubj.Add((int)myDataReader["SubjectId"], (int)myDataReader["GenHours"]);
                    //.WriteLine(Models.getSubjectById(currId));
                }
                //Console.WriteLine();
                myDataReader.Close();
            }
            List<Subject> subjectslist = new List<Subject>();
            foreach (int item in listIdSubj.Keys)
            {
                Subject tmp = _getData.getSubjectById(item, _dataBase.GetSqlConnection());
                tmp.Hours = listIdSubj[item];
                subjectslist.Add(tmp);
            }
            
            foreach (Subject subj in subjectslist)
            {
                Console.WriteLine("-------------\n");
                List<Lecturer> lecturers = new List<Lecturer>();                
                sqlSubj = $"SELECT LecturerId FROM SubLecturers where SubjectId = {subj.Id}";
                myCommand = new SqlCommand(sqlSubj, _dataBase.GetSqlConnection());
                List<int> lectId = new List<int>();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    
                    // Console.WriteLine("-------------\n");
                    while (myDataReader.Read())
                    {
                        lectId.Add((int)myDataReader["LecturerId"]);
                       
                       // Console.WriteLine((int)myDataReader["LecturerId"]);
                        
                        

                    }

                    //Console.WriteLine();
                   // Console.WriteLine("-------------\n");
                    myDataReader.Close();

                }
                foreach (int item in lectId)
                {
                    lecturers.Add(_getData.getLecturerById(item, _dataBase.GetSqlConnection()));
                    
                    //Console.WriteLine(subj);
                    //Console.WriteLine(LecturerToString(_getData.getLecturerById(item, _dataBase.GetSqlConnection())));
                    
                }
                subjLecturers.Add(subj, lecturers);
                //Console.WriteLine("-------------\n");
            }
            return subjLecturers;

        }
       //~LecturerService()
       // {           
       //     _dataBase.CloseConnection();
       // }
    }
}
