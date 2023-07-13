﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("ROCKET")]
    public class Rocket : BaseEntity
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("ID_CONFIGURATION")]
        public int IdConfiguration { get; set; }
        [JsonIgnore]
        [ForeignKey(nameof(IdConfiguration))]
        public Configuration Configuration { get; set; }
        public Rocket() 
        {
            Configuration = new Configuration();
        }
    }
}
