﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRPBackendWV
{
    public class RMCPacketResponseAchievementsService_Method4 : RMCPacketReply
    {
        public List<GR5_PlayerAchievementGroup> groups = new List<GR5_PlayerAchievementGroup>();
        
        public override byte[] ToBuffer()
        {
            MemoryStream m = new MemoryStream();
            Helper.WriteU32(m, (uint)groups.Count);
            foreach (GR5_PlayerAchievementGroup g in groups)
                g.toBuffer(m);
            return m.ToArray();
        }

        public override string ToString()
        {
            return "[RMCPacketResponseAchievementsService_Method4]";
        }

        public override string PayloadToString()
        {
            return "";
        }
    }
}
