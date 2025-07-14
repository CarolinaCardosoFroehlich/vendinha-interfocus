using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vendinha_backend.Interfaces
{
    public interface INomeavel
    {
        string Nome { get; set; }
    }
    class Pessoa : INomeavel
    {
        public string Nome { get; set; }
    }
}
