using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassUse
{
    class Moive
    {
         public Moive() { }
         public Moive(string actor, string director, string movieName, string movieType, string poster, double price,string putDay,int length)
        {
            this.Actor = actor;
            this.Director = director;
            this.MovieName = movieName;
            this.MovieType = movieType;
            this.Poster = poster;
            this.Price = price;
            this.PutDay = putDay;
            this.Length = length;

        }

        //导演
        public string Actor { get; set; }
        //演员
        public string Director { get; set; }
        //电影名
        public string MovieName { get; set; }
        //电影类型
        public string MovieType { get; set; }
        //海报路径
        public string Poster { get; set; }
        //电影价格
        public double Price { get; set; }
        public string PutDay { get; set; }
        public int Length { get; set; }
    }
}
