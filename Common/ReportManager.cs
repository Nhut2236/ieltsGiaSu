using Aspose.Cells;
using Aspose.Words;
using Aspose.Words.Replacing;
using Aspose.Words.Tables;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HooksLearning.Common
{
    public class ReportManager
    {
        public static string WebRootPath;
        private static string ReportPath;
        private static string ReportTmpPath;

        private static string ExcelPath;
        private static string ExcelTmpPath;

        private static string REPORTS = "Reports";

        private static string EXCEL = "Excel";

        private static string TMP_REPORTS = "Reports_TMP";

        private static string TMP_EXCEL = "Excel_TMP";

        public const string AsposeLicense = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz4NCjxMaWNlbnNlPg0KICAgIDxEYXRhPg0KICAgICAgICA8TGljZW5zZWRUbz5pckRldmVsb3BlcnMuY29tPC9MaWNlbnNlZFRvPg0KICAgICAgICA8RW1haWxUbz5pbmZvQGlyRGV2ZWxvcGVycy5jb208L0VtYWlsVG8+DQogICAgICAgIDxMaWNlbnNlVHlwZT5EZXZlbG9wZXIgT0VNPC9MaWNlbnNlVHlwZT4NCiAgICAgICAgPExpY2Vuc2VOb3RlPkxpbWl0ZWQgdG8gMTAwMCBkZXZlbG9wZXIsIHVubGltaXRlZCBwaHlzaWNhbCBsb2NhdGlvbnM8L0xpY2Vuc2VOb3RlPg0KICAgICAgICA8T3JkZXJJRD43ODQzMzY0Nzc4NTwvT3JkZXJJRD4NCiAgICAgICAgPFVzZXJJRD4xMTk0NDkyNDM3OTwvVXNlcklEPg0KICAgICAgICA8T0VNPlRoaXMgaXMgYSByZWRpc3RyaWJ1dGFibGUgbGljZW5zZTwvT0VNPg0KICAgICAgICA8UHJvZHVjdHM+DQogICAgICAgICAgICA8UHJvZHVjdD5Bc3Bvc2UuVG90YWwgUHJvZHVjdCBGYW1pbHk8L1Byb2R1Y3Q+DQogICAgICAgIDwvUHJvZHVjdHM+DQogICAgICAgIDxFZGl0aW9uVHlwZT5FbnRlcnByaXNlPC9FZGl0aW9uVHlwZT4NCiAgICAgICAgPFNlcmlhbE51bWJlcj57RjJCOTcwNDUtMUIyOS00QjNGLUJENTMtNjAxRUZGQTE1QUE5fTwvU2VyaWFsTnVtYmVyPg0KICAgICAgICA8U3Vic2NyaXB0aW9uRXhwaXJ5PjIwOTkxMjMxPC9TdWJzY3JpcHRpb25FeHBpcnk+DQogICAgICAgIDxMaWNlbnNlVmVyc2lvbj4zLjA8L0xpY2Vuc2VWZXJzaW9uPg0KICAgIDwvRGF0YT4NCiAgICA8U2lnbmF0dXJlPlFYTndiM05sTGxSdmRHRnNMb1B5YjJSMVkzUWdSbUZ0YVd4NTwvU2lnbmF0dXJlPg0KPC9MaWNlbnNlPg==";


        public ReportManager()
        {
            ReportPath = WebRootPath + "\\" + REPORTS + "\\";
            ReportTmpPath = WebRootPath + "\\" + TMP_REPORTS + "\\";
            //Register Word
            Stream stream = new MemoryStream(Convert.FromBase64String(AsposeLicense));
            stream.Seek(0, SeekOrigin.Begin);
            new Aspose.Words.License().SetLicense(stream);

            ///EXCEL
            ExcelPath = WebRootPath + "\\" + EXCEL + "\\";
            ExcelTmpPath = WebRootPath + "\\" + TMP_EXCEL + "\\";
            //Register Word
            Stream stream_excel = new MemoryStream(Convert.FromBase64String(AsposeLicense));
            stream_excel.Seek(0, SeekOrigin.Begin);
            new Aspose.Cells.License().SetLicense(stream_excel);

            Stream stream_pdf = new MemoryStream(Convert.FromBase64String(AsposeLicense));
            stream.Seek(0, SeekOrigin.Begin);
            new Aspose.Pdf.License().SetLicense(stream);

        }

        public Document GetReport(string name)
        {
            return new Document(ReportPath + "\\" + name);
        }



        public void SetVal(Document doc, string v, object val, bool noTrim = false)
        {
            if (val == null)
            {
                val = "";
            }

            FindReplaceOptions options = new FindReplaceOptions();
            options.MatchCase = true;
            options.FindWholeWordsOnly = true;

            if (noTrim)
                doc.Range.Replace("{{" + v.ToUpper() + "}}", val.ToString(), options);
            else
                doc.Range.Replace("{{" + v.ToUpper() + "}}", val.ToString().Trim(), options);
        }

        public string SaveTmpReport(Document doc, HttpContext httpContext, string MaHopDong = "")
        {

            System.IO.DirectoryInfo di = new DirectoryInfo(ReportTmpPath);
            if (di.GetFiles().Count() > 0)
            {
                DateTime DelDate = DateTime.Now.AddDays(-7);
                foreach (FileInfo file in di.GetFiles())
                {
                    DateTime DateCreate = file.CreationTime;
                    if (DateCreate <= DelDate)
                    {
                        file.Delete();
                    }
                }
            }
            FileInfo fi = new FileInfo(doc.OriginalFileName);
            string ext = fi.Extension;
            string fileName = MaHopDong + "_" + DateTime.Now.ToString("yyMMddHHmmss") + "_" + fi.Name.Replace(ext, ".pdf");
            string path = ReportTmpPath + "\\" + fileName;
            doc.Save(path, Aspose.Words.SaveFormat.Pdf);
            string rptLink = "/" + TMP_REPORTS + "/" + fileName;

            string link = "";
            if (httpContext.Request.IsHttps)
                link += "https://";
            else
                link += "http://";
            if (httpContext.Request.Host.Port.HasValue)
            {
                link += httpContext.Request.Host.Host + ":" + httpContext.Request.Host.Port.Value;
            }
            else
            {
                link += httpContext.Request.Host.Host;
            }
            return link + rptLink;
        }


        public void RemoveBookmark(Document doc, string v)
        {
            try
            {
                Bookmark bm = doc.Range.Bookmarks[v];
                if (bm != null)
                {
                    bm.Text = "";
                }
            }
            catch (Exception)
            {

            }

        }


        public void RemoveBookmarkRow(Document doc, string v)
        {
            Bookmark bm = doc.Range.Bookmarks[v];

            if (bm != null)
            {
                Paragraph row = (Paragraph)bm.BookmarkStart.GetAncestor(NodeType.Paragraph);
                //var cc = row.ParentNode;
                if (row.GetText().Contains("PL_TONG"))
                {
                    Table firstTable = (Table)doc.GetChild(NodeType.Table, 1, true);
                    if (firstTable.HasChildNodes)
                    {
                        firstTable.Remove();
                    }
                    row.Remove();

                }
                else
                {
                    row.Remove();
                }

            }
        }

        //get WorkBook GetExcel 
        public Workbook GetReportExcel(string name)
        {
            return new Workbook(ExcelPath + "\\" + name);
        }

        //save SaveTmpExcel
        public string SaveTmpExcel(Workbook doc, HttpContext httpContext, string template_name = "")
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(ExcelTmpPath);
            if (di.GetFiles().Count() > 0)
            {
                DateTime DelDate = DateTime.Now.AddDays(-7);
                foreach (FileInfo file in di.GetFiles())
                {
                    DateTime DateCreate = file.CreationTime;
                    if (DateCreate <= DelDate)
                    {
                        file.Delete();
                    }
                }
            }
            //FileInfo fi = new FileInfo(doc.FileName);
            //string ext = fi.Extension;
            string fileName = template_name + "_" + DateTime.Now.ToString("yyMMddHHmmss") + ".xlsx";
            string path = ExcelTmpPath + "\\" + fileName;
            doc.Save(path, Aspose.Cells.SaveFormat.Xlsx);
            string rptLink = "/" + TMP_EXCEL + "/" + fileName;

            string link = "";
            if (httpContext.Request.IsHttps)
                link += "https://";
            else
                link += "http://";
            if (httpContext.Request.Host.Port.HasValue)
            {
                link += httpContext.Request.Host.Host + ":" + httpContext.Request.Host.Port.Value;
            }
            else
            {
                link += httpContext.Request.Host.Host;
            }
            return link + rptLink;
        }

        public string MergerPDF(HttpContext httpContext, List<string> lstFileName)
        {
            Aspose.Pdf.Document pdfs = new Aspose.Pdf.Document();

            string link = "";
            foreach (string fileName in lstFileName)
            {
                string name = ReportTmpPath + fileName;
                Aspose.Pdf.Document pdf = new Aspose.Pdf.Document(name);
                pdfs.Pages.Add(pdf.Pages);
            }

            string outFileName = "HD_merge_" + DateTime.Now.ToString("yyMMddHHmmss") + ".pdf";
            pdfs.Save(ReportTmpPath + outFileName);

            ////
            if (httpContext.Request.IsHttps)
                link += "https://";
            else
                link += "http://";
            if (httpContext.Request.Host.Port.HasValue)
            {
                link += httpContext.Request.Host.Host + ":" + httpContext.Request.Host.Port.Value;
            }
            else
            {
                link += httpContext.Request.Host.Host;
            }


            return link + "/" + TMP_REPORTS + "/" + outFileName;

        }
    }
}

