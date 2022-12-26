using SubtitleTimeshift;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace SubtitleTimeshift
{ 
    public class Shifter
    {

        async static public Task Shift(Stream input, Stream output, TimeSpan timeSpan, 
            Encoding encoding, int bufferSize = 1024, bool leaveOpen = false)
        {
            string aux = "";
            using (StreamReader sr = new StreamReader(input))
            using (StreamWriter sw = new StreamWriter(output, encoding, bufferSize))
            {
                while (sr.Peek() >= 0)
                {
                    Task<string> task = sr.ReadLineAsync();
                    await task;

                    aux = task.Result;

                    if (aux.Contains(" --> "))
                    {
                        string[] time = aux.Split(' ');

                        time[0] = IncreaseTime(timeSpan, time[0]);
                        time[2] = IncreaseTime(timeSpan, time[2]);

                        aux = time[0] + " --> " + time[2];
                    }

                    await sw.WriteLineAsync(aux);
                }
            }

        }

        public static string IncreaseTime(TimeSpan timeSpan, string line)
        {
            int hr = Int32.Parse(line.Substring(0, 2));
            int mm = Int32.Parse(line.Substring(3, 2));
            string[] aux = line.Substring(6, line.Length - 6).Split(',');
            int ss = Int32.Parse(aux[0]);
            int ms = Int32.Parse(aux[1]);

            int hrT = (int)timeSpan.TotalHours;
            int mmT = (int)timeSpan.TotalMinutes;
            int ssT = (int)timeSpan.TotalSeconds;
            int msT = (int) timeSpan.TotalMilliseconds;

            ms = ms + msT;
            ssT = ssT + (ms / 1000);
            ms = ms % 1000;

            ss = ss + ssT;
            mmT = mmT + (ss / 60);
            ss = ss % 60;

            mm = mm + mmT;
            hrT = hrT + (mm / 60);
            mm = mm % 60;

            hr = hr + hrT;

            return (hr.ToString("D2") + ":" + mm.ToString("D2") + ":" + ss.ToString("D2") + "." + ms.ToString("D3"));
            
        }
    }
}
