using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace lab1Lect
{
    class Model
    {
       
        public  Address getAddressById(int id, SqlConnection sqlConnection)
        {
            //string sql = $"SELECT * from Addresses where IdAddress={id};";
            string sql = $"SELECT * from Addresses where Id={id};";
            SqlCommand myCommand = new SqlCommand(sql,sqlConnection);
            Address address=new Address();
            using (SqlDataReader myDataReader = myCommand.ExecuteReader())
            {
                while (myDataReader.Read())
                {
                    address= new Address((String)myDataReader["Country"], (String)myDataReader["City"], (String)myDataReader["Street"], (String)myDataReader["HouseNumber"],(int)myDataReader["Id"]);
                }
                myDataReader.Close();
            }
            return address;
        }
        public  Person getPersonById(int id, SqlConnection sqlConnection)
        {
            // string sql = $"SELECT * from Persons where IdPerson={id};";
            string sql = $"SELECT * from Persons where Id={id};";
            SqlCommand myCommand = new SqlCommand(sql, sqlConnection);
            Person person = new Person();
            using (SqlDataReader myDataReader = myCommand.ExecuteReader())
            {
                while (myDataReader.Read())
                {
                    person = new Person((int)myDataReader["Id"], (String)myDataReader["FirstName"],(String)myDataReader["LastName"], (String)myDataReader["MiddleName"], (String)myDataReader["PhoneNumber"], (int)myDataReader["AddressId"]);
                }
                myDataReader.Close();
            }
            return person;
        }
        public  PlaceWork getPlaceWorkById(int id, SqlConnection sqlConnection)
        {
            string sql = $"SELECT * from PlacesWork where Id={id};";
            SqlCommand myCommand = new SqlCommand(sql,sqlConnection);
            PlaceWork placeWork = new PlaceWork();
            using (SqlDataReader myDataReader = myCommand.ExecuteReader())
            {
                while (myDataReader.Read())
                {
                    placeWork = new PlaceWork((int)myDataReader["Id"], (String)myDataReader["PlaceName"]);
                }
                myDataReader.Close();
            }
            return placeWork;
        }
        public  Post getPostById(int id, SqlConnection sqlConnection)
        {
            string sql = $"SELECT * from Posts where Id={id};";
            SqlCommand myCommand = new SqlCommand(sql, sqlConnection);
            Post post = new Post();
            using (SqlDataReader myDataReader = myCommand.ExecuteReader())
            {
                while (myDataReader.Read())
                {
                    post = new Post((int)myDataReader["Id"], (String)myDataReader["PostName"]);
                }
                myDataReader.Close();
            }
            return post;
        }
        public  Stavka getStavkaById(int id, SqlConnection sqlConnection)
        {
            string sql = $"SELECT * from Stavka where Id={id};";
            SqlCommand myCommand = new SqlCommand(sql,sqlConnection);
            Stavka stavka = new Stavka();
            using (SqlDataReader myDataReader = myCommand.ExecuteReader())
            {
                while (myDataReader.Read())
                {
                    stavka = new Stavka((int)myDataReader["Id"], Convert.ToSingle(myDataReader["Count"]));
                }
                myDataReader.Close();
            }
            return stavka;
            
        }

        public Subject getSubjectById(int id, SqlConnection sqlConnection)
        {
            string sql = $"SELECT * FROM Subjects WHERE Id={id};";
            SqlCommand myCommand = new SqlCommand(sql, sqlConnection);
            Subject subject = new Subject();
            using (SqlDataReader myDataReader = myCommand.ExecuteReader())
            {
                while (myDataReader.Read())
                {
                   subject = new Subject((int)myDataReader["Id"], (String)(myDataReader["SubjName"]));
                }
                myDataReader.Close();
            }
            return subject;
        }
        public Lecturer getLecturerById(int id, SqlConnection sqlConnection)
        {
            string sql = $"SELECT * FROM Lecturers where Id={id};";
            SqlCommand myCommand = new SqlCommand(sql,sqlConnection);
            Lecturer lecturer = new Lecturer();
            using (SqlDataReader myDataReader = myCommand.ExecuteReader())
            {
                while (myDataReader.Read())
                {
                    lecturer = new Lecturer((int)myDataReader["Id"], (int)myDataReader["PersonId"], (int)myDataReader["PlaceWorkId"],
                        (int)myDataReader["StavkaId"], (int)myDataReader["PostId"], (String)myDataReader["Characteristic"]);
                }
                myDataReader.Close();
            }
            return lecturer;
        }
    }
}
