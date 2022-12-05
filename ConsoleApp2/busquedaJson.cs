using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusquedaJson
{
    public class Programa
    {
        public string? texto;
        public string? numero;
        public string? punto;
    }
    internal class BusquedaJson : busquedaJsonBaseVisitor<Programa>
    {
        public override Programa VisitArray([NotNull] busquedaJsonParser.ArrayContext context)
        {
            Programa programa = new Programa();
            programa.texto = context.DOBLEPUNTO().GetText();
            programa.numero = context.NUMERO().GetText();
            return programa;
        }

        public override Programa VisitInput([NotNull] busquedaJsonParser.InputContext context)
        {
            Programa programa;
            bool ARRAY = context.array() != null;
            bool PUNTO = context.PUNTO() != null;

            if (ARRAY)
            {
                programa = new Programa();
                programa.texto = context.TEXTO().GetText();
                programa = Visit(context.array());

                if (PUNTO)
                {
                    programa.punto = context.PUNTO().GetText();
                }
            }
            else
            {
                programa = new Programa();
                programa.texto = context.TEXTO().GetText();

                if (PUNTO)
                {
                    programa.punto = context.PUNTO().GetText();
                }
            }
           
            return programa;
        }

        public override Programa VisitProgram([NotNull] busquedaJsonParser.ProgramContext context)
        {
            Programa programa = new Programa();

            if (context.input().Length > 1)
            {
                for (int i = 0; i < context.input().Length; i++)
                {
                    programa = Visit(context.input()[i]);
                }
            }
            else
            {
                programa = Visit(context.input()[0]);
            }
            return programa;
        }
    }
}
