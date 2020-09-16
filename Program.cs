using lab1Lect.DataBaseWork;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace lab1Lect
{ 
    
    class Program
    {
        

        static void Main(string[] args)
        { 
            
           
            LecturersInfo lecturersInfo = new LecturersInfo();

            /*string sql = "Select * From Stavka inner join (Select * From Posts inner join(Select * From PlacesWork inner join " +
                         "(Select * From Lecturers inner join (Select * FROM Addresses inner join Persons on AddressId=IdAddress) as PersAdd on PersAdd.IdPerson=Lecturers.PersonId)" +
                         " as PersAddLect on IdPlaceWork=PlaceWorkId) as PersPlacLectAdd on IdPost=PostId)AS tab1 on Stavka.IdStavka=tab1.StavkaId;"*/
            //1Sel
            //string sql = "SELECT * from Lecturers";
            //SqlCommand myCommand = new SqlCommand(sql, Connections.dataBase.GetSqlConnection());
            //List<Lecturer> lecturers = new List<Lecturer>();
            //using (SqlDataReader myDataReader = myCommand.ExecuteReader())
            //{
            //    while (myDataReader.Read())
            //    {
            //        lecturers.Add(new Lecturer((int)myDataReader["Id"], (int)myDataReader["PersonId"], (int)myDataReader["PlaceWorkId"],

            //          (int)myDataReader["StavkaId"], (int)myDataReader["PostId"], (String)myDataReader["Characteristic"]));

            //    }
            //    myDataReader.Close();
            //}
            //SqlCommand myCommand;
           // List<Lecturer> lecturers = lecturersInfo.GetListLecturers();
            Dictionary<Lecturer,List<Subject>> keyValuePairs=  lecturersInfo.GetSubjectsWithLecturer();
            foreach(Lecturer lecturer in keyValuePairs.Keys)
            {
                Console.WriteLine($"Lecturer: {lecturersInfo.LecturerToString(lecturer)} teach such subjects as:");
                foreach(Subject subject in keyValuePairs[lecturer])
                {
                    Console.WriteLine(subject);
                }
                Console.WriteLine();
            }
            Dictionary<Subject, List<Lecturer>> keyValuePairs1 = lecturersInfo.GetSubjectWithLecturers();
            foreach (Subject subject1 in keyValuePairs1.Keys)
            {
                Console.WriteLine($"Subject: {subject1} taught by such teachers as:");
                foreach (Lecturer lecturer in keyValuePairs1[subject1])
                {
                    Console.WriteLine(lecturersInfo.LecturerToString(lecturer));
                }
                Console.WriteLine();
            }
            
            //string sqlSubj = "";
            //foreach (Lecturer lecturer in lecturers)
            //{
            //    Console.WriteLine($"{lecturer}\n********TEACH SUBJECT AS*********");
            //    sqlSubj = $"Select Subjects.Id AS Id,SubjName,Hours From SubLecturers inner join Subjects on Subjects.Id=SubLecturers.SubjectId where LecturerId={lecturer.Id};";
            //    myCommand = new SqlCommand(sqlSubj, Connections.dataBase.GetSqlConnection());
            //    using (SqlDataReader myDataReader = myCommand.ExecuteReader())
            //    {
            //        while (myDataReader.Read())
            //        {
            //            Console.WriteLine(new Subject((int)myDataReader["Id"], (String)myDataReader["SubjName"], (int)myDataReader["Hours"]));
            //        }
            //        Console.WriteLine();
            //        myDataReader.Close();
            //    }
            //    Console.WriteLine();

            //}
            //string sqlSubjTeachers = "";

            //sqlSubj = $"SELECT  SubjectId,SUM(Hours) as GenHours FROM SubLecturers Group by SubjectId";
            //myCommand = new SqlCommand(sqlSubj, Connections.dataBase.GetSqlConnection());
            //Dictionary<int,int> listIdSubj = new Dictionary<int, int>();
            //using (SqlDataReader myDataReader = myCommand.ExecuteReader())
            //{
            //    while (myDataReader.Read())
            //    {
            //        listIdSubj.Add((int)myDataReader["SubjectId"], (int)myDataReader["GenHours"]);
            //        //Console.WriteLine(GetData.getSubjectById(currId));
            //    }
            //    Console.WriteLine();
            //    myDataReader.Close();
            //}
            //List<Subject> subjectslist = new List<Subject>();
            //foreach(int item in listIdSubj.Keys)
            //{
            //    Subject tmp =Connections.getData.getSubjectById(item, Connections.dataBase.GetSqlConnection());
            //    tmp.Hours = listIdSubj[item];
            //    subjectslist.Add(tmp);
            //}
            //List<int> lectId = new List<int>();
            //foreach (Subject subj in subjectslist)
            //{
            //    Console.WriteLine($"{subj}\n********IS TAUGHT BY SUCH  TEACHERS AS*********");
            //    sqlSubj = $"SELECT LecturerId FROM SubLecturers where SubjectId = {subj.Id}";
            //    myCommand = new SqlCommand(sqlSubj, Connections.dataBase.GetSqlConnection());
            //    using (SqlDataReader myDataReader = myCommand.ExecuteReader())
            //    {
            //        while (myDataReader.Read())
            //        {
            //            lectId.Add((int)myDataReader["LecturerId"]);

            //        }

            //        Console.WriteLine();
            //        myDataReader.Close();

            //    }
            //    foreach(int item in lectId)
            //    {
            //        Console.WriteLine($"{Connections.getData.getLecturerById(item, Connections.dataBase.GetSqlConnection())}");
            //    }
            //    Console.WriteLine();





            //}

            //Connections.dataBase.CloseConnection();
        }
    }
}

