using System.Runtime.Versioning;
namespace sMkTaskManager.Classes;

[SupportedOSPlatform("windows")]
internal static class TaskManagerNetwork {
    private const int AF_INET = 2;
    private const int AF_INET6 = 23;

    public static bool GetIPStatistics(ref API.MIB_IPSTATS pStatsv4, ref API.MIB_IPSTATS pStatsv6) {
        try {
            return API.GetIpStatisticsEx(ref pStatsv4, AF_INET) == 0 && API.GetIpStatisticsEx(ref pStatsv6, AF_INET6) == 0;
        } catch (Exception ex) { Shared.DebugTrap(ex); return false; }
    }
    public static bool GetTCPStatistics(ref API.MIB_TCPSTATS pStatsv4, ref API.MIB_TCPSTATS pStatsv6) {
        try {
            return API.GetTcpStatisticsEx(ref pStatsv4, AF_INET) == 0 && API.GetTcpStatisticsEx(ref pStatsv6, AF_INET6) == 0;
        } catch (Exception ex) { Shared.DebugTrap(ex); return false; }
    }
    public static bool GetUDPStatistics(ref API.MIB_UDPSTATS pStatsv4, ref API.MIB_UDPSTATS pStatsv6) {
        try {
            return API.GetUdpStatisticsEx(ref pStatsv4, AF_INET) == 0 && API.GetUdpStatisticsEx(ref pStatsv6, AF_INET6) == 0;
        } catch (Exception ex) { Shared.DebugTrap(ex); return false; }
    }
    public static bool GetICMPv4Statistics(ref API.MIB_ICMPINFO pStatsv4) {
        try {
            return API.GetIcmpStatistics(ref pStatsv4) == 0;
        } catch (Exception ex) { Shared.DebugTrap(ex); return false; }
    }

}

