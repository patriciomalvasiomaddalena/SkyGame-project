using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Localization_Cutter
{
   public static Dictionary<Languaje,Dictionary<string,string>> LoadCodex(string Sheet,string Source)
   {
        var Codex = new Dictionary<Languaje,Dictionary<string,string>>();

        var LangColumn = new Dictionary<int, Languaje>();
        var IDColumn = 0;

        //separar lineas del codex, guardar codex en un array
        var Lines = Sheet.Split(new[] {'\n', '\r'}, System.StringSplitOptions.RemoveEmptyEntries);

        bool firstLine = true;


        foreach(var TextLine in Lines)
        {
            var Cells = TextLine.Split(',');


            if (firstLine)
            {
                firstLine = false;

                for(int I = 0; I < Cells.Length; I++)
                {
                    #region FirstLineSetup
                    if (!Cells[I].Contains("ID")) // check para saber que no es el ID del texto
                    {
                        try
                        {
                           //check si contiene un enum de lenguaje
                            LangColumn[I] = (Languaje)Enum.Parse(typeof(Languaje), Cells[I]); ;
                        }
                        catch (Exception e)
                        {
                            Debug.LogWarning("Source: " + Source + " Error en el parseo de datos del Localization Cutter: " + e.ToString());
                            continue;
                        }

                        // Preguntar si el codex ya contiene el Enum de lenguaje, en el caso de no tenerlo, creamos el diccionario que sera contenido en tal enum
                        if (!Codex.ContainsKey(LangColumn[I]))
                        {
                            Codex[LangColumn[I]] = new Dictionary<string, string>();
                        }

                    }
                    else
                    {
                        IDColumn = I;
                    }
                    #endregion
                }

                continue;
            }


            for (int B = 0; B < Cells.Length; B++)
            {
                if(B == IDColumn) { continue; }

                var lang = LangColumn[B];
                var langID = Cells[IDColumn];
                var textvalue = Cells[B];

                Codex[lang][langID] = textvalue;
            }
        }
        return Codex;
   }


}
