namespace SecureDesktop.Enums
{
    public enum DesktopAccess : uint
    {
        DesktopNone = 0,
        DesktopReadObjects = 0x0001,
        DesktopCreateWindow = 0x0002,
        DesktopCreatemenu = 0x0004,
        DesktopHookControl = 0x0008,
        DesktopJournalRecord = 0x0010,
        DesktopJournalPlayback = 0x0020,
        DesktopEnumerate = 0x0040,
        DesktopWriteObjects = 0x0080,
        DesktopSwitchDesktop = 0x0100,

        GenericAll = (DesktopReadObjects | DesktopCreateWindow | DesktopCreatemenu |
                        DesktopHookControl | DesktopJournalRecord | DesktopJournalPlayback |
                        DesktopEnumerate | DesktopWriteObjects | DesktopSwitchDesktop)
    }
}
