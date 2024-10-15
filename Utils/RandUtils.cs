using Discs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscGolfManagerAspire.Utils
{
    public static class RandUtils
    {
        public static List<Disc> GetRandomDiscs(List<Disc> discs, DiscType discType, int numDiscs)
        {
            Random random = new((int)DateTime.Now.TimeOfDay.TotalMilliseconds);

            var discsOfType = discs.Select(c => c).Where(c => c.DiscType == discType).OrderBy(c => random.Next()).Take(numDiscs);

            return discsOfType.ToList();
        }
    }
}
