using System.Collections.Generic;

namespace Ventus.Models
{
    public class Cliente : Paged
    {
        public List<BE.Cliente> Data { get; set; }
    }
}