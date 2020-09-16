using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace lab1Lect.DataBaseWork
{
    class LecturersInfo
    {
        private readonly DB dataBase;
        private readonly GetData getData;
        public LecturersInfo()
        {
            dataBase = new DB();
            dataBase.OpenConnection();
            getData = new GetData();

        }
        public string LecturerToString(Lecturer lecturer)
        {
            return $"{getData.getPersonById(lecturer.PersonId,dataBase.GetSqlConnection())} {getData.getPlaceWorkById(lecturer.PlaceWorkId, dataBase.GetSqlConnection())}  {getData.getPostById(lecturer.PostId, dataBase.GetSqlConnection())} {getData.getStavkaById(lecturer.StavkaId, dataBase.GetSqlConnection())}, Characteristic {lecturer.Characteristic}";
        }
        public string PersonToString(Person person)
        {
            return $"First name: {person.FirstName}, LastName {person.LastName}, Middle Name {person.MiddleName}, Phone Number {person.PhoneNumber}, Address {getData.getAddressById(person.AddressId,dataBase.GetSqlConnection())}";
        }
        public List<Lecturer> GetListLecturers()
        {
            string sql = "SELECT * from Lecturers";
            SqlCommand myCommand = new SqlCommand(sql, dataBase.GetSqlConnection());
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
            List<Lecturer> lecturers = this.GetListLecturers();
            foreach (Lecturer lecturer in lecturers)
            {

                //Console.WriteLine($"{lecturer}\n********TEACH SUBJECT AS*********");
                sqlSubj = $"Select Subjects.Id AS Id,SubjName,Hours From SubLecturers inner join Subjects on Subjects.Id=SubLecturers.SubjectId where LecturerId={lecturer.Id};";
                SqlCommand myCommand = new SqlCommand(sqlSubj, dataBase.GetSqlConnection());
                List<Subject> subjects = new List<Subject>();
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    while (myDataReader.Read())
                    {
                        subjects.Add(new Subject((int)myDataReader["Id"], (String)myDataReader["SubjName"], (int)myDataReader["Hours"]));
                    }
                    Console.WriteLine();
                    myDataReader.Close();
                }
                lecturerSubj.Add(lecturer, subjects);
                Console.WriteLine();

            }
            return lecturerSubj;
        }
        public Dictionary<Subject, List<Lecturer>> GetSubjectWithLecturers()
        {
            //string sqlSubjTeachers = "";
            Dictionary<Subject, List<Lecturer>> subjLecturers = new Dictionary<Subject, List<Lecturer>>();

            string sqlSubj = $"SELECT  SubjectId,SUM(Hours) as GenHours FROM SubLecturers Group by SubjectId";
            SqlCommand myCommand = new SqlCommand(sqlSubj, dataBase.GetSqlConnection());
            Dictionary<int, int> listIdSubj = new Dictionary<int, int>();
            using (SqlDataReader myDataReader = myCommand.ExecuteReader())
            {
                while (myDataReader.Read())
                {
                    listIdSubj.Add((int)myDataReader["SubjectId"], (int)myDataReader["GenHours"]);
                    //Console.WriteLine(GetData.getSubjectById(currId));
                }
                Console.WriteLine();
                myDataReader.Close();
            }
            List<Subject> subjectslist = new List<Subject>();
            foreach (int item in listIdSubj.Keys)
            {
                Subject tmp = getData.getSubjectById(item, dataBase.GetSqlConnection());
                tmp.Hours = listIdSubj[item];
                subjectslist.Add(tmp);
            }
            List<int> lectId = new List<int>();
            foreach (Subject subj in subjectslist)
            {
                List<Lecturer> lecturers = new List<Lecturer>();                
                sqlSubj = $"SELECT LecturerId FROM SubLecturers where SubjectId = {subj.Id}";
                myCommand = new SqlCommand(sqlSubj, dataBase.GetSqlConnection());
                using (SqlDataReader myDataReader = myCommand.ExecuteReader())
                {
                    while (myDataReader.Read())
                    {
                        lectId.Add((int)myDataReader["LecturerId"]);

                    }

                    Console.WriteLine();
                    myDataReader.Close();

                }
                foreach (int item in lectId)
                {
                    lecturers.Add(getData.getLecturerById(item, dataBase.GetSqlConnection()));
                }
                subjLecturers.Add(subj, lecturers);
            }
            return subjLecturers;

        }
       ~LecturersInfo()
        {           
            dataBase.CloseConnection();
        }
    }
}
