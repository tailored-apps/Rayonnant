using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Wise.Framework.Commons.TxF
{
    public static class NativeMethods
    {
        private const string Kernel32 = "kernel32.dll";

        // Error codes
        internal const int ErrorSuccess = 0;
        internal const int ErrorFileNotFound = 2;
        internal const int ErrorNoMoreFiles = 18;
        internal const int ErrorRecoveryNotNeeded = 6821;

        // Create file flags
        internal const int FileFlagBackupSemantics = 0x02000000;

        // Function control codes / masks
        private const int MethodBuffered = 0x00000000;
        private const int FileWriteData = 0x00000002;
        private const int FileDeviceFileSystem = 0x00000009;

        private const int RollforwardRedoFunction = 84;
        private const int RollforwardUndoFunction = 85;
        private const int StartRmFunction = 86;
        private const int ShutdownRmFunction = 87;
        private const int CreateSecondaryRmFunction = 90;

        // KTM IOCTLs
        internal const int FsctlTxfsRollforwardRedo =
            (FileDeviceFileSystem << 16) | (FileWriteData << 14) | (RollforwardRedoFunction << 2) | MethodBuffered;

        internal const int FsctlTxfsRollforwardUndo =
            (FileDeviceFileSystem << 16) | (FileWriteData << 14) | (RollforwardUndoFunction << 2) | MethodBuffered;

        internal const int FsctlTxfsStartRm =
            (FileDeviceFileSystem << 16) | (FileWriteData << 14) | (StartRmFunction << 2) | MethodBuffered;

        internal const int FsctlTxfsShutdownRm =
            (FileDeviceFileSystem << 16) | (FileWriteData << 14) | (ShutdownRmFunction << 2) | MethodBuffered;

        internal const int FsctlTxfsCreateSecondaryRm =
            (FileDeviceFileSystem << 16) | (FileWriteData << 14) | (CreateSecondaryRmFunction << 2) | MethodBuffered;

        // KTM Start flags
        internal const int TxfsStartRmFlagLogContainerCountMax = 0x00000001;
        internal const int TxfsStartRmFlagLogContainerCountMin = 0x00000002;
        internal const int TxfsStartRmFlagLogContainerSize = 0x00000004;
        internal const int TxfsStartRmFlagLogGrowthIncrementNumContainers = 0x00000008;
        internal const int TxfsStartRmFlagLogGrowthIncrementPercent = 0x00000010;
        internal const int TxfsStartRmFlagLogAutoShrinkPercentage = 0x00000020;
        internal const int TxfsStartRmFlagLogNoContainerCountMax = 0x00000040;
        internal const int TxfsStartRmFlagLogNoContainerCountMin = 0x00000080;

        internal const int TxfsStartRmFlagRecoverBestEffort = 0x00000200;
        internal const int TxfsStartRmFlagLoggingMode = 0x00000400;
        internal const int TxfsStartRmFlagPreserveChanges = 0x00000800;

        // KTM Logging modes
        internal const int TxfsLoggingModeSimple = 0x0001;
        internal const int TxfsLoggingModeFull = 0x0002;

        public enum FileAccess
        {
            GenericRead = unchecked((int)0x80000000),
            GenericWrite = 0x40000000
        }

        [Flags]
        public enum FileShare
        {
            None = 0x00,
            Read = 0x01,
            Write = 0x02,
            Delete = 0x04
        }

        public enum FileMode
        {
            New = 1,
            CreateAlways = 2,
            OpenExisting = 3,
            OpenAlways = 4,
            TruncateExisting = 5
        }

        [Flags]
        internal enum CopyFileFlags : uint
        {
            CopyFileFailIfExists = 0x00000001,
            CopyFileRestartable = 0x00000002,
            CopyFileOpenSourceForWrite = 0x00000004,
            CopyFileAllowDecryptedDestination = 0x00000008,
            CopyFileCopySymlink = 0x00000800
        }

        [Flags]
        internal enum MoveFileFlags : uint
        {
            MovefileReplaceExisting = 0x00000001,
            MovefileCopyAllowed = 0x00000002,
            MovefileDelayUntilReboot = 0x00000004,
            MovefileWriteThrough = 0x00000008,
            MovefileCreateHardlink = 0x00000010,
            MovefileFailIfNotTrackable = 0x00000020
        }

        internal enum FindexInfoLevels
        {
            FindExInfoStandard,
            FindExInfoMaxInfoLevel
        }
        internal enum FindexSearchOps
        {
            NameMatch,
            LimitToDirectories,
            LimitToDevices,
            MaxSearchOp
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct Filetime
        {
            public uint DateTimeLow;
            public uint DateTimeHigh;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct Win32FindData
        {
            public readonly int dwFileAttributes;
            public Filetime ftCreationTime;
            public Filetime ftLastAccessTime;
            public Filetime ftLastWriteTime;
            public readonly uint nFileSizeHigh;
            public readonly uint nFileSizeLow;
            public readonly uint dwReserved0;
            public readonly uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public readonly string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct TXFS_START_RM_INFORMATION
        {
            public UInt32 Flags;

            public UInt64 LogContainerSize;
            public UInt32 LogContainerCountMin;
            public UInt32 LogContainerCountMax;

            public UInt32 LogGrowthIncrement;
            public UInt32 LogAutoShrinkPercentage;

            public UInt32 TmLogPathOffset;
            public UInt16 TmLogPathLength;

            public UInt16 LoggingMode;
            public UInt16 LogPathLength;
            public UInt16 Reserved;

            public IntPtr LogPath;
        }

        //
        // Standard file operations
        //

        [DllImport(Kernel32, CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern SafeFileHandle CreateFile(
            [In] string lpFileName,
            [In] FileAccess dwDesiredAccess,
            [In] FileShare dwShareMode,
            [In] IntPtr lpSecurityAttributes,
            [In] FileMode dwCreationDisposition,
            [In] int dwFlagsAndAttributes,
            [In] IntPtr hTemplateFile);

        [DllImport(Kernel32, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FindNextFile(
            [In] SafeFileHandle hFindFile,
            [Out] out Win32FindData lpFindFileData);

        //
        // Transacted file operations
        //

        [DllImport(Kernel32, EntryPoint = "CreateFileTransacted", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern SafeFileHandle CreateFileTransacted(
            [In] string lpFileName,
            [In] FileAccess dwDesiredAccess,
            [In] FileShare dwShareMode,
            [In] IntPtr lpSecurityAttributes,
            [In] FileMode dwCreationDisposition,
            [In] int dwFlagsAndAttributes,
            [In] IntPtr hTemplateFile,
            [In] KtmTransactionHandle hTransaction,
            [In] IntPtr pusMiniVersion,
            [In] IntPtr pExtendedParameter);

        [DllImport(Kernel32, EntryPoint = "CopyFileTransacted", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CopyFileTransacted(
            [In] string lpExistingFileName,
            [In] string lpNewFileName,
            [In] IntPtr lpProgressRoutine,
            [In] IntPtr lpData,
            [In] [MarshalAs(UnmanagedType.Bool)] ref bool pbCancel,
            [In] CopyFileFlags dwCopyFlags,
            [In] KtmTransactionHandle hTransaction);

        [DllImport(Kernel32, EntryPoint = "DeleteFileTransacted", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteFileTransacted(
            [In] string lpFileName,
            [In] KtmTransactionHandle hTransaction);

        [DllImport(Kernel32, EntryPoint = "FindFirstFileTransacted", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern SafeFileHandle FindFirstFileTransacted(
            [In] string lpDirSpec,
            [In] FindexInfoLevels fInfoLevelId,
            [Out] out Win32FindData lpFindFileData,
            [In] FindexSearchOps fSearchOp,
            [In] IntPtr lpSearchFilter,
            [In] int dwAdditionalFlags,
            [In] KtmTransactionHandle hTransaction);

        [DllImport(Kernel32, EntryPoint = "MoveFileTransacted", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool MoveFileTransacted(
            [In] string lpExistingFileName,
            [In] string lpNewFileName,
            [In] IntPtr lpProgressRoutine,
            [In] IntPtr lpData,
            [In] MoveFileFlags dwFlags,
            [In] KtmTransactionHandle hTransaction);

        [DllImport(Kernel32, EntryPoint = "DeviceIoControl", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeviceIoControl(
            [In] SafeFileHandle hDevice,
            [In] int dwIoControlCode,
            [In] IntPtr lpInBuffer,
            [In] int nInBufferSize,
            [Out] IntPtr lpOutBuffer,
            [In] int nOutBufferSize,
            [Out] out int lpBytesReturned,
            [In] IntPtr lpOverlapped);

        //
        // Close handles
        //

        [DllImport(Kernel32, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CloseHandle(
            [In] IntPtr handle);
        [DllImport(Kernel32, CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FindClose(
            [In] SafeFileHandle handle);

        internal static void HandleCOMError(int error)
        {
            //Console.WriteLine("Got error {0}", error);
            throw new System.ComponentModel.Win32Exception(error);
        }
    }
}
