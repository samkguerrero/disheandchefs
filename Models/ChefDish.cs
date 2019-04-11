using System;
using System.ComponentModel.DataAnnotations;

namespace ChefsDishes.Models
{
    public class ChefDish
    {

        public Chef Chef {get;set;}
        public Dish Dish {get;set;}

    }
}