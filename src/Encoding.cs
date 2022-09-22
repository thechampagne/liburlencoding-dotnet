/*
* Copyright (c) 2022 XXIV
* 
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
* 
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/
#nullable enable
using System;
using System.Runtime.InteropServices;

namespace URL
{
    public class Encoding
    {
#if Linux
#   if Shared
        private const string LIB = "liburlencoding.so";
#   else
        private const string LIB = "liburlencoding.a";
#   endif
#elif Windows
#   if Shared
        private const string LIB = "liburlencoding.dll";
#   else
        private const string LIB = "liburlencoding.lib";
#   endif
#elif OSX
#   if Shared
        private const string LIB = "liburlencoding.dylib";
#   else
        private const string LIB = "liburlencoding.a";
#   endif
#else
        private const string LIB = "liburlencoding";
#endif
        [DllImport (LIB)]
        private static extern IntPtr url_encoding_encode_binary(string data, long length);
        [DllImport (LIB)]
        private static extern IntPtr url_encoding_decode_binary(string data, long length);

        [DllImport (LIB)]
        private static extern IntPtr url_encoding_encode(string data);
        [DllImport (LIB)]
        private static extern IntPtr url_encoding_decode(string data);
        [DllImport (LIB)]
        private static extern void url_encoding_free(IntPtr ptr);

        /// <summary>
        /// Percent-encodes every byte except alphanumerics and -, _, ., ~. Assumes UTF-8 encoding.
        /// </summary>
        /// <example>
        /// Example:
        /// <code>
        /// string? res = URL.Encoding.Encode("This string will be URL encoded.");
        /// Console.WriteLine(res);
        /// </code>
        /// </example>
        /// <param name="data">string</param>
        /// <returns>encoded string</returns>
        public static string? Encode(string data)
        {
            var res = url_encoding_encode(data);
            if (res == IntPtr.Zero)
            {
                return null;
            }
            var str = Marshal.PtrToStringAnsi(res);
            url_encoding_free(res);
            return str;
        }

        /// <summary>
        /// Percent-encodes every byte except alphanumerics and -, _, ., ~.
        /// </summary>
        /// <example>
        /// Example:
        /// <code>
        /// string? res = URL.Encoding.EncodeBinary("This string will be URL encoded.");
        /// Console.WriteLine(res);
        /// </code>
        /// </example>
        /// <param name="data">string</param>
        /// <returns>encoded string</returns>
        public static string? EncodeBinary(string data)
        {
            var res = url_encoding_encode_binary(data, data.Length);
            if (res == IntPtr.Zero)
            {
                return null;
            }
            var str = Marshal.PtrToStringAnsi(res);
            url_encoding_free(res);
            return str;
        }

        /// <summary>
        /// Decode percent-encoded string assuming UTF-8 encoding.
        /// </summary>
        /// <example>
        /// Example:
        /// <code>
        /// string? res = URL.Encoding.Decode("%F0%9F%91%BE%20Exterminate%21");
        /// Console.WriteLine(res);
        /// </code>
        /// </example>
        /// <param name="data">string</param>
        /// <returns>decoded string</returns>
        public static string? Decode(string data)
        {
            var res = url_encoding_decode(data);
            if (res == IntPtr.Zero)
            {
                return null;
            }
            var str = Marshal.PtrToStringAnsi(res);
            url_encoding_free(res);
            return str;
        }

        /// <summary>
        /// Decode percent-encoded string as binary data, in any encoding.
        /// </summary>
        /// <example>
        /// Example:
        /// <code>
        /// string? res = URL.Encoding.DecodeBinary("%F1%F2%F3%C0%C1%C2");
        /// Console.WriteLine(res);
        /// </code>
        /// </example>
        /// <param name="data">string</param>
        /// <returns>decoded string</returns>
        public static string? DecodeBinary(string data)
        {
            var res = url_encoding_decode_binary(data, data.Length);   
            if (res == IntPtr.Zero)
            {
                return null;
            }
            var str = Marshal.PtrToStringAnsi(res);
            url_encoding_free(res);
            return str;
        }
    }
}