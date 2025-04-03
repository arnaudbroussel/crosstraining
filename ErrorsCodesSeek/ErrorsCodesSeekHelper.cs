using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace crosstraining.ErrorsCodesSeek
{
    ////ouvrir tous les fichers dont le nom se termine par Mappings.cs d'un répertoire et tous ses sous-repertoires.
    ////pour chaque ligne qui contient 'ApiExceptionMapper.RegisterException', extraire la chaine '(' et ')' et remplacer le '.' par ';' et préparer une liste s1 avec cette chaine.
    ////extraire la chaine entre '<' et '>' et lister cette valeur concaténée avec s1 et les séparer par ';'. appelons s2 cette liste.
    ////pour chaque element l de la liste s2, separer les élements par ';'
    ////ouvrir le fichier dont le nom, n, est le deuxième élement de s2 concaténé avec '.cs' qui se trouve dans un sous-repertoire d'un répertoire donné.
    ////dans le fichier chercher la ligne lx qui contient le troisième élément de l, et en extraire la première ligne antérieure qui ne commence pas par '/// </summary>' et pas par '/// <summary>' appelons-la la et remplacer '/// ' par ''.
    ////dans lx, extraire la chaine entre '"' rt '"', appelons-la ex
    ////quand la se termine par '.' eliminer le '.'
    ////appelons firstofl, le premier element de l
    ////si firstofl = UnprocessableRequestException remplacer la valeur par 422
    ////si firstofl = ResourceNotFoundException remplacer la valeur par 404
    ////si firstofl = BadRequestException remplacer la valeur par 400
    ////si firstofl = ConflictException remplacer la valeur par 409
    ////si firstofl = UnauthorizedRequestException remplacer la valeur par 401
    ////si firstofl = ForbiddenRequestException remplacer la valeur par 403
    ////si la n'est pas null ou vide, preparer une liste, f, avec une chaine qui concatène firstofl-ex, firstofl-ex - la et séparer ces deux chaines par ';'
    ////générer une structure json dont las base est 'options' à partir de chaque ligne lj de f
    ////chaque valeur v1 de 'options' est le premier element de lj
    ////v1 a une valeur unique, ce n'est pas un tableau, constituée de 'index' qui est un numérique qui correspond au numéro de la ligne de f et de 'text' qui correspond au 2ème élément de lj
    public class ErrorsCodesSeekHelper
    {
        public static void Seek(string targetDirectory, string baseDirectory)
        {
            List<string> s2List = ProcessMappings(baseDirectory);
            List<string> finalResults = new List<string>();

            foreach (var s2 in s2List)
            {
                string[] elements = s2.Split(';');
                if (elements.Length < 3) continue;

                string firstofl = elements[0];
                string n = elements[1] + ".cs"; // Ajoute .cs au deuxième élément
                string searchValue = elements[2];

                // Remplacement des exceptions par codes HTTP
                firstofl = ReplaceExceptionWithHttpCode(firstofl);

                string foundFile = FindFile(targetDirectory, n);
                if (!string.IsNullOrEmpty(foundFile))
                {
                    string la, ex;
                    if (GetLineInfo(foundFile, searchValue, out la, out ex))
                    {
                        // Si la n'est pas null ou vide, préparer la liste f
                        if (!string.IsNullOrEmpty(la))
                        {
                            string result = $"{firstofl}-{ex}; {firstofl}-{ex} - {la}";
                            finalResults.Add(result);
                        }
                    }
                }
            }

            // Générer la structure JSON 'options'
            var options = new Dictionary<string, Dictionary<string, object>>();
            foreach (var line in finalResults.Select((value, index) => new { value, index }))
            {
                string[] parts = line.value.Split(';');
                if (parts.Length < 2) continue;

                string firstElement = parts[0].Trim();
                string text = parts[1].Trim();

                // Créer l'objet pour chaque élément
                var element = new Dictionary<string, object>
            {
                { "index", line.index },
                { "text", text }
            };

                // Ajouter cet élément dans le sous-noeud "options" sous le premier élément
                if (!options.ContainsKey(firstElement))
                {
                    options[firstElement] = new Dictionary<string, object>();
                }
                options[firstElement] = element;
            }

            // Sérialiser la structure JSON
            var root = new Dictionary<string, object>
        {
            { "options", options }
        };

            // Sauvegarder les résultats dans un fichier JSON
            string json = JsonConvert.SerializeObject(root, Formatting.Indented);
            File.WriteAllText("Results.json", json);
            Console.WriteLine("Le fichier JSON a été généré.");
        }

        // Processus de traitement des fichiers Mappings.cs
        static List<string> ProcessMappings(string directoryPath)
        {
            List<string> s2List = new List<string>();
            Regex regex = new Regex(@"ApiExceptionMapper\.RegisterException<([^>]+)>\(([^)]+)\)");

            foreach (string file in Directory.GetFiles(directoryPath, "*Mappings.cs", SearchOption.AllDirectories))
            {
                string[] lines = File.ReadAllLines(file);
                foreach (string line in lines)
                {
                    Match match = regex.Match(line);
                    if (match.Success)
                    {
                        string extractedType = match.Groups[1].Value.Trim();
                        string s1 = match.Groups[2].Value.Replace(".", ";").Trim(); // Remplace '.' par ';'
                        string s2 = extractedType + ";" + s1;
                        s2List.Add(s2);
                    }
                }
            }

            return s2List;
        }

        // Trouver le fichier cible en fonction du nom du fichier
        static string FindFile(string rootDirectory, string fileName)
        {
            return Directory.GetFiles(rootDirectory, fileName, SearchOption.AllDirectories).FirstOrDefault();
        }

        // Extraire les informations à partir du fichier cible
        static bool GetLineInfo(string filePath, string searchValue, out string la, out string ex)
        {
            string[] lines = File.ReadAllLines(filePath);
            la = null;
            ex = null;

            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].Contains(searchValue))
                {
                    // Extraire la valeur entre "..."
                    Match match = Regex.Match(lines[i], "\"(.*?)\"");
                    if (match.Success)
                    {
                        ex = match.Groups[1].Value;
                    }

                    // Rechercher la première ligne antérieure qui ne commence pas par "/// </summary>" et "/// <summary>"
                    for (int j = i - 1; j >= 0; j--)
                    {
                        string trimmedLine = lines[j].Trim();
                        if (!trimmedLine.StartsWith("/// </summary>") && !trimmedLine.StartsWith("/// <summary>"))
                        {
                            la = trimmedLine.Replace("/// ", "").Trim(); // Supprime "/// "
                            la = la.EndsWith(".") ? la[..^1] : la; // Supprime le "." final
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        // Remplacement des exceptions par leurs codes HTTP correspondants
        static string ReplaceExceptionWithHttpCode(string exception)
        {
            return exception switch
            {
                "UnprocessableRequestException" => "422",
                "ResourceNotFoundException" => "404",
                "BadRequestException" => "400",
                "ConflictException" => "409",
                "UnauthorizedRequestException" => "401",
                "ForbiddenRequestException" => "403",
                _ => exception
            };
        }
    }
}