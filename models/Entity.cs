
using System;
using System.ComponentModel.DataAnnotations;

namespace GestaoServico.Models{
    public abstract class Entity
    {
        [Key]
        public long Id { get; set; }
        public DateTime CreatedAt
        {
            get; set;
        }

        public DateTime UpdatedAt
        {
            get; set;
        }
        public bool Deleted
        {
            get; set;
        }

        public string EntityName(){
            return this.GetType().Name.ToLower();
        }
    }
}