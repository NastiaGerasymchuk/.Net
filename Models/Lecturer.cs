
using System;
using System.Collections.Generic;
using System.Text;

namespace lab1Lect
{
    class Lecturer
    {
       
        public int Id { get; set; }
        public int PersonId { get; set; }
        public int PlaceWorkId { get; set; }
        public int PostId { get; set; }
        public int StavkaId { get; set; }
        public String Characteristic { get; set; }
        //public override string ToString()
        //{
        //    return $"{Connections._getData.getPersonById(PersonId, Connections._dataBase.GetSqlConnection())} {Connections._getData.getPlaceWorkById(PlaceWorkId, Connections._dataBase.GetSqlConnection())}  {Connections._getData.getPostById(PostId, Connections._dataBase.GetSqlConnection())} {Connections._getData.getStavkaById(StavkaId, Connections._dataBase.GetSqlConnection())}, Characteristic {Characteristic}";
        //    //return $"{Connections._getData.getPersonById(PersonId,Connections._dataBase.GetSqlConnection())} {Connections._getData.getPlaceWorkById(PlaceWorkId, Connections._dataBase.GetSqlConnection())}  {Connections._getData.getPostById(PostId, Connections._dataBase.GetSqlConnection())} {Connections._getData.getStavkaById(StavkaId, Connections._dataBase.GetSqlConnection())}, Characteristic {Characteristic}";
        //}
        public Lecturer(int id, int personId, int placeWorkId,
            int stavkaId, int postId, string characteristic)
            {
            Id= id;
            PersonId = personId;
            PlaceWorkId = placeWorkId;           
            StavkaId = stavkaId;
            PostId = postId;
            Characteristic = characteristic;
            }
        public Lecturer() { }
    }
}
