using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GEA_Domain.Utils
{
    public class MimeTypes
    {
        private static List<string> knownTypes;

        private static Dictionary<string, string[]> mimeTypes;

        [DllImport("urlmon.dll", CharSet = CharSet.Auto)]
        private static extern UInt32 FindMimeFromData(
            UInt32 pBC, [MarshalAs(UnmanagedType.LPStr)]
        string pwzUrl, [MarshalAs(UnmanagedType.LPArray)]
        byte[] pBuffer, UInt32 cbSize, [MarshalAs(UnmanagedType.LPStr)]
        string pwzMimeProposed, UInt32 dwMimeFlags, ref UInt32 ppwzMimeOut, UInt32 dwReserverd
        );

        public static string GetContentType(byte[] fileName)
        {
            if (knownTypes == null || mimeTypes == null)
                InitializeMimeTypeLists();
            string contentType = "";
            if (string.IsNullOrEmpty(contentType) || knownTypes.Contains(contentType))
            {
                string headerType = ScanFileForMimeType(fileName);
                if (headerType != "application/octet-stream" || string.IsNullOrEmpty(contentType))
                    contentType = headerType;
            }
            return contentType;
        }
        public static List<string> GetExtensionFromContentType(string contenttype)
        {
            List<string> types = new List<string>();
            foreach (string key in mimeTypes.Keys)
            {
                foreach(string type in mimeTypes[key])
                {
                    if (type.Equals(contenttype))
                    {
                        types.Add(key);
                    }
                }
            }
            return types;
        }
        private static string ScanFileForMimeType(byte[] fileName)
        {
            try
            {
                UInt32 mimeType = default(UInt32);
                FindMimeFromData(0, null, fileName, Convert.ToUInt32(fileName.Length), null, 0, ref mimeType, 0);
                IntPtr mimeTypePtr = new IntPtr(mimeType);
                string mime = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);
                if (string.IsNullOrEmpty(mime))
                    mime = "application/octet-stream";
                return mime;
            }
            catch (Exception ex)
            {
                return "application/octet-stream";
            }
        }

        private static void InitializeMimeTypeLists()
        {
            knownTypes = new string[] {
        "text/plain",
        "text/html",
        "text/xml",
        "text/richtext",
        "text/scriptlet",
        "audio/x-aiff",
        "audio/basic",
        "audio/mid",
        "audio/wav",
        "image/gif",
        "image/jpeg",
        "image/pjpeg",
        "image/png",
        "image/x-png",
        "image/tiff",
        "image/bmp",
        "image/x-xbitmap",
        "image/x-jg",
        "image/x-emf",
        "image/x-wmf",
        "video/avi",
        "video/mpeg",
        "application/octet-stream",
        "application/postscript",
        "application/base64",
        "application/macbinhex40",
        "application/pdf",
        "application/xml",
        "application/atom+xml",
        "application/rss+xml",
        "application/x-compressed",
        "application/x-zip-compressed",
        "application/x-gzip-compressed",
        "application/java",
        "application/x-msdownload"
    }.ToList();

            mimeTypes = new Dictionary<string, string[]>();
            mimeTypes.Add("3dm", new string[]{"x-world/x-3dmf"});
            mimeTypes.Add("3dmf", new string[]{"x-world/x-3dmf"});
            mimeTypes.Add("a", new string[]{"application/octet-stream"});
            mimeTypes.Add("aab", new string[]{"application/x-authorware-bin"});
            mimeTypes.Add("aam", new string[]{"application/x-authorware-map"});
            mimeTypes.Add("aas", new string[]{"application/x-authorware-seg"});
            mimeTypes.Add("abc", new string[]{"text/vnd.abc"});
            mimeTypes.Add("acgi", new string[]{"text/html"});
            mimeTypes.Add("afl", new string[]{"video/animaflex"});
            mimeTypes.Add("ai", new string[]{"application/postscript"});
            mimeTypes.Add("aif", new string[]{"audio/aiff"});
            mimeTypes.Add("aifc", new string[]{"audio/aiff"});
            mimeTypes.Add("aiff", new string[]{"audio/aiff"});
            mimeTypes.Add("aim", new string[]{"application/x-aim"});
            mimeTypes.Add("aip", new string[]{"text/x-audiosoft-intra"});
            mimeTypes.Add("ani", new string[]{"application/x-navi-animation"});
            mimeTypes.Add("aos", new string[]{"application/x-nokia-9000-communicator-add-on-software"});
            mimeTypes.Add("aps", new string[]{"application/mime"});
            mimeTypes.Add("arc", new string[]{"application/octet-stream"});
            mimeTypes.Add("arj", new string[]{"application/arj"});
            mimeTypes.Add("art", new string[]{"image/x-jg"});
            mimeTypes.Add("asf", new string[]{"video/x-ms-asf"});
            mimeTypes.Add("asm", new string[]{"text/x-asm"});
            mimeTypes.Add("asp", new string[]{"text/asp"});
            mimeTypes.Add("asx", new string[]{"application/x-mplayer2"});
            mimeTypes.Add("au", new string[]{"audio/basic"});
            mimeTypes.Add("avi", new string[]{"video/avi"});
            mimeTypes.Add("avs", new string[]{"video/avs-video"});
            mimeTypes.Add("bat", new string[] { "text/plain" });
            mimeTypes.Add("bcpio", new string[]{"application/x-bcpio"});
            mimeTypes.Add("bin", new string[]{"application/octet-stream"});
            mimeTypes.Add("bm", new string[]{"image/bmp"});
            mimeTypes.Add("bmp", new string[]{"image/bmp"});
            mimeTypes.Add("boo", new string[]{"application/book"});
            mimeTypes.Add("book", new string[]{"application/book"});
            mimeTypes.Add("boz", new string[]{"application/x-bzip2"});
            mimeTypes.Add("bsh", new string[]{"application/x-bsh"});
            mimeTypes.Add("bz", new string[]{"application/x-bzip"});
            mimeTypes.Add("bz2", new string[]{"application/x-bzip2"});
            mimeTypes.Add("c", new string[]{"text/plain"});
            mimeTypes.Add("c++", new string[]{"text/plain"});
            mimeTypes.Add("cat", new string[]{"application/vnd.ms-pki.seccat"});
            mimeTypes.Add("cc", new string[]{"text/plain"});
            mimeTypes.Add("ccad", new string[]{"application/clariscad"});
            mimeTypes.Add("cco", new string[]{"application/x-cocoa"});
            mimeTypes.Add("cdf", new string[]{"application/cdf"});
            mimeTypes.Add("cer", new string[]{"application/pkix-cert"});
            mimeTypes.Add("cha", new string[]{"application/x-chat"});
            mimeTypes.Add("chat", new string[]{"application/x-chat"});
            mimeTypes.Add("class", new string[]{"application/java"});
            mimeTypes.Add("com", new string[]{"application/octet-stream"});
            mimeTypes.Add("conf", new string[]{"text/plain"});
            mimeTypes.Add("cpio", new string[]{"application/x-cpio"});
            mimeTypes.Add("cpp", new string[]{"text/x-c"});
            mimeTypes.Add("cpt", new string[]{"application/x-cpt"});
            mimeTypes.Add("crl", new string[]{"application/pkcs-crl"});
            mimeTypes.Add("css", new string[]{"text/css"});
            mimeTypes.Add("def", new string[]{"text/plain"});
            mimeTypes.Add("der", new string[]{"application/x-x509-ca-cert"});
            mimeTypes.Add("dif", new string[]{"video/x-dv"});
            mimeTypes.Add("dir", new string[]{"application/x-director"});
            mimeTypes.Add("dl", new string[]{"video/dl"});
            mimeTypes.Add("doc", new string[]{"application/msword"});
            mimeTypes.Add("dot", new string[]{"application/msword"});
            mimeTypes.Add("dp", new string[]{"application/commonground"});
            mimeTypes.Add("drw", new string[]{"application/drafting"});
            mimeTypes.Add("dump", new string[]{"application/octet-stream"});
            mimeTypes.Add("dv", new string[]{"video/x-dv"});
            mimeTypes.Add("dvi", new string[]{"application/x-dvi"});
            mimeTypes.Add("dwf", new string[]{"drawing/x-dwf (old)"});
            mimeTypes.Add("dwg", new string[]{"application/acad"});
            mimeTypes.Add("dxf", new string[]{"application/dxf"});
            mimeTypes.Add("eps", new string[]{"application/postscript"});
            mimeTypes.Add("es", new string[]{"application/x-esrehber"});
            mimeTypes.Add("etx", new string[]{"text/x-setext"});
            mimeTypes.Add("evy", new string[]{"application/envoy"});
            mimeTypes.Add("exe", new string[]{"application/octet-stream", "application/x-msdownload" });
            mimeTypes.Add("f", new string[]{"text/plain"});
            mimeTypes.Add("f90", new string[]{"text/x-fortran"});
            mimeTypes.Add("fdf", new string[]{"application/vnd.fdf"});
            mimeTypes.Add("fif", new string[]{"image/fif"});
            mimeTypes.Add("fli", new string[]{"video/fli"});
            mimeTypes.Add("flv", new string[]{"video/x-flv"});
            mimeTypes.Add("for", new string[]{"text/x-fortran"});
            mimeTypes.Add("fpx", new string[]{"image/vnd.fpx"});
            mimeTypes.Add("g", new string[]{"text/plain"});
            mimeTypes.Add("g3", new string[]{"image/g3fax"});
            mimeTypes.Add("gif", new string[]{"image/gif"});
            mimeTypes.Add("gl", new string[]{"video/gl"});
            mimeTypes.Add("gsd", new string[]{"audio/x-gsm"});
            mimeTypes.Add("gtar", new string[]{"application/x-gtar"});
            mimeTypes.Add("gz", new string[]{"application/x-compressed"});
            mimeTypes.Add("h", new string[]{"text/plain"});
            mimeTypes.Add("help", new string[]{"application/x-helpfile"});
            mimeTypes.Add("hgl", new string[]{"application/vnd.hp-hpgl"});
            mimeTypes.Add("hh", new string[]{"text/plain"});
            mimeTypes.Add("hlp", new string[]{"application/x-winhelp"});
            mimeTypes.Add("htc", new string[]{"text/x-component"});
            mimeTypes.Add("htm", new string[]{"text/html"});
            mimeTypes.Add("html", new string[]{"text/html"});
            mimeTypes.Add("htmls", new string[]{"text/html"});
            mimeTypes.Add("htt", new string[]{"text/webviewhtml"});
            mimeTypes.Add("htx", new string[]{"text/html"});
            mimeTypes.Add("ice", new string[]{"x-conference/x-cooltalk"});
            mimeTypes.Add("ico", new string[]{"image/x-icon"});
            mimeTypes.Add("idc", new string[]{"text/plain"});
            mimeTypes.Add("ief", new string[]{"image/ief"});
            mimeTypes.Add("iefs", new string[]{"image/ief"});
            mimeTypes.Add("iges", new string[]{"application/iges"});
            mimeTypes.Add("igs", new string[]{"application/iges"});
            mimeTypes.Add("ima", new string[]{"application/x-ima"});
            mimeTypes.Add("imap", new string[]{"application/x-httpd-imap"});
            mimeTypes.Add("inf", new string[]{"application/inf"});
            mimeTypes.Add("ins", new string[]{"application/x-internett-signup"});
            mimeTypes.Add("ip", new string[]{"application/x-ip2"});
            mimeTypes.Add("isu", new string[]{"video/x-isvideo"});
            mimeTypes.Add("it", new string[]{"audio/it"});
            mimeTypes.Add("iv", new string[]{"application/x-inventor"});
            mimeTypes.Add("ivr", new string[]{"i-world/i-vrml"});
            mimeTypes.Add("ivy", new string[]{"application/x-livescreen"});
            mimeTypes.Add("jam", new string[]{"audio/x-jam"});
            mimeTypes.Add("jav", new string[]{"text/plain"});
            mimeTypes.Add("java", new string[]{"text/plain"});
            mimeTypes.Add("jcm", new string[]{"application/x-java-commerce"});
            mimeTypes.Add("jfif", new string[]{"image/jpeg"});
            mimeTypes.Add("jfif-tbnl", new string[]{"image/jpeg"});
            mimeTypes.Add("jpe", new string[]{"image/jpeg"});
            mimeTypes.Add("jpeg", new string[]{"image/jpeg"});
            mimeTypes.Add("jpg", new string[]{"image/jpeg"});
            mimeTypes.Add("jps", new string[]{"image/x-jps"});
            mimeTypes.Add("js", new string[]{"application/x-javascript"});
            mimeTypes.Add("jut", new string[]{"image/jutvision"});
            mimeTypes.Add("kar", new string[]{"audio/midi"});
            mimeTypes.Add("ksh", new string[]{"application/x-ksh"});
            mimeTypes.Add("la", new string[]{"audio/nspaudio"});
            mimeTypes.Add("lam", new string[]{"audio/x-liveaudio"});
            mimeTypes.Add("latex", new string[]{"application/x-latex"});
            mimeTypes.Add("lha", new string[]{"application/lha"});
            mimeTypes.Add("lhx", new string[]{"application/octet-stream"});
            mimeTypes.Add("list", new string[]{"text/plain"});
            mimeTypes.Add("lma", new string[]{"audio/nspaudio"});
            mimeTypes.Add("log", new string[]{"text/plain"});
            mimeTypes.Add("lsp", new string[]{"application/x-lisp"});
            mimeTypes.Add("lst", new string[]{"text/plain"});
            mimeTypes.Add("lsx", new string[]{"text/x-la-asf"});
            mimeTypes.Add("ltx", new string[]{"application/x-latex"});
            mimeTypes.Add("lzh", new string[]{"application/octet-stream"});
            mimeTypes.Add("lzx", new string[]{"application/lzx"});
            mimeTypes.Add("m", new string[]{"text/plain"});
            mimeTypes.Add("m1v", new string[]{"video/mpeg"});
            mimeTypes.Add("m2a", new string[]{"audio/mpeg"});
            mimeTypes.Add("m2v", new string[]{"video/mpeg"});
            mimeTypes.Add("m3u", new string[]{"audio/x-mpequrl"});
            mimeTypes.Add("man", new string[]{"application/x-troff-man"});
            mimeTypes.Add("map", new string[]{"application/x-navimap"});
            mimeTypes.Add("mar", new string[]{"text/plain"});
            mimeTypes.Add("mbd", new string[]{"application/mbedlet"});
            mimeTypes.Add("mc$", new string[]{"application/x-magic-cap-package-1.0"});
            mimeTypes.Add("mcd", new string[]{"application/mcad"});
            mimeTypes.Add("mcf", new string[]{"image/vasa"});
            mimeTypes.Add("mcp", new string[]{"application/netmc"});
            mimeTypes.Add("me", new string[]{"application/x-troff-me"});
            mimeTypes.Add("mht", new string[]{"message/rfc822"});
            mimeTypes.Add("mhtml", new string[]{"message/rfc822"});
            mimeTypes.Add("mid", new string[]{"audio/midi"});
            mimeTypes.Add("midi", new string[]{"audio/midi"});
            mimeTypes.Add("mif", new string[]{"application/x-frame"});
            mimeTypes.Add("mime", new string[]{"message/rfc822"});
            mimeTypes.Add("mjf", new string[]{"audio/x-vnd.audioexplosion.mjuicemediafile"});
            mimeTypes.Add("mjpg", new string[]{"video/x-motion-jpeg"});
            mimeTypes.Add("mm", new string[]{"application/base64"});
            mimeTypes.Add("mme", new string[]{"application/base64"});
            mimeTypes.Add("mod", new string[]{"audio/mod"});
            mimeTypes.Add("moov", new string[]{"video/quicktime"});
            mimeTypes.Add("mov", new string[]{"video/quicktime"});
            mimeTypes.Add("movie", new string[]{"video/x-sgi-movie"});
            mimeTypes.Add("mp2", new string[]{"audio/mpeg"});
            mimeTypes.Add("mp3", new string[]{"audio/mpeg3"});
            mimeTypes.Add("mpa", new string[]{"audio/mpeg"});
            mimeTypes.Add("mpc", new string[]{"application/x-project"});
            mimeTypes.Add("mpe", new string[]{"video/mpeg"});
            mimeTypes.Add("mpeg", new string[]{"video/mpeg"});
            mimeTypes.Add("mpg", new string[]{"video/mpeg"});
            mimeTypes.Add("mpga", new string[]{"audio/mpeg"});
            mimeTypes.Add("mpp", new string[]{"application/vnd.ms-project"});
            mimeTypes.Add("mpt", new string[]{"application/x-project"});
            mimeTypes.Add("mpv", new string[]{"application/x-project"});
            mimeTypes.Add("mpx", new string[]{"application/x-project"});
            mimeTypes.Add("mrc", new string[]{"application/marc"});
            mimeTypes.Add("ms", new string[]{"application/x-troff-ms"});
            mimeTypes.Add("mv", new string[]{"video/x-sgi-movie"});
            mimeTypes.Add("my", new string[]{"audio/make"});
            mimeTypes.Add("mzz", new string[]{"application/x-vnd.audioexplosion.mzz"});
            mimeTypes.Add("nap", new string[]{"image/naplps"});
            mimeTypes.Add("naplps", new string[]{"image/naplps"});
            mimeTypes.Add("nc", new string[]{"application/x-netcdf"});
            mimeTypes.Add("ncm", new string[]{"application/vnd.nokia.configuration-message"});
            mimeTypes.Add("nif", new string[]{"image/x-niff"});
            mimeTypes.Add("niff", new string[]{"image/x-niff"});
            mimeTypes.Add("nix", new string[]{"application/x-mix-transfer"});
            mimeTypes.Add("nsc", new string[]{"application/x-conference"});
            mimeTypes.Add("nvd", new string[]{"application/x-navidoc"});
            mimeTypes.Add("o", new string[]{"application/octet-stream"});
            mimeTypes.Add("oda", new string[]{"application/oda"});
            mimeTypes.Add("omc", new string[]{"application/x-omc"});
            mimeTypes.Add("omcd", new string[]{"application/x-omcdatamaker"});
            mimeTypes.Add("omcr", new string[]{"application/x-omcregerator"});
            mimeTypes.Add("p", new string[]{"text/x-pascal"});
            mimeTypes.Add("p10", new string[]{"application/pkcs10"});
            mimeTypes.Add("p12", new string[]{"application/pkcs-12"});
            mimeTypes.Add("p7a", new string[]{"application/x-pkcs7-signature"});
            mimeTypes.Add("p7c", new string[]{"application/pkcs7-mime"});
            mimeTypes.Add("pas", new string[]{"text/pascal"});
            mimeTypes.Add("pbm", new string[]{"image/x-portable-bitmap"});
            mimeTypes.Add("pcl", new string[]{"application/vnd.hp-pcl"});
            mimeTypes.Add("pct", new string[]{"image/x-pict"});
            mimeTypes.Add("pcx", new string[]{"image/x-pcx"});
            mimeTypes.Add("pdf", new string[]{"application/pdf"});
            mimeTypes.Add("pfunk", new string[]{"audio/make"});
            mimeTypes.Add("pgm", new string[]{"image/x-portable-graymap"});
            mimeTypes.Add("pic", new string[]{"image/pict"});
            mimeTypes.Add("pict", new string[]{"image/pict"});
            mimeTypes.Add("pkg", new string[]{"application/x-newton-compatible-pkg"});
            mimeTypes.Add("pko", new string[]{"application/vnd.ms-pki.pko"});
            mimeTypes.Add("pl", new string[]{"text/plain"});
            mimeTypes.Add("plx", new string[]{"application/x-pixclscript"});
            mimeTypes.Add("pm", new string[]{"image/x-xpixmap"});
            mimeTypes.Add("png", new string[]{"image/png"});
            mimeTypes.Add("pnm", new string[]{"application/x-portable-anymap"});
            mimeTypes.Add("pot", new string[]{"application/mspowerpoint"});
            mimeTypes.Add("pov", new string[]{"model/x-pov"});
            mimeTypes.Add("ppa", new string[]{"application/vnd.ms-powerpoint"});
            mimeTypes.Add("ppm", new string[]{"image/x-portable-pixmap"});
            mimeTypes.Add("pps", new string[]{"application/mspowerpoint"});
            mimeTypes.Add("ppt", new string[]{"application/mspowerpoint"});
            mimeTypes.Add("ppz", new string[]{"application/mspowerpoint"});
            mimeTypes.Add("pre", new string[]{"application/x-freelance"});
            mimeTypes.Add("prt", new string[]{"application/pro_eng"});
            mimeTypes.Add("ps", new string[]{"application/postscript"});
            mimeTypes.Add("psd", new string[]{"application/octet-stream"});
            mimeTypes.Add("pvu", new string[]{"paleovu/x-pv"});
            mimeTypes.Add("pwz", new string[]{"application/vnd.ms-powerpoint"});
            mimeTypes.Add("py", new string[]{"text/x-script.phyton"});
            mimeTypes.Add("pyc", new string[]{"applicaiton/x-bytecode.python"});
            mimeTypes.Add("qcp", new string[]{"audio/vnd.qcelp"});
            mimeTypes.Add("qd3", new string[]{"x-world/x-3dmf"});
            mimeTypes.Add("qd3d", new string[]{"x-world/x-3dmf"});
            mimeTypes.Add("qif", new string[]{"image/x-quicktime"});
            mimeTypes.Add("qt", new string[]{"video/quicktime"});
            mimeTypes.Add("qtc", new string[]{"video/x-qtc"});
            mimeTypes.Add("qti", new string[]{"image/x-quicktime"});
            mimeTypes.Add("qtif", new string[]{"image/x-quicktime"});
            mimeTypes.Add("ra", new string[]{"audio/x-pn-realaudio"});
            mimeTypes.Add("ram", new string[]{"audio/x-pn-realaudio"});
            mimeTypes.Add("ras", new string[]{"application/x-cmu-raster"});
            mimeTypes.Add("rast", new string[]{"image/cmu-raster"});
            mimeTypes.Add("rexx", new string[]{"text/x-script.rexx"});
            mimeTypes.Add("rf", new string[]{"image/vnd.rn-realflash"});
            mimeTypes.Add("rgb", new string[]{"image/x-rgb"});
            mimeTypes.Add("rm", new string[]{"application/vnd.rn-realmedia"});
            mimeTypes.Add("rmi", new string[]{"audio/mid"});
            mimeTypes.Add("rmm", new string[]{"audio/x-pn-realaudio"});
            mimeTypes.Add("rmp", new string[]{"audio/x-pn-realaudio"});
            mimeTypes.Add("rng", new string[]{"application/ringing-tones"});
            mimeTypes.Add("rnx", new string[]{"application/vnd.rn-realplayer"});
            mimeTypes.Add("roff", new string[]{"application/x-troff"});
            mimeTypes.Add("rp", new string[]{"image/vnd.rn-realpix"});
            mimeTypes.Add("rpm", new string[]{"audio/x-pn-realaudio-plugin"});
            mimeTypes.Add("rt", new string[]{"text/richtext"});
            mimeTypes.Add("rtf", new string[]{"text/richtext"});
            mimeTypes.Add("rtx", new string[]{"application/rtf"});
            mimeTypes.Add("rv", new string[]{"video/vnd.rn-realvideo"});
            mimeTypes.Add("s", new string[]{"text/x-asm"});
            mimeTypes.Add("s3m", new string[]{"audio/s3m"});
            mimeTypes.Add("saveme", new string[]{"application/octet-stream"});
            mimeTypes.Add("sbk", new string[]{"application/x-tbook"});
            mimeTypes.Add("scm", new string[]{"application/x-lotusscreencam"});
            mimeTypes.Add("sdml", new string[]{"text/plain"});
            mimeTypes.Add("sdp", new string[]{"application/sdp"});
            mimeTypes.Add("sdr", new string[]{"application/sounder"});
            mimeTypes.Add("sea", new string[]{"application/sea"});
            mimeTypes.Add("set", new string[]{"application/set"});
            mimeTypes.Add("sgm", new string[]{"text/sgml"});
            mimeTypes.Add("sgml", new string[]{"text/sgml"});
            mimeTypes.Add("sh", new string[]{"application/x-bsh"});
            mimeTypes.Add("shtml", new string[]{"text/html"});
            mimeTypes.Add("sid", new string[]{"audio/x-psid"});
            mimeTypes.Add("sit", new string[]{"application/x-sit"});
            mimeTypes.Add("skd", new string[]{"application/x-koan"});
            mimeTypes.Add("skm", new string[]{"application/x-koan"});
            mimeTypes.Add("skp", new string[]{"application/x-koan"});
            mimeTypes.Add("skt", new string[]{"application/x-koan"});
            mimeTypes.Add("sl", new string[]{"application/x-seelogo"});
            mimeTypes.Add("smi", new string[]{"application/smil"});
            mimeTypes.Add("smil", new string[]{"application/smil"});
            mimeTypes.Add("snd", new string[]{"audio/basic"});
            mimeTypes.Add("sol", new string[]{"application/solids"});
            mimeTypes.Add("spc", new string[]{"application/x-pkcs7-certificates"});
            mimeTypes.Add("spl", new string[]{"application/futuresplash"});
            mimeTypes.Add("spr", new string[]{"application/x-sprite"});
            mimeTypes.Add("sprite", new string[]{"application/x-sprite"});
            mimeTypes.Add("src", new string[]{"application/x-wais-source"});
            mimeTypes.Add("ssi", new string[]{"text/x-server-parsed-html"});
            mimeTypes.Add("ssm", new string[]{"application/streamingmedia"});
            mimeTypes.Add("sst", new string[]{"application/vnd.ms-pki.certstore"});
            mimeTypes.Add("step", new string[]{"application/step"});
            mimeTypes.Add("stl", new string[]{"application/sla"});
            mimeTypes.Add("stp", new string[]{"application/step"});
            mimeTypes.Add("sv4cpio", new string[]{"application/x-sv4cpio"});
            mimeTypes.Add("sv4crc", new string[]{"application/x-sv4crc"});
            mimeTypes.Add("svf", new string[]{"image/vnd.dwg"});
            mimeTypes.Add("svr", new string[]{"application/x-world"});
            mimeTypes.Add("swf", new string[]{"application/x-shockwave-flash"});
            mimeTypes.Add("t", new string[]{"application/x-troff"});
            mimeTypes.Add("talk", new string[]{"text/x-speech"});
            mimeTypes.Add("tar", new string[]{"application/x-tar"});
            mimeTypes.Add("tbk", new string[]{"application/toolbook"});
            mimeTypes.Add("tcl", new string[]{"application/x-tcl"});
            mimeTypes.Add("tcsh", new string[]{"text/x-script.tcsh"});
            mimeTypes.Add("tex", new string[]{"application/x-tex"});
            mimeTypes.Add("texi", new string[]{"application/x-texinfo"});
            mimeTypes.Add("texinfo", new string[]{"application/x-texinfo"});
            mimeTypes.Add("text", new string[]{"text/plain"});
            mimeTypes.Add("tgz", new string[]{"application/x-compressed"});
            mimeTypes.Add("tif", new string[]{"image/tiff"});
            mimeTypes.Add("tr", new string[]{"application/x-troff"});
            mimeTypes.Add("tsi", new string[]{"audio/tsp-audio"});
            mimeTypes.Add("tsp", new string[]{"audio/tsplayer"});
            mimeTypes.Add("tsv", new string[]{"text/tab-separated-values"});
            mimeTypes.Add("turbot", new string[]{"image/florian"});
            mimeTypes.Add("txt", new string[]{"text/plain"});
            mimeTypes.Add("uil", new string[]{"text/x-uil"});
            mimeTypes.Add("uni", new string[]{"text/uri-list"});
            mimeTypes.Add("unis", new string[]{"text/uri-list"});
            mimeTypes.Add("unv", new string[]{"application/i-deas"});
            mimeTypes.Add("uri", new string[]{"text/uri-list"});
            mimeTypes.Add("uris", new string[]{"text/uri-list"});
            mimeTypes.Add("ustar", new string[]{"application/x-ustar"});
            mimeTypes.Add("uu", new string[]{"application/octet-stream"});
            mimeTypes.Add("vcd", new string[]{"application/x-cdlink"});
            mimeTypes.Add("vcs", new string[]{"text/x-vcalendar"});
            mimeTypes.Add("vda", new string[]{"application/vda"});
            mimeTypes.Add("vdo", new string[]{"video/vdo"});
            mimeTypes.Add("vew", new string[]{"application/groupwise"});
            mimeTypes.Add("viv", new string[]{"video/vivo"});
            mimeTypes.Add("vivo", new string[]{"video/vivo"});
            mimeTypes.Add("vmd", new string[]{"application/vocaltec-media-desc"});
            mimeTypes.Add("vmf", new string[]{"application/vocaltec-media-file"});
            mimeTypes.Add("voc", new string[]{"audio/voc"});
            mimeTypes.Add("vos", new string[]{"video/vosaic"});
            mimeTypes.Add("vox", new string[]{"audio/voxware"});
            mimeTypes.Add("vqe", new string[]{"audio/x-twinvq-plugin"});
            mimeTypes.Add("vqf", new string[]{"audio/x-twinvq"});
            mimeTypes.Add("vql", new string[]{"audio/x-twinvq-plugin"});
            mimeTypes.Add("vrml", new string[]{"application/x-vrml"});
            mimeTypes.Add("vrt", new string[]{"x-world/x-vrt"});
            mimeTypes.Add("vsd", new string[]{"application/x-visio"});
            mimeTypes.Add("vst", new string[]{"application/x-visio"});
            mimeTypes.Add("vsw", new string[]{"application/x-visio"});
            mimeTypes.Add("w60", new string[]{"application/wordperfect6.0"});
            mimeTypes.Add("w61", new string[]{"application/wordperfect6.1"});
            mimeTypes.Add("w6w", new string[]{"application/msword"});
            mimeTypes.Add("wav", new string[]{"audio/wav"});
            mimeTypes.Add("wb1", new string[]{"application/x-qpro"});
            mimeTypes.Add("wbmp", new string[]{"image/vnd.wap.wbmp"});
            mimeTypes.Add("web", new string[]{"application/vnd.xara"});
            mimeTypes.Add("wiz", new string[]{"application/msword"});
            mimeTypes.Add("wk1", new string[]{"application/x-123"});
            mimeTypes.Add("wmf", new string[]{"windows/metafile"});
            mimeTypes.Add("wml", new string[]{"text/vnd.wap.wml"});
            mimeTypes.Add("wmlc", new string[]{"application/vnd.wap.wmlc"});
            mimeTypes.Add("wmls", new string[]{"text/vnd.wap.wmlscript"});
            mimeTypes.Add("wmlsc", new string[]{"application/vnd.wap.wmlscriptc"});
            mimeTypes.Add("word", new string[]{"application/msword"});
            mimeTypes.Add("wp", new string[]{"application/wordperfect"});
            mimeTypes.Add("wp5", new string[]{"application/wordperfect"});
            mimeTypes.Add("wp6", new string[]{"application/wordperfect"});
            mimeTypes.Add("wpd", new string[]{"application/wordperfect"});
            mimeTypes.Add("wq1", new string[]{"application/x-lotus"});
            mimeTypes.Add("wri", new string[]{"application/mswrite"});
            mimeTypes.Add("wrl", new string[]{"application/x-world"});
            mimeTypes.Add("wrz", new string[]{"model/vrml"});
            mimeTypes.Add("wsc", new string[]{"text/scriplet"});
            mimeTypes.Add("wsrc", new string[]{"application/x-wais-source"});
            mimeTypes.Add("wtk", new string[]{"application/x-wintalk"});
            mimeTypes.Add("xbm", new string[]{"image/x-xbitmap"});
            mimeTypes.Add("xdr", new string[]{"video/x-amt-demorun"});
            mimeTypes.Add("xgz", new string[]{"xgl/drawing"});
            mimeTypes.Add("xif", new string[]{"image/vnd.xiff"});
            mimeTypes.Add("xl", new string[]{"application/excel"});
            mimeTypes.Add("xla", new string[]{"application/excel"});
            mimeTypes.Add("xlb", new string[]{"application/excel"});
            mimeTypes.Add("xlc", new string[]{"application/excel"});
            mimeTypes.Add("xld", new string[]{"application/excel"});
            mimeTypes.Add("xlk", new string[]{"application/excel"});
            mimeTypes.Add("xll", new string[]{"application/excel"});
            mimeTypes.Add("xlm", new string[]{"application/excel"});
            mimeTypes.Add("xls", new string[]{"application/excel"});
            mimeTypes.Add("xlsx", new string[]{"application/excel"});
            mimeTypes.Add("xlt", new string[]{"application/excel"});
            mimeTypes.Add("xlv", new string[]{"application/excel"});
            mimeTypes.Add("xlw", new string[]{"application/excel"});
            mimeTypes.Add("xm", new string[]{"audio/xm"});
            mimeTypes.Add("xml", new string[]{"text/xml"});
            mimeTypes.Add("xmz", new string[]{"xgl/movie"});
            mimeTypes.Add("xpix", new string[]{"application/x-vnd.ls-xpix"});
            mimeTypes.Add("xpm", new string[]{"image/x-xpixmap"});
            mimeTypes.Add("x-png", new string[]{"image/png"});
            mimeTypes.Add("xsr", new string[]{"video/x-amt-showrun"});
            mimeTypes.Add("xwd", new string[]{"image/x-xwd"});
            mimeTypes.Add("xyz", new string[]{"chemical/x-pdb"});
            mimeTypes.Add("z", new string[]{"application/x-compress"});
            mimeTypes.Add("zip", new string[]{"application/x-compressed"});
            mimeTypes.Add("zoo", new string[]{"application/octet-stream"});
            mimeTypes.Add("zsh", new string[]{"text/x-script.zsh"});
        }
    }
}
