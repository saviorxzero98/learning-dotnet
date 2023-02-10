using JiebaNet.Segmenter;
using JiebaNet.Segmenter.PosSeg;
using System;
using System.Linq;

namespace CS_JiebaSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var segmenter = new PosSegmenter();
            var content = "由於颱風假影響，2018年中ISBG組織異動改於下周一(7/16)進行，7/16(一)13:00關閉下列系統，預計於7/16(一)20:00重新開放，若作業提早完成，將另行通知";
            var results = segmenter.Cut(content, false);
        }
    }
}
