using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GestaoServico.Models
{
    public class Telefone : Entity {
        
        private static Regex rx = new Regex(@"^(?:\(?([1-9][0-9])\)?\s?)?(?:((?:9\d|[2-9])\d{3})\-?(\d{4}))$",
          RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public long PessoaId {get; set;}

        [Required(ErrorMessage = "Informe um ddd válido para o Brasil")]
        [Range(11,99)]
        public int Ddd {get; set;}

        [Required(ErrorMessage = "Informe um número de telefone válido")]
        [DataType(DataType.PhoneNumber)]
        // [RegularExpression(@"^(?:(?:\+|00)?(55)\s?)?(?:\(?([1-9][0-9])\)?\s?)?(?:((?:9\d|[2-9])\d{3})\-?(\d{4}))$")]
        [RegularExpression(@"^(?:((?:9\d|[2-9])\d{3})\-?(\d{4}))$", ErrorMessage = "Número de telefone inválido")]
        public string Numero {get; set;}

        public Telefone(){

        }
        public Telefone(
            [RegularExpression(@"^(?:\(?([1-9][0-9])\)?\s?)?(?:((?:9\d|[2-9])\d{3})\-?(\d{4}))$",
            ErrorMessage = "Número de telefone inválido")] string numero)
        {
            var aux =  Telefone.Parse(numero);
            this.Ddd = aux.Ddd;
            this.Numero = aux.Numero;
        }

        public static Telefone Parse( string numero){
            Match match = rx.Match(numero);
            Telefone tel = null;
            if(match.Groups.Count >=3){
                tel = new Telefone();
                GroupCollection groups = match.Groups;
                tel.Ddd = Int32.Parse(groups[0].Value);
                tel.Numero = $"{groups[1].Value}-{groups[1].Value}";
            }
            return tel;
        }

        override
        public string ToString(){
            GroupCollection groups = this.Tokenize();
            return $"({groups[0].Value}){groups[1].Value}-{groups[1].Value}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            // base.Equals (obj);
            Telefone tel = (Telefone) obj;
            return this.Ddd == tel.Ddd && this.Numero.Equals(tel.Numero);
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // throw new System.NotImplementedException();
            return base.GetHashCode();
        }

        private GroupCollection Tokenize(Match match = null){
            Match _match = match ?? rx.Match($"({this.Ddd}){this.Numero}");
            if(_match.Groups.Count >=3){
                return match.Groups;
            }
            return null;
        }
    }
}