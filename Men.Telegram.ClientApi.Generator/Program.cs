using Men.Telegram.ClientApi.Generator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Men.Telegram.ClientApi.Generator
{
    class Program
    {
        private const string c_SchemaUrl = "https://core.telegram.org/schema/json";
        private const string c_SchemaFileName = "schema.json";

        private const string c_TypeThis = "himself";
        private const string c_GeneratedNameSpace = "TeleSharp";

        private static string FullSchemaFileName => $"{Directory.GetCurrentDirectory()}/{c_SchemaFileName}";

        private static readonly List<string> keywords = new List<string>(new string[]
        {
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char",
            "checked", "class", "const", "continue", "decimal", "default", "delegate",
            "do", "double", "else", "enum", "event", "explicit", "extern", "false",
            "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit",
            "in", "int", "interface", "internal", "is", "lock", "long", "namespace",
            "new", "null", "object", "operator", "out", "override", "params",
            "private", "protected", "public", "readonly", "ref", "return", "sbyte",
            "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct",
            "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong",
            "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile",
            "while", "add", "alias", "ascending", "async", "await", "descending", "dynamic",
            "from", "get", "global", "group", "into", "join", "let", "orderby", "partial",
            "remove", "select", "set", "value", "var", "where", "yield"
        });

        private static readonly List<string> interfacesList = new List<string>();
        private static readonly List<string> classesList = new List<string>();


        static void Main(string[] args)
        {
            Console.WriteLine("--- TL generator ---");

            string absCsTemplate = File.ReadAllText("Tempaltes/ConstructorAbs.cstemplate");
            string normalCsTemplate = File.ReadAllText("Tempaltes/Constructor.cstemplate");
            string methodCsTemplate = File.ReadAllText("Tempaltes/Method.cstemplate");
            //string method = File.ReadAllText("constructor.tt");

            string schemaJson = LoadSchema()
                .GetAwaiter().GetResult();
            TlSchema schema = JsonConvert.DeserializeObject<TlSchema>(schemaJson);

            //FileStream file = File.OpenWrite("Result.cs");
            //StreamWriter sw = new StreamWriter(file);

            foreach (TlConstructor constructor in schema.Constructors)
            {
                interfacesList.Add(constructor.Type);
                classesList.Add(constructor.Predicate);
            }

            foreach (TlConstructor constructor in schema.Constructors)
            {
                IEnumerable<TlConstructor> list = schema.Constructors.Where(x => x.Type == constructor.Type);
                if (list.Count() > 1)
                {
                    string path = (GetNameSpace(constructor.Type).Replace("TeleSharp.TL", "TL\\").Replace(".", "") + "\\" + GetNameofClass(constructor.Type, true) + ".cs").Replace("\\\\", "\\");
                    FileStream classFile = MakeFile(path);
                    using (StreamWriter writer = new StreamWriter(classFile))
                    {
                        string nspace = GetNameSpace(constructor.Type).Replace("TeleSharp.TL", "TL\\").Replace(".", "").Replace("\\\\", "\\").Replace("\\", ".");
                        if (nspace.EndsWith("."))
                        {
                            nspace = nspace.Remove(nspace.Length - 1, 1);
                        }

                        string temp = absCsTemplate.Replace("/* NAMESPACE */", "TeleSharp." + nspace);
                        temp = temp.Replace("/* NAME */", GetNameofClass(constructor.Type, true));
                        writer.Write(temp);
                        writer.Close();
                        classFile.Close();
                    }
                }
                else
                {
                    interfacesList.Remove(list.First().Type);
                    list.First().Type = c_TypeThis;
                }
            }

            foreach (TlConstructor c in schema.Constructors)
            {
                string path = (GetNameSpace(c.Predicate).Replace("TeleSharp.TL", "TL\\").Replace(".", "") + "\\" + GetNameofClass(c.Predicate, false) + ".cs").Replace("\\\\", "\\");
                FileStream classFile = MakeFile(path);
                using (StreamWriter writer = new StreamWriter(classFile))
                {
                    #region About Class
                    string nspace = GetNameSpace(c.Predicate).Replace("TeleSharp.TL", "TL\\").Replace(".", "").Replace("\\\\", "\\").Replace("\\", ".");
                    if (nspace.EndsWith("."))
                    {
                        nspace = nspace.Remove(nspace.Length - 1, 1);
                    }

                    string temp = normalCsTemplate.Replace("/* NAMESPACE */", "TeleSharp." + nspace);
                    temp = c.Type == c_TypeThis ? temp.Replace("/* PARENT */", "TLObject") : temp.Replace("/* PARENT */", GetNameofClass(c.Type, true));
                    temp = temp.Replace("/*Constructor*/", c.Id.ToString());
                    temp = temp.Replace("/* NAME */", GetNameofClass(c.Predicate, false));
                    #endregion
                    #region Fields
                    string fields = "";
                    foreach (TlParam tmp in c.Params)
                    {
                        fields += $"     public {CheckForFlagBase(tmp.Type, GetTypeName(tmp.Type))} {CheckForKeywordAndPascalCase(tmp.Name)} " + "{get;set;}" + Environment.NewLine;
                    }
                    temp = temp.Replace("/* PARAMS */", fields);
                    #endregion
                    #region ComputeFlagFunc
                    if (!c.Params.Any(x => x.Name == "Flags"))
                    {
                        temp = temp.Replace("/* COMPUTE */", "");
                    }
                    else
                    {
                        string compute = "Flags = 0;" + Environment.NewLine;
                        foreach (TlParam param in c.Params.Where(x => IsFlagBase(x.Type)))
                        {
                            if (IsTrueFlag(param.Type))
                            {
                                compute += $"Flags = {CheckForKeywordAndPascalCase(param.Name)} ? (Flags | {GetBitMask(param.Type)}) : (Flags & ~{GetBitMask(param.Type)});" + Environment.NewLine;
                            }
                            else
                            {
                                compute += $"Flags = {CheckForKeywordAndPascalCase(param.Name)} != null ? (Flags | {GetBitMask(param.Type)}) : (Flags & ~{GetBitMask(param.Type)});" + Environment.NewLine;
                            }
                        }
                        temp = temp.Replace("/* COMPUTE */", compute);
                    }
                    #endregion
                    #region SerializeFunc
                    string serialize = "";

                    if (c.Params.Any(x => x.Name == "Flags"))
                    {
                        serialize += "ComputeFlags();" + Environment.NewLine + "bw.Write(Flags);" + Environment.NewLine;
                    }

                    foreach (TlParam p in c.Params.Where(x => x.Name != "Flags"))
                    {
                        serialize += WriteWriteCode(p) + Environment.NewLine;
                    }
                    temp = temp.Replace("/* SERIALIZE */", serialize);
                    #endregion
                    #region DeSerializeFunc
                    string deserialize = "";

                    foreach (TlParam p in c.Params)
                    {
                        deserialize += WriteReadCode(p) + Environment.NewLine;
                    }
                    temp = temp.Replace("/* DESERIALIZE */", deserialize);
                    #endregion
                    writer.Write(temp);
                    writer.Close();
                    classFile.Close();
                }
            }
            foreach (TlMethod c in schema.Methods)
            {
                string path = (GetNameSpace(c.Method).Replace("TeleSharp.TL", "TL\\").Replace(".", "") + "\\" + GetNameofClass(c.Method, false, true) + ".cs").Replace("\\\\", "\\");
                FileStream classFile = MakeFile(path);
                using (StreamWriter writer = new StreamWriter(classFile))
                {
                    #region About Class
                    string nspace = GetNameSpace(c.Method).Replace("TeleSharp.TL", "TL\\").Replace(".", "").Replace("\\\\", "\\").Replace("\\", ".");
                    if (nspace.EndsWith("."))
                    {
                        nspace = nspace.Remove(nspace.Length - 1, 1);
                    }

                    string temp = methodCsTemplate.Replace("/* NAMESPACE */", "TeleSharp." + nspace);
                    temp = temp.Replace("/* PARENT */", "TLMethod");
                    temp = temp.Replace("/*Constructor*/", c.Id.ToString());
                    temp = temp.Replace("/* NAME */", GetNameofClass(c.Method, false, true));
                    #endregion
                    #region Fields
                    string fields = "";
                    foreach (TlParam tmp in c.Params)
                    {
                        fields += $"        public {CheckForFlagBase(tmp.Type, GetTypeName(tmp.Type))} {CheckForKeywordAndPascalCase(tmp.Name)} " + "{get;set;}" + Environment.NewLine;
                    }
                    fields += $"        public {CheckForFlagBase(c.Type, GetTypeName(c.Type))} Response" + "{ get; set;}" + Environment.NewLine;
                    temp = temp.Replace("/* PARAMS */", fields);
                    #endregion
                    #region ComputeFlagFunc
                    if (!c.Params.Any(x => x.Name == "Flags"))
                    {
                        temp = temp.Replace("/* COMPUTE */", "");
                    }
                    else
                    {
                        string compute = "Flags = 0;" + Environment.NewLine;
                        foreach (TlParam param in c.Params.Where(x => IsFlagBase(x.Type)))
                        {
                            if (IsTrueFlag(param.Type))
                            {
                                compute += $"Flags = {CheckForKeywordAndPascalCase(param.Name)} ? (Flags | {GetBitMask(param.Type)}) : (Flags & ~{GetBitMask(param.Type)});" + Environment.NewLine;
                            }
                            else
                            {
                                compute += $"Flags = {CheckForKeywordAndPascalCase(param.Name)} != null ? (Flags | {GetBitMask(param.Type)}) : (Flags & ~{GetBitMask(param.Type)});" + Environment.NewLine;
                            }
                        }
                        temp = temp.Replace("/* COMPUTE */", compute);
                    }
                    #endregion
                    #region SerializeFunc
                    string serialize = "";

                    if (c.Params.Any(x => x.Name == "Flags"))
                    {
                        serialize += "ComputeFlags();" + Environment.NewLine + "bw.Write(Flags);" + Environment.NewLine;
                    }

                    foreach (TlParam p in c.Params.Where(x => x.Name != "Flags"))
                    {
                        serialize += WriteWriteCode(p) + Environment.NewLine;
                    }
                    temp = temp.Replace("/* SERIALIZE */", serialize);
                    #endregion
                    #region DeSerializeFunc
                    string deserialize = "";

                    foreach (TlParam p in c.Params)
                    {
                        deserialize += WriteReadCode(p) + Environment.NewLine;
                    }
                    temp = temp.Replace("/* DESERIALIZE */", deserialize);
                    #endregion
                    #region DeSerializeRespFunc
                    string deserializeResp = "";
                    TlParam p2 = new TlParam() { Name = "Response", Type = c.Type };
                    deserializeResp += WriteReadCode(p2) + Environment.NewLine;
                    temp = temp.Replace("/* DESERIALIZEResp */", deserializeResp);
                    #endregion
                    writer.Write(temp);
                    writer.Close();
                    classFile.Close();
                }
            }
        }

        private static async Task<string> LoadSchema()
        {
            if (File.Exists(FullSchemaFileName))
            {
                return File.ReadAllText(FullSchemaFileName);
            }

            string schemaJson = string.Empty;
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(c_SchemaUrl);
                response.EnsureSuccessStatusCode();

                schemaJson = await response.Content.ReadAsStringAsync();
            }

            File.WriteAllText(FullSchemaFileName, schemaJson);
            return schemaJson;
        }


        public static string FormatSentenceIntoCamelCaseString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException($"Argument {nameof(value)} can't be null or empty");
            }

            value = value
                .Replace(".", " ")
                .Replace(" ", null);

            string firstUpperCaseString = $"{value.First().ToString().ToUpper()}{value[1..]}";
            return firstUpperCaseString;
        }


        public static string CheckForKeywordAndPascalCase(string name)
        {
            name = name.Replace("_", " ");
            name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
            name = name.Replace(" ", "");

            if (keywords.Contains(name))
            {
                return "@" + name;
            }

            return name;
        }
        public static string GetNameofClass(string type, bool isinterface = false, bool ismethod = false)
        {
            if (!ismethod)
            {
                if (type.Contains('.') && !type.Contains('?'))
                {
                    return isinterface ? "TLAbs" + FormatSentenceIntoCamelCaseString(type.Split('.')[1]) : "TL" + FormatSentenceIntoCamelCaseString(type.Split('.')[1]);
                }
                else if (type.Contains('.') && type.Contains('?'))
                {
                    return isinterface ? "TLAbs" + FormatSentenceIntoCamelCaseString(type.Split('?')[1]) : "TL" + FormatSentenceIntoCamelCaseString(type.Split('?')[1]);
                }
                else
                {
                    return isinterface ? "TLAbs" + FormatSentenceIntoCamelCaseString(type) : "TL" + FormatSentenceIntoCamelCaseString(type);
                }
            }
            else
            {
                if (type.Contains('.') && !type.Contains('?'))
                {
                    return "TLRequest" + FormatSentenceIntoCamelCaseString(type.Split('.')[1]);
                }
                else if (type.Contains('.') && type.Contains('?'))
                {
                    return "TLRequest" + FormatSentenceIntoCamelCaseString(type.Split('?')[1]);
                }
                else
                {
                    return "TLRequest" + FormatSentenceIntoCamelCaseString(type);
                }
            }
        }
        private static bool IsFlagBase(string type)
        {
            return type.Contains("?");
        }
        private static int GetBitMask(string type)
        {
            return (int)Math.Pow(2, int.Parse(type.Split('?')[0].Split('.')[1]));
        }
        private static bool IsTrueFlag(string type)
        {
            return type.Split('?')[1] == "true";
        }

        public static string GetNameSpace(string type)
            => type.Contains('.')
                ? $"TeleSharp.TL.{FormatSentenceIntoCamelCaseString(type.Split('.')[0])}"
                : "TeleSharp.TL";

        public static string CheckForFlagBase(string type, string result)
        {
            if (!type.Contains('?'))
            {
                return result;
            }
            else
            {
                string innerType = type.Split('?')[1];
                if (innerType == "true")
                {
                    return result;
                }
                else if ((new string[] { "bool", "int", "uint", "long", "double" }).Contains(result))
                {
                    return result + "?";
                }
                else
                {
                    return result;
                }
            }
        }
        public static string GetTypeName(string type)
        {
            switch (type.ToLower())
            {
                case "#":
                case "int":
                    return "int";
                case "uint":
                    return "uint";
                case "long":
                    return "long";
                case "double":
                    return "double";
                case "string":
                    return "string";
                case "bytes":
                    return "byte[]";
                case "true":
                case "bool":
                    return "bool";
                case "!x":
                    return "TLObject";
                case "x":
                    return "TLObject";
            }

            if (type.StartsWith("Vector"))
            {
                return "TLVector<" + GetTypeName(type.Replace("Vector<", "").Replace(">", "")) + ">";
            }

            if (type.ToLower().Contains("inputcontact"))
            {
                return "TLInputPhoneContact";
            }

            if (type.Contains('.') && !type.Contains('?'))
            {

                if (interfacesList.Any(x => x.ToLower() == type.ToLower()))
                {
                    return FormatSentenceIntoCamelCaseString(type.Split('.')[0]) + "." + "TLAbs" + type.Split('.')[1];
                }
                else if (classesList.Any(x => x.ToLower() == type.ToLower()))
                {
                    return FormatSentenceIntoCamelCaseString(type.Split('.')[0]) + "." + "TL" + type.Split('.')[1];
                }
                else
                {
                    return FormatSentenceIntoCamelCaseString(type.Split('.')[1]);
                }
            }
            else if (!type.Contains('?'))
            {
                if (interfacesList.Any(x => x.ToLower() == type.ToLower()))
                {
                    return "TLAbs" + type;
                }
                else if (classesList.Any(x => x.ToLower() == type.ToLower()))
                {
                    return "TL" + type;
                }
                else
                {
                    return type;
                }
            }
            else
            {
                return GetTypeName(type.Split('?')[1]);
            }


        }
        public static string LookTypeInLists(string src)
        {
            if (interfacesList.Any(x => x.ToLower() == src.ToLower()))
            {
                return "TLAbs" + FormatSentenceIntoCamelCaseString(src);
            }
            else if (classesList.Any(x => x.ToLower() == src.ToLower()))
            {
                return "TL" + FormatSentenceIntoCamelCaseString(src);
            }
            else
            {
                return src;
            }
        }
        public static string WriteWriteCode(TlParam p, bool flag = false)
        {
            switch (p.Type.ToLower())
            {
                case "#":
                case "int":
                    return flag ? $"bw.Write({CheckForKeywordAndPascalCase(p.Name)}.Value);" : $"bw.Write({CheckForKeywordAndPascalCase(p.Name)});";
                case "long":
                    return flag ? $"bw.Write({CheckForKeywordAndPascalCase(p.Name)}.Value);" : $"bw.Write({CheckForKeywordAndPascalCase(p.Name)});";
                case "string":
                    return $"StringUtil.Serialize({CheckForKeywordAndPascalCase(p.Name)},bw);";
                case "bool":
                    return flag ? $"BoolUtil.Serialize({CheckForKeywordAndPascalCase(p.Name)}.Value,bw);" : $"BoolUtil.Serialize({CheckForKeywordAndPascalCase(p.Name)},bw);";
                case "true":
                    return $"BoolUtil.Serialize({CheckForKeywordAndPascalCase(p.Name)},bw);";
                case "bytes":
                    return $"BytesUtil.Serialize({CheckForKeywordAndPascalCase(p.Name)},bw);";
                case "double":
                    return flag ? $"bw.Write({CheckForKeywordAndPascalCase(p.Name)}.Value);" : $"bw.Write({CheckForKeywordAndPascalCase(p.Name)});";
                default:
                    if (!IsFlagBase(p.Type))
                    {
                        return $"ObjectUtils.SerializeObject({CheckForKeywordAndPascalCase(p.Name)},bw);";
                    }
                    else
                    {
                        if (IsTrueFlag(p.Type))
                        {
                            return $"";
                        }
                        else
                        {
                            TlParam p2 = new TlParam() { Name = p.Name, Type = p.Type.Split('?')[1] };
                            return $"if ((Flags & {GetBitMask(p.Type).ToString()}) != 0)" + Environment.NewLine +
                                WriteWriteCode(p2, true);
                        }
                    }
            }
        }
        public static string WriteReadCode(TlParam p)
        {
            switch (p.Type.ToLower())
            {
                case "#":
                case "int":
                    return $"{CheckForKeywordAndPascalCase(p.Name)} = br.ReadInt32();";
                case "long":
                    return $"{CheckForKeywordAndPascalCase(p.Name)} = br.ReadInt64();";
                case "string":
                    return $"{CheckForKeywordAndPascalCase(p.Name)} = StringUtil.Deserialize(br);";
                case "bool":
                case "true":
                    return $"{CheckForKeywordAndPascalCase(p.Name)} = BoolUtil.Deserialize(br);";
                case "bytes":
                    return $"{CheckForKeywordAndPascalCase(p.Name)} = BytesUtil.Deserialize(br);";
                case "double":
                    return $"{CheckForKeywordAndPascalCase(p.Name)} = br.ReadDouble();";
                default:
                    if (!IsFlagBase(p.Type))
                    {
                        if (p.Type.ToLower().Contains("vector"))
                        {
                            return $"{CheckForKeywordAndPascalCase(p.Name)} = ({GetTypeName(p.Type)})ObjectUtils.DeserializeVector<{GetTypeName(p.Type).Replace("TLVector<", "").Replace(">", "")}>(br);";
                        }
                        else
                        {
                            return $"{CheckForKeywordAndPascalCase(p.Name)} = ({GetTypeName(p.Type)})ObjectUtils.DeserializeObject(br);";
                        }
                    }
                    else
                    {
                        if (IsTrueFlag(p.Type))
                        {
                            return $"{CheckForKeywordAndPascalCase(p.Name)} = (Flags & {GetBitMask(p.Type).ToString()}) != 0;";
                        }
                        else
                        {
                            TlParam p2 = new TlParam() { Name = p.Name, Type = p.Type.Split('?')[1] };
                            return $"if ((Flags & {GetBitMask(p.Type).ToString()}) != 0)" + Environment.NewLine +
                                WriteReadCode(p2) + Environment.NewLine +
                            "else" + Environment.NewLine +
                                $"{CheckForKeywordAndPascalCase(p.Name)} = null;" + Environment.NewLine;
                        }
                    }
            }
        }
        public static FileStream MakeFile(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            return File.OpenWrite(path);
        }
    }

}