using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml;

using Mycelo.Parsecs;

namespace dcode
{
    class Program
    {
        static void Main(string[] args)
        {
            var clp = new ParsecsParser();
            clp.AddOption('h', "help");
            var clp_xml = clp.AddCommand("xml", "XML formatting");
            var clp_json = clp.AddCommand("json", "Json formatting");
            var clp_url = clp.AddCommand("url", "URL decoding");
            var clp_b64 = clp.AddCommand("b64", "Base64 decoding");

            if (clp.Parse(args))
            {
                if (clp['h'])
                {
                    Console.Write(clp.HelpText());
                }
                else
                {
                    using var stdin = Console.OpenStandardInput();
                    using var stdout = Console.OpenStandardOutput();

                    switch (clp.Command.Name)
                    {
                        case "xml":
                            {
                                using var xml_text_writer = new XmlTextWriter(stdout, Encoding.UTF8);
                                xml_text_writer.Formatting = Formatting.Indented;
                                var xml_document = new XmlDocument();
                                xml_document.Load(stdin);
                                xml_document.WriteContentTo(xml_text_writer);
                            }
                            break;
                        case "json":
                            {
                                using var utf8_json_writer = new Utf8JsonWriter(stdout, new JsonWriterOptions() { Indented = true });
                                using var json_document = JsonDocument.Parse(stdin);
                                json_document.WriteTo(utf8_json_writer);
                            }
                            break;
                        case "url":
                            {
                                using var stream_reader = new StreamReader(stdin, Encoding.UTF8);
                                using var stream_writer = new StreamWriter(stdout, Encoding.UTF8);
                                var url_string = stream_reader.ReadToEnd();
                                stream_writer.Write(Uri.UnescapeDataString(url_string.Replace('+', '\x20')));
                            }
                            break;
                        case "b64":
                            {
                                using var stream_reader = new StreamReader(stdin, Encoding.UTF8);
                                using var stream_writer = new StreamWriter(stdout, Encoding.UTF8);
                                var b64_string = stream_reader.ReadToEnd();
                                stream_writer.Write(Convert.FromBase64String(b64_string));
                            }
                            break;
                    }
                }
            }
        }
    }
}
