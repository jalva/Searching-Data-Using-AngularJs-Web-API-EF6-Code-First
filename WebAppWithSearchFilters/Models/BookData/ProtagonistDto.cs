using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppWithSearchFilters.Models.Dtos
{

    public class ProtagonistDto
    {
        public int ProtagonistId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ProtagonistDto() { }

        public ProtagonistDto(Protagonist p)
        {
            ProtagonistId = p.ProtagonistId;
            FirstName = p.FirstName;
            LastName = p.LastName;
        }
    }

}