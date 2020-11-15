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
            DBService dBService= new DBService();
            if (dBService.OpenConnection())
            {
                Console.WriteLine($"Connection is opened!!!");
                Model getData = new Model();

                LecturerService lecturersInfo = new LecturerService(dBService, getData);

                Dictionary<Lecturer, List<Subject>> keyValuePairs = lecturersInfo.GetSubjectsWithLecturer();
                foreach (Lecturer lecturer in keyValuePairs.Keys)
                {
                    Console.WriteLine($"Lecturer: {lecturersInfo.LecturerToString(lecturer)} teach such subjects as:");
                    foreach (Subject subject in keyValuePairs[lecturer])
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
                Console.ReadLine();
               if(dBService.CloseConnection())
                {
                    Console.WriteLine("Connection is closed!!!");
                }
               else
                {
                    Console.WriteLine("Connection is not closed!!!");
                }
            }
            else
            {
                Console.WriteLine("Connection is not opened!!!");
            }
            
        }

    }
}

