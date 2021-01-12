using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaModel.Models;

namespace Tomoiaga_AndreiBogdan_CinemaTAB.Models.CinemaViewModels
{
    public class ActorIndexData
    {
        public IEnumerable<Actor> Actors { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
        public IEnumerable<Studio> Studios { get; set; }
    }
}
