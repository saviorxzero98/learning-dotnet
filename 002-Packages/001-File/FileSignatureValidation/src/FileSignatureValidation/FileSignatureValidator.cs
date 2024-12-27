using FileSignatureValidation.FileSignatures.Audios;
using FileSignatureValidation.FileSignatures.Blobs;
using FileSignatureValidation.FileSignatures.Documents;
using FileSignatureValidation.FileSignatures.Images;
using FileSignatureValidation.FileSignatures.Videos;
using FileSignatureValidation.FileSignatureSubValidators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSignatureValidation
{
    public static class FileSignatureValidator
    {
        public static Dictionary<string, List<FileSignature>> FileSignature = new Dictionary<string, List<FileSignature>>()
        {
            // Images
            { PngFileSignature.Extension, PngFileSignature.Signatures },
            { JpgFileSignature.Extension, JpgFileSignature.Signatures },
            { JpegFileSignature.Extension, JpegFileSignature.Signatures },
            { GifFileSignature.Extension, GifFileSignature.Signatures },
            { WebPFileSignature.Extension, WebPFileSignature.Signatures },
            { TiffFileSignature.Extension, TiffFileSignature.Signatures },
            { TifFileSignature.Extension, TifFileSignature.Signatures },
            { IcoFileSignature.Extension, IcoFileSignature.Signatures },
            { BmpFileSignature.Extension, BmpFileSignature.Signatures },

            // Documents
            { PdfFileSignature.Extension, PdfFileSignature.Signatures },
            { DocFileSignature.Extension, DocFileSignature.Signatures },
            { DocxFileSignature.Extension, DocxFileSignature.Signatures },
            { XlsFileSignature.Extension, XlsFileSignature.Signatures },
            { XlsxFileSignature.Extension, XlsxFileSignature.Signatures },
            { PptFileSignature.Extension, PptFileSignature.Signatures },
            { PptxFileSignature.Extension, PptxFileSignature.Signatures },
            { OdfFileSignature.Extension, OdfFileSignature.Signatures },
            { OdgFileSignature.Extension, OdgFileSignature.Signatures },
            { OdpFileSignature.Extension, OdpFileSignature.Signatures },
            { OdsFileSignature.Extension, OdsFileSignature.Signatures },
            { OdtFileSignature.Extension, OdtFileSignature.Signatures },
            { RtfFileSignature.Extension, RtfFileSignature.Signatures },

            // Audio
            { Mp3FileSignature.Extension, Mp3FileSignature.Signatures },
            { WmaFileSignature.Extension, WmaFileSignature.Signatures },
            { WavFileSignature.Extension, WavFileSignature.Signatures },
            { FlacFileSignature.Extension, FlacFileSignature.Signatures },

            // Video
            { Mp4FileSignature.Extension, Mp4FileSignature.Signatures },
            { MpegFileSignature.Extension, MpegFileSignature.Signatures },
            { MpgFileSignature.Extension, MpgFileSignature.Signatures },
            { MkvFileSignature.Extension, MkvFileSignature.Signatures },
            { WebMFileSignature.Extension, WebMFileSignature.Signatures },
            { WmvFileSignature.Extension, WmvFileSignature.Signatures },
            { AviFileSignature.Extension, AviFileSignature.Signatures },
            { RmvbFileSignature.Extension, RmvbFileSignature.Signatures },
            { RmFileSignature.Extension, RmFileSignature.Signatures },
            { FlvFileSignature.Extension, FlvFileSignature.Signatures },
            { SwfFileSignature.Extension, SwfFileSignature.Signatures },
            { M3u8FileSignature.Extension, M3u8FileSignature.Signatures },
            { M3uFileSignature.Extension, M3uFileSignature.Signatures },

            // Blob
            { ZipFileSignature.Extension, ZipFileSignature.Signatures },
            { SevenZFileSignature.Extension, SevenZFileSignature.Signatures },
            { RarFileSignature.Extension, RarFileSignature.Signatures },
            { GzFileSignature.Extension, GzFileSignature.Signatures },
            { XzFileSignature.Extension, XzFileSignature.Signatures },
            { TarFileSignature.Extension, TarFileSignature.Signatures },
            { ExeFileSignature.Extension, ExeFileSignature.Signatures },
            { DllFileSignature.Extension, DllFileSignature.Signatures },
            { SysFileSignature.Extension, SysFileSignature.Signatures },
            { XpiFileSignature.Extension, XpiFileSignature.Signatures },
            { CrxFileSignature.Extension, CrxFileSignature.Signatures },
        };

        public static List<IFileSignatureSubValidator> SubValidators = new List<IFileSignatureSubValidator>()
        {
            new SvgFileSignatureSubValidator(),
            new HtmlFileSignatureSubValidator(),
            new TextFileSignatureSubValidator()
        };

        /// <summary>
        /// 驗證檔案與副檔名是否一致
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileData"></param>
        /// <param name="allowedChars"></param>
        /// <returns></returns>
        public static bool IsValidFileExtension(string fileName, byte[] fileData, byte[] allowedChars = null)
        {
            if (string.IsNullOrEmpty(fileName) || fileData == null || fileData.Length == 0)
            {
                return false;
            }

            bool flag = false;
            string ext = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(ext))
            {
                return false;
            }

            ext = ext.ToLower();


            foreach (var subValidator in SubValidators)
            {
                var isValid = subValidator.IsValidFileExtension(ext, fileData, allowedChars);
                if (isValid.HasValue)
                {
                    return isValid.Value;
                }
            }

            if (!FileSignature.ContainsKey(ext))
            {
                return false;
            }

            List<FileSignature> sigs = FileSignature[ext];
            foreach (var sig in sigs)
            {
                byte[] b = sig.Signature;
                int offset = sig.Offset;
                var curFileSig = new byte[b.Length];
                Array.Copy(fileData.Skip(offset).ToArray(), curFileSig, b.Length);
                flag = curFileSig.SequenceEqual(b);
                int skip = sig.Skip;
                if (skip > 0 && flag)
                {
                    byte[] secondSignature = sig.SecondSignature;
                    var secondFileSig = new byte[secondSignature.Length];
                    Array.Copy(fileData.Skip(skip).ToArray(), secondFileSig, secondSignature.Length);
                    flag = secondFileSig.SequenceEqual(secondSignature);
                }
                if (flag) break;
            }

            return flag;
        }
    }
}
