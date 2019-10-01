using System;
using Mycelo.Parsecs;

namespace dcode
{
    class Program
    {
        static void Main(string[] args)
        {
            var clp = new ParsecsParser();
            var clp_xml = clp.AddCommand("xml", "XML formatting");
            var clp_xml_r = clp_xml.AddOption('r', "remove", "remove all XML formatting");
            var clp_json = clp.AddCommand("json", "Json formatting");
            var clp_json_r = clp_json.AddOption('r', "remove", "remove all Json formatting");
            var clp_url = clp.AddCommand("url", "URL percent decoding");
            var clp_url_d = clp_url.AddOption('e', "encode", "perform URL encoding instead");
            var clp_b64 = clp.AddCommand("b64", "Base64 decoding");
            var clp_b64_d = clp_b64.AddOption('e', "encode", "perform Base64 encoding instead");
        }
    }
}
